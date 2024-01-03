using Data.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {

        private FlightDbRepository _flightDbRepository;
        private TicketDBRepository _ticketDBRepository;
        public AdminController(TicketDBRepository ticketDBRepository, FlightDbRepository flightDbRepository)
        {
            _ticketDBRepository = ticketDBRepository;
            _flightDbRepository = flightDbRepository;
        }

        public IActionResult ListAllFlights()
        {


            if (User.Identity.IsAuthenticated == false)
            {
                //Can make a toast which says "Only logged in users can view history of purchased tickets"
                TempData["errorMsg"] = "You must be an Admin in to view this";

                // Return to home (Index page) or other page? 
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", Request) //Was used for error handling testing (Brings up html page displaying error)
            }

            IQueryable<Flight> list = _flightDbRepository.GetFlights();

            var output = from flight in list
            select new ListFlightViewModel()
            {
                Id = flight.Id,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                CountryFrom = flight.CountryFrom,
                CountryTo = flight.CountryTo
            };

            return View(output);
        }


        public IActionResult ListAllTickets(Guid id) 
        {
            if (User.Identity.IsAuthenticated == false)
            {
                //Can make a toast which says "Only logged in users can view history of purchased tickets"
                TempData["errorMsg"] = "You must be an Admin in to view this";

                // Return to home (Index page) or other page? 
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", Request) //Was used for error handling testing (Brings up html page displaying error)
            }

            IQueryable<Ticket> ticketList = _ticketDBRepository.GetTickets(id);

            if (ticketList == null)
            {
                TempData["msg"] = "There are no tickets booked for this flight";
                return RedirectToAction("ListAllFlights");
            }

            var model = from ticket in ticketList
                select new AdminListAllTicketsViewModel()
                {
                    Id = ticket.Id,
                    Row = ticket.Row,
                    Column = ticket.Column,
                    Passport = ticket.Passport,
                    PricePaid = ticket.PricePaid,
                    Cancelled = ticket.Cancelled
                };

            return View (model);
        }
        

    }//Close controller
}
