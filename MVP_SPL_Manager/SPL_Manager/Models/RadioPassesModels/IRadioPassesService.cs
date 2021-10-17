using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPL_Manager.Models.RadioPassesModels
{
    public interface IRadioPassesService
    {
        public Task<List<pass>> GetNextPasses(int SatId = -1);
    }

    public struct pass
    {
        public long startUTC;
        public long endUTC;
    }
}