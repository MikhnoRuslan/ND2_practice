using System;
using MailKit.Search;

namespace TicketReSail.Core.Queries
{
    public class EventQuery : PageQuery, ISortQuery
    {
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public DateTime FistDataTime { get; set; }
        public DateTime SecondDataTime { get; set; }
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
