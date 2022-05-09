using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SPL_Manager.Library.SatRadioPass
{
    public class RadioPassesPresenter
    {
        private readonly IRadioPassesService _service;
        private IRadioPassesView _view;
        private long UnixOfNextPass;
        private List<pass> NextRadioPasses = new List<pass>();

        private bool doTick = false;
        private Timer EverySecondTimer;
        private Timer EveryHourTimer;
        private Timer UtcTimer;

        public RadioPassesPresenter(IRadioPassesService service)
        {
            _service = service;

            // timers
            UtcTimer = new Timer(UtcTimerCallback, "", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            EveryHourTimer = new Timer(EveryHourCallback, "", TimeSpan.FromHours(1), TimeSpan.FromHours(1));
            EverySecondTimer = new Timer(EverySecondCallback, "", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));


        }
        public RadioPassesPresenter() : this(new RadioPassesService()) { }

        public void SetView(IRadioPassesView view)
        {
            _view = view;
        }

        public async Task LoadNextRadioPasses()
        {
            doTick = false;

            NextRadioPasses = await _service.GetNextPasses(_view.MainTabGroupIndex);
            if (NextRadioPasses.Count == 0) return;

            // put next pass on timer display
            UnixOfNextPass = NextRadioPasses[0].startUTC;
            _view.TimeTillNextPass = GetTimeStringOf(GetSecondTill(UnixOfNextPass));

            //put other passes on list display
            _view.NextPassesDates = NextRadioPasses.Select(x =>
                ConvertUnixToString(x.startUTC)).ToList();

            doTick = true;

        }

        private void UtcTimerCallback(object state)
        {
            _view.UtcDate = DateTime.UtcNow.ToString("G");
        }

        private async void EveryHourCallback(object state)
        {
            if (!doTick) return;
            await LoadNextRadioPasses();
        }



        private bool passOccured = false;
        private async void EverySecondCallback(object state)
        {

            if (!doTick) return;

            long secondsTillNextPass = GetSecondTill(UnixOfNextPass);

            if (secondsTillNextPass == 60)
            {
                doTick = false;
                await LoadNextRadioPasses();
                return;
            }

            if (secondsTillNextPass <= 0) passOccured = true;
            string timeToSet;
            if (!passOccured)
            {
                timeToSet = GetTimeStringOf(secondsTillNextPass);
                _view.TimeTillNextPass = timeToSet;
                return;
            }
            else
            {
                timeToSet = "00:00:00";
                _view.TimeTillNextPass = timeToSet;
            }


            if (NextRadioPasses[0].endUTC + 60 >= DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                _view.RadioPassStatus = "Passing";
                return;
            }
            else
            {
                doTick = false;
                _view.LastPassDate = ConvertUnixToString(NextRadioPasses[0].endUTC);
                passOccured = false;
                _view.RadioPassStatus = "Before Pass";

                await LoadNextRadioPasses();
            }
        }




        #region general
        private long GetSecondTill(long unix) => unix - DateTimeOffset.Now.ToUnixTimeSeconds();
        private string GetTimeStringOf(long SecondsTill) => TimeSpan.FromSeconds(SecondsTill).ToString(@"hh\:mm\:ss");
        private string ConvertUnixToString(long unix) => DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().ToString("G");


        public void Dispose()// TODO: use finalizer?
        {
            UtcTimer.Dispose();
            EverySecondTimer.Dispose();
            EveryHourTimer.Dispose();
        }
        ~RadioPassesPresenter()
        {
            UtcTimer.Dispose();
            EverySecondTimer.Dispose();
            EveryHourTimer.Dispose();
        }
        #endregion
    }
}