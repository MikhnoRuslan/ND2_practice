using MailKit.Search;

namespace TicketReSail.Core.Queries
{
    public class SortQuery
    {
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}