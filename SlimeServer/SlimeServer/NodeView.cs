using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class NodeView : Form
    {
        public Thread thread;
        public string NodeName = null;
        public NodeView(string FileName)
        {
            InitializeComponent();
            thread = new Thread(() => { GetFileNode(FileName); });
            thread.Start();
        }
        private void GetFileNode(string Path)
        {
            TreeNode node = null;
            FileInfo info = new FileInfo(Path);
            string StartTime = null;
            string EndTime = null;
            int counter = 0;
            int count = 0;
            if (info.Exists)
            {
                string line;
                using (StreamReader r = new StreamReader(info.FullName))
                {
                    while (r.ReadLine() != null) { count++; }
                }

                StreamReader file = new StreamReader(info.FullName);
                while ((line = file.ReadLine()) != null)
                {
                    if(line.Length > 3)
                    {
                        if (line.Substring(0, 2) == "ST")
                            StartTime = line.Substring(3, line.Length - 3);
                        else if (line.Substring(0, 2) == "ET")
                            EndTime = line.Substring(3, line.Length - 3);
                        else if (line.Substring(0, 2) == "ID" && node == null)
                        {
                            NodeName = line.Substring(3, line.Length - 3);
                            node = new TreeNode(NodeName);
                        }
                        else
                        {
                            string[] tmp = line.Split('\x01');
                            if (tmp.Length == 2)
                            {
                                if (node == null)
                                    node = new TreeNode("null");
                                CreateNode(node, tmp[1], tmp[0] == "F");
                            }
                        }
                    }
                    ProgressUpdate(counter++ * 100 / count);
                }
                file.Close();
                if (file != null)
                    file.Dispose();
            }
            ProgressUpdate(100);
            Invoke(new MethodInvoker(delegate ()
            {
                Text = string.Format("NodeView - {0}", NodeName);
                label2.Text = string.Format("PCID:\n{0}\nStartTime:\n{1}\nEndTime:\n{2}", NodeName, StartTime, EndTime);
                ViewNode.Nodes.Add(node);
            }));
        }
        private void ProgressUpdate(int value)
        {
            if(progressBar1.Value < value)
                Invoke(new MethodInvoker(delegate ()
                {
                    label1.Text = string.Format("{0}%", value);
                    progressBar1.Value = value;
                    if (value == 100)
                        LoadingPanel.Visible = false;
                }));
        }
        public void CreateNode(TreeNode node, string path, bool IsFile)
        {
            TreeNode tmp = null;
            string[] Nodes = path.Split('\\');
            foreach (string item in Nodes)
            {
                if (tmp == null)
                {
                    if (!IsNode(node, item))
                    {
                        TreeNode tree = GetNode(node, item);
                        if (IsFile)
                            tree.ImageIndex = 1;
                        else
                            tree.ImageIndex = 0;
                        node.Nodes.Add(tree);
                        tmp = tree;
                    }
                    else
                        tmp = GetNode(node, item);
                }
                else
                {
                    if (!IsNode(tmp, item))
                    {
                        TreeNode tree = GetNode(tmp, item);
                        if (IsFile)
                            tree.ImageIndex = 1;
                        else
                            tree.ImageIndex = 0;
                        tmp.Nodes.Add(tree);
                        tmp = tree;
                    }
                    else
                        tmp = GetNode(tmp, item);
                }
            }
        }
        public static bool IsNode(TreeNode node, string Name)
        {
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Text == Name)
                    return true;
            }
            return false;
        }
        public static TreeNode GetNode(TreeNode node, string Name)
        {
            foreach (TreeNode item in node.Nodes)
            {
                if (item.Text == Name)
                    return item;
            }
            return new TreeNode(Name);
        }

        private void NodeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(thread != null)
            {
                thread.Abort();
                thread.Join();
            }
            ViewNode.Nodes.Clear();
        }
        public TreeNode SelectNode = null;
        private void ViewNodeClick(object sender, MouseEventArgs e)
        {
            SelectNode = ViewNode.GetNodeAt(ViewNode.PointToClient(MousePosition));
            if (e.Button == MouseButtons.Right && SelectNode != null)
            {
                if(IsDirectory(SelectNode))
                {
                    DirectoryNameToolStrip.Text = SelectNode.FullPath.Replace(NodeName + "\\", "");
                    DirectoryContextMenu.Show(MousePosition);
                }
                else
                {
                    FileNameToolStrip.Text = SelectNode.FullPath.Replace(NodeName + "\\", "");
                    FileContextMenu.Show(MousePosition);
                }
            }
        }
        private static bool IsDirectory(TreeNode node)
        {
            if (node.ImageIndex == 0)
                return true;
            else 
                return false;
        }

        private void PathClick(object sender, EventArgs e)
        {
            Clipboard.SetText(((ToolStripMenuItem)sender).Text);
        }
    }
}
