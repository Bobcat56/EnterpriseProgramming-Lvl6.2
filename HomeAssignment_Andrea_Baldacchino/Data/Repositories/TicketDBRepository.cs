using Data.DataContext;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TicketDBRepository
    {
        private AirlineDbContext _AirLineDBContext;

        //Constructor
        public TicketDBRepository(AirlineDbContext airlineDbContext) {
            //This is using the dependancy injection as to not have multiple instances of airlineDbContext class taking up memory
            _AirLineDBContext = airlineDbContext;
        }

        //Methods
        public void Book(Ticket ticket)
        {
            // A method that allows any client to book a ticket.
            // Double booking a seat is not allowed[1].
            _AirLineDBContext.Tickets.Add(ticket);
            _AirLineDBContext.SaveChanges();
        }

        public void Cancel(Guid id)
        {
            // A method which allows the booked ticket to be cancelled and not deleted.
            // Once cancelled someone else can book the released seat again[1]
            var CanlledTicket = _AirLineDBContext.Tickets.FirstOrDefault(t => t.Id == id);

            //Validation to check if ticket exists
            if (CanlledTicket != null)
            {
                //Set the boolean to false and save
                CanlledTicket.Canelled = true;
                _AirLineDBContext.SaveChanges();
            }


        }

        public IQueryable<Ticket> GetTickets(Guid id)
        {
            // A method which returns all the tickets for a flight selected [1]
            return _AirLineDBContext.Tickets.Where(ticket => ticket.Id == id);
        }
    }
}
