using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.RunnerManagement.Converters
{
    internal class BoolToVisibilityCollapsedConverter : IValueConverter
    {
        public bool Reverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (value is bool b && b) ^ Reverse
            ? Visibility.Visible
            : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new InvalidOperationException();
    }
}
