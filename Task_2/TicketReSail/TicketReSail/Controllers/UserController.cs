using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TicketReSail.Core.Interface;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStringLocalizer<UserController> _localizer;

        public UserController(IUserService userService, IStringLocalizer<UserController> localizer)
        {
            _userService = userService;
            _localizer = localizer;
        }

        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            var user = new UserViewModel
            {
                Users = (await _userService.GetUserById(id)).ToArray()
            };

            return View("UserInfo", user);
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel, string returnUrl)
        {
            try
            {
                if (!_userService.ValidatePassword(loginViewModel.Login, loginViewModel.Password))
                {
                    ModelState.AddModelError(nameof(loginViewModel.Password), _localizer["Wrong password!"].Value);
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginViewModel.Login),
                    new Claim(ClaimTypes.Role, _userService.GetRole(loginViewModel.Login))
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            } catch (ArgumentException)
            {
                ModelState.AddModelError(nameof(loginViewModel.Login), _localizer["Message"].Value);
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
