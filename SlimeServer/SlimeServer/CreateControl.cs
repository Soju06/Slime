using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace SlimeServer
{
    public partial class CreateControl : UserControl
    {
        public CreateControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int BufferSize = int.Parse(textBox3.Text);
                int Port = int.Parse(textBox2.Text);
                string Address = textBox1.Text;
                bool Mode = checkBox2.Checked;
                JObject jObject = SettingObject;
                jObject["ServerSocket"]["DNSHost_Mode"] = Mode;
                jObject["ServerSocket"]["Address"] = Address;
                jObject["ServerSocket"]["Port"] = Port;
                jObject["ServerSocket"]["BufferSize"] = BufferSize;
                File.WriteAllText(@"Build\SlimeClient\Resources\Setting.json", jObject.ToString());
                LogBox.Text = "";
                LogBox.Visible = true;
                button2.Visible = true;
                new Thread(() => { BuildProgram(); }).Start();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
        }

        private void BuildProgram()
        {
            try
            {
                Log(string.Format("Program Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString()));
                RegistryKey rkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0", false);
                string dir = (string)rkey.GetValue("MSBuildToolsPath");
                FileInfo msbuild = new FileInfo(Path.Combine(dir, "msbuild.exe"));
                Log(string.Format("MSBuild Path: {0}", msbuild.FullName));
                if(!msbuild.Exists)
                {
                    Log("Msbuild does not exist!");
                    return;
                }
                Process process = new Process();
                process.StartInfo.FileName = msbuild.FullName;
                if(checkBox1.Checked)
                    process.StartInfo.Arguments = Application.StartupPath + @"\Build\SlimeClient\SlimeClientDebug.csproj";
                else
                    process.StartInfo.Arguments = Application.StartupPath + @"\Build\SlimeClient\SlimeClient.csproj";
                process.StartInfo.CreateNoWindow = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                Log(process.StandardOutput.ReadToEnd());
                Log(process.StandardError.ReadToEnd());
                OpenFolderAndSelectItem(Application.StartupPath + @"\Build\SlimeClient\bin\Debug", "SlimeClient.exe");
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        private static JObject SettingObject = new JObject
        (
            new JProperty
            (
                "ServerSocket",
                new JObject
                (
                    new JProperty("DNSHost_Mode", false),
                    new JProperty("Address", "127.0.0.1"),
                    new JProperty("Port", 65422),
                    new JProperty("BufferSize", 8192)
                )
            )
        );

        #region Log
        public void Log(string txt)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate ()
                {
                    LogBox.AppendText(txt + Environment.NewLine);
                    LogBox.ScrollToCaret();
                }));
            }
            else
            {
                LogBox.AppendText(txt + Environment.NewLine);
                LogBox.ScrollToCaret();
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            LogBox.Visible = false;
            button2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogBox.Visible = true;
            button2.Visible = true;
        }

        #region OpenFolderAndSelectItem
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);

        public static void OpenFolderAndSelectItem(string folderPath, string file)
        {
            IntPtr nativeFolder;
            uint psfgaoOut;
            SHParseDisplayName(folderPath, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

            if (nativeFolder == IntPtr.Zero)
            {
                // Log error, can't find folder
                return;
            }

            IntPtr nativeFile;
            SHParseDisplayName(Path.Combine(folderPath, file), IntPtr.Zero, out nativeFile, 0, out psfgaoOut);

            IntPtr[] fileArray;
            if (nativeFile == IntPtr.Zero)
            {
                // Open the folder without the file selected if we can't find the file
                fileArray = new IntPtr[0];
            }
            else
            {
                fileArray = new IntPtr[] { nativeFile };
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            Marshal.FreeCoTaskMem(nativeFolder);
            if (nativeFile != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(nativeFile);
            }
        }
        #endregion
    }
}
