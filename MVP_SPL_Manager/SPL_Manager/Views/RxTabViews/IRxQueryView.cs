using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Views.RxTabViews
{
    public interface IRxQueryView : IView
    {
        public string RxQueryLimit { get; }
        public DateTime RxQueryMinDate { get; }
        public DateTime RxQueryMaxDate { get; }

        public Presenters.RxTabPresenters.RxTabPresenter RxTabPresenter { get; set; }
        public Presenters.RxTabPresenters.RxQueryPresenter RxQueryPresenter { get; set; }

    }
}
