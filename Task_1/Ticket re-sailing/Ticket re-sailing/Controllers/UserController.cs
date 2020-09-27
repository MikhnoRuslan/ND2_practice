using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket_re_sailing.Business;
using Ticket_re_sailing.Models;

namespace Ticket_re_sailing.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            var user = new UserViewModel
            {
                Users = _userRepository.GetUserById(id)
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
                if (!_userRepository.ValidatePassword(loginViewModel.Login, loginViewModel.Password))
                {
                    ModelState.AddModelError(nameof(loginViewModel.Password), "Wrong password!");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginViewModel.Login),
                    new Claim(ClaimTypes.Role, _userRepository.GetRole(loginViewModel.Login))
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (AggregateException e)
            {
                ModelState.AddModelError(nameof(loginViewModel.Login), e.Message);
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
