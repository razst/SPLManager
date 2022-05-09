using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPL_Manager.Library.Shared;
using SPL_Manager.Library.PacketModel;

namespace SPL_Manager.Library.PacketLifecycle.Query.Advanced
{
    public struct PacketsWithDates
    {
        public PacketObject Packet;
        public DateTime Date;
    }
    public class AdvancedQueryPresenter
    {
        private IAdvancedQueryView _view;
        private PacketSubtype CurrentSubtype;
        private readonly PacketProtocolService jsonService;


        public AdvancedQueryPresenter()
        {
            jsonService = new PacketProtocolService(ProgramProps.PacketJsonFiles["Rx"]);
        }

        public void SetView(IAdvancedQueryView view)
        {
            _view = view;
        }

        // update selected subtype on view
        public void UpdateSubtype()
        {
            if (_view.QuerySubtype == "All") return;

            CurrentSubtype = jsonService.GetSubtypeByName(_view.QuerySubtype);

            _view.QueryFileds = CurrentSubtype.Parmas.ConvertAll(par => par.Name);
        }

        // update query by params
        public void UpdateFieldType()
        {
            if (_view.QueryTargetField == "") return;
            Params currentPar = jsonService.GetParamByName(CurrentSubtype, _view.QueryTargetField);
            if (currentPar.ParamType.StartsWith("date")) _view.TargetFieldIsDate = true;
            else _view.TargetFieldIsDate = false;
        }


        public async Task<Dictionary<string, List<PacketsWithDates>>> PreformQuery()
        {
            var result = new Dictionary<string, List<PacketsWithDates>>();
            if (!ProgramProps.DataBaseEnabled) return result;


            // max number of items in query
            if (_view.QuerySizeLimit == "") return result;
            int size = 10000;
            if (_view.QuerySizeLimit != "No Limit") size = int.Parse(_view.QuerySizeLimit);


            int rxCount = 0, txCount = 0;
            if (_view.IsIdQuery)
            {
                result = await GetIdPackets();
                rxCount = result["Rx"].Count;
                txCount = result["Tx"].Count;

            }
            else
            {
                if (_view.IsRxQuery)
                {
                    var RxLst = await GetRxPackets(size);
                    rxCount = RxLst.Count;
                    result.Add("Rx", RxLst);
                }
                if (_view.IsTxQuery)
                {
                    var TxLst = await GetTxPackets(size);
                    txCount = TxLst.Count;
                    result.Add("Tx", TxLst);
                }
            }


            //TODO: notify user:
            //MessageBox.Show($"{rxCount} RX packet were recived. \n{txCount} TX packet were recived.", "Query finished", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            return result;
        }



        private async Task<List<PacketsWithDates>> GetRxPackets(int size)
        {
            RepositoryWorker rxWorker = new RepositoryWorker();


            // adding all filters selected in view to custom db worker
            rxWorker.AddDateRangeFilter("time", _view.QueryMinTime, _view.QueryMaxTime);
            if (_view.QuerySatGroupIndex != 0)
                rxWorker.AddMatchFilter("satId", _view.QuerySatGroupIndex.ToString());

            if (_view.QuerySubtype != "All")
                rxWorker.AddTermFilter("subtype", _view.QuerySubtype);

            if (_view.IsFieldSpecificQuery)
            {
                switch (_view.QueryFieldOprt)
                {
                    case "=":
                        string match = _view.TargetFieldIsDate ?
                            _view.QueryTargetFieldValueDate.ToString() : _view.QueryTargetFieldValueStr;
                        rxWorker.AddMatchFilter(_view.QueryTargetField, match);
                        break;

                    case ">":
                        if (_view.TargetFieldIsDate)
                        {
                            rxWorker.AddDateRangeFilter(_view.QueryTargetId, _view.QueryTargetFieldValueDate, null);
                            break;
                        }
                        rxWorker.AddRangeFilter(_view.QueryTargetId, double.Parse(_view.QueryTargetFieldValueStr), null);
                        break;

                    case "<":
                        if (_view.TargetFieldIsDate)
                        {
                            rxWorker.AddDateRangeFilter(_view.QueryTargetId, null, _view.QueryTargetFieldValueDate);
                            break;
                        }
                        rxWorker.AddRangeFilter(_view.QueryTargetId, null, double.Parse(_view.QueryTargetFieldValueStr));
                        break;

                    case "!=":
                        if (_view.TargetFieldIsDate) break;
                        rxWorker.AddMustNotMatchFilter(_view.QueryTargetField, _view.QueryTargetFieldValueStr);
                        break;
                }
            }

            rxWorker.AddQuerySize(size);
            rxWorker.AddSortAscending();

            return ConvertDocToPacket(await rxWorker.StartWork("parsed-rx"), "rx");
        }


        private async Task<List<PacketsWithDates>> GetTxPackets(int size)
        {
            RepositoryWorker txWorker = new RepositoryWorker();
            txWorker.AddDateRangeFilter("time", _view.QueryMinTime, _view.QueryMaxTime);
            if (_view.QuerySatGroupIndex != 0)
                txWorker.AddMatchFilter("satId", _view.QuerySatGroupIndex.ToString());
            return ConvertDocToPacket(await txWorker.StartWork("parsed-tx"), "tx");
        }


        private async Task<Dictionary<string, List<PacketsWithDates>>> GetIdPackets()
        {
            var result = new Dictionary<string, List<PacketsWithDates>>();
            RepositoryWorker worker = new RepositoryWorker();

            worker.AddMatchPhraseFilter("splID", _view.QueryTargetId);
            worker.AddQuerySize(100);
            worker.AddSortAscending();

            var rxDocs = await worker.StartWork("parsed-rx");
            result.Add("Rx", ConvertDocToPacket(rxDocs, "rx"));
            var txDocs = await worker.StartWork("parsed-tx");
            result.Add("Tx", ConvertDocToPacket(txDocs, "tx"));

            return result;
        }


        // converts all db worker results to the packet type
        private List<PacketsWithDates> ConvertDocToPacket(List<Dictionary<string, object>> docs, string type)
        {
            var result = new List<PacketsWithDates>();
            PacketProtocol json;
            if (type == "rx") json = ProgramProps.PacketJsonFiles["Rx"];
            else json = ProgramProps.PacketJsonFiles["Tx"];

            foreach (var doc in docs)
            {
                var po = new PacketObject(json, (string)doc["packetString"]);
                var dt = DateTime.Parse((string)doc["time"]);
                result.Add(new PacketsWithDates { Packet = po, Date = dt });
            }
            return result;
        }

    }
}