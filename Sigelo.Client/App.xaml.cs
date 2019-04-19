using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Type vistaAnterior;
        /// <summary>
        /// En general es <c>null</c> solo se llena para indicar que una pagina fue llamada
        /// desde una opción diferente a la pantalla principal.
        /// </summary>
        internal OpcionesNavegacion? OpcionAnterior { get; set; }
        internal IFiltrosCaravana EstadoFiltros { get; set; }
        internal void Navegar(OpcionesNavegacion opcion)
        {
            if (
                opcion == OpcionesNavegacion.Escoltas ||
                opcion == OpcionesNavegacion.FinRecorrido ||
                opcion == OpcionesNavegacion.CambiarCaravana ||
                opcion == OpcionesNavegacion.Refuerzos
                )
            {
                if (Contexto.CaravanaSeleccionada == null)
                {
                    MessageBox.Show(Mensajes.SeleccionarCaravana, Mensajes.TituloAlerta, MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            Dictionary<OpcionesNavegacion, Uri> listaOpciones = new Dictionary<OpcionesNavegacion, Uri>();
            listaOpciones.Add(OpcionesNavegacion.Caravanas, new Uri("/Vistas/CaravanasView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.CrearCaravana, new Uri("/Vistas/CrearCaravanaView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.Escoltas, new Uri("/Vistas/EscoltasView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.FinRecorrido, new Uri("/Vistas/FinRecorridoView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.InicioRecorrido, new Uri("/Vistas/InicioRecorridoView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.Refuerzos, new Uri("/Vistas/RefuerzosView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.ControlCarretera, new Uri("/Vistas/ControlCarreteraView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.CambiarCaravana, new Uri("/Vistas/CambiarCaravanaView.xaml", UriKind.RelativeOrAbsolute));
            listaOpciones.Add(OpcionesNavegacion.AsignarSatelital, new Uri("/Vistas/SatelitalView.xaml", UriKind.RelativeOrAbsolute));

            NavigationService servicio = null;
            try
            {
                servicio = ((NavegationView)this.MainWindow).NavigationService;
                servicio.Navigated += (s, e) =>
                    {
                        if (opcion == OpcionesNavegacion.Caravanas)
                        {
                            CaravanasView caravana = e.Content as CaravanasView;
                            if (caravana != null)
                            {
                                CaravanasViewModel vm = caravana.DataContext as CaravanasViewModel;
                                vm.TieneFoco(true);
                            }
                        }
                    };
                try
                {
                    if (opcion == OpcionesNavegacion.Caravanas)
                        if (servicio.CanGoBack && this.vistaAnterior.Equals(typeof(CaravanasView)))
                            servicio.GoBack();
                        else
                            servicio.Navigate(listaOpciones[OpcionesNavegacion.Caravanas]);
                    else
                    {
                        CaravanasView caravana = servicio.Content as CaravanasView;
                        if (caravana != null)
                        {
                            CaravanasViewModel vm = caravana.DataContext as CaravanasViewModel;
                            vm.TieneFoco(false);
                        }
                        servicio.Navigate(listaOpciones[opcion]);
                    }
                    // Solo se puede asignar al final.  No cambiar de posición.
                    this.vistaAnterior = servicio.Content.GetType();
                }
                catch
                {
                    servicio.Navigate(listaOpciones[OpcionesNavegacion.ControlCarretera]);
                }
            }
            catch (Exception)
            {
                SmartDispatcher.BeginInvoke(() => Navegar(opcion));
            }
        }
        internal static List<PuntoEntrega> TranformarPuntosEntrega(PuntoEntrega[] puntos)
        {
            List<PuntoEntrega> temp = new List<PuntoEntrega>();
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
            temp.AddRange(
                                    from w in puntos
                                    where w.fidOperacion != null && !w.fdtEntrega.HasValue && w.fdtCompromisoCliente != null
                                    orderby new TimeSpan(w.fdtCompromisoCliente.Value.Ticks).TotalMinutes - ts.TotalMinutes
                                    select w
                );
            temp.AddRange(
                                    from w in puntos
                                    where w.fidOperacion != null && !w.fdtEntrega.HasValue && w.fdtCompromisoCliente == null
                                    select w
                );

            temp.AddRange(
                                    from w in puntos
                                    where w.fidOperacion != null && w.fdtEntrega.HasValue
                                    orderby w.fdtEntrega
                                    select w
                );
            return temp;
        }
    }
}
