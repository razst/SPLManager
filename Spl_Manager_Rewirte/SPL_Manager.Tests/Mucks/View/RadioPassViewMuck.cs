using SPL_Manager.Library.Views.MainTabViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Tests.Mucks.View
{
    internal class RadioPassViewMuck : IRadioPassesView
    {
        public int MainTabGroupIndex { get; set; }

        public string TimeTillNextPass { get; set; }
        public List<string> NextPassesDates { get; set; }
        public string RadioPassStatus { get; set; }
        public string LastPassDate { get; set; }
        public string UtcDate { get; set; }

        public object Invoke(Delegate method, params object[] args)
        {
            method.DynamicInvoke(args);
            return "nill";
        }

        public object Invoke(Delegate method)
        {
            method.DynamicInvoke();
            return "nill";
        }

    }
}
