using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SlimeServer
{
    class JsonSetting
    {
        public static string SettingPath = string.Format(@"{0}\{1}.json", Application.StartupPath, Process.GetCurrentProcess().ProcessName);
        public static void Reset()
        {
            if (!File.Exists(SettingPath))
                File.WriteAllText(SettingPath, SettingObject.ToString());
        }
        public static JObject Object
        {
            get
            {
                Reset();
                return JObject.Parse(File.ReadAllText(SettingPath));
            }
            set
            {
                File.WriteAllText(SettingPath, value.ToString());
            }
        }
        public class Get
        {
            public static StartInfo Info
            {
                get
                {
                    return new StartInfo((int)Object["ServerSocket"]["BufferSize"],
                        new IPEndPoint(IPAddress.Parse(Object["ServerSocket"]["IP"].ToString()), (int)Object["ServerSocket"]["Port"]));
                }
            }
            public static DirectoryInfo FTPLocation
            {
                get
                {
                    return new DirectoryInfo(Object["FTP_Clicet"]["Location"].ToString());
                }
            }
            public static IPEndPoint FTPHost
            {
                get
                {
                    return IPEndPointParse(Object["FTP_Clicet"]["Host"].ToString());
                }
            }
        }
        public static JObject SettingObject = new JObject
        (
            new JProperty
            (
                "ServerSocket",
                new JObject
                (
                    new JProperty("IP", 0),
                    new JProperty("Port", 65422),
                    new JProperty("BufferSize", 8192)
                )
            ),
            new JProperty
            (
                "FTP_Clicet",
                new JObject
                (
                    new JProperty("Host", "127.0.0.1:65421"),
                    new JProperty("Location", "D:\\AFFS")
                )
            )
        );

        private static IPEndPoint IPEndPointParse(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            IPAddress ip = IPAddress.Loopback;
            int port = 21;
            if (ep.Length > 1)
                int.TryParse(ep[1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port);
            IPAddress.TryParse(ep[0], out ip);
            return new IPEndPoint(ip, port);
        }
    }
}
