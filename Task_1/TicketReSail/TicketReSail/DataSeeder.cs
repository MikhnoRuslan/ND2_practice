using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TicketReSail.Core.Interface;
using TicketReSail.DAL.Model;

namespace TicketReSail
{
    public class DataSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILocalizationService _localization;

        public DataSeeder(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ILocalizationService localization)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _localization = localization;
        }

        public async Task SeedDataAsync()
        {
            await _localization.AddLocalization();

            if (await _roleManager.FindByNameAsync(Constants.Administrator) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Administrator));

            if (await _roleManager.FindByNameAsync(Constants.Moderator) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Moderator));

            if (await _roleManager.FindByNameAsync(Constants.Employee) == null)
                await _roleManager.CreateAsync(new IdentityRole(Constants.Employee));

            if (await _userManager.FindByNameAsync(Constants.Email) == null)
            {
                var result = await _userManager.CreateAsync(GetAdmin(), Constants.Password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(GetAdmin(), Constants.Administrator);
            }
        }

        private User GetAdmin()
        {
            var user = new User
            {
                Email = Constants.Email,
                UserName = Constants.Email,
                EmailConfirmed = true,
                Login = Constants.Administrator,
                LocalizationId = 1,
                FirstName = Constants.Administrator,
                LastName = Constants.Administrator
            };

            return user;
        }
    }
}
