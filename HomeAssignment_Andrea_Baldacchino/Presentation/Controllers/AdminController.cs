using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult ListAllTickets()
        {
            //Display all past tickets & be able to view ALL data
            return View();
        }
    }
}
