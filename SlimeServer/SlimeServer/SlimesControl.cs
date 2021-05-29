using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace SlimeServer
{
    public partial class SlimesControl : UserControl
    {
        public List<Slime> slimes = new List<Slime>();
        private SlimeRemoteControl remoteControl;
        public SlimesControl()
        {
            InitializeComponent();
        }
        public void SlimeAdd(Socket socket)
        {
            Slime slime = new Slime();
            slime.ForeText = socket.RemoteEndPoint.ToString();
            slime.Tag = socket;
            slime.sClick += SlimesClick;
            slimes.Add(slime);
            FlowLayoutSlimePanel.Controls.Add(slime);
            slime.Show();
        }
        public void ReceiveFailed(ReceiveFailedInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (remoteControl != null)
                    remoteControl.ReceiveFailed(info);
            }));
        }

        public void Receive(MessageReceive info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (remoteControl != null)
                    remoteControl.Receive(info);
            }));
        }

        public void Disconnected(ClientDisconnectedInfo info)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (remoteControl != null && info.ClientSocket == remoteControl.TSocket)
                    RemoteControlExitClick();
            }));
        }

        private void SlimesClick(Socket socket)
        {
            if (remoteControl != null)
            {
                Controls.Remove(remoteControl);
                remoteControl.Dispose();
            }
            remoteControl = new SlimeRemoteControl(JsonSetting.Get.FTPHost, JsonSetting.Get.FTPLocation, socket);
            remoteControl.exitClick += RemoteControlExitClick;
            remoteControl.Dock = DockStyle.Fill;
            Controls.Add(remoteControl);
            FlowLayoutSlimePanel.Visible = false;
        }

        private void RemoteControlExitClick()
        {
            if (remoteControl != null)
            {
                remoteControl.Closing();
                Controls.Remove(remoteControl);
                remoteControl.Dispose();
                FlowLayoutSlimePanel.Visible = true;
            }
        }

        public void SlimeRemove(Socket socket)
        {
            foreach (Slime item in FlowLayoutSlimePanel.Controls)
            {
                if (socket == item.Tag)
                {
                    FlowLayoutSlimePanel.Controls.Remove(item);
                    slimes.Remove(item);
                    if (item != null)
                        item.Dispose();
                }
            }
        }
        public void RefSlimes(Socket[] clients)
        {
            SlimeRemoveAll();
            foreach (Socket item in clients)
            {
                SlimeAdd(item);
            }
        }
        public void SlimeRemoveAll()
        {
            foreach (Slime item in FlowLayoutSlimePanel.Controls)
            {
                FlowLayoutSlimePanel.Controls.Remove(item);
                slimes.Remove(item);
                if (item != null)
                    item.Dispose();
            }
        }
    }
}
