using AutoMapper;
using System.Collections.Generic;
using X.PagedList;

namespace GenericApp.Infra.CC.Mapping.Extensions
{
    public static class PagedDataExtensions
    {
        public static IPagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this IPagedList<TSource> list, IMapper mapper)
        {
            return new StaticPagedList<TDestination>(mapper.Map<IEnumerable<TDestination>>(list), list.GetMetaData());
        }
    }
}
