using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace packet_maker.MainComponents
{
    public class PacketHistoryTracker
    {
        public ComboBox GroupsComboBox { get; set; }
        public ListBox PacketsListBox { get; set; }
        public Dictionary<string, List<PacketObject>> Packets { get; set; }
        public TextBox PacketDisplayTextbox { get; set; }

        public PacketHistoryTracker(ComboBox toTrack, ListBox toInsert, TextBox toDisplay)
        {
            GroupsComboBox = toTrack;
            PacketsListBox = toInsert;
            PacketDisplayTextbox = toDisplay;
            Packets = new Dictionary<string, List<PacketObject>>();
            foreach (string g in Program.groups)
            {
                Packets.Add(g, new List<PacketObject>());
            }
        }
        public void AddPacket(PacketObject PacObj)
        {
            Packets[Program.groups[0]].Add(PacObj);
            if (PacObj.Type == -1)
            {
                GetCurrentPacketList().Add(PacObj);
                AddItemToListBox(PacObj);
                return;
            }
            Packets[PacObj.SateliteGroup].Add(PacObj);
            if (PacObj.SateliteGroup == GroupsComboBox.Text || GroupsComboBox.SelectedIndex == 0) AddItemToListBox(PacObj);
        }

        public List<PacketObject> GetCurrentPacketList() => Packets[GroupsComboBox.Text];
        public PacketObject GetCurrentPacket() => GetCurrentPacketList()[PacketsListBox.SelectedIndex];
        public int FindPacketIndexById(int Id) => GetCurrentPacketList().FindIndex(x => x.Id == Id);

        public void UpdateDisplayGroup()
        {
            ClearUI();
            foreach (var item in GetCurrentPacketList())
            {
                AddItemToListBox(item);
            }
        }


        private void ClearUI()
        {
            PacketDisplayTextbox.Clear();
            PacketsListBox.Items.Clear();
        }
        private void AddItemToListBox(PacketObject PacObj)
        {
            string display = PacObj.ToHeaderString(DateTime.Now);

            PacketsListBox.Items.Add(display);

            if (PacketsListBox.SelectedIndex == -1)
            {
                PacketsListBox.SelectedIndex = 0;
                return;
            }

            if (PacketsListBox.SelectedIndex + 1 == PacketsListBox.Items.Count - 1)
                PacketsListBox.SelectedIndex += 1;
        }
    }
}
