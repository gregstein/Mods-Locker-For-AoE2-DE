using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ModLocker
{



    public partial class Main : Form
    {
        public int i = 0;
        private Image saved;
        private Image unsaved;
        public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        Dictionary<string, string> USERprofiles = new Dictionary<string, string>();
        Dictionary<string, string> MODict = new Dictionary<string, string>();
        public Root dc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
        PipeSecurity lPipeSecurity = new PipeSecurity();
        public Main()
        {
            InitializeComponent();

        }
        private void cbox_MouseDown(Object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                Dictionary<string, int> tmpst = new Dictionary<string, int>();
                int evs = 0;
                foreach (Control control in fp.Controls)
                {
                    evs++;
                    if (control is CheckBox)
                        tmpst.Add(control.Text, evs);


                }
                int inxr = tmpst[((sender as CheckBox).Text)];
                if (inxr > 0)
                {
                    inxr -= 1;

                    fp.Controls.SetChildIndex((sender as CheckBox), (inxr - 1));
                    modsta.Visible = true;
                    modsta.Image = Properties.Resources.unsaved;

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saved = Properties.Resources.saved;
            unsaved = Properties.Resources.unsaved;
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
                    listPROFILE.SelectedIndex = getINDEX;
            }
            catch (System.IO.DirectoryNotFoundException mp)
            {

                MessageBox.Show(mp.Message + "\n Run AoE2 DE through steam to fix the problem.", "Mod Locker"); ;
            }


        }




        private void locker_Click(object sender, EventArgs e)
        {

            int curINDEX = listPROFILE.SelectedIndex;
            resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
            resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");

            try
            {
                if (locker.ImageIndex == 1)
                {
                    locker.ImageIndex = 0; //Changed to Locked Here
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.IDGROUP._admins, FileProcess.PT._readEXEC);
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.IDGROUP._system, FileProcess.PT._readEXEC);
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.IDGROUP._users, FileProcess.PT._readEXEC);
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._admins, FileProcess.PT._readEXEC);
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._users, FileProcess.PT._readEXEC);
                    FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._system, FileProcess.PT._readEXEC);

                    foreach (Control control in fp.Controls)
                        control.Enabled = false;

                    listPROFILE.Items.Clear();
                    ScanDirectories(curINDEX);

                }
                else

                {
                    locker.ImageIndex = 1;//Change to Unlocked here
                    resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
                    resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
                    foreach (Control control in fp.Controls)
                        control.Enabled = true;

                    listPROFILE.Items.Clear();
                    ScanDirectories(curINDEX);

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
        private void resetPERMISIONS(string path)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c ICACLS \"" + path + "\" /reset /T";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }

        }

        private void applychange_Click(object sender, EventArgs e)
        {
            int _curINDEX = listPROFILE.SelectedIndex;
            resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
            resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            applychange.Enabled = false;
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None
            };
            Root rc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
            int prioMOD = 0;
            foreach (Control t in fp.Controls)
            {
                prioMOD++;
                if (((CheckBox)t).Checked)
                {
                    foreach (var item in dc.Mods)
                    {
                        if (((CheckBox)t).Text == item.Title)
                        {
                            item.Title = t.Text;
                            item.Priority = prioMOD;
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
                modsta.Visible = true;
                modsta.Image = Properties.Resources.saved;
                var task = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);
                    modsta.Visible = false;
                });
                

            }


            string sc = JsonConvert.SerializeObject(rc);
            if (listPROFILE.SelectedItem == null)
            {
                MessageBox.Show("Please Run AoE2 DE Then close it once you reach the menu screen. Then Restart Mods Locker.", "Mods Locker");
                applychange.Enabled = true;
                return;
            }
            Thread.Sleep(100);
            var fileInfo = new FileInfo(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            if (fileInfo.IsReadOnly)
                fileInfo.IsReadOnly = false;

            string jsonPath = USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json";
            try
            {
                if (File.Exists(jsonPath))
                {
                    File.Delete(jsonPath);
                }
                //Wait for disposable
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (File.Exists(jsonPath))
                {
                    File.Delete(jsonPath);
                }
                using (StreamWriter writer = new StreamWriter(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", false))
                {
                    writer.Write(sc);
                }

            }
            catch (SystemException)
            {

                applychange.Enabled = true;
                File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", sc);

            }
            listPROFILE.Items.Clear();

            ScanDirectories(_curINDEX);


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
                    if (fp.Controls.Count > 0)
                    {
                        foreach (Control c in fp.Controls)
                            if (c is CheckBox)
                                c.MouseDown -= cbox_MouseDown;
                    }
                    fp.Controls.Clear();
                    MODict.Clear();
                    applychange.Enabled = true;

                    if (listPROFILE.SelectedItem == null)
                    {
                        return;

                    }

                    dc = JsonConvert.DeserializeObject<Root>(File.ReadAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json"));
                    var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);

                    int j = 0;
                    foreach (var item in dc.Mods)
                    {
                        i++;
                        fp.Controls.Add(new CheckBox() { Name = "cb_" + i.ToString(), Text = item.Title, Checked = item.Enabled, AutoSize = true });
                        MODict.Add(item.Path.Replace(@"subscribed//", @""), USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed\" + item.Path.Replace(@"subscribed//", @""));

                    }
                    var lstcb = new List<string>();
                    foreach (Control cr in fp.Controls)
                        if (cr is CheckBox)
                            lstcb.Add(cr.Text);



                    foreach (string m in mdirs)
                    {
                        j++;


                        if (lstcb.Contains(Path.GetFileName(m)) || MODict.ContainsKey(Path.GetFileName(m)))
                        {

                            continue;
                        }
                        fp.Controls.Add(new CheckBox() { Name = "cb_" + i.ToString(), Text = Path.GetFileName(m), AutoSize = true, Checked = false });

                    }

                    foreach (Control c in fp.Controls)
                        if (c is CheckBox)
                            c.MouseUp += cbox_MouseDown;

                    foreach (Control control in fp.Controls)
                        if (control is CheckBox)
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
                    MessageBox.Show("Your Mods are Currently Locked.\nClick The Lock icon to Unlock them.", "Mod Locker"); ;
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

      

        private void modsta_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void modsta_Click(object sender, EventArgs e)
        {

        }

        private void modsta_Paint(object sender, PaintEventArgs e)
        {
           

        }
    }
}
