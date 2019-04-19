using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Infragistics.Windows.DataPresenter;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorTipoVehiculo : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            Vehiculo datos = (value as DataRecord).DataItem as Vehiculo;
            if (datos == null) return null;
            Image img = null;
            switch (datos.VinculacionId)
            {
                case 1:
                    img = new Image()
                   {
                       Source = new BitmapImage(new Uri("/Resources/LogoIcoltrans32.png", UriKind.RelativeOrAbsolute)),
                       Width = 28,
                       Height = 32,
                       Margin = new Thickness(3, 0, 3, 0)
                   };
                    return img;

                case 2:
                    img = new Image()
                    {
                        Source = new BitmapImage(new Uri("/Resources/preferencial.png", UriKind.Relative)),
                        Width = 14,
                        Height = 32,
                        Margin = new Thickness(3, 0, 3, 0)
                    };
                    return img;
                default:
                    TextBlock tbl = new TextBlock()
                    {
                        Text = Mensajes.LetraVehiculo,
                        FontFamily = new FontFamily("Webdings"),
                        FontSize = 30
                    };
                    return tbl;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        #endregion
    }
}
