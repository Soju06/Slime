using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlimeClient
{
    class JsonSetting
    {
        public static JObject Object
        {
            get
            {
                return JObject.Parse(Encoding.UTF8.GetString(Properties.Resources.Setting));
            }
        }
        public class Get
        {
            public static bool DNSMode
            {
                get
                {
                    return (bool)Object["ServerSocket"]["DNSHost_Mode"];
                }
            }
            public static string Address
            {
                get
                {
                    return Object["ServerSocket"]["Address"].ToString();
                }
            }
            public static int Port
            {
                get
                {
                    return (int)Object["ServerSocket"]["Port"];
                }
            }
            public static int BufferSize
            {
                get
                {
                    return (int)Object["ServerSocket"]["BufferSize"];
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
                    new JProperty("DNSHost_Mode", false),
                    new JProperty("Address", "127.0.0.1"),
                    new JProperty("Port", 65422),
                    new JProperty("BufferSize", 8192)
                )
            )
        );
    }
}
