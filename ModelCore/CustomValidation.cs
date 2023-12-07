using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelCore
{
    public class CustomValidation : ValidationAttribute
    {
        protected override ValidationResult?  IsValid(object? value, ValidationContext validationContext)
        {
            var product = (Product)validationContext.ObjectInstance;
            if (product.Price > 200)
            {
                // product.CreatedDate.Value.Year;
                if (product?.CreatedDate.Value > DateTime.Now)
                {
                    return new ValidationResult("Mast be less then today");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("Mast be greate then 200");
            }

        }
    }
}
