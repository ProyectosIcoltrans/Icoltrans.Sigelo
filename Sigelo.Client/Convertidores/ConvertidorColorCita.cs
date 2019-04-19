using System;
using System.Windows.Data;
using System.Windows.Media;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Infragistics.Windows.DataPresenter;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorColorCita : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                PuntoEntrega punto = (value as DataRecord).DataItem as PuntoEntrega;
                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                if (punto.fdtCompromisoCliente.HasValue)
                {
                    TimeSpan cita = new TimeSpan(punto.fdtCompromisoCliente.Value.Ticks);
                    double t = cita.TotalMinutes - ts.TotalMinutes;

                    if (t > 60)
                        return (Brush)App.Current.Resources["GreenBrush"];
                    else if (t <= 60 && t >= 30)
                        return (Brush)App.Current.Resources["YellowBrush"];
                    else if (t <= 30)
                        return (Brush)App.Current.Resources["RedBrush"];
                    else
                        return (Brush)App.Current.Resources["RedBrush"];
                }
                else
                {
                    TimeSpan cita = new TimeSpan(punto.fdtHoraFinal.Value.Ticks);
                    double t = cita.TotalMinutes - ts.TotalMinutes;

                    if (t > 60)
                        return (Brush)App.Current.Resources["GreenBrush"];
                    else if (t <= 60 && t >= 30)
                        return (Brush)App.Current.Resources["YellowBrush"];
                    else if (t <= 30)
                        return (Brush)App.Current.Resources["RedBrush"];
                    else
                        return (Brush)App.Current.Resources["RedBrush"];
                }
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
