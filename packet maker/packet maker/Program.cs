#define DB
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
#if DB 
using Google.Cloud.Firestore; 
#endif


namespace packet_maker
{


    static class Program
    {
        static public bool testMode = false;

#if DB
        static public FirestoreDb db;
#endif



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {
#if DB
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\key\satelite packets-a1af32f2133c.json");
            db = FirestoreDb.Create("satelite-packets");
#endif







            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
