using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var contextTicket = services.GetRequiredService<TicketsContext>();
            var contextUser = services.GetRequiredService<UserManager<User>>();
            var contextRole = services.GetRequiredService<RoleManager<IdentityRole>>();
            var seeder = new DataSeeder(contextTicket, contextUser, contextRole);
            await seeder.SeedDataAsync();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
