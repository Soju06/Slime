using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class GetUrl : Form
    {
        private string url = null;
        public GetUrl()
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
            else if (keyData == Keys.Enter)
                button1.PerformClick();
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;
            url = textBox1.Text;
            Close();
        }
        new public static string Show()
        {
            GetUrl form = new GetUrl();
            form.ShowDialog();
            string s = form.url;
            form.Dispose();
            return s;
        }
        public static string Show(string Title)
        {
            GetUrl form = new GetUrl();
            form.Text = Title;
            form.ShowDialog();
            string s = form.url;
            form.Dispose();
            return s;
        }
        public static string Show(string Title, string Text)
        {
            GetUrl form = new GetUrl();
            form.Text = Title;
            form.textBox1.Text = Text;
            form.ShowDialog();
            string s = form.url;
            form.Dispose();
            return s;
        }
    }
}
