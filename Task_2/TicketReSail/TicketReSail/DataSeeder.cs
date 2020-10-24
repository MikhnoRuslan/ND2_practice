using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketReSail.DAL;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class DataSeeder
    {
        private readonly TicketsContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public DataSeeder(TicketsContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
           
            if (await _roleManager.FindByNameAsync("Admin") == null)
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await _roleManager.FindByNameAsync("employee") == null)
                await _roleManager.CreateAsync(new IdentityRole("Employee"));

            if (await _userManager.FindByNameAsync(Constants.Email) == null)
            {
                var result = await _userManager.CreateAsync(GetAdmin(), Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(GetAdmin(), "Admin");
            }
        }

        private User GetAdmin()
        {
            var user = new User
            {
                Email = Constants.Email,
                UserName = Constants.Email,
                EmailConfirmed = true,
            };

            return user;
        }
    }
}
