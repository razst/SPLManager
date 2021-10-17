using System;
using System.Collections.Generic;
using System.Text;
using SPL_Manager.Models.SatPacketModels;
using SPL_Manager.Views.QueryTabViews;

namespace SPL_Manager.Presenters.QueryTabPresenters
{

    public class QueryPacketDisplayPresenter
    {
        private IQueryPacketDisplayView _view;
        private List<PacketsWithDates> RxPackets;
        private List<PacketsWithDates> TxPackets;

        public QueryPacketDisplayPresenter(IQueryPacketDisplayView view)
        {
            _view = view;
            RxPackets = new List<PacketsWithDates>();
            TxPackets = new List<PacketsWithDates>();
        }
        public async void AddAllPacketsToView()
        {
            ClearQueryTab();
            Dictionary<string, List<PacketsWithDates>> Packets = null;
            try
            {
               Packets  = await _view.QuerySelctionPresenter.PreformQuery();
            }
            catch
            {
                return;
            }
            if (Packets.ContainsKey("Rx"))
            {
                RxPackets = Packets["Rx"];
                Packets["Rx"].ForEach(packet =>
                    _view.AddRxPacket(packet.Packet.ToHeaderString(packet.Date)));
            }
                

            if (Packets.ContainsKey("Tx"))
            {
                TxPackets = Packets["Tx"];
                Packets["Tx"].ForEach(packet =>
                    _view.AddTxPacket(packet.Packet.ToHeaderString(packet.Date)));
            }

        }


        public void UpdateDisplayedPacket(string LibxType)
        {
            _view.PacketDetails = "";

            PacketObject po;
            switch (LibxType)
            {
                case "Tx":
                    if (_view.SelectedTxIndex == -1) return;
                    po = TxPackets[_view.SelectedTxIndex].Packet;
                    break;

                case "Rx":
                    if (_view.SelectedRxIndex == -1) return;
                    po = RxPackets[_view.SelectedRxIndex].Packet;
                    break;

                default:
                    return;
            }

            _view.PacketDetails = po.GetDescriptionString();
        }


        public void ClearQueryTab()
        {
            RxPackets.Clear();
            TxPackets.Clear();
            _view.ClearPacketsOnDisplay();
        }
    }
}
