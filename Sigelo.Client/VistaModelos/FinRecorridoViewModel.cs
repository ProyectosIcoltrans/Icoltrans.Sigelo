
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model del final de recorrido
    /// </summary>
    public sealed class FinRecorridoViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private string nombreCaravana;
        private string ruta;
        private string origen;
        private string destino;
        private string ultimoPunto;
        private string ultimaActualizacion;
        private string tipo;
        private string mensaje;
        private bool habilitarGrabar;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public FinRecorridoViewModel()
        {
            GrabarCommand = new DelegateCommand(Grabar, (e) => true);

            if (!base.IsInDesignMode)
                ObtenerDatosIniciales();
        }
        #endregion

        #region Propiedades
        public ICommand GrabarCommand { get; private set; }
        public string NombreCaravana
        {
            get { return this.nombreCaravana; }
            set
            {
                if (this.nombreCaravana != value)
                {
                    this.nombreCaravana = value;
                    OnPropertyChanged("NombreCaravana");
                }
            }
        }
        public string Ruta
        {
            get { return this.ruta; }
            set
            {
                if (this.ruta != value)
                {
                    this.ruta = value;
                    OnPropertyChanged("Ruta");
                }
            }
        }
        public string Origen
        {
            get { return this.origen; }
            set
            {
                if (this.origen != value)
                {
                    this.origen = value;
                    OnPropertyChanged("Origen");
                }
            }
        }
        public string Destino
        {
            get { return this.destino; }
            set
            {
                if (this.destino != value)
                {
                    this.destino = value;
                    OnPropertyChanged("Destino");
                }
            }
        }
        public string UltimoPunto
        {
            get { return this.ultimoPunto; }
            set
            {
                if (this.ultimoPunto != value)
                {
                    this.ultimoPunto = value;
                    OnPropertyChanged("UltimoPunto");
                }
            }
        }
        public string UltimaActualizacion
        {
            get { return this.ultimaActualizacion; }
            set
            {
                if (this.ultimaActualizacion != value)
                {
                    this.ultimaActualizacion = value;
                    OnPropertyChanged("UltimaActualizacion");
                }
            }
        }
        public string Tipo
        {
            get { return this.tipo; }
            set
            {
                if (this.tipo != value)
                {
                    this.tipo = value;
                    OnPropertyChanged("Tipo");
                }
            }
        }
        public string Mensaje
        {
            get { return this.mensaje; }
            set
            {
                if (this.mensaje != value)
                {
                    this.mensaje = value;
                    OnPropertyChanged("Mensaje");
                }
            }
        }
        public bool HabilitarGrabar
        {
            get { return this.habilitarGrabar; }
            set
            {
                if (this.habilitarGrabar != value)
                {
                    this.habilitarGrabar = value;
                    OnPropertyChanged("HabilitarGrabar");
                }
            }
        }
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void ObtenerDatosIniciales()
        {
            this.EsUltimoReporte();
            //base.MostrarProgreso = true;
            //var caravana = Contexto.CaravanaSeleccionada.Model.Caravana;
            ////if (caravana.fvcUltimoPunto != "FIN DEL RECORRIDO")
            //if (this.EsUltimoReporte())
            //{
            //    NombreCaravana = caravana.fvcDescripcion;
            //    Ruta = caravana.ruta;
            //    Origen = caravana.origen;
            //    Destino = caravana.destino;
            //    UltimoPunto = caravana.fvcUltimoPunto;
            //    UltimaActualizacion = caravana.fdtUltimoPunto.ToString("MMM-dd hh:mm tt", CultureInfo.CurrentCulture);
            //    Tipo = string.Format(CultureInfo.CurrentCulture, "{0:G}", caravana.TipoCaravana);
            //    Mensaje = "¿ Esta seguro de finalizar la siguiente caravana ?";
            //    HabilitarGrabar = true;
            //}
            //else
            //{
            //    NombreCaravana = "N.D.";
            //    Ruta = "N.D.";
            //    Origen = "N.D.";
            //    Destino = "N.D.";
            //    UltimoPunto = "N.D.";
            //    UltimaActualizacion = "N.D.";
            //    Tipo = "N.D.";
            //    Mensaje = "No se encuentra seleccionada ninguna caravana valida";
            //    HabilitarGrabar = false;
            //}
            //base.MostrarProgreso = false;
        }
        private void Grabar(object param)
        {
            MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarFinalizarRecorrido += (s, e) =>
            {
                MostrarProgreso = false;
                base.Cancelar(null);
            };
            ctx.FinalizarRecorrido();
        }


        private bool EsUltimoReporte()
        {
            //MostrarProgreso = true;
            base.MostrarProgreso = true;

            bool esUltimoReporte = true; 
            Contexto ctx = new Contexto(MostarVentaError);
            int? idUltimo = Contexto.CaravanaSeleccionada.Model.Caravana.finUltimoPunto;

            ctx.FinalizarObtenerReportesYEstados += (s, e) =>
            {
                ObservableCollection<SiguientePuntoControl> puntosControl = new ObservableCollection<SiguientePuntoControl>(e.Result.PuntosReporte);

                if (puntosControl.Count > 0)
                {
                    if (idUltimo.HasValue)
                    {
                        var car = puntosControl.LastOrDefault();
                        if (!car.finIdPuntoRuta.Equals(idUltimo))
                        {
                            esUltimoReporte = false;
                        }
                    }
                    else
                    {
                        esUltimoReporte = false;
                    }
                }

                var caravana = Contexto.CaravanaSeleccionada.Model.Caravana;
                //if (caravana.fvcUltimoPunto != "FIN DEL RECORRIDO")
                if (esUltimoReporte || caravana.fvcUltimoPunto == "FIN DEL RECORRIDO")
                {
                    NombreCaravana = caravana.fvcDescripcion;
                    Ruta = caravana.ruta;
                    Origen = caravana.origen;
                    Destino = caravana.destino;
                    UltimoPunto = caravana.fvcUltimoPunto;
                    UltimaActualizacion = caravana.fdtUltimoPunto.ToString("MMM-dd hh:mm tt", CultureInfo.CurrentCulture);
                    Tipo = string.Format(CultureInfo.CurrentCulture, "{0:G}", caravana.TipoCaravana);
                    Mensaje = "¿ Esta seguro de finalizar la siguiente caravana ?";
                    HabilitarGrabar = true;
                }
                else
                {
                    base.MostrarProgreso = false;
                    MessageBox.Show("La caravana no se encuentra en el ultimo punto de reporte.", Mensajes.TituloAlerta, MessageBoxButton.OK, MessageBoxImage.Information);

                    var actual = ((App)App.Current);
                    OpcionesNavegacion opcion = OpcionesNavegacion.Caravanas;
                    if (actual.OpcionAnterior.HasValue)
                    {
                        opcion = actual.OpcionAnterior.Value;
                        actual.OpcionAnterior = null;
                    }
                    actual.Navegar(opcion);

                    NombreCaravana = "N.D.";
                    Ruta = "N.D.";
                    Origen = "N.D.";
                    Destino = "N.D.";
                    UltimoPunto = "N.D.";
                    UltimaActualizacion = "N.D.";
                    Tipo = "N.D.";
                    Mensaje = "No se encuentra seleccionada ninguna caravana valida";
                    HabilitarGrabar = false;
                }
                base.MostrarProgreso = false;
            };

            ctx.ObtenerReportesYEstados();

            return esUltimoReporte;
        }


        #endregion

        #region Eventos
        #endregion
    }
}
