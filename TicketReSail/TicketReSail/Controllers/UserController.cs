using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Core.Interface;
using TicketReSail.Models;

namespace TicketReSail.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var user = new UserViewModel
            {
                Users = await _userService.GetUserById(id)
            };
            return View("UserInfo", user);
        }
    }
}
