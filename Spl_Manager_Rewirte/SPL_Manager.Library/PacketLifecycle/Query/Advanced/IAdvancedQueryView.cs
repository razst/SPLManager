using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;

namespace SPL_Manager.Library.PacketLifecycle.Query.Advanced
{
    public interface IAdvancedQueryView : ICustomView
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


        public AdvancedQueryPresenter QuerySelctionPresenter { get; }
    }
}