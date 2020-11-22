using MailKit.Search;
using TicketReSail.Core.Enuns;

namespace TicketReSail.Core.Queries
{
    public interface ISortQuery
    {
        public SortBy SortBy { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}