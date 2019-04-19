namespace Icoltrans.Sigelo.Win.Vehiculos
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    public class ConvertidorVisibleVehiculoPorSalir : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Guid? caravana = (Guid?)value;
            return !caravana.HasValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
