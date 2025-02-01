using System.ComponentModel.DataAnnotations;
using MyCinema.ViewModels;

namespace MyCinema.Attributes
{
    public class TicketCountValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (SelectTicketViewModel)validationContext.ObjectInstance;
            int totalTickets = model.RegularTicketCount + model.VipTicketCount;

            if (totalTickets > 8)
            {
                return new ValidationResult("Total number of tickets cannot exceed 8.");
            }

            return ValidationResult.Success;
        }
    }
}
