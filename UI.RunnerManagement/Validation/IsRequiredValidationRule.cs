using Core.Models;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace UI.RunnerManagement.Validation
{
    internal class IsRequiredValidationRule : ValidationRule
    {
        public string Fieldname { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Fieldname == "Geburtsjahr" && value is int year && year < DateTime.Now.Year - 100)
                return new ValidationResult(false, "Geburtsjahr darf nicht länger als 100 Jahre zurück liegen.");

            switch (value)
            {
                case Category c when c is null:
                case string s when string.IsNullOrWhiteSpace(s):
                    return new ValidationResult(false, $"Der Wert{(Fieldname == null ? "" : $" für das Feld {Fieldname}")} darf nicht leer sein.");

                case int i when i <= 0:
                    return new ValidationResult(false, $"Der Wert{(Fieldname == null ? "" : $" für das Feld {Fieldname}")} darf nicht kleiner oder gleich 0 sein.");
            }

            return ValidationResult.ValidResult;
        }

        private bool IsEmpty(Runner runner)
        {
            return string.IsNullOrWhiteSpace(runner.Firstname) &&
                   string.IsNullOrWhiteSpace(runner.Lastname) &&
                   string.IsNullOrWhiteSpace(runner.SportsClub) &&
                   string.IsNullOrWhiteSpace(runner.ChipId) &&
                   runner.Category == null &&
                   runner.YearOfBirth == 0 &&
                   runner.Startnumber == 0;
        }
    }
}
