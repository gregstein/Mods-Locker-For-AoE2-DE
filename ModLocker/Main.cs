using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModLocker
{



    public partial class Main : Form
    {
        public int i = 0;
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
            isDERUNNING();
            ScanDirectories();


        }
        private void isDERUNNING()
        {
            Process[] pname = Process.GetProcessesByName("AoE2DE_s");
            if (pname.Length != 0)
            {
                MessageBox.Show("Please Close AoE2DE Before Using The Tool!", "Close Age of Empires 2 DE");
                Application.Exit();
            }
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
            FileProcess.resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
            FileProcess.resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");

            try
            {
                if (locker.ImageIndex == 1)
                {
                    locker.ImageIndex = 0; //Changed to Locked Here
                    FileProcess.setREADONLY(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
                    FileProcess.setREADONLYDIR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._deny, FileProcess.IDGROUP._admins, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._grant, FileProcess.IDGROUP._admins, FileProcess.PT._readEXEC);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._deny, FileProcess.IDGROUP._users, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._grant, FileProcess.IDGROUP._users, FileProcess.PT._readEXEC);
                    ////FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._deny, FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.GD._grant, FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._deny, FileProcess.IDGROUP._admins, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._grant, FileProcess.IDGROUP._admins, FileProcess.PT._readEXEC);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._deny, FileProcess.IDGROUP._users, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._grant, FileProcess.IDGROUP._users, FileProcess.PT._readEXEC);
                    ////FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._deny, FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);
                    //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.GD._grant, FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);

                    foreach (Control control in fp.Controls)
                        control.Enabled = false;

                    listPROFILE.Items.Clear();
                    ScanDirectories(curINDEX);

                }
                else

                {
                    locker.ImageIndex = 1;//Change to Unlocked here
                    FileProcess.resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed");
                    FileProcess.resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
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

        private void applychange_Click(object sender, EventArgs e)
        {
            int _curINDEX = listPROFILE.SelectedIndex;
            //resetPERMISIONS(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.IDGROUP._admins, FileProcess.PT._fullCONTROLL);
            //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);
            //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._admins, FileProcess.PT._fullCONTROLL);
            //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._users, FileProcess.PT._fullCONTROLL);
            //FileProcess._processHANDLR(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", FileProcess.IDGROUP._system, FileProcess.PT._fullCONTROLL);
            applychange.Enabled = false;
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None
            };
            Root rc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
            var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);

            //Wait for disposable
            if (dc.Mods.Count != fp.Controls.Count)
            {
                dc.Mods.Clear();
                int js = 0;
                foreach (string m in mdirs)
                {
                    js++;
                    dc.Mods.Add(new Mod() { Enabled = true, Title = Path.GetFileName(m), Priority = js, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });
                }
                string scx = JsonConvert.SerializeObject(dc);
                File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", scx);
            }




            int prioMOD = 0;
            foreach (Control t in fp.Controls)
            {
                prioMOD++;
                //Start Parsing
                if (((CheckBox)t).Checked)
                {
                    //if 0 json entries

                    foreach (var item in dc.Mods)
                    {
                        if (((CheckBox)t).Text == item.Path.Replace("subscribed//", ""))
                        {
                            item.Title = t.Text;
                            item.Priority = prioMOD;
                            item.Enabled = true;
                            rc.Mods.Add(item);
                            continue;
                        }


                    }

                    int i = 0;
                    foreach (string m in mdirs)
                    {
                        i++;
                        if (Path.GetFileName(m) == ((CheckBox)t).Text)
                        {
                            dc.Mods.Add(new Mod() { Enabled = true, Title = Path.GetFileName(m), Priority = i, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });
                        }

                    }



                }
                else if (((CheckBox)t).Checked == false)
                {
                    try
                    {
                        int cmdir = mdirs.Count();
                        progressBar1.Maximum = cmdir;
                        progressBar1.Value = 0;
                        progressBar1.Visible = true;
                        int i = 0;
                        foreach (string m in mdirs)
                        {
                            i++;

                            //string str = Regex.Replace(Path.GetFileName(m), "\\d+_", "");
                            //str = Regex.Replace(str, "_", " ");
                            //str = Regex.Replace(str, @"[[][\s\S]*?[]]", "");

                            if (Path.GetFileName(m) == ((CheckBox)t).Text)
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
                //END PARSING
                modsta.Visible = true;
                modsta.Image = Properties.Resources.saved;



            }

            string sc = JsonConvert.SerializeObject(rc);
            if (listPROFILE.SelectedItem == null)
            {
                MessageBox.Show("Please Run AoE2 DE Then close it once you reach the menu screen. Then Restart Mods Locker.", "Mods Locker");
                applychange.Enabled = true;
                return;
            }
            Thread.Sleep(100);
            //var fileInfo = new FileInfo(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json");
            //if (fileInfo.IsReadOnly)
            //    fileInfo.IsReadOnly = false;

            string jsonPath = USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json";
            try
            {


                if (File.Exists(jsonPath))
                {
                    File.Delete(jsonPath);
                }
                ////Wait for disposable
                //GC.Collect();
                //GC.WaitForPendingFinalizers();


                using (StreamWriter writer = new StreamWriter(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", false))
                {
                    writer.Write(sc);
                }

            }
            catch (SystemException res)
            {
                MessageBox.Show(res.ToString());
                applychange.Enabled = true;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", sc);

            }
            listPROFILE.Items.Clear();

            ScanDirectories(_curINDEX);

            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                modsta.Visible = false;
            });
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
        private bool isEDIBL(string path)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    bool canRead = fs.CanRead;
                    bool canWrite = fs.CanWrite;
                    if (canRead && canWrite)
                        return true;
                    else
                        return false;
                }
            }
            catch (SystemException)
            {
                return false;
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
                    if (isEDIBL(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json"))
                        dc = JsonConvert.DeserializeObject<Root>(File.ReadAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json"));
                    else
                    {
                        locker.ImageIndex = 0;
                        fp.Controls.Add(new Label() { Name = "warnlbl", Text = "Press The Lock Icon To Edit and Unlock Your Mods", AutoSize = true, ForeColor = System.Drawing.Color.DarkRed });
                        MessageBox.Show("Your Mods are Currently Locked.\nYou can Click The Lock icon to enable the edit mode.", "Mod Locker");
                        return;
                    }

                    var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);

                    if (dc == null)
                    {
                        dc = JsonConvert.DeserializeObject<Root>(Properties.Resources.empty);
                        MODict.Clear();
                        int js = 0;
                        foreach (string m in mdirs)
                        {
                            js++;
                            dc.Mods.Add(new Mod() { Enabled = false, Title = Path.GetFileName(m), Priority = js, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });
                        }
                        string scx = JsonConvert.SerializeObject(dc);
                        File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", scx);
                    }

                    int j = 0;
                    foreach (var item in dc.Mods)
                    {
                        i++;
                        fp.Controls.Add(new CheckBox() { Name = "cb_" + i.ToString(), Text = item.Path.Replace("subscribed//", ""), Checked = item.Enabled, AutoSize = true });
                        string itpath = item.Path ?? "";
                        try
                        {
                            MODict.Add(itpath.Replace(@"subscribed//", @"") ?? "subscribed//", USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed\" + itpath.Replace(@"subscribed//", @"") ?? "subscribed//");

                        }
                        catch (Exception)
                        {
                            MODict.Clear();
                            dc.Mods.Clear();
                            int js = 0;
                            foreach (string m in mdirs)
                            {
                                js++;
                                dc.Mods.Add(new Mod() { Enabled = true, Title = Path.GetFileName(m), Priority = js, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });
                            }
                            string scx = JsonConvert.SerializeObject(dc);
                            File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", scx);
                            break;
                        }



                    }
                    var lstcb = new List<string>();
                    foreach (Control cr in fp.Controls)
                        if (cr is CheckBox)
                            lstcb.Add(cr.Text);
                    bool isEMPTY = false;

                    if (dc.Mods.Count == 0)
                        isEMPTY = true;
                    else
                        isEMPTY = false;

                    foreach (string m in mdirs)
                    {

                        j++;

                        if (isEMPTY)
                        {
                            dc.Mods.Add(new Mod() { Enabled = true, Title = Path.GetFileName(m), Priority = j, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });

                        }

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

                    MessageBox.Show(ty.ToString(), "Mod Locker");
                }
                catch (Newtonsoft.Json.JsonReaderException)
                {
                    var mdirs = Directory.GetDirectories(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\subscribed", "*", System.IO.SearchOption.TopDirectoryOnly);
                    int js = 0;
                    foreach (string m in mdirs)
                    {
                        js++;
                        dc.Mods.Add(new Mod() { Enabled = true, Title = Path.GetFileName(m), Priority = js, Path = @"//" + Path.GetFileName(m), LastUpdate = "", PublishID = 0, WorkshopID = 0 });
                    }
                    string scx = JsonConvert.SerializeObject(dc);
                    File.WriteAllText(USERprofiles[listPROFILE.SelectedItem.ToString()] + @"\mods\mod-status.json", scx);
                }
                catch (Exception exd)
                {
                    MessageBox.Show(exd.ToString());
                    locker.ImageIndex = 0;
                    fp.Controls.Add(new Label() { Name = "warnlbl", Text = "Press The Lock Icon To Edit and Unlock Your Mods", AutoSize = true, ForeColor = System.Drawing.Color.DarkRed });
                    MessageBox.Show("Your Mods are Currently Locked.\nYou can Click The Lock icon to enable the edit mode.", "Mod Locker");
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
