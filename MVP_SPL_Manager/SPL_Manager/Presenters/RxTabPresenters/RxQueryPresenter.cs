using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SPL_Manager.Views.RxTabViews;
using SPL_Manager.Models.DataAccess;
using SPL_Manager.Models.SatPacketModels;
using SPL_Manager.Models.SatPacketModels.JsonModels;
using System.Threading.Tasks;

namespace SPL_Manager.Presenters.RxTabPresenters
{
    public class RxQueryPresenter
    {
        private readonly IRxQueryView _view;
        private static readonly PacketTypeList RxJson = ProgramProps.PacketJsonFiles["Rx"];
        private static readonly PacketTypeList TxJson = ProgramProps.PacketJsonFiles["Tx"];

        public RxQueryPresenter(IRxQueryView view)
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

            MessageBox.Show
                (
                $"{RxDocs.Count} Rx Packets Recived \n",
                "Query Result",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
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
