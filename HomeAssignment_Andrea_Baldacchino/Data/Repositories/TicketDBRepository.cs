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

            var flight = _AirLineDBContext.Flights.Find(ticket.FlightIdFK);

            //FlightSeating will save the SeatLocation as a concatinated as follows: 6,3
            var seatLocation = $"{ticket.Row},{ticket.Column}";

            /*Lambda expression does as follows:
             *Find the first entry that mathes the following:
             *FlidghtIdFk == The newly generated tickets FlightIdFK & the seatLocation == tickets newly generated seating*/
            var existingSeat = _AirLineDBContext.FlightSeatings.FirstOrDefault
                (fs => fs.FlightIdFK == ticket.FlightIdFK && fs.SeatLocation == seatLocation);

            //If 
            if (existingSeat != null && existingSeat.BookedSeat)
            {
                // Seat is already booked, exception handling
                throw new InvalidOperationException("Seat is already booked.");
            }


            //If seat does not exist create it.
            if (existingSeat == null)
            {
                var newSeat = new FlightSeating
                {
                    FlightIdFK = ticket.FlightIdFK,
                    TicketIdFK = ticket.Id,
                    BookedSeat = true,
                    SeatLocation = seatLocation
                };

                _AirLineDBContext.FlightSeatings.Add(newSeat);
            }else {
                // Seat exists but is not booked, update the seat
                existingSeat.BookedSeat = true;
                existingSeat.TicketIdFK = ticket.Id;
            }

            _AirLineDBContext.Tickets.Add(ticket);
            // Save changes to the database
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
