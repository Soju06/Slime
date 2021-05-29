using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SlimeServer
{
    public class Server
    {
        public static Socket ServerSocket = null;
        private static int BufferSize = 8192;
        public static List<Socket> Clients = new List<Socket>();

        public delegate void ClientConnected(ClientInfo info);
        public static event ClientConnected Connected;
        public delegate void ClientDisconnected(ClientDisconnectedInfo info);
        public static event ClientDisconnected Disconnected;
        public delegate void ClientMessageReceive(MessageReceive info);
        public static event ClientMessageReceive Receive;
        public delegate void ClientMessageReceiveFailed(ReceiveFailedInfo info);
        public static event ClientMessageReceiveFailed ReceiveFailed;

        public static void Start(StartInfo startInfo)
        {
            BufferSize = startInfo.BufferSize;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            ServerSocket.Bind(startInfo.ServerAddress);
            ServerSocket.Listen(10);
            ServerSocket.BeginAccept(AcceptCallback, null);
        }

        public static void Stop()
        {
            try
            {
                if (ServerSocket != null)
                    ServerSocket.Close();
                for (int i = 0; i < Clients.Count; i++)
                {
                    try { Clients[i].Close(); } catch { }
                }
                ServerSocket.Dispose();
            }
            catch { }
        }

        public static bool IsAalive
        {
            get
            {
                try { if (ServerSocket != null) return ServerSocket.IsBound; else return false; } catch { return false; }
            }
        }

        #region AcceptCallback
        private static void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = ServerSocket.EndAccept(ar);
                ServerSocket.BeginAccept(AcceptCallback, null);
                AsyncObject obj = new AsyncObject(BufferSize);
                obj.WorkingSocket = client;
                Clients.Add(client);
                Connected(new ClientInfo(client));
                client.BeginReceive(obj.Buffer, 0, BufferSize, 0, DataReceived, obj);
            }
            catch { }
        }
        #endregion

        #region DataReceived
        private static void DataReceived(IAsyncResult ar)
        {
            try
            {
                AsyncObject obj = (AsyncObject)ar.AsyncState;
                int received = 0;
                try
                {
                    received = obj.WorkingSocket.EndReceive(ar);
                }
                catch
                {
                    Disconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Exception, obj.WorkingSocket));
                    foreach (Socket item in Clients)
                    {
                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
                            Clients.Remove(item);
                    }
                    return;
                }
                if (received <= 0)
                {
                    Disconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Unknown, obj.WorkingSocket));
                    foreach (Socket item in Clients)
                    {
                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
                            Clients.Remove(item);
                    }
                    obj.WorkingSocket.Close();
                    return;
                }
                ReadBuffer(obj.Buffer, obj.WorkingSocket);
                obj.ClearBuffer();
                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, BufferSize, 0, DataReceived, obj);
            }
            catch { }
        }
        #endregion

        private static void ReadBuffer(byte[] Buffer, Socket Address)
        {
            try
            {
                JObject Value = JObject.Parse(Encoding.UTF8.GetString(Buffer));
                MessageCode code;
                string value;
                try
                {
                    code = (MessageCode)int.Parse(Value["Type"].ToString());
                    value = Value["Value"].ToString();
                }
                catch
                {
                    ReceiveFailed(new ReceiveFailedInfo(Encoding.UTF8.GetString(Buffer), Address));
                    return;
                }
                Receive(new MessageReceive(value, code, Address, Value));
            }
            catch
            {
                ReceiveFailed(new ReceiveFailedInfo(Encoding.UTF8.GetString(Buffer), Address));
            }
        }
        public static void Send(MessageCode code, string value, Socket socket)
        {
            try
            {
                if (socket != null)
                {
                    byte[] bt = Encoding.UTF8.GetBytes(MessageCase.Default(code, value).ToString());
                    socket.Send(bt);
                }
                else
                    MessageBox.Show("소켓이 설정되있지 않음.");
            }
            catch { MessageBox.Show("전송 실패"); }
        }
    }
}
