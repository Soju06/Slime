using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SlimeServer
{
    public partial class ServerLogControl : UserControl
    {
        public ServerLogControl()
        {
            InitializeComponent();
            logView.Items.Clear();
            logView.DoubleBuffered(true);
        }
        public void Add(ListViewItem item)
        {
            logView.Items.Add(item);
        }
        public void Add(string code, string value, string IP)
        {
            ListViewItem item = new ListViewItem(GetTime);
            item.SubItems.Add(code);
            item.SubItems.Add(value);
            item.SubItems.Add(IP);
            logView.Items.Add(item);
        }
        public void Add(string code, string value, string IP, object Tag)
        {
            ListViewItem item = new ListViewItem(GetTime);
            item.SubItems.Add(code);
            item.SubItems.Add(value);
            item.SubItems.Add(IP);
            item.Tag = Tag;
            logView.Items.Add(item);
        }
        public string GetTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            }
        }
    }
}
