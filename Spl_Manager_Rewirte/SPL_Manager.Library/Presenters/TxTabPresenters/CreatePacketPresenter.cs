using SPL_Manager.Library.Models.DataAccess;
using SPL_Manager.Library.Models.PacketServer;
using SPL_Manager.Library.Models.SatPacketModels;
using SPL_Manager.Library.Models.SatPacketModels.JsonModels;
using SPL_Manager.Library.Views.TxTabViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Presenters.TxTabPresenters
{
    public class CreatePacketPresenter
    {
        private readonly PacketTypeList TxOptions;
        private ITxTabView _view;
        private readonly JsonService _jsonService;
        private readonly IPacketsRepository _repository;

        public CreatePacketPresenter(IPacketsRepository repository)
        {
            TxOptions = ProgramProps.PacketJsonFiles["Tx"];
            _jsonService = new JsonService(TxOptions);
            _repository = repository;
        }

        public void SetView(ITxTabView view)
        {
            _view = view;
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
                    ParamValues = par.Values?.ConvertAll(val => val.VarName) ?? new List<string>()
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
            catch (Exception e)
            {
                //TODO notify user
                //MessageBox.Show($"invaled input: {Environment.NewLine}-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //TODO: for some reason, clipboard is in the windows library - so ask view to do this instead
                    //if (Hex != null && Hex != "")
                    //Clipboard.SetText(Hex);
                    break;
            }
        }

        public void CastPacketToView(PacketObject packet)
        {
            _view.TxCurrentTypeIndex = packet.GetTypeIndex();
            _view.TxCurrentSubtypeIndex = packet.GetSubtypeIndex();

            _view.FillTxDgvValues(packet.DataCatalog.Values.ToList().ConvertAll(val => val?.ToString() ?? "nill"));
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
                foreach (var param in _view.TxDgvParams)
                {
                    txPac.DataCatalog.Add(param.ParamName, param.ParamValues[0]);
                }

            txPac.RawPacket = txPac.CastToString(mode);
            return txPac;
        }
    }
}