using SPL_Manager.Models.DataAccess;
using SPL_Manager.Models.PacketServer;
using SPL_Manager.Models.SatPacketModels;
using SPL_Manager.Models.SatPacketModels.JsonModels;
using SPL_Manager.Views.TxTabViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPL_Manager.Presenters.TxTabPresenters
{
    public class CreatePacketPresenter
    {
        private static readonly PacketTypeList TxOptions = ProgramProps.PacketJsonFiles["Tx"];
        private readonly ITxTabView _view;
        private readonly JsonService _jsonService;
        private readonly IPacketsRepository _repository;

        public CreatePacketPresenter(ITxTabView view, IPacketsRepository repository = null)
        {
            _view = view;
            _jsonService = new JsonService(TxOptions);

            if (repository == null) _repository = new PacketsRepository();
            else _repository = repository;

            LoadTypes();
        }


        private void LoadTypes()
        {
            _view.TxPacketsTypesList = _jsonService.GetAllTypeNames();
        }
        public void LoadTypeDesc()
        {
            _view.TxTypeDesc = _jsonService.GetTypeByIndex(_view.TxCurrentTypeIndex).Desc;
        }


        public void LoadSubtypes()
        {
            _view.TxPacketsSubtypesList = _jsonService.GetAllSubtypeNamesOfType(_view.TxCurrentTypeIndex);
        }
        public void LoadSubtypeDesc()
        {
            _view.TxSubtypeDesc = _jsonService.GetSubTypeByIndex(_view.TxCurrentTypeIndex, _view.TxCurrentSubtypeIndex).Desc;
        }

        public void LoadGridParams()
        {
            _view.TxDgvParams = 
                _jsonService.GetSubTypeByIndex(_view.TxCurrentTypeIndex, _view.TxCurrentSubtypeIndex)
                .Parmas.ConvertAll(par => new TxDgvLine() 
                { 
                    ParamName = par.Name,
                    ParamDescription = par.Desc,
                    ParamType = par.ParamType,
                    ParamValues = par.Values?.ConvertAll(val => val.VarName)
                });
        }




        public async Task GeneratePacket(string mode)
        {

            string Hex;
            try
            {
                var po = await CreatePacketFromView(mode);
                Hex = po.RawPacket;
            }
            catch(Exception e)
            {
                MessageBox.Show($"invaled input: {Environment.NewLine}-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _view.TxPacketHexStr = "";
                return; 
            }

            _view.TxPacketHexStr = Hex;

            switch (mode)
            {
                case "satelite":
                    await new PacketObject(TxOptions, Hex).Upload("parsed-tx");
                    break;

                case "clipboard":
                    if (Hex != null && Hex != "")
                        Clipboard.SetText(Hex);
                    break;
            }        
        }

        public void CastPacketToView(PacketObject packet)
        {
            _view.TxCurrentTypeIndex = packet.GetTypeIndex();
            _view.TxCurrentSubtypeIndex = packet.GetSubtypeIndex();

            _view.FillTxDgvValues(packet.DataCatalog.Values.ToList().ConvertAll(val => val.ToString()));
        }

        public async Task<PacketObject> CreatePacketFromView(string mode)
        {
            int id = await _repository.GetSplId(_view.TxCurrentSatGroup);
            PacketObject txPac = new PacketObject()
            {
                JsonObject = TxOptions,
                Type = _view.TxCurrentTypeIndex,
                Subtype = _view.TxCurrentSubtypeIndex,
                Id = id,
                SateliteGroup = _view.TxCurrentSatGroup
            };

            if (_view.TxDgvParams.Count > 0)
                foreach(var param in _view.TxDgvParams)
                {
                    txPac.DataCatalog.Add(param.ParamName, param.ParamValues[0]);
                }

            txPac.RawPacket =  txPac.CastToString(mode);
            return txPac;
        }
    }
}
