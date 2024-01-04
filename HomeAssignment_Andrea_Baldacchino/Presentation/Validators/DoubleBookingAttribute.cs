using Domain.Interfaces;
using Domain.Models;
using Presentation.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Validators
{
    public class DoubleBookingAttribute : ValidationAttribute
    {
        public DoubleBookingAttribute() { }

        public string GetErrorMessage() => $"The seat selected has already been booked";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var bookViewModel = value as BookViewModel;
            if (bookViewModel == null)
            {
                throw new ArgumentException("Attribute not applied on BookViewModel");
            }

            // Service locator pattern to resolve the repository
            var ticketRepository = (ITicketRepository)validationContext.GetService(typeof(ITicketRepository));
            if (ticketRepository == null)
            {
                throw new ArgumentException("TicketRepository not found in service provider");
            }

            // Check if there's already a booked ticket with the same row and column for the given flight
            var isBooked = ticketRepository.IsSeatAlreadyBooked(new Ticket
            {
                Row = bookViewModel.Row,
                Column = bookViewModel.Column,
                FlightIdFK = bookViewModel.FlightIdFK
            });

            if (isBooked)
            {
                // If the seat is already booked, return a validation error
                return new ValidationResult("The seat selected has already been booked.");
            }

            // If no issue, validation is successful
            return ValidationResult.Success;
        }

    }
}
