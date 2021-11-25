using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModLocker
{
    public partial class Main : Form
    {
        public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        Dictionary<string, string> USERprofiles = new Dictionary<string, string>();
        public Root dc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScanDirectories();

        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }


        private void ScanDirectories(int getINDEX = 0)
        {
            try
            {
                USERprofiles.Clear();

                var subdirectoryEntries = new DirectoryInfo(dePATH).GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly).OrderBy(x => x.LastWriteTime);
                foreach (DirectoryInfo subdirectory in subdirectoryEntries)
                {

                    if (subdirectory.FullName.Length > 4 && subdirectory.FullName.Replace(dePATH + "\\", "") != "0" && IsDigitsOnly(subdirectory.Name))
                    {
                        listPROFILE.Items.Add(subdirectory.Name);
                        USERprofiles.Add(subdirectory.Name, subdirectory.FullName);
                    }
                }
                if (listPROFILE.Items.Count >= 0)
                    listPROFILE.SelectedIndex = 0;
            }
            catch (System.IO.DirectoryNotFoundException mp)
            {

                MessageBox.Show(mp.Message + "\n Run AoE2 DE through steam to fix the problem.", "Mod Locker"); ;
            }


        }

        private void setREADONLY(string path)
        {
            try
            {
                FileInfo di = new FileInfo(path);
                if (di.Exists)
                {
                    di.Attributes |= FileAttributes.System;
                    //di.Attributes |= FileAttributes.Hidden;
                    FileSecurity ds = di.GetAccessControl();
                    AuthorizationRuleCollection rules = ds.GetAccessRules(true, true, typeof(NTAccount));
                    foreach (AuthorizationRule rule in rules)
                    {
                        if (rule is FileSystemAccessRule)
                        {
                            ds.RemoveAccessRule((FileSystemAccessRule)rule);
                        }
                    }
                    ds.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.Write, AccessControlType.Deny));
                    ds.AddAccessRule(new FileSystemAccessRule(@"Users", FileSystemRights.Write, AccessControlType.Deny));
                    ds.AddAccessRule(new FileSystemAccessRule(@"System", FileSystemRights.Write, AccessControlType.Deny));
                    //ds.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.Write, AccessControlType.Deny));
                    di.SetAccessControl(ds);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "MOd Locker");
            }
        }



        private void setNORMAL(string path)
        {
            try
            {
                using (var user = WindowsIdentity.GetCurrent())
                {
                    var ownerSecurity = new FileSecurity();
                    ownerSecurity.SetOwner(user.User);
                    var accessSecurity = new FileSecurity();
                    accessSecurity.AddAccessRule(new FileSystemAccessRule(user.User, FileSystemRights.FullControl, AccessControlType.Allow));
                    File.SetAccessControl(path, accessSecurity);
                }
                FileInfo di = new FileInfo(path);
                di.Attributes = FileAttributes.Normal;
                FileSecurity ds1 = di.GetAccessControl();
                ds1.SetOwner(WindowsIdentity.GetCurrent().User);
                File.SetAccessControl(path, ds1);
                AuthorizationRuleCollection rules = ds1.GetAccessRules(true, true, typeof(NTAccount));
                foreach (AuthorizationRule rule in rules)
                {
                    if (rule is FileSystemAccessRule)
                    {
                        ds1.RemoveAccessRule((FileSystemAccessRule)rule);
                    }
                }
                ds1.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.FullControl, AccessControlType.Allow));
                ds1.AddAccessRule(new FileSystemAccessRule(@"Users", FileSystemRights.FullControl, AccessControlType.Allow));
                ds1.AddAccessRule(new FileSystemAccessRule(@"System", FileSystemRights.FullControl, AccessControlType.Allow));
                //ds1.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, AccessControlType.Allow));


                di.SetAccessControl(ds1);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Mod Locker");
            }

        }
        private void LockDIR(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    di.Attributes |= FileAttributes.System;
                    //di.Attributes |= FileAttributes.Hidden;
                    DirectorySecurity ds = di.GetAccessControl();
                    AuthorizationRuleCollection rules = ds.GetAccessRules(true, true, typeof(NTAccount));
                    foreach (AuthorizationRule rule in rules)
                    {
                        if (rule is FileSystemAccessRule)
                        {
                            ds.RemoveAccessRule((FileSystemAccessRule)rule);
                        }
                    }
                    ds.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.Write, AccessControlType.Deny));
                    ds.AddAccessRule(new FileSystemAccessRule(@"Users", FileSystemRights.Write, AccessControlType.Deny));
                    ds.AddAccessRule(new FileSystemAccessRule(@"System", FileSystemRights.Write, AccessControlType.Deny));
                    //ds.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.Write, AccessControlType.Deny));
                    di.SetAccessControl(ds);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Mod Locker");
            }
        }
        private void UnlockDIR(string foldnam)
        {
            try
            {
                using (var user = WindowsIdentity.GetCurrent())
                {
                    var ownerSecurity = new DirectorySecurity();
                    ownerSecurity.SetOwner(user.User);
                    var accessSecurity = new DirectorySecurity();
                    accessSecurity.AddAccessRule(new FileSystemAccessRule(user.User, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                    Directory.SetAccessControl(foldnam, accessSecurity);
                }
                DirectoryInfo di = new DirectoryInfo(foldnam);
                di.Attributes = FileAttributes.Normal;
                DirectorySecurity ds1 = di.GetAccessControl();
                ds1.SetOwner(WindowsIdentity.GetCurrent().User);
                Directory.SetAccessControl(foldnam, ds1);
                AuthorizationRuleCollection rules = ds1.GetAccessRules(true, true, typeof(NTAccount));
                foreach (AuthorizationRule rule in rules)
                {
                    if (rule is FileSystemAccessRule)
                    {
                        ds1.RemoveAccessRule((FileSystemAccessRule)rule);
                    }
                }
                ds1.AddAccessRule(new FileSystemAccessRule(@"Administrators", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                ds1.AddAccessRule(new FileSystemAccessRule(@"Users", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                ds1.AddAccessRule(new FileSystemAccessRule(@"System", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                //ds1.AddAccessRule(new FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));

                di.SetAccessControl(ds1);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Mod Locker");
            }
        }
        private void locker_Click(object sender, EventArgs e)
        {
            setNORMAL(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            applychange.PerformClick();
            try
            {
                if (locker.ImageIndex == 1)
                {
                    locker.ImageIndex = 0;
                    LockDIR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
                    setREADONLY(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
                    foreach (Control control in fp.Controls)
                        control.Enabled = false;

                    listPROFILE.Items.Clear();
                    ScanDirectories(listPROFILE.SelectedIndex);

                }
                else

                {
                    locker.ImageIndex = 1;
                    UnlockDIR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
                    setNORMAL(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
                    foreach (Control control in fp.Controls)
                        control.Enabled = true;

                    listPROFILE.Items.Clear();
                    ScanDirectories(listPROFILE.SelectedIndex);

                }

            }
            catch (System.ArgumentException er)
            {

                MessageBox.Show("Access To mod-status.json is denied!" + er.ToString());
            }
            catch (Exception xe)
            {

                MessageBox.Show(xe.Message, "Mods Locker");
            }

        }

        private void applychange_Click(object sender, EventArgs e)
        {
            applychange.Enabled = false;
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None
            };
            Root rc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
            foreach (Control t in fp.Controls)
            {
                if (((CheckBox)t).Checked)
                {
                    foreach (var item in dc.Mods)
                    {
                        if (((CheckBox)t).Text == item.Title)
                        {
                            item.Title = t.Text;
                            item.Enabled = true;
                            rc.Mods.Add(item);
                            continue;
                        }


                    }

                }
                else if (((CheckBox)t).Checked == false)
                {
                    try
                    {
                        var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);
                        int cmdir = mdirs.Count();
                        progressBar1.Maximum = cmdir;
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        int i = 0;
                        foreach (string m in mdirs)
                        {
                            i++;

                            string str = Regex.Replace(Path.GetFileName(m), "\\d+_", "");
                            str = Regex.Replace(str, "_", " ");
                            str = Regex.Replace(str, @"[[][\s\S]*?[]]", "");

                            if (str == ((CheckBox)t).Text)
                            {
                                FileSystem.DeleteDirectory(m, DeleteDirectoryOption.DeleteAllContents);

                            }
                            progressBar1.Value = i;

                        }
                        progressBar1.Visible = false;

                    }
                    catch (Exception ds)
                    {
                        MessageBox.Show(ds.Message, "Mods Locker");
                        applychange.Enabled = true;
                        progressBar1.Visible = false;

                    }

                }


            }


            string sc = JsonConvert.SerializeObject(rc);
            if (listPROFILE.SelectedItem == null)
            {
                MessageBox.Show("Please Run AoE2 DE Then close it once you reach the menu screen. Then Restart Mods Locker.", "Mods Locker");
                applychange.Enabled = true;
                return;
            }
            FileInfo fileInfo = new FileInfo(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            if (fileInfo.IsReadOnly)
                fileInfo.IsReadOnly = false;

            File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", sc);
            listPROFILE.Items.Clear();

            ScanDirectories(listPROFILE.SelectedIndex);


            applychange.Enabled = true;

        }
        private void LockStatus()
        {
            FileInfo dInfo = new FileInfo(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            FileSecurity dSecurity = dInfo.GetAccessControl();
            var fr = dSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            bool isFullControl = true;
            foreach (FileSystemAccessRule item in fr)
            {
                if (item.FileSystemRights.ToString() != "FullControl")
                {
                    isFullControl = false;
                    break;
                }


            }

            if (isFullControl)
            {
                locker.ImageIndex = 1;

                foreach (Control control in fp.Controls)
                    control.Enabled = true;
            }
            else
            {
                locker.ImageIndex = 0;
                foreach (Control control in fp.Controls)
                    control.Enabled = false;
            }
        }
        private void listPROFILE_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listPROFILE.SelectedIndex >= 0)
            {
                try
                {
                    fp.Controls.Clear();
                    applychange.Enabled = true;

                    if (listPROFILE.SelectedItem == null)
                    {
                        return;

                    }

                    dc = JsonConvert.DeserializeObject<Root>(File.ReadAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json"));
                    var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);
                    int i = 0;
                    int j = 0;
                    foreach (var item in dc.Mods)
                    {
                        i++;
                        fp.Controls.Add(new CheckBox() { Name = "cb" + i.ToString(), Text = item.Title, Checked = item.Enabled, AutoSize = true });

                    }
                    var lstcb = new List<string>();
                    foreach (CheckBox cr in fp.Controls)
                        lstcb.Add(cr.Text);



                    foreach (string m in mdirs)
                    {
                        j++;
                        string str = Regex.Replace(Path.GetFileName(m), "\\d+_", "");
                        str = Regex.Replace(str, "_", " ");
                        str = Regex.Replace(str, @"[[][\s\S]*?[]]", "");


                        if (lstcb.Contains(str))
                        {

                            continue;
                        }

                        fp.Controls.Add(new CheckBox() { Name = "stee" + j.ToString(), Text = str, AutoSize = true, Checked = false });

                    }



                    foreach (CheckBox control in fp.Controls)
                        fp.SetFlowBreak(control, true);


                    LockStatus();
                }
                catch (System.NullReferenceException ty)
                {
                    MessageBox.Show(ty.Message, "Mod Locker");
                }
                catch (Exception)
                {
                    locker.ImageIndex = 0;
                    fp.Controls.Add(new Label() { Name = "warnlbl", Text = "Press The Lock Icon To Edit and Unlock Your Mods", AutoSize = true, ForeColor = System.Drawing.Color.DarkRed });
                    MessageBox.Show("Your Mods are Currently Locked!", "Mod Locker"); ;
                }

            }
        }

        private void listPROFILE_Click(object sender, EventArgs e)
        {
            if (listPROFILE.SelectedItem == null)
            {
                try
                {

                    listPROFILE_SelectedIndexChanged(this, new EventArgs());

                }
                catch (SystemException)
                {

                }

            }
        }
    }
}
