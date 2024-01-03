using Data.Repositories;
using Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Presentation.Models.ViewModels
{
    public class BookViewModel
    {
        //Ticket properties
        public int Row { get; set; }
        public int Column { get; set; }
        public Guid FlightIdFK { get; set; }// Foreign Key
        //public string? Passport { get; set; }
        public IFormFile Passport {  get; set; }
        [DisplayName("Price")]
        public double PricePaid { get; set; }

        //Flight Properties
        [DisplayName("Departure Date & Time")]
        public DateTime DepartureDate { get; set; }
        [DisplayName("Arrival Date & Time")]
        public DateTime ArrivalDate { get; set; }
        [DisplayName("Country From")]
        public string? CountryFrom { get; set; }
        [DisplayName("Country To")]
        public string? CountryTo { get; set; }
    }
}

