using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class SelectControl : UserControl
    {
        public delegate void SClick(SelectControl control, string sender);
        [Description("SClick")]
        public event SClick sClick;
        public SelectControl()
        {
            InitializeComponent();
        }
        [Description("Image")]
        public Image Image
        {
            get
            {
                return TButton.Image;
            }
            set
            {
                TButton.Image = value;
            }
        }
        [Description("Text")]
        public string ForeText
        {
            get
            {
                return TButton.Text;
            }
            set
            {
                TButton.Text = value;
            }
        }
        [Description("Checked")]
        public bool Checked
        {
            get
            {
                if (TSL.BackColor == Color.FromArgb(137, 170, 255)) return true; else return false;
            }
            set
            {
                if (value)
                    TSL.BackColor = Color.FromArgb(137, 170, 255);
                else
                    TSL.BackColor = Color.FromArgb(22, 22, 22);
            }
        }
        private void TButton_Click(object sender, EventArgs e)
        {
            sClick(this, ControlTag);
        }
        public string ControlTag { get; set; }
    }
}
