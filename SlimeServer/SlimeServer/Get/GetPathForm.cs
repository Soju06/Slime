using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class GetPathForm : Form
    {
        private string FilePath = null;
        private FileInfo SelectFile;
        public GetPathForm(FileInfo SelFile)
        {
            SelectFile = SelFile;
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else if(keyData == Keys.Enter)
                button8.PerformClick();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string tmp = textBox1.Text;
            bool tbep = string.IsNullOrWhiteSpace(textBox1.Text);
            if (button.Text == "OK")
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                    return;
                FilePath = textBox1.Text;
                Close();
            }
            else if (button.Text == "FileName")
            {
                if (tbep)
                    textBox1.Text += SelectFile.FullName;
                else
                {
                    if (textBox1.Text.Substring(textBox1.Text.Length - 1, 1) != "\\")
                        textBox1.Text += "\\";
                    textBox1.Text += SelectFile.Name;
                }
            }
            else if (button.Text == "RandomName")
            {
                if (textBox1.Text.Substring(textBox1.Text.Length - 1, 1) != "\\")
                    textBox1.Text += "\\";
                textBox1.Text += "file" + GetRandomString(8) + ".tmp";
            }
            else
            {
                textBox1.Text = string.Format("%{0}%", button.Text);
                if (!tbep)
                    try { textBox1.Text += "\\" + new FileInfo(tmp).Name; }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
            }
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

        public static string Show(FileInfo file)
        {
            GetPathForm form = new GetPathForm(file);
            form.ShowDialog();
            string f = form.FilePath;
            form.Dispose();
            return f;
        }
        public static string Show(FileInfo file, string Title)
        {
            GetPathForm form = new GetPathForm(file);
            form.Text = Title;
            form.ShowDialog();
            string f = form.FilePath;
            form.Dispose();
            return f;
        }
    }
}
