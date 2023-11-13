using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            //Check if person is logged in
            //If person is logged in then Continue
            if (User.Identity.IsAuthenticated == false)
            {

                TempData["error"] = "Access Denied";

                //return View("Error, Request"); //This will look for a view with the name found in the parameter (brackets)
                return RedirectToAction ("Index", "Home");
            }
            //Else do not allow in == access denied / redirect the user to the login page

            return View();
        }
    }
}
