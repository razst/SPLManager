using SPL_Manager.Library.PacketLifecycle.Query.Advanced;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.PacketLifecycle.Query.Simple
{
    public interface IQueryResultsView : ICustomView
    {
        public void ClearPacketsOnDisplay();

        public void AddRxPacket(string displayPacket);
        public void AddTxPacket(string displayPacket);

        public int SelectedRxIndex { get; set; }
        public int SelectedTxIndex { get; set; }
        public string PacketDetails { set; }

        public AdvancedQueryResultsPresenter QueryPacketDisplayPresenter { get; }
        public AdvancedQueryPresenter QuerySelctionPresenter { get; }
    }
}