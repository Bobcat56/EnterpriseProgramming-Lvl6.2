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
            // A method that allows any client to book a ticket. Double booking a seat is not allowed[1].
            using (var transaction = _AirLineDBContext.Database.BeginTransaction())
            {
                try
                {
                    var seatLocation = $"{ticket.Row},{ticket.Column}"; //FlightSeating will save the SeatLocation as a concatinated as follows: 6,3
                    /*Lambda expression does as follows: Find the first entry that matches the following:
                     *FlidghtIdFk == The newly generated tickets FlightIdFK & the seatLocation == tickets newly generated seating*/
                    var existingSeat = _AirLineDBContext.FlightSeatings.FirstOrDefault
                        (fs => fs.FlightIdFK == ticket.FlightIdFK && fs.SeatLocation == seatLocation);

                    if (existingSeat != null && existingSeat.BookedSeat == true) {
                        throw new InvalidOperationException("Seat is already booked.");
                    }

                    if (existingSeat == null) {
                        var newSeat = new FlightSeating
                        {
                            FlightIdFK = ticket.FlightIdFK,
                            TicketIdFK = ticket.Id,
                            BookedSeat = true,
                            SeatLocation = seatLocation
                        };

                        _AirLineDBContext.FlightSeatings.Add(newSeat);
                    }else {// If seat exists but is not booked, update the seat
                        existingSeat.BookedSeat = true;
                        existingSeat.TicketIdFK = ticket.Id;
                    }

                    var availableSeating = _AirLineDBContext.Flights.FirstOrDefault(f => f.Id == ticket.FlightIdFK);

                    // Check if there are available seats before decrementing
                    if (availableSeating != null && availableSeating.AvailableSeats > 0) {
                        availableSeating.AvailableSeats--;
                    }else {
                        // Handle the case when there are no available seats
                        throw new InvalidOperationException("Error: No available seats for booking.");
                    }

                    _AirLineDBContext.Tickets.Add(ticket);
                    _AirLineDBContext.SaveChanges();

                    transaction.Commit();
                }catch (Exception ex) { //This way, no errors will effect the database
                    transaction.Rollback();
                    throw;
                }//Close Try Catch
            }//Close transaction
        }//Close Book()

        public void Cancel(Guid id)
        {
            // A method which allows the booked ticket to be cancelled and not deleted.
            // Once cancelled someone else can book the released seat again[1]
            var CanlledTicket = _AirLineDBContext.Tickets.FirstOrDefault
                (t => t.Id == id);

            //Validation to check if ticket exists
            if (CanlledTicket != null)
            {
                //Mark the ticket as cancelled
                CanlledTicket.Cancelled = true;

                //Expression means: Find the first that matches: TicketIdFK from table FligthSeating
                var releaseSeat = _AirLineDBContext.FlightSeatings.FirstOrDefault
                    (fs => fs.TicketIdFK == id);                

                if (releaseSeat != null)
                {
                    //Remove the connection between this ticket and the seat, and free up the seat
                    releaseSeat.TicketIdFK = null;
                    releaseSeat.BookedSeat = false;

                    var availableSeating = _AirLineDBContext.Flights.FirstOrDefault
                        (f => f.Id == id);
                    availableSeating.AvailableSeats++;
                }
                else {
                    throw new InvalidOperationException("Error: Seat not found");
                }

                    _AirLineDBContext.SaveChanges();
            }else {
                throw new InvalidOperationException("This ticket does not exist");
            }
        }

        public IQueryable<Ticket> GetTickets(Guid id)
        {
            // A method which returns all the tickets for a flight selected [1]
            return _AirLineDBContext.Tickets.Where(ticket => ticket.Id == id);
        }
    }
}
