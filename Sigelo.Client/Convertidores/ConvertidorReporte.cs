using System;
using System.Windows.Data;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorReporte : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime reporte = (DateTime)value;
            string txt = reporte.ToString("t", culture);
            if (reporte.Date < DateTime.Now.Date)
                txt = string.Format(culture, "{0}\r\n{1:m}", txt, reporte);
            return txt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        #endregion
    }
}
