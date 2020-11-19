using MailKit.Search;

namespace TicketReSail.Core.Queries
{
    public interface ISortQuery
    {
        public string SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}