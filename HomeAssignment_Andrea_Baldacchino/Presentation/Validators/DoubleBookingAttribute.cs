using Domain.Interfaces;
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
            var categoryInputBYTheUser = (int)value;

            var categoriesRepository = (categoriesRepository)validationContext.GetService(typeof(categoriesRepository));
            var minCarId = categoriesRepository.GetCategories().Min(x => x.Id);
            var minCarId = categoriesRepository.GetCategories().Max(x => x.Id);

            if (categoryInputBYTheUser <minCategoryId || categoryInputBYTheUser > maxCategoryId)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;

        }
            


    }
}
