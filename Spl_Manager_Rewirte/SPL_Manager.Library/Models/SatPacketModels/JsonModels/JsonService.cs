using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Models.SatPacketModels.JsonModels
{
    public class JsonService
    {
        private readonly PacketTypeList TxOptions;
        public JsonService(PacketTypeList tx)
        {
            TxOptions = tx;
        }




        public List<string> GetAllTypeNames()
        {
            return TxOptions.Types.ConvertAll(type => type.Name);
        }
        public PacketType GetTypeByIndex(int typeIndex)
        {
            return TxOptions.Types[typeIndex];
        }


        public List<string> GetAllSubtypeNamesOfType(int typeIndex)
        {
            return TxOptions.Types[typeIndex].SubTypes.ConvertAll(subtype => subtype.Name);
        }
        public List<PacketSubtype> GetAllSubtypes()
        {
            List<PacketSubtype> subtypes = new List<PacketSubtype>();
            TxOptions.Types.ForEach(type => subtypes.AddRange(type.SubTypes));
            return subtypes;
        }
        public PacketSubtype GetSubTypeByIndex(int typeIndex, int subtypeIndex)
        {
            return TxOptions.Types[typeIndex].SubTypes[subtypeIndex];
        }

        public PacketSubtype GetSubtypeByName(string subtypeName)
        {
            var subtypes = GetAllSubtypes();
            return subtypes.Find(sub => sub.Name == subtypeName);
        }


        public Params GetParamByName(PacketSubtype targetSubtype, string paramName)
            => targetSubtype.Parmas.Find(par => par.Name == paramName);
    }
}