﻿using SPL_Manager.Views.MainTabViews;
using SPL_Manager.Models.DataAccess;
using SPL_Manager.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Presenters.MainTabPresenters
{
    public class SatDataPresenter
    {
        private readonly ISatDataView _view;
        private readonly IPacketsRepository _repository;
        public SatDataPresenter(ISatDataView view, IPacketsRepository repository = null)
        {
            _view = view;
            if(repository == null) _repository = new PacketsRepository();
        }
        public async Task UpdateSatDataView()
        {
            if (!ProgramProps.DataBaseEnabled) return;
            PacketObject po = await _repository.LoadLastBeacon(_view.MainTabGroupIndex);
            var RxTxBeaconFrames = await _repository.LoadLastFrameDates(_view.MainTabGroupIndex);

            if (RxTxBeaconFrames.Count == 0) return;
            _view.LastRxFrameDate = RxTxBeaconFrames[0];
            _view.LastTxFrameDate = RxTxBeaconFrames[1];
            _view.LastBeaconDate = RxTxBeaconFrames[2];
            
            if (po.Type == -1) return;

            

            _view.VBatValue = ((float)po.DataCatalog["vbat"]) / 1000;
            _view.OBCTempValue = ((float)po.DataCatalog["MCU Temperature"]) / 100;
            _view.BatTempValue = ((float)po.DataCatalog["Battery Temperature"]) / 1000;
            _view.FreeSpaceValue = ((float)po.DataCatalog["free_memory"]) / 1000000;

            _view.OBCTimeDate = (string)po.DataCatalog["sat_time"];

            _view.NumberOfSatResets = po.DataCatalog["number_of_resets"].ToString();
            _view.NumberOfCmdResets = po.DataCatalog["number_of_cmd_resets"].ToString();
            _view.NumberOfCorruptBytes = po.DataCatalog["corrupt_bytes"].ToString();

            var tempTS = TimeSpan.FromSeconds(int.Parse(po.DataCatalog["sat_uptime"].ToString()));
            _view.SatUptimeStr = $"{tempTS.Days}:{tempTS.Hours}:{tempTS.Seconds + tempTS.Minutes * 60}";
        }
    }
}