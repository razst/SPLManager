using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packet_maker.MainComponents
{
    class DataBaseWorker
    {
        public List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>> QryFilters;
        public dynamic Documents;

        public DataBaseWorker()
        {
            QryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>();
        }
        public async Task StartWork(int qrySize, string targetIndex, string sortBy = null)
        {
            var searchDisc = new SearchDescriptor<Dictionary<string, object>>();
            searchDisc.Query(q => q.Bool(ft => ft.Must(QryFilters)));
            searchDisc.Index(targetIndex);
            searchDisc.Size(qrySize);
            if (sortBy != null) searchDisc.Sort(q => q.Ascending(obj => obj["time"]));
            //var work = await Program.db.SearchAsync<Dictionary<string, object>>(s => searchDisc);
            //Documents = work.Documents;
        }

        public void AddTermFilter(string fieldName, string WantedTerm)
        {
            QryFilters.Add(fq => fq.Term(f => f[fieldName].Suffix("keyword"), WantedTerm));
        }

        public void AddMatchFilter(string fieldName, string WantedMatch)
        {
            QryFilters.Add(q => q.Match(m => m.Field(fieldName).Query(WantedMatch)));
        }
        public void AddMatchPhraseFilter(string fieldName, string WantedPhrase)
        {
            QryFilters.Add(q => q.MatchPhrase(mt =>
            mt.Field(fieldName)
              .Query(WantedPhrase)));
        }
        public void AddRangeFilter(string fieldName, double? min, double? max)
        {
            var range = new NumericRangeQueryDescriptor<Dictionary<string, object>>();
            range.Field(fieldName);
            if (min != null) range.GreaterThanOrEquals(min);
            if (max != null) range.LessThanOrEquals(max);
            QryFilters.Add(q => q.Range(r => range));
        }
        public void AddDateRangeFilter(string fieldName, DateTime? min,DateTime? max)
        {
            var dateRange = new DateRangeQueryDescriptor<Dictionary<string, object>>();
            dateRange.Field(fieldName);
            if (min != null) dateRange.GreaterThanOrEquals(min);
            if (max != null) dateRange.LessThanOrEquals(max);
            QryFilters.Add(q => q.DateRange(dr => dateRange));
        }
        public void AddMustNotMatchFilter(string fieldName, string UnwantedValue)
        {
            QryFilters.Add(q => q.Bool(b => b.MustNot(mn => mn.Match(m =>
            m.Field(fieldName)
             .Query(UnwantedValue)))));
        }

    }
}
