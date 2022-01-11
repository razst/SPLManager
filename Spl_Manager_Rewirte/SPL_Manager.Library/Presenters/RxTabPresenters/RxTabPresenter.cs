using SPL_Manager.Library.Models.SatPacketModels;
using SPL_Manager.Library.Views.RxTabViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Presenters.RxTabPresenters
{
    internal struct RxGroupData
    {
        public List<PacketObject> SentPackets;
        public List<DateTime> SentPacketsDates;
        public List<PacketObject> RecivedPackets;
        public List<DateTime> RecivedPacketsDates;
    }
    public class RxTabPresenter
    {
        private IPacketDisplayView _view;
        private readonly List<RxGroupData> SatGroupsData;

        public RxTabPresenter()
        {
            SatGroupsData = new List<RxGroupData>();
            ProgramProps.groups.ForEach(group =>
            {

                SatGroupsData.Add(new RxGroupData()
                {
                    SentPackets = new List<PacketObject>(),
                    RecivedPackets = new List<PacketObject>(),
                    SentPacketsDates = new List<DateTime>(),
                    RecivedPacketsDates = new List<DateTime>()
                });

            });
        }

        public void SetView(IPacketDisplayView view)
        {
            _view = view;
        }

        public List<PacketObject> GetAllRxPackets() => SatGroupsData[0].RecivedPackets;
        public List<DateTime> GetAllRxDates() => SatGroupsData[0].RecivedPacketsDates;
        private RxGroupData GetCurrentGroupData() => SatGroupsData[_view.RxGroupIndex];
        public PacketObject GetCurrentTxItem() => GetCurrentGroupData().SentPackets[_view.TxItemsIndex];
        public PacketObject GetCurrentRxItem() => GetCurrentGroupData().RecivedPackets[_view.RxItemsIndex];


        public void AddSentPacket(PacketObject packet, DateTime time)
        {
            SatGroupsData[0].SentPackets.Add(packet);
            SatGroupsData[0].SentPacketsDates.Add(time);

            if (packet.GetSatDex() != 0)
            {
                SatGroupsData[packet.GetSatDex()].SentPackets.Add(packet);
                SatGroupsData[packet.GetSatDex()].SentPacketsDates.Add(time);
            }

            if (packet.GetSatDex() != _view.RxGroupIndex)
                if (_view.RxGroupIndex != 0) return;

            string poFormated = packet.ToHeaderString(time);
            _view.Invoke((Action<string>)_view.AddTxItem, poFormated);
        }

        public void AddRecivedPacket(PacketObject packet, DateTime time)
        {

            SatGroupsData[0].RecivedPackets.Add(packet);
            SatGroupsData[0].RecivedPacketsDates.Add(time);

            if (packet.GetSatDex() != 0)
            {
                SatGroupsData[packet.GetSatDex()].RecivedPackets.Add(packet);
                SatGroupsData[packet.GetSatDex()].RecivedPacketsDates.Add(time);
            }

            if (packet.GetSatDex() != _view.RxGroupIndex)
                if (_view.RxGroupIndex != 0) return;

            string poFormated = packet.ToHeaderString(time);
            _view.Invoke((Action<string>)_view.AddRxItem, poFormated);
        }

        public void AddTranslatedPacket()
        {
            PacketObject po = new PacketObject(_view.RxPacketHex);
            if (po.JsonObject == ProgramProps.PacketJsonFiles["Tx"]) return;
            AddRecivedPacket(po, DateTime.Now);
        }


        public void UpdateCurrentRxGroup()
        {
            _view.ClearPacketDisplay();
            int i = -1;

            GetCurrentGroupData().SentPackets.ForEach(packet =>
            {
                i++;
                string poFormated = packet.ToHeaderString(GetCurrentGroupData().SentPacketsDates[i]);
                _view.AddTxItem(poFormated);
            });

            i = -1;
            GetCurrentGroupData().RecivedPackets.ForEach(packet =>
            {
                i++;
                string poFormated = packet.ToHeaderString(GetCurrentGroupData().RecivedPacketsDates[i]);
                _view.AddRxItem(poFormated);
            });
        }

        public void UpdateDisplayedPacket(string LibxType)
        {
            _view.PacketDetalis = "";
            _view.RxPacketHex = "";

            PacketObject po;
            switch (LibxType)
            {
                case "Sent":
                    if (_view.TxItemsIndex == -1) return;
                    po = GetCurrentGroupData().SentPackets[_view.TxItemsIndex];
                    break;

                case "Recived":
                    if (_view.RxItemsIndex == -1) return;
                    po = GetCurrentGroupData().RecivedPackets[_view.RxItemsIndex];
                    break;

                default:
                    return;
            }

            _view.PacketDetalis = po.GetDescriptionString();
            _view.RxPacketHex = po.RawPacket;
        }


        public void FindMatchingRx()
        {
            if (_view.TxItemsIndex == -1) return;
            if (GetCurrentGroupData().RecivedPackets.Count == 0) return;

            int TragetRxIndex = GetCurrentGroupData().RecivedPackets
                .FindIndex(pac => pac.Id == GetCurrentTxItem().Id);

            _view.RxItemsIndex = TragetRxIndex;
        }
        public void FindMatchingTx()
        {
            if (_view.RxItemsIndex == -1) return;
            if (GetCurrentGroupData().SentPackets.Count == 0) return;

            int TragetRxIndex = GetCurrentGroupData().SentPackets
                .FindIndex(pac => pac.Id == GetCurrentRxItem().Id);

            _view.TxItemsIndex = TragetRxIndex;
        }



        public void ClearRxTab()
        {
            _view.ClearPacketDisplay();
            SatGroupsData.ForEach(satGroupData =>
            {
                satGroupData.SentPackets.Clear();
                satGroupData.RecivedPackets.Clear();
                satGroupData.SentPacketsDates.Clear();
                satGroupData.RecivedPacketsDates.Clear();
            });
        }

    }
}