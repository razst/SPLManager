using SPL_Manager.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Models.DataAccess
{
    public interface IPacketsRepository
    {
        public Task<PacketObject> LoadLastBeacon(int groupIndex);

        public Task<List<string>> LoadLastFrameDates(int groupIndex);

        public Task<int> GetSplId(string group);
    }
}
