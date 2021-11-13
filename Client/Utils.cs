using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AcraBackend.Client
{
    public class InvertibleBooleanToVisibilityConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Inverted, NormalHidden, InvertedHidden
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            var param = parameter != null ? (Parameters)Enum.Parse(typeof(Parameters), (string)parameter) : Parameters.Normal;

            switch (param)
            {
                case Parameters.Normal:
                    return boolValue ? Visibility.Visible : Visibility.Collapsed;
                case Parameters.Inverted:
                    return !boolValue ? Visibility.Visible : Visibility.Collapsed;
                case Parameters.NormalHidden:
                    return boolValue ? Visibility.Visible : Visibility.Hidden;
                case Parameters.InvertedHidden:
                    return !boolValue ? Visibility.Visible : Visibility.Hidden;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class InvertibleNullToVisibilityConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Inverted, NormalHidden, InvertedHidden
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var param = parameter != null ? (Parameters)Enum.Parse(typeof(Parameters), (string)parameter) : Parameters.Normal;

            switch (param)
            {
                case Parameters.Normal:
                    return value != null ? Visibility.Visible : Visibility.Collapsed;
                case Parameters.Inverted:
                    return value == null ? Visibility.Visible : Visibility.Collapsed;
                case Parameters.NormalHidden:
                    return value != null ? Visibility.Visible : Visibility.Hidden;
                case Parameters.InvertedHidden:
                    return value == null ? Visibility.Visible : Visibility.Hidden;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class InvertibleNullToBooleanConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Inverted
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var param = parameter != null ? (Parameters)Enum.Parse(typeof(Parameters), (string)parameter) : Parameters.Normal;

            switch (param)
            {
                case Parameters.Normal:
                    return value != null;
                case Parameters.Inverted:
                    return value == null;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
