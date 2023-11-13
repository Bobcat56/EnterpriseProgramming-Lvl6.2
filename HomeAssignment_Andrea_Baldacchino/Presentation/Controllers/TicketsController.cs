using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult ListFlights()
        {   
            /* 
                This is to return a web page which displays flights that can be booked
                    a.) Fully booked flights CAN NOT be selected, BUT still displayed 
                    b.) If Departure date is in the past, DO NOT DISPLAY
            */

            return View();
        }

        public IActionResult BookFlight()
        {
            /*
                 
            */

            return View();
        }


        public IActionResult ListTicketHistory()
        {
            /*
                A LOGGED IN user can view a list of their previously purchased tickets
            */

            if (User.Identity.IsAuthenticated == false)
            {
                //Can make a toast which says "Only logged in users can view history of purchased tickets"
                TempData["error"] = "You must be logged in to view this";

                // Return to home (Index page) or other page? 
                return RedirectToAction("Index", "Home");
            }

            return View();
        }





    }
}
