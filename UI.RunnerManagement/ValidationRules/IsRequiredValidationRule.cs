using Core.Models;
using System.Globalization;
using System.Windows.Controls;

namespace UI.RunnerManagement.ValidationRules
{
    internal class IsRequiredValidationRule : ValidationRule
    {
        public string Fieldname { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            switch (value)
            {
                case Category c when c is null:
                case string s when string.IsNullOrWhiteSpace(s):
                    return new ValidationResult(false, $"Der Wert{(Fieldname == null ? "" : $"für das Feld {Fieldname}")} darf nicht leer sein.");

                case int i when i <= 0:
                    return new ValidationResult(false, $"Der Wert{(Fieldname == null ? "" : $"für das Feld {Fieldname}")} darf nicht kleiner oder gleich 0 sein.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
