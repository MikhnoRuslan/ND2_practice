using Microsoft.EntityFrameworkCore;
using TicketReSail.DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TicketReSail.DAL
{
    public sealed class TicketsContext : IdentityDbContext<User>
    {
        public TicketsContext(DbContextOptions<TicketsContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Category>().ToTable("Category");

            model.Entity<City>().ToTable("Cities");

            model.Entity<Event>().ToTable("Events")
                .HasOne(e => e.Category)
                .WithMany(c => c.Events);

            model.Entity<Order>().ToTable("Orders");

            model.Entity<Ticket>().ToTable("Tickets")
                .HasOne(t => t.Event)
                .WithMany(e => e.Tickets);

            model.Entity<Ticket>()
                .HasOne(t => t.Event)
                .WithMany(u => u.Tickets);

            model.Entity<Venue>().ToTable("Venues")
                .HasOne(v => v.City)
                .WithMany(c => c.Venues);
        }
    }
}
