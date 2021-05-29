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

namespace SlimeServer
{
    public partial class Slime : UserControl
    {
        [DllImport("gdi32.dll")] 
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
        [DllImport("user32.dll")] 
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        public Slime()
        {
            InitializeComponent();
            IntPtr ip = CreateRoundRectRgn(0, 0, Width, Height, 20, 20);
            int i = SetWindowRgn(Handle, ip, true);
            IntPtr ip2 = CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20);
            int i2 = SetWindowRgn(button1.Handle, ip2, true);
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sClick((Socket)Tag);
        }
        [Description("Text")]
        public string ForeText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }
        public delegate void SClick(Socket socket);
        [Description("SClick")]
        public event SClick sClick;
    }
}
