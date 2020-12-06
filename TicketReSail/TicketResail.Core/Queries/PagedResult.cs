using System.Collections.Generic;

namespace TicketReSail.Core.Queries
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }
    }
}