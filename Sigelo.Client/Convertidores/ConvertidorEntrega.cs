using System;
using System.Windows.Data;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorEntrega : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            string fdtEntrega = value.ToString();

            if (!string.IsNullOrEmpty(fdtEntrega))
                return "a";
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
