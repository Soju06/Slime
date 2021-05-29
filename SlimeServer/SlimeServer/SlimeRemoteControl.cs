using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;
using System.Reflection;

namespace SlimeServer
{
    public partial class SlimeRemoteControl : UserControl
    {
        #region DllImport
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        #endregion
        [Description("Socket")]
        public Socket TSocket { get; set; }
        private IPEndPoint FTPPoint;
        private DirectoryInfo FTPPath;
        private NodeView NodeView = null;
        private Explorer FileExplorer = null;
        public SlimeRemoteControl(IPEndPoint ftpPoint, DirectoryInfo ftpPath, Socket socket)
        {
            TSocket = socket;
            FTPPath = ftpPath;
            FTPPoint = ftpPoint;
            if (!FTPPath.Exists)
                FTPPath.Create();
            InitializeComponent();
            logView.Items.Clear();
            IntPtr ip = CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 21, 21);
            int i = SetWindowRgn(button1.Handle, ip, true);
            IntPtr ip2 = CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 21, 21);
            int i2 = SetWindowRgn(button2.Handle, ip2, true);
            Refresh();
            label2.Text = "\\ " + TSocket.RemoteEndPoint.ToString();
            comboBox1.DataSource = Enum.GetValues(typeof(MessageCode));
            comboBox1.SelectedIndex = 0;
            logView.DoubleBuffered(true);
        }
        
        private void CommandButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            #region System
            if (button.Text == "Shutdown")
            {
                if(TaskStart())
                {
                    try
                    {
                        Send(MessageCase.Default(MessageCode.Command, "Shutdown"));
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            else if (button.Text == "Reboot")
            {
                if (TaskStart())
                {
                    try
                    {
                        Send(MessageCase.Default(MessageCode.Command, "Reboot"));
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Screenshot
            else if (button.Text == "Screenshot")
            {
                if (TaskStart())
                {
                    try
                    {
                        if (!Directory.Exists(string.Format(@"{0}\Screenshot", FTPPath.FullName)))
                            Directory.CreateDirectory(string.Format(@"{0}\Screenshot", FTPPath.FullName));
                        string tmp = GetRandomString(8);
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.ScreenShot, 
                                string.Format
                                (
                                    "ftp://{0}:{1}/ScreenShot/ScreenShot_{2}.png",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    tmp
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region FTP Download
            else if (button.Text == "FTP Download")
            {
                if (TaskStart())
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.InitialDirectory = FTPPath.FullName;
                    if(dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.FileName.Contains(FTPPath.FullName + "\\"))
                        {
                            try
                            {
                                string uri = dialog.FileName.Replace(FTPPath.FullName + "\\", "");
                                uri = uri.Replace("\\", "/");
                                string filePath = GetPathForm.Show(new FileInfo(dialog.FileName), "FilePath");
                                if (filePath == null)
                                    return;
                                Send
                                (
                                    MessageCase.Default
                                    (
                                        MessageCode.FTPDownload,
                                        string.Format
                                        (
                                            "ftp://{0}:{1}/{2}\x01{3}",
                                            FTPPoint.Address,
                                            FTPPoint.Port,
                                            uri,
                                            filePath
                                        )
                                    )
                                );
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage(ex);
                            }
                        }
                        else
                            MessageBox.Show("FTP서버 하위 경로만 가능합니다.");
                    }
                    if(dialog != null)
                        dialog.Dispose();
                }
            }
            #endregion
            #region FTP Upload
            else if (button.Text == "FTP Upload")
            {
                if (TaskStart())
                {
                    string filePath = GetPathForm.Show(new FileInfo("temp"), "FilePath");
                    if (filePath == null)
                        return;
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = "File|*.*";
                    dialog.InitialDirectory = FTPPath.FullName;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.FileName.Contains(FTPPath.FullName + "\\"))
                        {
                            try
                            {
                                string uri = dialog.FileName.Replace(FTPPath.FullName + "\\", "");
                                uri = uri.Replace("\\", "/");
                                Send
                                (
                                    MessageCase.Default
                                    (
                                        MessageCode.FTPUpload,
                                        string.Format
                                        (
                                            "ftp://{0}:{1}/{2}\x01{3}",
                                            FTPPoint.Address,
                                            FTPPoint.Port,
                                            uri,
                                            filePath
                                        )
                                    )
                                );
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage(ex);
                            }
                        }
                        else
                            MessageBox.Show("FTP서버 하위 경로만 가능합니다.");
                    }
                    if (dialog != null)
                        dialog.Dispose();
                }
            }
            #endregion
            #region HTTP Download
            else if (button.Text == "HTTP Download")
            {
                if (TaskStart())
                {
                    string url = GetUrl.Show();
                    if (url == null)
                        return;
                    string filePath = GetPathForm.Show(new FileInfo("temp"),"FilePath");
                    if (filePath == null)
                        return;
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.HTTPDownload,
                                string.Format
                                (
                                    "{0}\x01{1}",
                                    url,
                                    filePath
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Get Node
            else if (button.Text == "Get Node")
            {
                if (TaskStart())
                {
                    string dirPath = GetPathForm.Show(new FileInfo("temp"), "Directory");
                    if (dirPath == null)
                        return;
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.GetNode,
                                string.Format
                                (
                                    "ftp://{0}:{1}/Nodes{2}.Nodes\x01{3}",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    GetRandomString(8),
                                    dirPath
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region File Info
            else if (button.Text == "File Info")
            {
                if (TaskStart())
                {
                    try
                    {
                        string filePath = GetPathForm.Show(new FileInfo("temp"));
                        if (filePath == null)
                            return;
                        Send(MessageCase.Default(MessageCode.FileInfo, filePath));
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Process Start
            else if (button.Text == "Process Start")
            {
                if (TaskStart())
                {
                    string filePath = GetPathForm.Show(new FileInfo("temp"), "File");
                    if (filePath == null)
                        return;
                    string args = GetPathForm.Show(new FileInfo("temp"), "Arguments");
                    if (args == null)
                        args = "";
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.ProcessStart,
                                string.Format
                                (
                                    "{0}\x01{1}",
                                    filePath,
                                    args
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Process Kill
            else if (button.Text == "Process Kill")
            {
                if (TaskStart())
                {
                    string processName = GetUrl.Show("ProcessName");
                    if (processName == null)
                        return;
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.ProcessKill,
                                processName
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Get Process
            else if (button.Text == "Get Processes")
            {
                if (TaskStart())
                {
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.GetProcess,
                                string.Format
                                (
                                    "ftp://{0}:{1}/GetProcesses{2}.Processes",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    GetRandomString(8)
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Slime Log
            else if (button.Text == "Slime Log")
            {
                if (TaskStart())
                {
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FTPUpload,
                                string.Format
                                (
                                    "ftp://{0}:{1}/ClientLog{2}.log\x01{3}",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    GetRandomString(8),
                                    @"%ClientPath%\ClientLog.log"
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
            #region Explorer
            else if (button.Text == "Explorer")
            {
                if (TaskStart())
                {
                    try
                    {
                        if (FileExplorer != null)
                            FileExplorer.Dispose();
                        FileExplorer = new Explorer();
                        FileExplorer.FormClosed += FileExplorerClosed;
                        FileExplorer.button.Click += FileExplorerClick;

                        FileExplorer.FileView.DoubleClick += FileViewDoubleClick;
                        FileExplorer.FilePropertyToolStrip.Click += FileExplorerPropertyClick;
                        FileExplorer.FileUploadToolStrip.Click += FileExplorerUploadClick;
                        FileExplorer.FileRenameToolStrip.Click += FileExplorerRenameClick;
                        FileExplorer.FileMoveToolStrip.Click += FileExplorerMoveClick;
                        FileExplorer.FileRemoveToolStrip.Click += FileExplorerRemoveClick;
                        FileExplorer.DirectoryMoveToolStrip.Click += DirectoryExplorerMoveClick;
                        FileExplorer.DirectoryRemoveToolStrip.Click += DirectoryExplorerRemoveClick;
                        FileExplorer.ProcessStartToolStrip.Click += ProcessStartClick;

                        FileExplorer.PathTextBox.Text = @"C:\";

                        FileExplorer.Show();
                        FileExplorer.button.PerformClick();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
        }
        #region Explorer

        #region ProcessStart
        private void ProcessStartClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                if (MessageBox.Show("Are you sure you want to remove the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string args = GetPathForm.Show(new FileInfo("temp"), "Arguments");
                        if (args == null)
                            args = "";
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.ProcessStart,
                                string.Format
                                (
                                    "{0}\x01{1}",
                                    filePath,
                                    args
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region DirectoryRemove
        private void DirectoryExplorerRemoveClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string dirPath = FileExplorer.DirectoryNameToolStrip.Text;
                if (MessageBox.Show("Are you sure you want to remove the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.DirectoryDelete,
                                dirPath
                            )
                        );
                        FileExplorerRefresh();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region DirectoryMove
        private void DirectoryExplorerMoveClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string dirPath = FileExplorer.DirectoryNameToolStrip.Text;
                if (MessageBox.Show("Do you want to move the directory?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string move = GetUrl.Show("Move", dirPath);
                    if (move == null)
                        return;
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.DirectoryMove,
                                string.Format
                                (
                                    "{0}\x01{1}",
                                    dirPath,
                                    move
                                )
                            )
                        );
                        FileExplorerRefresh();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region Remove
        private void FileExplorerRemoveClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                if (MessageBox.Show("Are you sure you want to remove the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FileDelete,
                                filePath
                            )
                        );
                        FileExplorerRefresh();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region Move
        private void FileExplorerMoveClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                if (MessageBox.Show("Do you want to move the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string move = GetUrl.Show("Move", filePath);
                        if (move == null)
                            return;
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FileMove,
                                string.Format
                                (
                                    "{0}\x01{1}",
                                    filePath,
                                    move
                                )
                            )
                        );
                        FileExplorerRefresh();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region Rename
        private void FileExplorerRenameClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                if (MessageBox.Show("Do you want to change the name?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        FileInfo info = new FileInfo(filePath);
                        string rename = GetUrl.Show("Rename", info.Name);
                        if (rename == null)
                            return;
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FileMove,
                                string.Format
                                (
                                    "{0}\x01{1}\\{2}",
                                    info.FullName,
                                    info.Directory,
                                    rename
                                )
                            )
                        );
                        FileExplorerRefresh();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region Upload
        private void FileExplorerUploadClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                if (TaskStart())
                {
                    try
                    {
                        if (!Directory.Exists(FTPPath.FullName + @"\Explorer\Upload"))
                            Directory.CreateDirectory(FTPPath.FullName + @"\Explorer\Upload");
                        FileInfo info = new FileInfo(filePath);
                        string uri = string.Format("Explorer/Upload/{0}-{1}", GetRandomString(4), info.Name);
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FTPUpload,
                                string.Format
                                (
                                    "ftp://{0}:{1}/{2}\x01{3}",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    uri,
                                    filePath
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
        }
        #endregion

        #region Property
        private void FileExplorerPropertyClick(object sender, EventArgs e)
        {
            if (FileExplorer != null)
            {
                string filePath = FileExplorer.FileNameToolStrip.Text;
                try
                {
                    Send(MessageCase.Default(MessageCode.FileInfo, filePath));
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region SubPathRead
        private void FileViewDoubleClick(object sender, EventArgs e)
        {
            if(FileExplorer.FileView.SelectedItems.Count == 1 && FileExplorer.FileView.SelectedItems[0].ImageIndex == 0)
            {
                string text = FileExplorer.FileView.SelectedItems[0].Text;
                string path;
                if (text == "..")
                {
                    FileInfo info = new FileInfo(FileExplorer.path);
                    DirectoryInfo directory = info.Directory;
                    if (directory == null)
                    {
                        path = "/DriveList";
                        FileExplorer.path = "/D";
                    }
                    else
                        path = directory.FullName;
                }
                else if(FileExplorer.path == "/D")
                    path = text;
                else
                    path = string.Format("{0}\\{1}", FileExplorer.path, text);
                try
                {
                    CreateExplorerDirectory();
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.GetFiles,
                            string.Format
                            (
                                "ftp://{0}:{1}/Explorer/Explorer{2}.ExplorerNodes\x01{3}",
                                FTPPoint.Address,
                                FTPPoint.Port,
                                GetRandomString(8),
                                path
                            )
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region CreateExplorerDirectory
        private void CreateExplorerDirectory()
        {
            if (!Directory.Exists(FTPPath.FullName + @"\Explorer"))
                Directory.CreateDirectory(FTPPath.FullName + @"\Explorer");
        }
        #endregion

        #region FileExplorerClick
        private void FileExplorerClick(object sender, EventArgs e)
        {
            try
            {
                CreateExplorerDirectory();
                Send
                (
                    MessageCase.Default
                    (
                        MessageCode.GetFiles,
                        string.Format
                        (
                            "ftp://{0}:{1}/Explorer/Explorer{2}.ExplorerNodes\x01{3}",
                            FTPPoint.Address,
                            FTPPoint.Port,
                            GetRandomString(8),
                            FileExplorer.PathTextBox.Text
                        )
                    )
                );
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        #endregion

        #region FileExplorerClosed
        private void FileExplorerClosed(object sender, FormClosedEventArgs e)
        {
            if (FileExplorer != null)
                FileExplorer.Dispose();
        }
        #endregion

        #region Refresh
        private void FileExplorerRefresh()
        {
            try
            {
                CreateExplorerDirectory();
                Send
                (
                    MessageCase.Default
                    (
                        MessageCode.GetFiles,
                        string.Format
                        (
                            "ftp://{0}:{1}/Explorer/Explorer{2}.ExplorerNodes\x01{3}",
                            FTPPoint.Address,
                            FTPPoint.Port,
                            GetRandomString(8),
                            FileExplorer.path
                        )
                    )
                );
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        #endregion

        #endregion

        #region ReceiveFailed
        public void ReceiveFailed(ReceiveFailedInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                Add("ReceiveFailed", "Invalid json code sent.", info.ClientSocket.RemoteEndPoint.ToString(), info.Code);
            }));
        }
        #endregion

        public void Receive(MessageReceive info)
        {
            try
            {
                if (info.Address == TSocket)
                {
                    Add(info.Code.ToString(), info.Message, info.Address.RemoteEndPoint.ToString(), info);
                    if (info.Code == MessageCode.ScreenShotOK)
                    {
                        try { Process.Start(new ProcessStartInfo(UrlToPath(info.Message))); }
                        catch (Exception ex) { ErrorMessage(ex); }
                    }
                    else if (info.Code == MessageCode.FTPUploadOK)
                    {
                        FileInfo file = new FileInfo(UrlToPath(info.Message));
                        OpenFolderAndSelectItem(file.DirectoryName, file.Name);
                    }
                    else if (info.Code == MessageCode.FileInfoReturn)
                        FileInfoForm.Show(info.Object);
                    else if (info.Code == MessageCode.GetNodeOK)
                    {
                        NodeViewShow(UrlToPath(info.Message));
                    }
                    else if (info.Code == MessageCode.FileMoveOK && SelcetNode != null)
                    {
                        FileInfo file = new FileInfo(info.Message);
                        if (file.Directory.FullName == new FileInfo(SelcetNode.FullPath.Replace(NodeView.NodeName + "\\", "")).Directory.FullName)
                        {
                            string name = new FileInfo(info.Message).Name;
                            SelcetNode.Name = name;
                            SelcetNode.Text = name;
                        }
                        else
                        {
                            SelcetNode.Remove();
                            NodeView.CreateNode(NodeView.ViewNode.Nodes[0], info.Message, true);
                        }
                    }
                    else if (info.Code == MessageCode.FileDeleteOK && SelcetNode != null)
                    {
                        SelcetNode.Remove();
                    }
                    else if (info.Code == MessageCode.DirectoryMoveOK && SelcetNode != null)
                    {
                        FileInfo file = new FileInfo(info.Message);
                        if (file.Directory.FullName == new FileInfo(SelcetNode.FullPath.Replace(NodeView.NodeName + "\\", "")).Directory.FullName)
                        {
                            string name = new DirectoryInfo(info.Message).Name;
                            SelcetNode.Name = name;
                            SelcetNode.Text = name;
                        }
                        else
                        {
                            SelcetNode.Remove();
                            NodeView.CreateNode(NodeView.ViewNode.Nodes[0], info.Message, false);
                        }
                    }
                    else if (info.Code == MessageCode.DirectoryDeleteOK && SelcetNode != null)
                    {
                        SelcetNode.Remove();
                    }
                    else if (info.Code == MessageCode.GetFilesOK && FileExplorer != null)
                        FileExplorer.Receive(UrlToPath(info.Message));
                    else if (info.Code == MessageCode.GetFilesFailed && FileExplorer != null)
                        MessageBox.Show(info.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if(info.Code == MessageCode.Text)
                    {
                        string[] tmp = info.Message.Split('\x01');
                        if(tmp.Length > 1)
                        {
                            if (tmp[0] == "ClientVersion")
                                ClientVersionButton.Text = string.Format("Client Version: {0}", tmp[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        #region NodeView    

        private void NodeViewShow(string path)
        {
            if (NodeView != null)
                NodeView.Dispose();
            NodeView = new NodeView(path);
            NodeView.FormClosed += NodeViewClosed;
            NodeView.FilePropertyToolStrip.Click += FilePropertyClick;
            NodeView.FileUploadToolStrip.Click += FileUploadClick;
            NodeView.FileRenameToolStrip.Click += FileRenameClick;
            NodeView.FileMoveToolStrip.Click += FileMoveClick;
            NodeView.FileRemoveToolStrip.Click += FileRemoveClick;
            NodeView.DirectoryMoveToolStrip.Click += DirectoryMoveClick;
            NodeView.DirectoryRemoveToolStrip.Click += DirectoryRemoveClick;
            NodeView.Show();
        }

        #region DirectoryRemoveClick
        private void DirectoryRemoveClick(object sender, EventArgs e)
        {
            SelcetNode = NodeView.SelectNode;
            string dirPath = NodeView.DirectoryNameToolStrip.Text;
            if (MessageBox.Show("Are you sure you want to remove the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.DirectoryDelete,
                            dirPath
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region DirectoryMoveClick
        private void DirectoryMoveClick(object sender, EventArgs e)
        {
            SelcetNode = NodeView.SelectNode;
            string dirPath = NodeView.DirectoryNameToolStrip.Text;
            if (MessageBox.Show("Do you want to move the directory?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string move = GetUrl.Show("Move", dirPath);
                if (move == null)
                    return;
                try
                {
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.DirectoryMove,
                            string.Format
                            (
                                "{0}\x01{1}",
                                dirPath,
                                move
                            )
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region FileRemoveClick
        private void FileRemoveClick(object sender, EventArgs e)
        {
            SelcetNode = NodeView.SelectNode;
            string filePath = NodeView.FileNameToolStrip.Text;
            if (MessageBox.Show("Are you sure you want to remove the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.FileDelete,
                            filePath
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region FileMoveClick
        private void FileMoveClick(object sender, EventArgs e)
        {
            SelcetNode = NodeView.SelectNode;
            string filePath = NodeView.FileNameToolStrip.Text;
            if (MessageBox.Show("Do you want to move the file?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    string move = GetUrl.Show("Move", filePath);
                    if (move == null)
                        return;
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.FileMove,
                            string.Format
                            (
                                "{0}\x01{1}", 
                                filePath,
                                move
                            )
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        private TreeNode SelcetNode = null;

        #region FileRenameClick
        private void FileRenameClick(object sender, EventArgs e)
        {
            SelcetNode = NodeView.SelectNode;
            string filePath = NodeView.FileNameToolStrip.Text;
            if (MessageBox.Show("Do you want to change the name?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    FileInfo info = new FileInfo(filePath);
                    string rename = GetUrl.Show("Rename", info.Name);
                    if (rename == null)
                        return;
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.FileMove,
                            string.Format
                            (
                                "{0}\x01{1}{2}", 
                                info.FullName,
                                info.Directory,
                                rename
                            )
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        private void FilePropertyClick(object sender, EventArgs e)
        {
            #region NodeView FileProperty
            if (NodeView != null)
            {
                string filePath = NodeView.FileNameToolStrip.Text; 
                try
                {
                    Send(MessageCase.Default(MessageCode.FileInfo, filePath));
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
            #endregion
        }

        private void FileUploadClick(object sender, EventArgs e)
        {
            #region NodeView FTP Upload
            if (NodeView != null)
            {
                string filePath = NodeView.FileNameToolStrip.Text;
                if (TaskStart())
                {
                    try
                    {
                        FileInfo info = new FileInfo(filePath);
                        string uri = string.Format("Upload{0}_{1}", GetRandomString(8), info.Name);
                        Send
                        (
                            MessageCase.Default
                            (
                                MessageCode.FTPUpload,
                                string.Format
                                (
                                    "ftp://{0}:{1}/{2}\x01{3}",
                                    FTPPoint.Address,
                                    FTPPoint.Port,
                                    uri,
                                    filePath
                                )
                            )
                        );
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex);
                    }
                }
            }
            #endregion
        }

        #region NodeViewClosed
        private void NodeViewClosed(object sender, FormClosedEventArgs e)
        {
            if (NodeView != null)
                NodeView.Dispose();
            NodeView = null;
        }
        #endregion

        #endregion


        //===============================//
        // :      Auxiliary area      :  //
        //===============================//

        private bool TaskStart()
        {
            if (checkBox1.Checked)
                return true;
            if (MessageBox.Show("Are you sure you want to start this task?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                return true;
            else
                return false;
        }
        private void ErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Send(JObject jObject)
        {
            byte[] message = Encoding.UTF8.GetBytes(jObject.ToString());
            TSocket.Send(message);
            Add(((MessageCode)int.Parse(jObject["Type"].ToString())).ToString(),jObject["Value"].ToString(),TSocket.RemoteEndPoint.ToString());
        }

        #region GetRandomString
        public static string GetRandomString(int _totLen)
        {
            Random rand = new Random();
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, _totLen).Select(x => input[rand.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }
        #endregion

        #region GetTime
        public string GetTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            }
        }
        #endregion

        #region ListAdd
        public void Add(ListViewItem item)
        {
            logView.Items.Add(item);
            logView.Items[logView.Items.Count - 1].EnsureVisible();
        }
        public void Add(string code, string value, string IP)
        {
            ListViewItem item = new ListViewItem(GetTime);
            item.SubItems.Add(code);
            item.SubItems.Add(value);
            item.SubItems.Add(IP);
            logView.Items.Add(item);
            logView.Items[logView.Items.Count - 1].EnsureVisible();
        }
        public void Add(string code, string value, string IP, object Tag)
        {
            ListViewItem item = new ListViewItem(GetTime);
            item.SubItems.Add(code);
            item.SubItems.Add(value);
            item.SubItems.Add(IP);
            item.Tag = Tag;
            logView.Items.Add(item);
            logView.Items[logView.Items.Count - 1].EnsureVisible();
        }
        #endregion

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

        #region ExitClick
        public delegate void ExitClick();
        [Description("ExitClick")]
        public event ExitClick exitClick;
        private void BackButton_Click(object sender, EventArgs e)
        {
            exitClick();
        }
        #endregion

        #region SendClick
        private void SendClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ValueTextBox.Text))
                return;
            try
            {
                Send
                (
                    MessageCase.Default
                    (
                        (MessageCode)comboBox1.SelectedValue,
                        ValueTextBox.Text
                    )
                );
                ValueTextBox.Text = "";
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        #endregion

        #region X01Click
        private void X01Click(object sender, EventArgs e)
        {
            ValueTextBox.SelectedText = "\x01";
        }
        #endregion

        private string UrlToPath(string FtpUrl)
        {
            string tmp = string.Format("ftp://{0}:{1}", FTPPoint.Address, FTPPoint.Port);
            return FTPPath.FullName + FtpUrl.Replace(tmp, "").Replace("/", "\\");
        }

        #region GetTreeViewClick
        private void GetTreeViewClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Node Files|*.Nodes|All Files|*.*";
            dialog.InitialDirectory = FTPPath.FullName;
            if(dialog.ShowDialog() == DialogResult.OK)
                NodeViewShow(dialog.FileName);
            if(dialog != null)
                dialog.Dispose();
        }
        #endregion

        #region Closing
        public void Closing()
        {
            if (NodeView != null)
            {
                NodeView.Close();
                NodeView.Dispose();
            }
            if (FileExplorer != null)
            {
                FileExplorer.Close();
                FileExplorer.Dispose();
            }
        }
        #endregion

        #region SystemInfoButtonClick
        private void SystemInfoButtonClick(object sender, EventArgs e)
        {
            if (TaskStart())
            {
                try
                {
                    if (!Directory.Exists(string.Format(@"{0}\SystemInfo", FTPPath.FullName)))
                        Directory.CreateDirectory(string.Format(@"{0}\SystemInfo", FTPPath.FullName));
                    Send
                    (
                        MessageCase.Default
                        (
                            MessageCode.Command,
                            string.Format
                            (
                                "SysInfo{3}ftp://{0}:{1}/SystemInfo/Info_{2}.log",
                                FTPPoint.Address,
                                FTPPoint.Port,
                                GetRandomString(8),
                                '\x01'
                            )
                        )
                    );
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex);
                }
            }
        }
        #endregion

        #region GetClientVersion
        private void GetClientVersion(object sender, EventArgs e)
        {
            try
            {
                Send
                (
                    MessageCase.Default
                    (
                        MessageCode.Command,
                        "ClientVersion"
                    )
                );
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }
        #endregion
    }

    #region DoubleBuffered
    public static class Extensions
    {
        public static void DoubleBuffered(this Control control, bool enabled)
        {
            var prop = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(control, enabled, null);
        }
    }
    #endregion
}
