using Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace UI.RunnerManagement.Converters
{
    public class ChipIdNotUnicErrorMessageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(!IsErrorMessageNeeded(values))
                return string.Empty;

            var runners = values[1] as IEnumerable<Runner>;

            var errorMessageStringBuilder = new StringBuilder();
            errorMessageStringBuilder.AppendLine("Die Chip Ids müssen eindeutig sein!");
            errorMessageStringBuilder.AppendLine("Folgende Läufer haben die gleiche Chip Id: ");

            var chipIds = runners.Where(r => !string.IsNullOrWhiteSpace(r.ChipId)).Select(r => r.ChipId);
            chipIds = chipIds.Where(c => runners.Count(r => r.ChipId == c) > 1).Distinct();

            foreach (var chipId in chipIds)
            {
                var incorrectRunners = runners.Where(r => r.ChipId == chipId);
                foreach (var r in incorrectRunners)
                    errorMessageStringBuilder.AppendLine($" - Startnummer: {r.Startnumber,3} | ChipId: {r.ChipId} | {r.Firstname} {r.Lastname}");
            }

            return errorMessageStringBuilder.ToString();
        }

        internal bool IsErrorMessageNeeded(object[] values)
        {
            if (values[0] is bool isValid && isValid)
                return false;

            var runners = values[1] as IEnumerable<Runner>;
            if (runners is null || runners.Count() == 0)
                return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
