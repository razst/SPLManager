using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPL_Manager.Library.Shared;
using SPL_Manager.Library.PacketModel;

namespace SPL_Manager.Library.PacketLifecycle.Query.Simple
{
    public class SimpleQueryPresenter
    {
        private ISimpleQueryView _view;
        private readonly PacketProtocol RxJson;
        private readonly PacketProtocol TxJson;

        public SimpleQueryPresenter()
        {
            RxJson = ProgramProps.PacketJsonFiles["Rx"];
            TxJson = ProgramProps.PacketJsonFiles["Tx"];
        }
        public void SetView(ISimpleQueryView view)
        {
            _view = view;
        }


        public async Task PrefomQuery()
        {
            _view.RxTabPresenter.ClearRxTab();


            RepositoryWorker worker = new RepositoryWorker();

            worker.AddDateRangeFilter("time", _view.RxQueryMinDate, _view.RxQueryMaxDate);

            int limit = 0;
            if (_view.RxQueryLimit == "No Limit") limit = -1;
            else limit = int.Parse(_view.RxQueryLimit);

            worker.AddQuerySize(limit);
            worker.AddSortAscending();

            var RxDocs = await worker.StartWork("parsed-rx");
            var TxDocs = await worker.StartWork("parsed-tx");
            AddRxQueryItems(RxDocs);
            AddTxQueryItems(TxDocs);



            _view.NotifyUser("Query Result",
                            $"{RxDocs.Count} Rx Packets Recived \n");
        }


        private void AddRxQueryItems(List<Dictionary<string, object>> rxQueryDocs)
        {
            foreach (var doc in rxQueryDocs)
            {
                _view.RxTabPresenter.AddRecivedPacket
                    (
                    new PacketObject(RxJson, (string)doc["packetString"]),
                    DateTime.Parse((string)doc["time"]).ToLocalTime()
                    );
            }
        }
        private void AddTxQueryItems(List<Dictionary<string, object>> txQueryDocs)
        {
            foreach (var doc in txQueryDocs)
            {
                _view.RxTabPresenter.AddSentPacket
                    (
                    new PacketObject(TxJson, (string)doc["packetString"]),
                    DateTime.Parse((string)doc["time"]).ToLocalTime()
                    );
            }
        }
    }
}