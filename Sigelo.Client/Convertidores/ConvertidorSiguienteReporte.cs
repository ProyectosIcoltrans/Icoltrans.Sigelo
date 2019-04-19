using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorSiguienteReporte : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Seguimiento seguimiento = (Seguimiento)value;

            if (seguimiento.fvcUltimoPunto != "FIN DEL RECORRIDO")
            {
                TextBlock txt = new TextBlock()
                {
                    TextTrimming = TextTrimming.WordEllipsis,
                    TextAlignment = TextAlignment.Center,
                    ToolTip = seguimiento.fvcSiguientePunto,
                    Cursor = Cursors.Hand
                };
                txt.Inlines.Add(new Run() { Text = seguimiento.fvcSiguientePunto, FontWeight = FontWeights.Bold });

                if (seguimiento.fdtUltimoPunto.Date > DateTime.Now.Date)
                {
                    txt.Inlines.Add(new LineBreak());
                    txt.Inlines.Add(new Run { Text = seguimiento.fdtSiguientePunto.ToString("MMMM dd", culture).ToUpperInvariant() });
                }
                txt.Inlines.Add(new LineBreak());
                txt.Inlines.Add(new Run { Text = seguimiento.fdtSiguientePunto.ToString("t", culture) });

                return txt;
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
