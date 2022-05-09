using System.Collections.Generic;
using System.Linq;

namespace SPL_Manager.Library.PacketModel
{
    public class PacketProtocolService
    {
        private readonly PacketProtocol _packetProtocol;
        public PacketProtocolService(PacketProtocol tx)
        {
            _packetProtocol = tx;
        }




        public List<string> GetAllTypeNames()
        {
            return _packetProtocol.Types.ConvertAll(type => type.Name);
        }
        public PacketType GetTypeByIndex(int typeIndex)
        {
            return _packetProtocol.Types[typeIndex];
        }


        public List<string> GetAllSubtypeNamesOfType(int typeIndex)
        {
            return _packetProtocol
                .Types[typeIndex]
                .SubTypes
                .Select(subtype => subtype.Name)
                .ToList();
        }
        public List<PacketSubtype> GetAllSubtypes()
        {
            List<PacketSubtype> subtypes = new List<PacketSubtype>();
            _packetProtocol.Types.ForEach(type => subtypes.AddRange(type.SubTypes));
            return subtypes;
        }
        public PacketSubtype GetSubTypeByIndex(int typeIndex, int subtypeIndex)
        {
            return _packetProtocol.Types[typeIndex].SubTypes[subtypeIndex];
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