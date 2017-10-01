using Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace UI.RunnerManagement.Converters
{
    internal class InvalidRunnersErrorMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Runner> runners)
            {
                var sb = new StringBuilder();

                foreach (var r in runners)
                {
                    if (string.IsNullOrWhiteSpace(r.Firstname))
                        sb.AppendLine($"Läufer mit Startnummer {r.Startnumber}: Vorname ist erforderlich.");
                    if (string.IsNullOrWhiteSpace(r.Lastname))
                        sb.AppendLine($"Läufer mit Startnummer {r.Startnumber}: Nachname ist erforderlich.");
                    if (r.YearOfBirth < 1900)
                        sb.AppendLine($"Läufer mit Startnummer {r.Startnumber}: Geburtsjahr ist erforderlich.");
                    if (r.Category == null && r.CategoryId == 0)
                        sb.AppendLine($"Läufer mit Startnummer {r.Startnumber}: Kategorie ist erforderlich.");
                }
                return sb.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
