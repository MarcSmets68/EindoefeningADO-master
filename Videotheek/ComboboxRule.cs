using System.Windows.Controls;
using System.Globalization;

namespace Videotheek
{
    public class ComboboxRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Genre moet gekozen worden");
            }

            return ValidationResult.ValidResult;
        }
    }
}
