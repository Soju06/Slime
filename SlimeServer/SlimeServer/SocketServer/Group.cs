using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SlimeServer
{
    public class StartInfo
    {
        public int BufferSize = 8192;
        public IPEndPoint ServerAddress = new IPEndPoint(0, 65422);
        public StartInfo() { }
        public StartInfo(int Buffer, IPEndPoint Address)
        {
            BufferSize = Buffer;
            ServerAddress = Address;
        }
    }
    public class ClientInfo
    {
        public Socket ClientSocket = null;
        public ClientInfo() { }
        public ClientInfo(Socket clientSocket)
        {
            ClientSocket = clientSocket;
        }
    }
    public class MessageReceive
    {
        public string Message;
        public MessageCode Code;
        public Socket Address;
        public JObject Object;
        public MessageReceive() { }
        public MessageReceive(string message, MessageCode code, Socket address, JObject jobject)
        {
            Message = message;
            Code = code;
            Address = address;
            Object = jobject;
        }
    }
    public class ReceiveFailedInfo
    {
        public string Code;
        public Socket ClientSocket;
        public ReceiveFailedInfo() { }
        public ReceiveFailedInfo(string code, Socket clientSocket)
        {
            Code = code;
            ClientSocket = clientSocket;
        }
    }
    public class ClientDisconnectedInfo
    {
        public DisconnectedCode DisconnectedCode = DisconnectedCode.Unknown;
        public IPEndPoint ClientAddress = null;
        public Socket ClientSocket = null;
        public ClientDisconnectedInfo() { }
        public ClientDisconnectedInfo(IPEndPoint Address)
        {
            ClientAddress = Address;
        }
        public ClientDisconnectedInfo(IPEndPoint Address, DisconnectedCode Code, Socket clientSocket)
        {
            ClientAddress = Address;
            DisconnectedCode = Code;
            ClientSocket = clientSocket;
        }
    }
    public enum DisconnectedCode
    {
        Exception = 2,
        None = 1,
        Unknown = 3
    }
    public class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;
        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }
        public void ClearBuffer()
        {
            Array.Clear(Buffer, 0, BufferSize);
        }
    }
    public class MessageCase
    {
        public static JObject Default(MessageCode Code, string Value)
        {
            return new JObject
            (
                new JProperty
                (
                    "Type",
                    (int)Code
                ),
                new JProperty
                (
                    "Value",
                    Value
                )
            );
        }
        public static JObject FileInfo(string file)
        {
            FileInfo info = null;
            string ErrorCode = "None";
            try
            {
                info = new FileInfo(file);
            }
            catch (Exception ex)
            {
                ErrorCode = ex.ToString();
            }
            JObject jObject = new JObject
            (
                new JProperty
                (
                    "Type",
                    (int)MessageCode.FileInfoReturn
                ),
                new JProperty
                (
                    "Value",
                    file
                )
            );
            jObject.Add("ErrorCode", ErrorCode);
            if (info == null)
                return jObject;
            try
            {
                jObject.Add("Name", info.Name);
                jObject.Add("Exists", info.Exists);
                jObject.Add("Size", info.Length);
                jObject.Add("CreationTime", info.CreationTime.ToString("yyyy-MM-dd H:mm:ss"));
                jObject.Add("LastWriteTime", info.LastWriteTime.ToString("yyyy-MM-dd H:mm:ss"));
                jObject.Add("LastAccessTime", info.LastAccessTime.ToString("yyyy-MM-dd H:mm:ss"));
                jObject.Add("IsReadOnly", info.IsReadOnly);
                jObject.Add("DirectoryName", info.DirectoryName);
                return jObject;
            }
            catch (Exception ex)
            {
                jObject["ErrorCode"] = ex.ToString();
                return jObject;
            }
        }
    }
    public enum MessageCode
    {
        Command = 0xC1,
        Message = 0xC2,
        Text = 0xC3,
        Data = 0xD1,
        DataReset = 0xD2,
        DataSendEnd = 0xD3,
        DataSendStart = 0xD4,
        DataSendFailed = 0xD5,
        DataSendSize = 0xD5,
        DataOK = 0xD6,
        FTPDownload = 0xF1,
        FTPUpload = 0xF2,
        FTPDownloadFailed = 0xF3,
        FTPUploadFailed = 0xF4,
        FTPUploadOK = 0xF5,
        FTPDownloadOK = 0xF6,
        HTTPDownload = 0x91,
        HTTPDownloadFailed = 0x92,
        HTTPDownloadOK = 0x93,
        ProcessKill = 0x81,
        ProcessStart = 0x82,
        ProcessKillFailed = 0x83,
        ProcessKillOK = 0x84,
        ProcessStartFailed = 0x85,
        ProcessStartOK = 0x86,
        FileDelete = 0x71,
        FileMove = 0x72,
        FileCopy = 0x73,
        FileDeleteFailed = 0x74,
        FileDeleteOK = 0x75,
        FileMoveFailed = 0x76,
        FileMoveOK = 0x77,
        FileCopyFailed = 0x78,
        FileCopyOK = 0x79,
        DirectoryCreate = 0x60,
        DirectoryMove = 0x61,
        DirectoryCopy = 0x62,
        DirectoryDelete = 0x63,
        DirectoryDeleteFailed = 0x64,
        DirectoryDeleteOK = 0x65,
        DirectoryMoveFailed = 0x66,
        DirectoryMoveOK = 0x67,
        DirectoryCopyFailed = 0x68,
        DirectoryCopyOK = 0x69,
        DirectoryCreateFailed = 0x50,
        DirectoryCreateOK = 0x51,
        ScreenShot = 0x56,
        ScreenShotOK = 0x57,
        ScreenShotFailed = 0x58,
        GetNode = 0x59,
        GetNodeOK = 0x40,
        GetNodeFailed = 0x41,
        GetProcess = 0x42,
        GetProcessOK = 0x43,
        GetProcessFailed = 0x44,
        Cmd = 0x45,
        CmdOK = 0x46,
        CmdFailed = 0x47,
        FileInfo = 0x48,
        FileInfoReturn = 0x49,
        GetFiles = 0x30,
        GetFilesOK = 0x31,
        GetFilesFailed = 0x32,
        LogShowMessageBox = 0x33
    }
}