using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenDayApplication.ValidationRules
{
    public class StringPropertyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value != null && value != string.Empty && Regex.IsMatch((string)value, @"^[a-zA-Z]+$"))
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Please enter not null value.");
        }
    }
}
