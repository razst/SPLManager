using Nest;
using Newtonsoft.Json.Linq;
using SPL_Manager.Models.SatPacketModels.JsonModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace SPL_Manager
{
    public class ProgramProps
    {
        public static dynamic settings;
        public static List<string> groups;

        private static ElasticClient _db;
        public static ElasticClient Database 
        {
            get
            {
                if(!DataBaseEnabled) return null;
                if (_db != null) return _db;

                var settings = new ConnectionSettings(new Uri("https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com"))
                .DefaultIndex("testing_index");

                _db = new ElasticClient(settings);
                return _db;

            }
        }

        public static Dictionary<string, PacketTypeList> PacketJsonFiles;

        public static bool IsOnline 
        {
            get 
            {
                try
                {
                    return new Ping().Send("www.google.com.mx").Status == IPStatus.Success;
                }
                catch
                {
                }
                return false;
            } 
        }

        private static bool _dbOn;
        public static bool DataBaseEnabled
        {
            get
            {
                return _dbOn && IsOnline;
            }
            set
            {
                _dbOn = value;
            }
        }

        public static void Init()
        {
            groups = new List<string>();
            PacketJsonFiles = new Dictionary<string, PacketTypeList>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path += "\\CustomResources\\";
            settings = JObject.Parse(File.ReadAllText($"{path}settings.json"));

            foreach (var group in settings.satInfo)
            {
                groups.Add($"{group.name} ({group.desc})");
            }


            _dbOn = (bool)settings.dataBaseEnabled;
            if (DataBaseEnabled)
            {
                var settings = new ConnectionSettings(new Uri("https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com"))
                                    .DefaultIndex("testing_index");

                _db = new ElasticClient(settings);
            }
            
            PacketJsonFiles.Add("Tx", new PacketTypeList($"{path}tx.json"));
            PacketJsonFiles.Add("Rx", new PacketTypeList($"{path}rx.json"));
            PacketJsonFiles.Add("RxTau", new PacketTypeList($"{path}TauRx.json"));
        }

        public static void Dispose()
        {
            settings = null;
            groups.Clear();
            _db = null;
            PacketJsonFiles.Clear();
            DataBaseEnabled = false;
        }
    }
}
