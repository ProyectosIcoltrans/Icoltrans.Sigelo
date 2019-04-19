using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class ConvertidorUltimoReporte : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Seguimiento seguimiento = (Seguimiento)value;

            TextBlock txt = new TextBlock();

            txt.TextWrapping = TextWrapping.Wrap;
            txt.TextTrimming = TextTrimming.CharacterEllipsis;
            txt.TextAlignment = TextAlignment.Center;
            txt.ToolTip = seguimiento.fvcUltimoPunto;
            if (seguimiento.fvcUltimoPunto != "FIN DEL RECORRIDO") txt.Cursor = Cursors.Hand;

            txt.Inlines.Add(new Run() { Text = seguimiento.fvcUltimoPunto, FontWeight = FontWeights.Bold });
            if (seguimiento.TipoCaravana != TipoCaravana.Nacional && !string.IsNullOrEmpty(seguimiento.ftxObservacion))
            {
                txt.Inlines.Add(new LineBreak());
                txt.ToolTip = seguimiento.ftxObservacion;
                txt.Inlines.Add(new Run { Text = AjustarObservacion(seguimiento.ftxObservacion), FontSize = 9 });
            }

            if (seguimiento.fdtUltimoPunto.Date < DateTime.Now.Date)
            {
                txt.Inlines.Add(new LineBreak());
                txt.Inlines.Add(new Run { Text = seguimiento.fdtUltimoPunto.ToString("MMMM dd", CultureInfo.CurrentCulture).ToUpperInvariant() });
            }

            txt.Inlines.Add(new LineBreak());
            txt.Inlines.Add(new Run { Text = seguimiento.fdtUltimoPunto.ToString("t", CultureInfo.CurrentCulture) });

            return txt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
        private static string AjustarObservacion(string observacion)
        {
            int pos = observacion.IndexOf(Mensajes.SeparadorObservacion, StringComparison.OrdinalIgnoreCase);
            if (pos > -1)
                observacion = observacion.Substring(pos + Mensajes.SeparadorObservacion.Length);
            if (observacion.Length > 20)
                return observacion.Replace("\r\n", " / ").Substring(0, 19) + "...";
            else
                return observacion.Replace("\r\n", " / ");
        }
    }
}