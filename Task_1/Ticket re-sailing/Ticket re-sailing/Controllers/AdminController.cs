using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticket_re_sailing.Business.Model;

namespace Ticket_re_sailing.Controllers
{
    [Authorize(Roles = Constants.Administrator)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
