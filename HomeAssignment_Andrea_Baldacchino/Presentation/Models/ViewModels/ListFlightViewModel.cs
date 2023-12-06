﻿

namespace Presentation.Models.ViewModels
{
    public class ListFlightViewModel
    {
        public Guid Id { get; set; }
        public int AvailableSeats { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string? CountryFrom { get; set; }
        public string? CountryTo { get; set; }
        public double RetailPrice { get; set; }

    }
}
