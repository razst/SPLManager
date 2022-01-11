using System;
using System.Collections.Generic;
using System.Text;
using SPL_Manager.Library.Views.RxTabViews;
using SPL_Manager.Library.Models.DataAccess;
using SPL_Manager.Library.Models.SatPacketModels;
using SPL_Manager.Library.Models.SatPacketModels.JsonModels;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Presenters.RxTabPresenters
{
    public class RxQueryPresenter
    {
        private IRxQueryView _view;
        private readonly PacketTypeList RxJson;
        private readonly PacketTypeList TxJson;

        public RxQueryPresenter()
        {
            RxJson = ProgramProps.PacketJsonFiles["Rx"];
            TxJson = ProgramProps.PacketJsonFiles["Tx"];
        }
        public void SetView(IRxQueryView view)
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


            //TODO notify user:
            /*
            MessageBox.Show
                (
                $"{RxDocs.Count} Rx Packets Recived \n",
                "Query Result",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
            */
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