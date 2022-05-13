using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.PacketLifecycle.History
{
    public interface IPacketHistoryView : ICustomView
    {
        public void AddTxItem(string DisplayText);
        public void AddRxItem(string DisplayText);

        public void ClearPacketDisplay();

        public int RxGroupIndex { get; }

        public string RxPacketHex { get; set; }
        public string PacketDetalis { set; }


        public int TxItemsIndex { get; set; }
        public int RxItemsIndex { get; set; }

        string AskUserForFileName(string title, string defaultFileName);

        public PacketHistoryPresenter RxTabPresenter { get; }
    }
}