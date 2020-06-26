using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

using Google.Cloud.Firestore; 



namespace packet_maker
{


    static class Program
    {


        static public FirestoreDb db;
        static public BasicSettings settings = new BasicSettings();




        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {
            StreamReader Tsettings = new StreamReader(@"settings.json");
            JsonSerializer Serializer = new JsonSerializer();
            settings = (BasicSettings)Serializer.Deserialize(Tsettings,typeof(BasicSettings));


            if (Program.settings.dataBaseEnabled)
            {
                System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", settings.dataBaseKeyPath);
                db = FirestoreDb.Create(settings.dataBaseName);
            }









            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
