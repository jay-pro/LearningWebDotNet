using lms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lms.ValidationAttributes
{
    public class Class_EnsureFutureFinishTimeOnCreationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _class = validationContext.ObjectInstance as Class;
            if (_class.FinishTime < DateTime.Now)
            {
                return new ValidationResult("Thời gian kết thúc khóa học phải ở tương lai");
            }
            return ValidationResult.Success;
        }
    }
}
