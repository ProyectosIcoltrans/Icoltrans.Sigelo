using System;
using System.Linq;
using System.Windows.Data;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorAutorizacion : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //bool respuesta = false;
            //if (parameter != null && Contexto.ParametrosIniciales!=null)
            //{
            //    try
            //    {
            //        Guid idOpcion = new Guid(parameter.ToString());
            //        respuesta = Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == idOpcion);
            //    }
            //    catch { }
            //}
            //return respuesta;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
