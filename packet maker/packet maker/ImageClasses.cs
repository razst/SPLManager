using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace packet_maker
{
    [FirestoreData]
    public class imageInfo
    {
        [FirestoreDocumentId]
        public string splDocId { get; set; }

        [FirestoreProperty]
        public int imageId { get; set; }

        [FirestoreProperty]
        public string imageType { get; set; }

        [FirestoreProperty]
        public int TotalChunks { get; set; }

        [FirestoreProperty]
        public int recivedChuncks { get; set; }

        [FirestoreProperty]
        public DateTime when { get; set; }

    }



    [FirestoreData]
    public class imageData
    {
        [FirestoreProperty]
        public string chunkData { get; set; }

        [FirestoreProperty]
        public int chunkId { get; set; }

        [FirestoreProperty]
        public int splId { get; set; }
    
        [FirestoreProperty]
        public DateTime when { get; set; }
    }

    public class imagePropeties
    {
        public imageInfo Inf { get; set; }

        public List<imageData> chunks { get; set; }
    }
}
