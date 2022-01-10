using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Views.QueryTabViews
{
    public interface IQuerySelectionView
    {
        public DateTime QueryMinTime { get; }
        public DateTime QueryMaxTime { get; }
        public int QuerySatGroupIndex { get; }
        public string QuerySubtype { get; }
        public string QuerySizeLimit { get; }


        public bool IsTxQuery { get; }
        public bool IsRxQuery { get; }
        public bool IsIdQuery { get; }
        public bool IsFieldSpecificQuery { get; }
        public bool TargetFieldIsDate { get; set; }

        public string QueryTargetId { get; }
        public string QueryTargetField { get; }
        public List<string> QueryFileds { set; }
        public string QueryFieldOprt { get; }
        public string QueryTargetFieldValueStr { get; }
        public DateTime QueryTargetFieldValueDate { get; }


        public Presenters.QueryTabPresenters.QuerySelctionPresenter QuerySelctionPresenter { get; }
    }
}