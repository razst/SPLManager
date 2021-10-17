using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Views.TxTabViews
{
    public interface ITxTabView : IView
    {
        public List<string> TxPacketsTypesList { set; }
        public List<string> TxPacketsSubtypesList { set; }

        public int TxCurrentTypeIndex { get; set; }
        public int TxCurrentSubtypeIndex { get; set; }

        public string TxTypeDesc { set; }
        public string TxSubtypeDesc { set; }

        public string TxPacketHexStr { get; set; }

        public string TxCurrentSatGroup { get; }


        public List<TxDgvLine> TxDgvParams { get; set; }
        public void FillTxDgvValues(List<string> values);



        public Presenters.PacketServerPresenter PacketServerPresenter { get; set; }

        public Presenters.TxTabPresenters.CreatePacketPresenter CreatePacketPresenter { get; set; }

    }

    public struct TxDgvLine
    {
        public string ParamName;
        public string ParamDescription;
        public string ParamType;
        public List<string> ParamValues;
    }
}
