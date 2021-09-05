using System.Collections.Generic;

namespace GenericApp.Infra.Common.Objects
{
    public class PagedDataSpecification
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Dictionary<string, object> Filters { get; set; }
        public string SortBy { get; set; }
        public bool Descending { get; set; }
    }
}
