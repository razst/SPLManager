using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace packet_maker
{
    [FirestoreData]
    public class PLInfo
    {
        [FirestoreDocumentId]
        public string name { get; set; }

        [FirestoreProperty]
        public List<string> commands { get; set; }

        [FirestoreProperty]
        public int sleepBetweenCommands { get; set; }
    }
}
