using System;
using MailKit.Search;
using TicketReSail.Core.Enuns;

namespace TicketReSail.Core.Queries
{
    public class EventQuery : PageQuery, ISortQuery
    {
        public string EventName { get; set; }
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public SortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
