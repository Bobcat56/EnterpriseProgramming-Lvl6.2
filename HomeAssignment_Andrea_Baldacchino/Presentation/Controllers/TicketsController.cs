using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class TicketsController : Controller
    {
        /*Task 4*/
        private FlightDbRepository _flightDbRepository;
        private TicketDBRepository _ticketDBRepository;
        public TicketsController(TicketDBRepository ticketDBRepository, FlightDbRepository flightDbRepository) { 
            _ticketDBRepository = ticketDBRepository;
            _flightDbRepository = flightDbRepository;
        }


        /* Method (and View) which returns and shows on page a list of Flights that one can choose with RETAIL prices displayed.
         * Requirements [1.5] */
        public IActionResult ListFlights()
        {
            /* 
                This is to return a web page which displays flights that can be booked
                    a.) Fully booked flights CAN NOT be selected, BUT still displayed 
                    b.) If Departure date is in the past, DO NOT DISPLAY
            */
            IQueryable<Flight> list = _flightDbRepository.GetFlights().Where(x=> x.DepartureDate > DateTime.Now);

            var output = from flight in list
                         select new ListFlightViewModel()
                         {
                            Id = flight.Id,
                            AvailableSeats = flight.AvailableSeats,
                            DepartureDate = flight.DepartureDate,
                            ArrivalDate = flight.ArrivalDate,
                            CountryFrom = flight.CountryFrom,
                            CountryTo = flight.CountryTo,
                            RetailPrice = flight.WholeSalePrice + (flight.WholeSalePrice * (flight.ComissionRate / 100)) //((comission% / 100) * wholesalePrice) + wholesalePrice = Retail price
                         };

            return View(output);
        }

        /* Method (and View) which allows the user to book a flight after entering the requested details to book a ticket. 
         * Requirements [2]*/
        [HttpGet]
        public IActionResult BookFlight()
        {
            //Load the page with the empty fields
            BookViewModel myModel = new BookViewModel(_flightDbRepository);
            return View(myModel);
        }

        [HttpPost]
        public IActionResult BookFlight(BookViewModel myModel)
        {
            /* Allows the user to book a flight after entering their details.
             *      a.) Flight must NOT be fully booked 
             *      c.) Flight must NOT be cancelled
             *      b.) Flight must NOT be in the past
             *      c.) PricePaid is filled in automatically after calculating commission on WholeSalePrice */
            _ticketDBRepository.Book(new Ticket()
            {
                Row = myModel.Row,
                Column = myModel.Column,
                FlightIdFK = myModel.FlightIdFK,
                PricePaid = myModel.PricePaid,
                Cancelled = false
            }) ;

            return View(myModel);
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
