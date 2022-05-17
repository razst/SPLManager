using SPL_Manager.Library.PacketLifecycle.Save;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.PacketModel.Converters;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPL_Manager.Library.PacketLifecycle.Create
{
    public class CreatePacketPresenter
    {
        private readonly PacketProtocol TxOptions;
        private ICreatePacketView _view;
        private readonly PacketProtocolService _jsonService;
        private readonly IPacketsRepository _repository;

        public CreatePacketPresenter(IPacketsRepository repository)
        {
            TxOptions = ProgramProps.PacketJsonFiles["Tx"];
            _jsonService = new PacketProtocolService(TxOptions);
            _repository = repository;
        }
        public CreatePacketPresenter() : this(PacketsRepository.Instance) { }

        public void SetView(ICreatePacketView view)
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
                .Parmas.ConvertAll(par => new PacketParamView()
                {
                    Name = par.Name,
                    Description = par.Desc,
                    ValueType = par.ParamType,
                    OptinalValues = par.Values?.ConvertAll(val => val.VarName) ?? new List<string>()
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
                _view.AlertUser("Error", $"invaled input: \n-{e.Message}");
                _view.TxPacketHexStr = "";
                return;
            }

            _view.TxPacketHexStr = Hex;
        }

        public void CastPacketToView(PacketObject packet)
        {
            _view.TxCurrentTypeIndex = packet.GetTypeIndex();
            _view.TxCurrentSubtypeIndex = packet.GetSubtypeIndex();

            _view.FillTxDgvValues(packet.DataCatalog.Values.ToList().ConvertAll(val => val?.ToString() ?? "nill"));
        }

        public async Task<PacketObject> CreatePacketFromView(string mode)
        {
            long id = await _repository.GetSplId(_view.TxCurrentSatGroup);
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
                    txPac.DataCatalog.Add(param.Name, param.OptinalValues[0]);
                }

            txPac.RawPacket = txPac.ToRawString(mode);
            return txPac;
        }
    }
}