using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace packet_maker
{
    [FirestoreData]
    internal class imageInfo
    {
        [FirestoreDocumentId]
        public string splDocId { get; set; }

        [FirestoreProperty]
        public int TotalChunks { get; set; }

        [FirestoreProperty]
        public int imageId { get; set; }

        [FirestoreProperty]
        public DateTime when { get; set; }

    }
}
