using System;
using System.Threading.Tasks;
using System.Linq;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.SatStatus
{
    public class SatStatusPresenter
    {
        private ISatStatusView _view;
        private readonly LatestBeaconRepository _repository;
        public SatStatusPresenter(LatestBeaconRepository repository)
        {
            _repository = repository;
        }
        public SatStatusPresenter():this(new LatestBeaconRepository()) { }

        public void SetView(ISatStatusView view)
        {
            _view = view;
        }

        public async Task UpdateSatDataView()
        {
            if (!ProgramProps.DataBaseEnabled) return;
            PacketObject po = await _repository.GetLastBeacon(_view.MainTabGroupIndex);
            var RxTxBeaconFrames = await _repository.GetLatestPacketsDates(_view.MainTabGroupIndex);

            _view.LastRxFrameDate = RxTxBeaconFrames.ElementAtOrDefault(0) ?? "---";
            _view.LastTxFrameDate = RxTxBeaconFrames.ElementAtOrDefault(1) ?? "---";
            _view.LastBeaconDate = RxTxBeaconFrames.ElementAtOrDefault(2) ?? "---";

            if (po.Type == -1) return;



            _view.VBatValue = (float)po.DataCatalog["vbat"] / 1000;
            _view.OBCTempValue = (float)po.DataCatalog["MCU Temperature"] / 100;
            _view.BatTempValue = (float)po.DataCatalog["Battery Temperature"] / 1000;
            _view.FreeSpaceValue = (float)po.DataCatalog["free_memory"] / 1000000;

            _view.OBCTimeDate = ((DateTimeOffset)po.DataCatalog["sat_time"]).ToString("G");


            _view.NumberOfSatResets = po.DataCatalog["number_of_resets"].ToString() ?? "-";
            _view.NumberOfCmdResets = po.DataCatalog["number_of_cmd_resets"].ToString() ?? "-";
            _view.NumberOfCorruptBytes = po.DataCatalog["corrupt_bytes"].ToString() ?? "-";

            var tempTS = TimeSpan.FromSeconds(int.Parse(po.DataCatalog["sat_uptime"].ToString() ?? "0"));
            _view.SatUptimeStr = $"{tempTS.Days}:{tempTS.Hours}:{tempTS.Seconds + tempTS.Minutes * 60}";


        }
    }
}
