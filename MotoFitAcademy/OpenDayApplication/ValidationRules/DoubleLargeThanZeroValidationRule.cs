using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OpenDayApplication.ValidationRules
{
    public class DoubleLargeThanZeroValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double d;
            bool result = double.TryParse(value.ToString(), out d);
            if (result && d>0)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Please enter correct value.");
        }
    }
}
