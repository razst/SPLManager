using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Views.RxTabViews
{
    public interface IPacketDisplayView : IView
    {
        public void AddTxItem(string DisplayText);
        public void AddRxItem(string DisplayText);

        public void ClearPacketDisplay();

        public int RxGroupIndex { get; }

        public string RxPacketHex { get; set; }
        public string PacketDetalis { set; }


        public int TxItemsIndex { get; set; }
        public int RxItemsIndex { get; set; }


        public Presenters.RxTabPresenters.RxTabPresenter RxTabPresenter { get; set; }
    }
}
