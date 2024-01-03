using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
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


        /* Method (and View) which returns and shows on page a list of Flights that one can choose with RETAIL prices displayed.*/
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
                            RetailPrice = flight.WholeSalePrice + (flight.WholeSalePrice * (flight.ComissionRate / 100)), //((comission% / 100) * wholesalePrice) + wholesalePrice = Retail price
                            CanBook = flight.AvailableSeats > 0 //To remove the ability to book a fully booked flight
                         };

            return View(output);
        }

        /* Method (and View) which allows the user to book a flight after entering the requested details to book a ticket.*/
        [HttpGet]
        public IActionResult BookFlight(Guid Id)
        {
            //Fetch the flight the user wanted to book
            var flight = _flightDbRepository.GetFlight(Id);

            /*      a.) Flight must NOT be fully booked 
             *      c.) Flight must NOT be cancelled
             *      b.) Flight must NOT be in the past
             *      c.) PricePaid is filled in automatically after calculating commission on WholeSalePrice */
            if (flight == null || flight.AvailableSeats <= 0 || flight.CancelledFlight || flight.DepartureDate <= DateTime.Now) 
            {
                TempData["errorMsg"] = "Error: Can't book flight";
                return RedirectToAction("ListFlights");
            }
            //ViewBags helps pass data to the view
            ViewBag.MaxRows = flight.Rows;
            ViewBag.MaxColumns = flight.Columns;
            
            //Prefill the price and allocate the FK
            BookViewModel model = new BookViewModel()
            {
                //Ticket details
                FlightIdFK = flight.Id,
                PricePaid = flight.WholeSalePrice + (flight.WholeSalePrice * (flight.ComissionRate / 100)), // Automatically fill in the PricePaid with the calculated retail price

                //Flight Details
                CountryFrom = flight.CountryFrom,
                CountryTo = flight.CountryTo,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate
            };
            
            return View(model);
        }

        [HttpPost]
        public IActionResult BookFlight(BookViewModel myModel, [FromServices] IWebHostEnvironment host)
        {

            string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(myModel.Passport.FileName);
            string absolutePath = host.ContentRootPath +  @"\Data\Images\" + fileName;
            string relativePath = @"/Images/" + fileName;

            using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew))
            {
                myModel.Passport.CopyTo(fs);
                fs.Flush();
                fs.Close();
            }

            if (ModelState.IsValid) { 
                // Allows the user to book a flight after entering their details.
                _ticketDBRepository.Book(new Ticket()
                {
                    Row = myModel.Row,
                    Column = myModel.Column,
                    FlightIdFK = myModel.FlightIdFK,
                    PricePaid = myModel.PricePaid,
                    Passport = relativePath,
                    Cancelled = false
                }) ;
                TempData["msg"] = "Flight successfully booked";
                return RedirectToAction("ListFlights");
            }
            TempData["errorMsg"] = "Error encountered while booking flight";
            //If it encounters an error
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
