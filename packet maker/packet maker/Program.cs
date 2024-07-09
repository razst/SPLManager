using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Google.Cloud.Firestore;
using Nest;
using System.Web.Helpers;
using System.Net.NetworkInformation;
using MongoDB.Driver;

namespace packet_maker
{


    static class Program
    {


        //static public ElasticClient db;
        static public IMongoDatabase mongoDB;
        
        static public dynamic settings = null;
        public static List<string> groups = new List<string>();
        public static bool Online;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {

            using (StreamReader Tsettings = new StreamReader(@"settings.json"))
            {
                settings = Json.Decode(Tsettings.ReadToEnd());
                foreach (var group in settings.satInfo)
                {
                    groups.Add($"{group.name} ({group.desc})");
                }

                try
                {
                    Online = new Ping().Send("www.google.com.mx").Status == IPStatus.Success;
                    settings.dataBaseEnabled = settings.dataBaseEnabled && Online;
                }
                catch
                {
                    Online = false;
                    settings.dataBaseEnabled = false;
                }

            }

            if (settings.dataBaseEnabled)
            {
                var settings = new ConnectionSettings(new Uri("https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com"))
                                    .DefaultIndex("testing_index");

                //db = new ElasticClient(settings);

                MongoClient clientDB = new MongoClient("mongodb+srv://raz:raz@cluster0.mzxtq.mongodb.net/");
                mongoDB = clientDB.GetDatabase("Tevel");
                Console.WriteLine("Connected to Mongo DB");


            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
