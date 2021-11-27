using System.Diagnostics;
using System.Security.Principal;
using System.Threading;

namespace ModLocker
{
    class FileProcess
    {

        /// <summary>
        /// Author: GregStein
        /// </summary>
        public static class PT
        {
            public const string _readEXEC = "RX";
            public const string _fullCONTROLL = "F";
        }
        public class IDGROUP
        {

            public static string _admins = (new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Translate(typeof(NTAccount)).Value).Split('\\')[1];
            public static string _users = (new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null).Translate(typeof(NTAccount)).Value).Split('\\')[1];
            public static string _system = (new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null).Translate(typeof(NTAccount)).Value).Split('\\')[1];
        }

        /// <summary>
        /// Construct icacls command
        /// </summary>
        /// <param name="path">folder or file path</param>
        /// <param name="identity">from IDGROUP</param>
        /// <param name="perm">from PT</param>
        public static void _processHANDLR(string path, string identity, string perm)
        {

            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c icacls \"" + path + "\" /inheritance:r /grant:r " + identity + ":(OI)(CI)" + perm + " /T";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                while (!process.HasExited && process.Responding)
                {
                    Thread.Sleep(100);
                }
            }
        }

    }
}
