using System;
using System.Windows.Data;
using System.Windows.Media;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorFondoColorEstado : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                Seguimiento seguimiento = (Seguimiento)value;

                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);

                double t = seguimiento.frlDiferencia - ts.TotalMinutes;

                if (t >= 0)
                {
                    return (Brush)App.Current.Resources["GreenBackgroundBrush"];
                }
                else
                {
                    t = Math.Abs(t);

                    if (seguimiento.ftyEstAlerta != 0 && seguimiento.ftyEstReporte != 0)
                    {
                        return (t > seguimiento.ftyEstAlerta ? (Brush)App.Current.Resources["RedBackgroundBrush"] : (Brush)App.Current.Resources["YellowBackgroundBrush"]);
                    }
                    else
                    {
                        return (t > seguimiento.ftyMinutosAlerta ? (Brush)App.Current.Resources["RedBackgroundBrush"] : (Brush)App.Current.Resources["YellowBackgroundBrush"]);
                    }
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
