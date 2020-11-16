using System;

namespace TicketReSail.Core.Queries
{
    public class EventQuery
    {
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
        public DateTime FistDataTime { get; set; }
        public DateTime SecondDataTime { get; set; }
    }
}
