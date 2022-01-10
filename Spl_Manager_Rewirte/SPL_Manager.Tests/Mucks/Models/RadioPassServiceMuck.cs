using SPL_Manager.Library.Models.RadioPassesModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Tests.Mucks.Models
{
    public class RadioPassServiceMuck : IRadioPassesService
    {
        private List<pass> passesMuck = new List<pass>()
        {
            new pass() 
            {
                startUTC = DateTimeOffset.Parse("10/01/2020 10:00:00").ToUnixTimeSeconds(),
                endUTC = DateTimeOffset.Parse("10/01/2020 10:01:30").ToUnixTimeSeconds()
            }
        };
        public Task<List<pass>> GetNextPasses(int SatId = -1)
        {
            return Task.FromResult(passesMuck);
        }
    }
}
