using System;
using System.Windows.Data;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorDataItem : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null) throw new ArgumentNullException("parameter");
            
            object valor = null;
            try
            {
                if (value != null)
                    valor = value.GetType().GetProperty(parameter.ToString()).GetValue(value, null);
            }
            catch { }
            return valor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
