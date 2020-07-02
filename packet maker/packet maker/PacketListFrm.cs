using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;

namespace packet_maker
{
    public partial class PacketListFrm : Form
    {
        public PacketListFrm()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();
        private void PacketListFrm_Load(object sender, EventArgs e)
        {
#if DB
            dt.Columns.Add("Packet",typeof(string));
            dt.Columns.Add("Description:", typeof(string));

            storedPacketsDGV.Rows.Clear();
            Query capitalQuery = Program.db.Collection("stored packets");
            QuerySnapshot QuerySnapshot = await capitalQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
            {
                ListPacketItem item = documentSnapshot.ConvertTo<ListPacketItem>();

                dt.Rows.Add(item.packetString,item.Description);
            }

            storedPacketsDGV.DataSource = dt;
#endif
        }

        private  void addBtn_Click(object sender, EventArgs e)
        {
#if DB
            DocumentReference docRef = Program.db.Collection("stored packets").Document();
            ListPacketItem lpi = new ListPacketItem
            {
                packetString = pacStringTxb.Text,
                Description = descTxb.Text
            };

            await docRef.SetAsync(lpi);
#endif
        }

    }


}
