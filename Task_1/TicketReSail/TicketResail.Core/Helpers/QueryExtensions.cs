using System.Linq;
using TicketReSail.Core.Queries;

namespace TicketReSail.Core.Helpers
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, PageQuery query)
        {
            if (query.PageSize <= 0)
                query.PageSize = 10;

            if (query.PageSize <= 0)
                query.PageSize = 1;

            return queryable.Skip(query.PageSize * (query.PageSize - 1)).Take(query.PageSize);
        }
    }
}