using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.SatRadioPass
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

    public class RadioPassesService : IRadioPassesService
    {
        public async Task<List<pass>> GetNextPasses(int satId = -1)
        {
            var emptyResult = new List<pass>();

            if (!ProgramProps.GetIfOnline()) return emptyResult;
            if (satId == -1) return emptyResult;

            int noradID = -1;


            dynamic SatSettings = ((IEnumerable<dynamic>)ProgramProps.settings.satInfo).ToList().Find(x => x.id == satId);
            noradID = (int?)SatSettings?.noradID ?? -1;
            if (noradID == -1) return emptyResult;







            dynamic t2 = ProgramProps.settings.groundStationLocation;

            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.n2yo.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"/rest/v1/satellite/radiopasses/{noradID}/{t2.lat}/{t2.lng}/{t2.alt}/7/{t2.minElevation}/&apiKey=78C29S-25XY9W-E8SWZD-4L2F");
            }
            catch
            {
                return emptyResult;
            }
            if (!response.IsSuccessStatusCode) return emptyResult;


            var txtResponse = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(txtResponse);
            return ((JArray)data.passes).Select(x =>
                new pass
                {
                    startUTC = (long)x["startUTC"],
                    endUTC = (long)x["endUTC"]
                }).ToList();




        }
    }
}