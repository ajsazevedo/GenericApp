using System.Collections.Generic;
using X.PagedList;

namespace GenericApp.Infra.Common.Objects
{
    public class PagedData<T>
    {
        public PagedData(IEnumerable<T> items, PagedListMetaData pagedListInfo)
        {
            Items = items;
            PagedListInfo = pagedListInfo;
        }

        public IEnumerable<T> Items { get; set; }
        public PagedListMetaData PagedListInfo { get; set; }
    }
}
