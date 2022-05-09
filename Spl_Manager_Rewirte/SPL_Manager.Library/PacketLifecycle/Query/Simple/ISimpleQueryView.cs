using SPL_Manager.Library.PacketLifecycle.History;
using System;

namespace SPL_Manager.Library.PacketLifecycle.Query.Simple
{
    public interface ISimpleQueryView
    {
        public string RxQueryLimit { get; }
        public DateTime RxQueryMinDate { get; }
        public DateTime RxQueryMaxDate { get; }

        public PacketHistoryPresenter RxTabPresenter { get; }
        public SimpleQueryPresenter RxQueryPresenter { get; }

    }
}