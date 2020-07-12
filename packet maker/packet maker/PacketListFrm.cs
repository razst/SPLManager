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

        readonly RadioServer RadioServer = new RadioServer();

        private List<ListPacketItem> items = new List<ListPacketItem>();
        private async void PacketListFrm_Load(object sender, EventArgs e)
        {
            storedPacketsDGV.Rows.Clear();
            if (Program.settings.dataBaseEnabled)
            {
                items = await Task.Run(()=> {
                    return downloadList();
                });

                foreach (var item in items)
                {
                    storedPacketsDGV.Rows.Add(item.Description, "send");
                }
                storedPacketsDGV.AutoResizeColumns();
            }
        }

        private async void addBtn_Click(object sender, EventArgs e)
        {
            DocumentReference docRef = Program.db.Collection(Program.settings.collectionPrefix + "stored packets").Document();
            ListPacketItem lpi = new ListPacketItem
            {
                packetString = pacStringTxb.Text,
                Description = descTxb.Text
            };

            await docRef.SetAsync(lpi);
        }

        private async Task<List<ListPacketItem>> downloadList()
        {
            List<ListPacketItem> listPackets = new List<ListPacketItem>();
            Query capitalQuery = Program.db.Collection(Program.settings.collectionPrefix + "stored packets");
            QuerySnapshot QuerySnapshot = await capitalQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in QuerySnapshot.Documents)
            {
                listPackets.Add(documentSnapshot.ConvertTo<ListPacketItem>());
            }

            return listPackets;
        }

        private async void storedPacketsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
               await RadioServer.Send(items[e.RowIndex].packetString.Trim());
            }
        }
    }


}
