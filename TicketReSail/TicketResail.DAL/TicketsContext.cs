using Microsoft.EntityFrameworkCore;
using TicketReSail.DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketReSail.DAL
{
    public class TicketsContext : IdentityDbContext<User>
    {
        public TicketsContext(DbContextOptions<TicketsContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public DbSet<Localization> Localizations { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Category>().ToTable("Category");

            model.Entity<City>().ToTable("Cities");

            model.Entity<Event>().ToTable("Events");

            model.Entity<Order>().ToTable("Orders");

            model.Entity<Ticket>().ToTable("Tickets");

            model.Entity<Venue>().ToTable("Venues");

            model.Entity<Localization>();
        }
    }
}
