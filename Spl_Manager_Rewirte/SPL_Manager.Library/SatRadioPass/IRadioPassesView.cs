using System.Collections.Generic;
using System.Linq;

namespace SPL_Manager.Library.SatRadioPass
{
    public interface IRadioPassesView
    {
        public int MainTabGroupIndex { get; }

        public string TimeTillNextPass { set; }

        public List<string> NextPassesDates { set; }

        public string RadioPassStatus { set; }

        public string LastPassDate { set; }

        public string UtcDate { set; }
    }

    public static class IRadioPassesViewExtentions
    {
        public static void Clear(this IRadioPassesView view)
        {
            view.TimeTillNextPass = "-- : -- : --";
            view.NextPassesDates = Enumerable.Repeat("____", 5).ToList();
            view.RadioPassStatus = "Before Pass";
            view.LastPassDate = "-- : -- : --";
        }
    }
}