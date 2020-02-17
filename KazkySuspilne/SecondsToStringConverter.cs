using System;
using System.Globalization;

namespace KazkySuspilne
{
    public class SecondsToStringConverter : MvvmCross.Converters.MvxValueConverter<double, string>
    {
        private const double SecondsPerMinute = 60;

        public SecondsToStringConverter()
        {
        }

        protected override string Convert(double value, Type targetType, object parameter, CultureInfo culture)
        {
            var minutes = Math.Floor(value / SecondsPerMinute);
            var seconds = Math.Floor(value % SecondsPerMinute);

            return $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        }
    }
}
