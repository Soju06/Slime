using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class FileInfoForm : Form
    {
        public FileInfoForm()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public static void Show(JObject jObject)
        {
            Thread thread = new Thread(() =>
            {
                FileInfoForm fileInfoForm = new FileInfoForm();
                bool nofile = false;
                try { fileInfoForm.NameTextBox.Text = jObject["Name"].ToString(); }
                catch
                {
                    nofile = true;
                    fileInfoForm.NameTextBox.Text = jObject["Value"].ToString();
                }
                if (!nofile)
                    try
                    {
                        fileInfoForm.Exists.Text = "Exists: " + jObject["Exists"].ToString();
                        fileInfoForm.SizeLabel.Text = jObject["Size"] == null ? "" : GetFileSize((double)jObject["Size"]) + ", " + jObject["Size"].ToString() + "Bytes";
                        fileInfoForm.CreationTime.Text = jObject["CreationTime"] == null ? "" : jObject["CreationTime"].ToString();
                        fileInfoForm.LastWriteTime.Text = jObject["LastWriteTime"] == null ? "" : jObject["LastWriteTime"].ToString();
                        fileInfoForm.LastAccessTime.Text = jObject["LastAccessTime"] == null ? "" : jObject["LastAccessTime"].ToString();
                        fileInfoForm.IsReadOnlyLabel.Text = string.Format("IsReadOnly: {0}", jObject["IsReadOnly"] == null ? "null" : jObject["IsReadOnly"].ToString());
                        fileInfoForm.LocationLabel.Text = jObject["DirectoryName"] == null ? "" : jObject["DirectoryName"].ToString();
                    }
                    catch { }
                try { fileInfoForm.ErrorTextBox.Text = jObject["ErrorCode"] == null ? "" : jObject["ErrorCode"].ToString(); } catch { }
                fileInfoForm.ShowDialog();
                fileInfoForm.Dispose();
            });
            thread.Start();
        }

        private static string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = string.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = string.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = string.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }
    }
}
