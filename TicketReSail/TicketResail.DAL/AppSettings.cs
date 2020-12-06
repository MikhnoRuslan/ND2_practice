using Microsoft.EntityFrameworkCore;

namespace TicketReSail.DAL
{
    public class AppSettings
    {
        public DbContextOptions<TicketsContext> ConnString { get; set; }
    }
}
