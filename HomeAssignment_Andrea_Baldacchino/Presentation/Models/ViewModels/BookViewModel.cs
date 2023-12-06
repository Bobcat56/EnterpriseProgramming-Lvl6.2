using Data.Repositories;
using Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel(FlightDbRepository flightDbRepository) { 
            //Populating the flights 
            Flights = flightDbRepository.GetFlights();

        }
        
        public int Row { get; set; }
        public int Column { get; set; }
        public IQueryable<Flight> Flights { get; set; }
        public Guid FlightIdFK { get; set; }// Foreign Key
        public string? Passport { get; set; }
        [DisplayName("Price")]
        public double PricePaid { get; set; }

    }
}

