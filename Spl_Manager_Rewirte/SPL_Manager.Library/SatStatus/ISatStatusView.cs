namespace SPL_Manager.Library.SatStatus
{
    public interface ISatStatusView
    {
        public int MainTabGroupIndex { get; }

        public string LastRxFrameDate { set; }
        public string LastTxFrameDate { set; }
        public string LastBeaconDate { set; }
        public string OBCTimeDate { set; }


        public string NumberOfSatResets { set; }
        public string NumberOfCmdResets { set; }
        public string NumberOfCorruptBytes { set; }

        public string SatUptimeStr { set; }

        public float FreeSpaceValue { set; }
        public float BatTempValue { set; }
        public float OBCTempValue { set; }
        public float VBatValue { set; }
    }

    public static class ISatDataViewExtentions
    {
        public static void Clear(this ISatStatusView view)
        {
            view.LastRxFrameDate = "-- : -- : --";
            view.LastTxFrameDate = "-- : -- : --";
            view.LastBeaconDate = "-- : -- : --";
            view.OBCTimeDate = "-- : -- : --";

            view.NumberOfSatResets = "---";
            view.NumberOfCmdResets = "---";
            view.NumberOfCorruptBytes = "---";

            view.SatUptimeStr = "-- : -- : --";

            view.FreeSpaceValue = 2000;
            view.BatTempValue = 0;
            view.OBCTempValue = 0;
            view.VBatValue = 8;
        }
    }
}