using SPL_Manager.Library.PacketLifecycle.Send;
using System.Collections.Generic;

namespace SPL_Manager.Library.PacketLifecycle.Create
{
    public interface ICreatePacketView
    {
        public List<string> TxPacketsTypesList { set; }
        public List<string> TxPacketsSubtypesList { set; }

        public int TxCurrentTypeIndex { get; set; }
        public int TxCurrentSubtypeIndex { get; set; }

        public string TxTypeDesc { set; }
        public string TxSubtypeDesc { set; }

        public string TxPacketHexStr { get; set; }

        public string TxCurrentSatGroup { get; }


        public List<PacketParamView> TxDgvParams { get; set; }
        public void FillTxDgvValues(List<string> values);



        public PacketServerPresenter PacketServerPresenter { get; }

        public CreatePacketPresenter CreatePacketPresenter { get; }

    }

    public struct PacketParamView
    {
        public string Name;
        public string Description;
        public string ValueType;
        public List<string> OptinalValues;
    }
}