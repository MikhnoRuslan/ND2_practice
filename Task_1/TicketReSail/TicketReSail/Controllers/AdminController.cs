using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketReSail.Business.Model;

namespace TicketReSail.Controllers
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