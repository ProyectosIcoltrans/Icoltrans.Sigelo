using System;
using System.Windows;
using System.Windows.Data;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Infragistics.Windows.DataPresenter;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorPrioridad : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            Vehiculo datos = (value as DataRecord).DataItem as Vehiculo;

            if (datos != null && datos.Prioridad)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        #endregion
    }
}
