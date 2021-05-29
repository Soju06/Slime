using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SlimeClient
{
    public class Client
    {
        private static Socket ClientSocket = null;
        private static int BufferSize = 8192;

        public delegate void ServerMessageReceive(MessageReceive info);
        public static event ServerMessageReceive Receive;
        public delegate void ServerMessageReceiveFailed(string code);
        public static event ServerMessageReceiveFailed ReceiveFailed;
        public delegate void ServerDisconnected(Exception ex);
        public static event ServerDisconnected Disconnected;
        public static Exception JoinServer(StartInfo info)
        {
            if (ClientSocket != null)
                ClientSocket.Dispose();
            try
            {
                BufferSize = info.BufferSize;
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                ClientSocket.Connect(info.ServerAddress);
                AsyncObject ao = new AsyncObject(BufferSize);
                ao.WorkingSocket = ClientSocket;
                ClientSocket.BeginReceive(ao.Buffer, 0, ao.BufferSize, 0, DataReceived, ao);
                return null;
            }
            catch (Exception ex) { return ex; }
        }
        public static void LeaveServer()
        {
            try
            {
                ClientSocket.Close();
                ClientSocket.Dispose();
            }
            catch { }
        }
        public static bool IsAalive
        {
            get
            {
                try { if (ClientSocket != null) return ClientSocket.IsBound; else return false; } catch { return false; }
            }
        }
        private static void DataReceived(IAsyncResult ar)
        {
            AsyncObject obj = (AsyncObject)ar.AsyncState;
            int received = 0;
            try { received = obj.WorkingSocket.EndReceive(ar); }
            catch (Exception ex) { Disconnected(ex); }
            if (received <= 0)
            {
                try { obj.WorkingSocket.Close(); } catch { }
                Disconnected(new Exception("Error " + received));
                return;
            }
            ReadBuffer(obj.Buffer, obj.WorkingSocket);
            obj.ClearBuffer();
            obj.WorkingSocket.BeginReceive(obj.Buffer, 0, BufferSize, 0, DataReceived, obj);
        }
        private static void ReadBuffer(byte[] Buffer, Socket socket)
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
                    ReceiveFailed(Encoding.UTF8.GetString(Buffer));
                    return;
                }
                Receive(new MessageReceive(value, code, socket, Value));
            }
            catch
            {
                ReceiveFailed(Encoding.UTF8.GetString(Buffer));
            }
        }
        public static void Send(MessageCode code, string value)
        {
            try
            {
                byte[] bt = Encoding.UTF8.GetBytes(MessageCase.Default(code, value).ToString());
                ClientSocket.Send(bt);
            }
            catch { }
        }
        public static void Send(JObject jObject)
        {
            try
            {
                byte[] bt = Encoding.UTF8.GetBytes(jObject.ToString());
                ClientSocket.Send(bt);
            }
            catch { }
        }
    }
}
