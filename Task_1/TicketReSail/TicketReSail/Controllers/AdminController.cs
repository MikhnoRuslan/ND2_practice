using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.DAL.Model;

namespace TicketReSail.Controllers
{
    [Authorize(Roles = Constants.Administrator)]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public IActionResult ChangeRole(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View(_userManager.Users.ToList());
        }
    }
}