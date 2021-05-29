using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeServer
{
    public partial class MainForm : Form //MaterialForm
    {
        SlimesControl slimes = new SlimesControl();
        ServerLogControl serverLog = new ServerLogControl();
        CreateControl createControl = new CreateControl();

        public MainForm()
        {
            InitializeComponent();
            ControlsInitializeComponent();
            LOInitializeComponent();
            JsonSetting.Reset();
            //var skin = MaterialSkinManager.Instance;
            //skin.AddFormToManage(this);
            //skin.Theme = MaterialSkinManager.Themes.DARK;
            //skin.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            StartInfo info = JsonSetting.Get.Info;
            serverLog.Add("Server", "Started", info.ServerAddress.ToString());
            Server.Start(info);
            Server.Connected += Connected;
            Server.Disconnected += Disconnected;
            Server.Receive += Receive;
            Server.ReceiveFailed += ReceiveFailed;
            Server.Disconnected += slimes.Disconnected;
            Server.Receive += slimes.Receive;
            Server.ReceiveFailed += slimes.ReceiveFailed;
        }

        private void ReceiveFailed(ReceiveFailedInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                serverLog.Add("ReceiveFailed", "Invalid json code sent.", info.ClientSocket.RemoteEndPoint.ToString(), info.Code);
            }));
        }

        private void Receive(MessageReceive info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                serverLog.Add(info.Code.ToString(), info.Message, info.Address.RemoteEndPoint.ToString(), info);
            }));
        }

        private void Disconnected(ClientDisconnectedInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                serverLog.Add(info.DisconnectedCode.ToString(), "Client is Disconnected.", info.ClientAddress.ToString());
                slimes.SlimeRemove(info.ClientSocket);
            }));
        }

        private void Connected(ClientInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                serverLog.Add("None", "Client is Connected.", info.ClientSocket.RemoteEndPoint.ToString());
                slimes.SlimeAdd(info.ClientSocket);
            }));
        }

        #region MenuClick
        private void MenuClick(SelectControl control, string Name)
        {
            foreach (Control item in LeftControlPanel.Controls)
            {
                if(item.GetType().ToString() == "SlimeServer.SelectControl")
                {
                    if(control == item)
                        ((SelectControl)item).Checked = true;
                    else
                        ((SelectControl)item).Checked = false;
                }
            }
            foreach (Control item in GetControls)
            {
                item.Visible = Name == item.Name;
            }
        }
        #endregion

        #region ControlsInitializeComponent
        private Control[] GetControls;
        private void ControlsInitializeComponent()
        {
            GetControls = new Control[] { slimes, serverLog, createControl };
            foreach (Control item in GetControls)
            {
                DrawingPanel.Controls.Add(item);
                item.Dock = DockStyle.Fill;
                item.Show();
            }
        }
        #endregion

        #region CloseClick
        private void CloseClick(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region FormMove
        // 도대체 예전의 나는 뭐 하던 놈일까
        private Timer locationOpacity = new Timer();
        private bool OLTick = true;
        private double OpTick = 1;
        private Point MouseDownPoint;
        private void LOInitializeComponent()
        {
            locationOpacity.Tick += OpacitylocationTick;
            locationOpacity.Interval = 15;
            locationOpacity.Enabled = true;
        }
        private void FormMouseDown(object sender, MouseEventArgs e)
        {
            MouseDownPoint = new Point(e.X, e.Y);
            OLTick = false;
            OpTick = 0.8;
            locationOpacity.Start();
        }

        private void FormMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(Left - (MouseDownPoint.X - e.X),
                    Top - (MouseDownPoint.Y - e.Y));
            else
            {
                OLTick = true;
                OpTick = 1;
                locationOpacity.Start();
            }
        }
        private void OpacitylocationTick(object sender, EventArgs e)
        {
            if (OLTick)
            {
                if (Opacity != 1)
                    if (Opacity < OpTick)
                        Opacity += 0.05;
                    else
                        locationOpacity.Stop();
            }
            else
            {
                if (Opacity != 0)
                    if (Opacity > OpTick)
                        Opacity -= 0.05;
                    else
                        locationOpacity.Stop();
                else
                    locationOpacity.Stop();
            }
        }
        #endregion

        private void ThisClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }

        #region WindowsResize
        private const int cGrip = 16;
        private const int cCaption = 64;
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if(pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if(pos.X >= ClientSize.Width - cGrip && pos.Y >= ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion
    }
}