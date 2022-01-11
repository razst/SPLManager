using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.DataAccess
{
    public class RepositoryWorker
    {
        private List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>> QryFilters;
        private SearchDescriptor<Dictionary<string, object>> SearchDisc;
        public RepositoryWorker()
        {
            QryFilters = new List<Func<QueryContainerDescriptor<Dictionary<string, object>>, QueryContainer>>();
            SearchDisc = new SearchDescriptor<Dictionary<string, object>>();
        }

        public async Task<List<Dictionary<string, object>>> StartWork(string targetIndex)
        {
            if (!ProgramProps.DataBaseEnabled) return new List<Dictionary<string, object>>();
            if (QryFilters.Count == 0) SearchDisc.Query(q => q.MatchAll());
            else SearchDisc.Query(q => q.Bool(ft => ft.Must(QryFilters)));

            SearchDisc.Index(targetIndex);


            var work = await ProgramProps.Database.SearchAsync<Dictionary<string, object>>(s => SearchDisc);
            return (List<Dictionary<string, object>>)work.Documents;
        }



        //############### Size and sort ##############//

        public void AddQuerySize(int size)
        {
            if (size == -1) SearchDisc.Size(10000);
            else SearchDisc.Size(size);
        }

        public void AddSortDescending(string field = "time") => SearchDisc.Sort(q => q.Descending(obj => obj[field]));
        public void AddSortAscending(string field = "time") => SearchDisc.Sort(q => q.Ascending(obj => obj[field]));

        //##########################//



        //############### Filters ###############//
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
        public void AddDateRangeFilter(string fieldName, DateTime? min, DateTime? max)
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

        public void ClearFilters()
        {
            QryFilters.Clear();
            SearchDisc = new SearchDescriptor<Dictionary<string, object>>();
        }

        //##############################//
    }
}