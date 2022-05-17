using Nest;
using Newtonsoft.Json.Linq;
using SPL_Manager.Library.PacketModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SPL_Manager.Library.Shared
{
    public class ProgramProps
    {
        public static dynamic settings = "nill";
        public static List<string> groups = new List<string>();

        public static ElasticClient Database { get; private set; }
        private static readonly string ConnectionString = "https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com";

        public static Dictionary<string, PacketProtocol> PacketJsonFiles = new Dictionary<string, PacketProtocol>();


        public static bool GetIfOnline()
        {
            try
            {
                var url = "https://google.com/";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = 2000;
                using var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // true if db is enabled
        private static bool _dbOn;
        public static bool DataBaseEnabled
        {
            get
            {
                return _dbOn && GetIfOnline();
            }
        }

        public static void Init()
        {
            groups = new List<string>();
            PacketJsonFiles = new Dictionary<string, PacketProtocol>();

            //load settings in json files
            var asm = (typeof(ProgramProps)).Assembly;
            var path = Path.GetDirectoryName(asm.Location);
            path += "\\CustomResources";

            settings = JObject.Parse(File.ReadAllText($"{path}\\settings.json"));

            // get all group names
            foreach (var group in settings.satInfo)
            {
                groups.Add($"{group.name} ({group.desc})");
            }


            _dbOn = (bool)settings.dataBaseEnabled;

            var dbSettings = new ConnectionSettings(new Uri(ConnectionString))
                                .DefaultIndex("testing_index");

            Database = new ElasticClient(dbSettings);



            // load packet protocols to types
            PacketJsonFiles.Add("Tx", new PacketProtocol($"{path}\\tx.json"));
            PacketJsonFiles.Add("Rx", new PacketProtocol($"{path}\\rx.json"));
            PacketJsonFiles.Add("RxTau", new PacketProtocol($"{path}\\TauRx.json"));
        }

        public static void Dispose()
        {
            settings = "";
            groups.Clear();
            PacketJsonFiles.Clear();
            _dbOn = false;
        }
    }
}
