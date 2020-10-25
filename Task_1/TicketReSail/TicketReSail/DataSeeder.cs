using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class DataSeeder
    {
        
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public DataSeeder(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync()
        {
            if (await _roleManager.FindByNameAsync("Admin") == null)
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await _roleManager.FindByNameAsync("Employee") == null)
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
