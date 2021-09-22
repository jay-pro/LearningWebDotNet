using lms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lms.ValidationAttributes
{
    public class Class_EnsureFinishTimeAfterStartTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _class = validationContext.ObjectInstance as Class;
            if (_class.FinishTime < _class.StartTime)
            {
                return new ValidationResult("Thời gian kết thúc phải lớn hơn thời gian bắt đầu");
            }
            return ValidationResult.Success;
        }
    }
}
