using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {

        /*Task 4*/


        /* Method (and View) which returns and shows on page a list of Flights that one can choose with RETAIL prices displayed.
         * Requirements [1.5] */
        public IActionResult ListFlights()
        {   
            /* 
                This is to return a web page which displays flights that can be booked
                    a.) Fully booked flights CAN NOT be selected, BUT still displayed 
                    b.) If Departure date is in the past, DO NOT DISPLAY
            */

            return View();
        }

        /* Method (and View) which allows the user to book a flight after entering the requested details to book a ticket. 
         * Requirements [2]*/
        public IActionResult BookFlight()
        {
            /*
                 Allows the user to book a flight after entering their details.
                    a.) Flight must NOT be fully booked 
                    c.) Flight must NOT be cancelled
                    b.) Flight must NOT be in the past
                    c.) PricePaid is filled in automatically after calculating commission on WholeSalePrice
            */

            return View();
        }



        /*Method (and View) which returns a list of Tickets (i.e. use GetTickets from Repository) which then returns a history list 
         *of tickets purchased by the logged in client (See Se3.3 for authentication) [1.5]
*/
        public IActionResult ListTicketHistory()
        {
            /*
                A LOGGED IN user can view a list of their previously purchased tickets
            */

            if (User.Identity.IsAuthenticated == false)
            {
                //Can make a toast which says "Only logged in users can view history of purchased tickets"
                TempData["errorMsg"] = "You must be logged in to view this";

                // Return to home (Index page) or other page? 
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", Request) //Was used for error handling testing (Brings up html page displaying erro)
            }

            return View();
        }





    }
}
