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

namespace packet_maker
{


    static class Program
    {


        static public ElasticClient db;
        static public dynamic settings = null;




        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {
           using(StreamReader Tsettings = new StreamReader(@"settings.json"))
           {
                settings = Json.Decode(Tsettings.ReadToEnd());
           }

            if (settings.dataBaseEnabled)
            {
                var settings = new ConnectionSettings(new Uri("https://tevel:tevelData1!@search-satpackets-wmzdkqufk2m6tr6zyz4n6zfuhu.us-east-2.es.amazonaws.com"))
                                    .DefaultIndex("testing_index");

                db = new ElasticClient(settings);
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
