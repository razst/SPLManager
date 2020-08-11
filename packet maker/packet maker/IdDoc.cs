using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace packet_maker
{
    [FirestoreData]
    internal class IdDoc
    {
        [FirestoreProperty]
        public int CommandId { get; set; }
    }
}
