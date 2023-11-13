using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult ListFlights()
        {   
            /* This is to return a web page which displays flights that can be booked
                    a.) Fully booked flights CAN NOT be selected, BUT still displayed 
                    b.) If Departure date is in the past, DO NOT DISPLAY
            */

            return View();
        }

        public IActionResult BookFlight()
        {


            return View();
        }


        public IActionResult ListTicketHistory()
        {



            return View();
        }





    }
}
