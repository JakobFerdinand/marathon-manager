using Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace UI.RunnerManagement.Converters
{
    public class StartnumberNotUnicErrorMessageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is bool isValid && isValid)
                return string.Empty;

            var runners = values[1] as IEnumerable<Runner>;
            if (runners is null)
                return string.Empty;

            var errorMessageStringBuilder = new StringBuilder();
            errorMessageStringBuilder.AppendLine("Die Startnummern müssen eindeutig sein!");
            errorMessageStringBuilder.AppendLine("Folgende Läufer haben die gleiche Startnummer: ");

            var startNummbers = runners.Select(r => r.Startnumber);
            startNummbers = startNummbers.Where(s => runners.Count(r => r.Startnumber == s) > 1).Distinct();

            foreach (var startNumber in startNummbers)
            {
                var incorrectRunners = runners.Where(r => r.Startnumber == startNumber);
                foreach(var r in incorrectRunners)
                    errorMessageStringBuilder.AppendLine($" - Startnummer: {r.Startnumber, 3} | {r.Firstname} {r.Lastname}");
            }

            return errorMessageStringBuilder.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
