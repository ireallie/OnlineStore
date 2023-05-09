using Newtonsoft.Json;
using OnlineStore.Data.Contracts.Models;

namespace OnlineStore.Api.Helpers
{
    public static class PagedListExtensions
    {
        public static string GetMetadata<T>(this PagedList<T> pageList)
            where T : class
        {
            var metadata = new
            {
                pageList.TotalCount,
                pageList.PageSize,
                pageList.CurrentPage,
                pageList.TotalPages,
                pageList.HasNext,
                pageList.HasPrevious
            };

            return JsonConvert.SerializeObject(metadata);
        }
    }
}
