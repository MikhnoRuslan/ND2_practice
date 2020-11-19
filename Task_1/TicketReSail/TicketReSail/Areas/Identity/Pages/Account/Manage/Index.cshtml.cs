using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketReSail.DAL.Model;

namespace TicketReSail.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Localization")]
            public int LocalizationId { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var address = await GetAddressAsync(user);
            var localization = await GetLocalizationAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Address = address,
                LocalizationId = localization
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userAddress = await GetAddressAsync(user);
            var userLocalization = await GetLocalizationAsync(user);

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Address != userAddress)
            {
                var setUserAddress = await SetAddressAsync(user, Input.Address);
                if (!setUserAddress.Succeeded)
                {
                    StatusMessage = "Unexpected error while trying to set user address";
                    return RedirectToPage();
                }
            }

            if (Input.LocalizationId != userLocalization)
            {
                var setLocalizationId = await SetLocalizationAsync(user, Input.LocalizationId);

                if (!setLocalizationId.Succeeded)
                {
                    StatusMessage = "Unexpected error while trying to set user localization";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task<string> GetAddressAsync(User user)
        {
            var result = await _userManager.FindByNameAsync(user.UserName);

            return result.Address;
        }

        private async Task<IdentityResult> SetAddressAsync(User user, string address)
        {
            if (user != null)
            {
                user.Address = address;
                await _userManager.UpdateAsync(user);

                return new IdentityResult();
            }

            return new IdentityResult();
        }

        private async Task<int> GetLocalizationAsync(User user)
        {
            var result = await _userManager.FindByNameAsync(user.UserName);

            return result.LocalizationId;
        }

        private async Task<IdentityResult> SetLocalizationAsync(User user, int localizationId)
        {
            if (user != null)
            {
                user.LocalizationId = localizationId;
                await _userManager.UpdateAsync(user);

                return new IdentityResult();
            }

            return new IdentityResult();
        }
    }
}
