using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Linq;
using SPL_Manager.Library.Models.RadioPassesModels;
using System.Threading.Tasks;
using SPL_Manager.Library.Views.MainTabViews;
using SPL_Manager.Library.Views;

namespace SPL_Manager.Library.Presenters.MainTabPresenters
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
            _view.NextPassesDates = NextRadioPasses.ConvertAll(x =>
                ConvertUnixToString(x.startUTC));

            doTick = true;

        }

        private void UtcTimerCallback(object? state)
        {
            _view.SafelyRun((string txt) => _view.UtcDate = txt, DateTime.UtcNow.ToString("G"));
        }

        private void EveryHourCallback(object? state)
        {
            if (!doTick) return;
            _view.SafelyRun(LoadNextRadioPasses);
        }



        private bool passOccured = false;
        private void EverySecondCallback(object? state)
        {

            if (!doTick) return;

            long secondsTillNextPass = GetSecondTill(UnixOfNextPass);

            if (secondsTillNextPass == 60)
            {
                doTick = false;
                _view.SafelyRun(LoadNextRadioPasses);
                return;
            }

            if (secondsTillNextPass <= 0) passOccured = true;
            string timeToSet;
            if (!passOccured)
            {
                timeToSet = GetTimeStringOf(secondsTillNextPass);
                _view.SafelyRun((string txt) => _view.TimeTillNextPass = txt, timeToSet);
                return;
            }
            else
            {
                timeToSet = "00:00:00";
                _view.SafelyRun((string txt) => _view.TimeTillNextPass = txt, timeToSet);
            }


            if (NextRadioPasses[0].endUTC + 60 >= DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                _view.SafelyRun((string txt) => _view.RadioPassStatus = txt, "Passing");
                return;
            }
            else
            {
                doTick = false;
                _view.SafelyRun((string txt) => _view.LastPassDate = txt, ConvertUnixToString(NextRadioPasses[0].endUTC));
                passOccured = false;
                _view.SafelyRun((string txt) => _view.RadioPassStatus = txt, "Before Pass");

                _view.SafelyRun(LoadNextRadioPasses);
            }
        }




        #region general
        private long GetSecondTill(long unix) => unix - DateTimeOffset.Now.ToUnixTimeSeconds();
        private string GetTimeStringOf(long SecondsTill) => TimeSpan.FromSeconds(SecondsTill).ToString(@"hh\:mm\:ss");
        private string ConvertUnixToString(long unix) => DateTimeOffset.FromUnixTimeSeconds(unix).ToLocalTime().ToString("G");


        public void Dispose()
        {
            UtcTimer.Dispose();
            EverySecondTimer.Dispose();
            EveryHourTimer.Dispose();
        }
        #endregion
    }
}