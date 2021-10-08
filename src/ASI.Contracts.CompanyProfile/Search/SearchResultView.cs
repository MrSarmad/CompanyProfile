using System;
using System.Collections.Generic;

namespace ASI.Contracts.CompanyProfile.Search
{
    [Serializable]
    public sealed class SearchResultView<T>
    {
        public List<T>? Results { get; set; }
        public long? ResultsTotal { get; set; }
        public Dictionary<string, List<string>>? Aggregations { get; set; }
    }
}
