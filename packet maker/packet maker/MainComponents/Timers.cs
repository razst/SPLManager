using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;


namespace packet_maker.MainComponents
{
    public class NextPassWorker
    {
        private readonly Timer EverySecondTimer;
        private readonly Timer EveryHourTimer;
        public dynamic passesInfo;
        public List<dynamic> passesArr;
        private bool passOccured = false;
        private string[] NextPassesArr = new string[4];



        public string LastPassStr { get; private set; }
        public string PassStatus;
        public int CurrnetGroup
        {
            get; private set;
        }

        public async void SetCurrentGroup(int newCurrentGroup)
        {
            CurrnetGroup = newCurrentGroup;
            EverySecondTimer.Stop();

            if (newCurrentGroup == 0 || !Program.Online) return;

            await GetNextPass(newCurrentGroup);
            for (int i = 0; i < 4; i++)
            {
                NextPassesArr[i] = DateTimeOffset.FromUnixTimeSeconds((long)passesArr[i + 1].startUTC).LocalDateTime.ToString();
            }

            EverySecondTimer.Start();
            EveryHourTimer.Stop();
            EveryHourTimer.Start();
        }


        public string TimeTilPassStr {
            get;private set;
        }
        private long SecondsTilPass;

        private void SetSecondsTilPass(long NewSecondsTilPass)
        {
            SecondsTilPass = NewSecondsTilPass;
            TimeSpan timespan = TimeSpan.FromSeconds(NewSecondsTilPass);
            TimeTilPassStr = timespan.ToString(@"hh\:mm\:ss");
        }


        public NextPassWorker(int GroupIndex)
        {
            PassStatus = "Before Pass";
            LastPassStr = "-- : -- : --";

            EverySecondTimer = new Timer { Interval = 1000 };
            EverySecondTimer.Tick += new EventHandler(SecondTimerTick);

            EveryHourTimer = new Timer { Interval = 3600000 };
            EveryHourTimer.Tick += new EventHandler(HourTimerTick);
            EveryHourTimer.Start();

            SetCurrentGroup(GroupIndex);
        }

        private void HourTimerTick(object sender, EventArgs e)
        {
            SetCurrentGroup(CurrnetGroup);
        }

        private void SecondTimerTick(object sender, EventArgs e)

        {
            if (!passOccured)
            {
                SetSecondsTilPass(SecondsTilPass - 1);

                if (SecondsTilPass == 60) SetCurrentGroup(CurrnetGroup);
                if (SecondsTilPass == 0) passOccured = true;
            }

            if (!passOccured || passesArr == null) return;

            if ((long)passesArr[0].endUTC + 60 >= DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                PassStatus = "Passing";
                return;
            }

            EverySecondTimer.Stop();
            LastPassStr = DateTimeOffset.FromUnixTimeSeconds((long)passesArr[0].endUTC).LocalDateTime.ToString();
            SetCurrentGroup(CurrnetGroup);
            passOccured = false;
            PassStatus = "Before Pass";
            EverySecondTimer.Start();
        }

        private async Task GetNextPass(int satId)
        {
            DateTime dt = new DateTime();
            dynamic t = 43199;

            var a = ((IEnumerable<dynamic>)Program.settings.satInfo).AsEnumerable().ToList().Find(x => x.id == satId);
            if (a.noradID == null) throw new Exception();//TODO:Show an error
            t = a.noradID;


            var t2 = Program.settings.groundStationLocation;
            dynamic data = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.n2yo.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"/rest/v1/satellite/radiopasses/{t}/{t2.lat}/{t2.lng}/{t2.alt}/7/{t2.minElevation}/&apiKey=78C29S-25XY9W-E8SWZD-4L2F");
                if (response.IsSuccessStatusCode)
                {
                    var txtResponse = await response.Content.ReadAsStringAsync();

                    data = Json.Decode(txtResponse);
                }
            }

            if (data == null) return;

            long time = (long)data.passes[0].startUTC;

            dt = DateTimeOffset.FromUnixTimeSeconds(time).LocalDateTime;

            passesInfo = data.info;
            passesArr = ((IEnumerable<dynamic>)data.passes).ToList();
            SetSecondsTilPass(GetSecondsTil(dt));
        }

        private long GetSecondsTil(DateTime dt) => (dt.Ticks - DateTime.Now.Ticks) / TimeSpan.TicksPerSecond;
    }
}
