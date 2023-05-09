using OnlineStore.Data.Contracts.Interfaces;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace OnlineStore.Data.Helpers
{
    public class SortHelper<T> : ISortHelper<T> where T : class
    {
        public IQueryable<T> ApplySorting(IQueryable<T> query, string ordering)      
        {
            if (!query.Any())
            {
                return query;
            }

            if (string.IsNullOrWhiteSpace(ordering))
            {
                return query;
            }

            var orderParams = ordering.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var orderParam in orderParams)
            {
                var propertyFromQueryName = orderParam.Split(" ")[0];
                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty != null)
                {
                    var sortingOrder = orderParam.EndsWith("desc") ? "descending" : "ascending";
                    orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
                }
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return query.OrderBy(orderQuery);
        }
    }
}
