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
        static public bool testMode = false;
        static public bool UploadToDB = false;

        static public FirestoreDb db;



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\key\satelite packets-a1af32f2133c.json");
            db = FirestoreDb.Create("satelite-packets");





            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
