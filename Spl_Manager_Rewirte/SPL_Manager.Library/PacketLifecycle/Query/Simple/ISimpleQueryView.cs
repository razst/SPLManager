using SPL_Manager.Library.PacketLifecycle.History;
using SPL_Manager.Library.Shared;
using System;

namespace SPL_Manager.Library.PacketLifecycle.Query.Simple
{
    public interface ISimpleQueryView : ICustomView
    {
        public string RxQueryLimit { get; }
        public DateTime RxQueryMinDate { get; }
        public DateTime RxQueryMaxDate { get; }

        public PacketHistoryPresenter RxTabPresenter { get; }
        public SimpleQueryPresenter RxQueryPresenter { get; }

    }
}