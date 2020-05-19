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



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]





        static void Main()
        {

            db = FirestoreDb.Create("climatehistory-3ff7e");


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
