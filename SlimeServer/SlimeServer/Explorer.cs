using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class Explorer : Form
    {
        public Explorer()
        {
            InitializeComponent();
            FileView.Items.Clear();
            FileView.DoubleBuffered(true);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                PathTextBox.Text = path;
                button.PerformClick();
            }
            else if (keyData == Keys.Enter)
                button.PerformClick();
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void Receive(string FilePath)
        {
            ProgressUpdate(0);
            try
            {
                FileInfo info = new FileInfo(FilePath);
                int counter = 0;
                int count = 0;
                if (info.Exists)
                {
                    string line;
                    using (StreamReader r = new StreamReader(info.FullName))
                    {
                        while (r.ReadLine() != null) { count++; }
                    }
                    List<ListViewItem> items = new List<ListViewItem>();
                    ListViewItem exit = new ListViewItem("..");
                    exit.ImageIndex = 0;
                    items.Add(exit);
                    StreamReader file = new StreamReader(info.FullName);
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Length > 3)
                        {
                            string[] tmp = line.Split('\x01');
                            if (tmp.Length == 2)
                            {
                                if (tmp[0] == "P")
                                    Invoke(new MethodInvoker(delegate ()
                                    {
                                        path = tmp[1];
                                    }));
                                else if (tmp[0] == "D")
                                {
                                    ListViewItem item = new ListViewItem(tmp[1]);
                                    item.ImageIndex = 0;
                                    items.Add(item);
                                }
                                else if (tmp[0] == "F")
                                {
                                    ListViewItem item = new ListViewItem(tmp[1]);
                                    item.ImageIndex = 1;
                                    items.Add(item);
                                }
                            }
                        }
                        ProgressUpdate(counter++ * 100 / count);
                    }
                    file.Close();
                    if (file != null)
                        file.Dispose();
                    if (ClearTempFileCheck.Checked)
                        info.Delete();
                    Invoke(new MethodInvoker(delegate ()
                    {
                        FileView.Items.Clear();
                        FileView.Items.AddRange(items.ToArray());
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ProgressUpdate(100);
        }
        private void ProgressUpdate(int value)
        {
            if (progressBar1.Value < value)
                Invoke(new MethodInvoker(delegate ()
                {
                    label1.Text = string.Format("{0}%", value);
                    progressBar1.Value = value;
                    if (value == 100)
                        LoadingPanel.Visible = false;
                    else if (LoadingPanel.Visible == false)
                        LoadingPanel.Visible = true;
                }));
        }
        private string _path;
        public string path
        {
            get { return _path; }
            set
            {
                _path = value;
                PathTextBox.Text = value;
            }
        }
        private void ItemMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && FileView.SelectedItems.Count == 1)
            {
                ListViewItem SelectItem = FileView.SelectedItems[0];
                if (IsDirectory(SelectItem))
                {
                    DirectoryNameToolStrip.Text = string.Format("{0}\\{1}", path, SelectItem.Text);
                    DirectoryContextMenu.Show(MousePosition);
                }
                else
                {
                    FileNameToolStrip.Text = string.Format("{0}\\{1}", path, SelectItem.Text);
                    FileContextMenu.Show(MousePosition);
                }
            }
        }

        private static bool IsDirectory(ListViewItem item)
        {
            if (item.ImageIndex == 0)
                return true;
            else
                return false;
        }
    }
}
