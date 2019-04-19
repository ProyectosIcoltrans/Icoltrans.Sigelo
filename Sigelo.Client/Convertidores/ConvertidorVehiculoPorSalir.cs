using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorVehiculoPorSalir : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Guid? caravana = (Guid?)value;

            if (caravana.HasValue)
                return new SolidColorBrush(Colors.Red);
            else
                return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
