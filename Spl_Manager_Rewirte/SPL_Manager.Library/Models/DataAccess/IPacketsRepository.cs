using SPL_Manager.Library.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.DataAccess
{
    public interface IPacketsRepository
    {
        public Task<PacketObject> LoadLastBeacon(int groupIndex);

        public Task<List<string>> LoadLastFrameDates(int groupIndex);

        public Task<int> GetSplId(string group);

        public Task UploadPacket(PacketObject packet, string collection);
    }
}