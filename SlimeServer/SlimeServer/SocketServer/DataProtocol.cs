//using System;
//using System.CodeDom.Compiler;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.IO;
//using System.IO.Pipes;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace SlimeServer
//{
//    class DataProtocol
//    {
//        private static int BufferSize = 8192;
//        public static Socket DataChannel = null;
//        public static Socket CommandChannel = null;
//        public static List<Socket> DataChannelClients = new List<Socket>();
//        public static List<Socket> CommandChannelClients = new List<Socket>();

//        public delegate void CommandChannelClientConnected(ClientInfo info);
//        public static event CommandChannelClientConnected CommandChannelConnected;
//        public delegate void DataChannelClientConnected(ClientInfo info);
//        public static event DataChannelClientConnected DataChannelConnected;

//        public delegate void CommandChannelClientDisconnected(ClientDisconnectedInfo info);
//        public static event CommandChannelClientDisconnected CommandChannelDisconnected;
//        public delegate void DataChannelClientDisconnected(ClientDisconnectedInfo info);
//        public static event DataChannelClientDisconnected DataChannelDisconnected;


//        public delegate void CommandChannelClientMessageReceive(DataMessageReceive info);
//        public static event CommandChannelClientMessageReceive CommandChannelReceive;

//        public static Exception Start(StartDataInfo info)
//        {
//            try
//            {
//                BufferSize = info.BufferSize;
//                DataChannel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                CommandChannel = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
//                DataChannel.Bind(info.DataServerAddress);
//                CommandChannel.Bind(info.CommandServerAddress);
//                DataChannel.BeginAccept(DataChannelCallback, null);
//                CommandChannel.BeginAccept(CommandChannelCallback, null);
//                return null;
//            }
//            catch (Exception ex)
//            {
//                return ex;
//            }
//        }

//        //This Command Channel
//        #region CommandChannelCallback
//        private static void CommandChannelCallback(IAsyncResult ar)
//        {
//            try
//            {
//                Socket client = CommandChannel.EndAccept(ar);
//                CommandChannel.BeginAccept(CommandChannelCallback, null);
//                AsyncObject obj = new AsyncObject(BufferSize);
//                obj.WorkingSocket = client;
//                CommandChannelClients.Add(client);
//                CommandChannelConnected(new ClientInfo(client));
//                client.BeginReceive(obj.Buffer, 0, BufferSize, 0, CommandChannelReceived, obj);
//            }
//            catch { }
//        }
//        #endregion

//        #region CommandChannelReceived
//        private static void CommandChannelReceived(IAsyncResult ar)
//        {
//            try
//            {
//                AsyncObject obj = (AsyncObject)ar.AsyncState;
//                int received = 0;
//                try
//                {
//                    received = obj.WorkingSocket.EndReceive(ar);
//                }
//                catch
//                {
//                    CommandChannelDisconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Exception, obj.WorkingSocket));
//                    foreach (Socket item in CommandChannelClients)
//                    {
//                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
//                            CommandChannelClients.Remove(item);
//                    }
//                    return;
//                }
//                if (received <= 0)
//                {
//                    CommandChannelDisconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Unknown, obj.WorkingSocket));
//                    foreach (Socket item in CommandChannelClients)
//                    {
//                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
//                            CommandChannelClients.Remove(item);
//                    }
//                    obj.WorkingSocket.Close();
//                    return;
//                }
//                CommandChannelReadBuffer(obj.Buffer, obj.WorkingSocket);
//                obj.ClearBuffer();
//                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, BufferSize, 0, CommandChannelReceived, obj);
//            }
//            catch { }
//        }
//        #endregion

//        private static void CommandChannelReadBuffer(byte[] buffer, Socket workingSocket)
//        {
//            string message = Encoding.UTF8.GetString(buffer);
//            string[] tmp = message.Split('\x01');
//            if (tmp.Length > 1)
//            {
//                switch (tmp[0])
//                {
//                    case "UPLD":

//                        break;
//                    default:
//                        break;
//                }
//            }
//            CommandChannelReceive(new DataMessageReceive(message, workingSocket));
//        }

//        //This Data Channel
//        #region DataChannelReceived
//        private static void DataChannelReceived(IAsyncResult ar)
//        {
//            try
//            {
//                Socket client = DataChannel.EndAccept(ar);
//                DataChannel.BeginAccept(DataChannelReceived, null);
//                AsyncObject obj = new AsyncObject(BufferSize);
//                obj.WorkingSocket = client;
//                DataChannelClients.Add(client);
//                DataChannelConnected(new ClientInfo(client));
//                client.BeginReceive(obj.Buffer, 0, BufferSize, 0, DataChannelReceived, obj);
//            }
//            catch { }
//        }
//        #endregion

//        #region DataChannelCallback
//        private static void DataChannelCallback(IAsyncResult ar)
//        {
//            try
//            {
//                AsyncObject obj = (AsyncObject)ar.AsyncState;
//                int received = 0;
//                try
//                {
//                    received = obj.WorkingSocket.EndReceive(ar);
//                }
//                catch
//                {
//                    DataChannelDisconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Exception, obj.WorkingSocket));
//                    foreach (Socket item in DataChannelClients)
//                    {
//                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
//                            DataChannelClients.Remove(item);
//                    }
//                    return;
//                }
//                if (received <= 0)
//                {
//                    DataChannelDisconnected(new ClientDisconnectedInfo((IPEndPoint)obj.WorkingSocket.RemoteEndPoint, DisconnectedCode.Unknown, obj.WorkingSocket));
//                    foreach (Socket item in DataChannelClients)
//                    {
//                        if (item.RemoteEndPoint == (IPEndPoint)obj.WorkingSocket.RemoteEndPoint)
//                            DataChannelClients.Remove(item);
//                    }
//                    obj.WorkingSocket.Close();
//                    return;
//                }
//                DataChannelReadBuffer(obj.Buffer, obj.WorkingSocket);
//                obj.ClearBuffer();
//                obj.WorkingSocket.BeginReceive(obj.Buffer, 0, BufferSize, 0, DataChannelCallback, obj);
//            }
//            catch { }
//        }
//        #endregion

//        private static void DataChannelReadBuffer(byte[] buffer, Socket workingSocket)
//        {

//        }
//    }
//}
