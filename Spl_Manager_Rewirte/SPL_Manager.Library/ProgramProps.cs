using Nest;
using Newtonsoft.Json.Linq;
using SPL_Manager.Library.Models.SatPacketModels.JsonModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace SPL_Manager
{
    public class ProgramProps
    {
        public static dynamic settings = "nill";
        public static List<string> groups = new List<string>();

        public static ElasticClient? Database { get; private set; }

        public static Dictionary<string, PacketTypeList> PacketJsonFiles = new Dictionary<string, PacketTypeList>();


        public static bool GetIfOnline()
        {
            try
            {
                var url = "http://www.gstatic.com/generate_204";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = 5000;
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
            PacketJsonFiles = new Dictionary<string, PacketTypeList>();

            //load settings in json files
            string path = Directory.GetParent(Directory.GetCurrentDirectory() ?? "")?.Parent?.Parent?.Parent?.FullName ?? "nill";
            if (path == "nill") throw new Exception("no settings found");//TODO: softer way to handle this
            path += "\\SPL_Manager.Library\\CustomResources\\";
            settings = JObject.Parse(File.ReadAllText($"{path}settings.json"));

            // get all group names
            foreach (var group in settings.satInfo)
            {
                groups.Add($"{group.name} ({group.desc})");
            }


            _dbOn = (bool)settings.dataBaseEnabled;

            var dbSettings = new ConnectionSettings(new Uri("https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com"))
                                .DefaultIndex("testing_index");

            Database = new ElasticClient(dbSettings);



            // load packet protocols to types
            PacketJsonFiles.Add("Tx", new PacketTypeList($"{path}tx.json"));
            PacketJsonFiles.Add("Rx", new PacketTypeList($"{path}rx.json"));
            PacketJsonFiles.Add("RxTau", new PacketTypeList($"{path}TauRx.json"));
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
