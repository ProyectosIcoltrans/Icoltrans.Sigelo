using System;
using System.Text;
using System.Windows.Data;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.Editors;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConvertidorCitaEntrega : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XamTextEditor ctrl = parameter as XamTextEditor;
            PuntoEntrega punto = (ctrl.DataContext as DataRecord).DataItem as PuntoEntrega;
            if (punto != null)
            {
                StringBuilder txt = new StringBuilder();

                if (punto.fdtCompromisoCliente.HasValue && punto.fdtCompromisoCliente.Value.Hour != 0)
                    txt.AppendFormat(culture, "{0:t}", punto.fdtCompromisoCliente.Value);

                if (punto.fdtHoraInicial.HasValue)
                {
                    txt.AppendFormat(culture, "{0:t}", punto.fdtHoraInicial.Value);

                    if (punto.fdtHoraFinal.HasValue)
                        txt.AppendFormat(culture, "{0}{1:t}",
                                Mensajes.Separador,
                                punto.fdtHoraFinal.Value);
                }
                return txt.ToString();
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
        #endregion
    }
}
