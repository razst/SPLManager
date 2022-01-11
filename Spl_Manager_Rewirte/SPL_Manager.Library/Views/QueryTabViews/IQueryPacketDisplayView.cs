using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Views.QueryTabViews
{
    public interface IQueryPacketDisplayView
    {
        public void ClearPacketsOnDisplay();

        public void AddRxPacket(string displayPacket);
        public void AddTxPacket(string displayPacket);

        public int SelectedRxIndex { get; set; }
        public int SelectedTxIndex { get; set; }
        public string PacketDetails { set; }

        public Presenters.QueryTabPresenters.QueryPacketDisplayPresenter QueryPacketDisplayPresenter { get; }
        public Presenters.QueryTabPresenters.QuerySelctionPresenter QuerySelctionPresenter { get; }
    }
}