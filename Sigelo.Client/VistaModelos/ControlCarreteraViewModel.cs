using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// ViewModel asosiado al la vista de Control de carretera
    /// </summary>
    public sealed class ControlCarreteraViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private ObservableCollection<SiguientePuntoControl> puntosControl;
        private ObservableCollection<EstadoVehiculo> estados;
        private ObservableCollection<RazonEstado> razones;
        private ObservableCollection<PuntoEntrega2> puntosEntrega;
        private SiguientePuntoControl puntoControl;
        private EstadoVehiculo estado;
        private RazonEstado razon;
        private string observaciones;
        private string seleccionados;
        private byte pasos;
        private bool? seleccionarTodos;
        private string nombreCaravana;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public ControlCarreteraViewModel()
        {
            GrabarCommand = new DelegateCommand(Grabar, (e) => true);
            seleccionarTodos = false;

            if (!base.IsInDesignMode)
                ObtenerDatosIniciales();
        }
        #endregion

        #region Propiedades
        public ICommand GrabarCommand { get; private set; }
        public ObservableCollection<SiguientePuntoControl> PuntosControl
        {
            get { return this.puntosControl; }
            private set
            {
                if (this.puntosControl != value)
                {
                    this.puntosControl = value;
                    OnPropertyChanged("PuntosControl");
                }
            }
        }
        public ObservableCollection<EstadoVehiculo> Estados
        {
            get { return this.estados; }
            private set
            {
                if (this.estados != value)
                {
                    this.estados = value;
                    OnPropertyChanged("Estados");
                }
            }
        }
        public ObservableCollection<RazonEstado> Razones
        {
            get { return this.razones; }
            private set
            {
                if (this.razones != value)
                {
                    this.razones = value;
                    OnPropertyChanged("Razones");
                }
            }
        }
        public ObservableCollection<PuntoEntrega2> PuntosEntrega
        {
            get { return this.puntosEntrega; }
            private set
            {
                if (this.puntosEntrega != value)
                {
                    this.puntosEntrega = value;
                    OnPropertyChanged("PuntosEntrega");
                }
            }
        }
        public SiguientePuntoControl PuntoControl
        {
            get { return this.puntoControl; }
            set
            {
                if (this.puntoControl != value)
                {
                    this.puntoControl = value;
                    OnPropertyChanged("PuntoControl");
                }
            }
        }
        public EstadoVehiculo Estado
        {
            get { return this.estado; }
            set
            {
                if (this.estado != value)
                {
                    this.estado = value;
                    OnPropertyChanged("Estado");
                    ObtenerRazonesEstados();
                }
            }
        }
        public RazonEstado Razon
        {
            get { return this.razon; }
            set
            {
                if (this.razon != value)
                {
                    this.razon = value;
                    OnPropertyChanged("Razon");
                }
            }
        }
        public string Observaciones
        {
            get { return this.observaciones; }
            set
            {
                if (this.observaciones != value)
                {
                    this.observaciones = value;
                    OnPropertyChanged("Observaciones");
                }
            }
        }
        public string Seleccionados
        {
            get { return this.seleccionados; }
            set
            {
                if (this.seleccionados != value)
                {
                    this.seleccionados = value;
                    OnPropertyChanged("Seleccionados");
                }
            }
        }
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
        public bool? SeleccionarTodos
        {
            get
            {
                bool? retorno = null;
                if (puntosEntrega != null && puntosEntrega.Count > 0)
                {
                    retorno = puntosEntrega[0].Seleccionado;
                    if (puntosEntrega.Any(p => p.Seleccionado != retorno.Value))
                        retorno = null;
                }
                return retorno;
            }
            set
            {
                if (value.HasValue && seleccionarTodos != value)
                {
                    seleccionarTodos = value;
                    foreach (var item in puntosEntrega)
                        item.Seleccionado = value.Value;
                    OnPropertyChanged("SeleccionarTodos");
                }
            }
        }
        #endregion

        #region Metodos
        internal void AjustarObservacion()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in puntosEntrega.Where(w => w.Seleccionado))
                sb.AppendLine(item.fvcDescripcion);

            Seleccionados = sb.ToString();
            OnPropertyChanged("SeleccionarTodos");
        }
        #endregion

        #region Funciones
        private void Grabar(object param)
        {
            MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarGrabarReporteCarretera += (s, e) =>
                {
                    MostrarProgreso = false;
                    base.Cancelar(null);
                };
            if (puntoControl != null && razon != null)
            {
                StringBuilder textoFinal = new StringBuilder();
                if (!string.IsNullOrEmpty(observaciones))
                {
                    textoFinal.Append(observaciones);
                    if (!string.IsNullOrEmpty(seleccionados))
                        textoFinal.AppendFormat(CultureInfo.CurrentCulture, Resources.Mensajes.SeparadorObservacion);
                }
                textoFinal.Append(seleccionados);

                ctx.GrabarReporteCarretera(
                    puntoControl.finIdPuntoRuta,
                    estado.fsmIdEstadoVehiculo,
                    (short)razon.fsmIdRazonEstadoV,
                    textoFinal.Length > 0 ? textoFinal.ToString() : null,
                    puntosEntrega != null ? (from w in puntosEntrega where w.Seleccionado select w.fidPuntoEntrega).ToArray() : null
                    );
            }
        }
        private void ObtenerDatosIniciales()
        {
            MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            NombreCaravana = Contexto.CaravanaSeleccionada.Model.Caravana.fvcDescripcion;
            bool siguiente = Contexto.CaravanaSeleccionada.SiguientePunto;
            int? idUltimo = Contexto.CaravanaSeleccionada.Model.Caravana.finUltimoPunto;
            int? idSiguiente = Contexto.CaravanaSeleccionada.Model.Caravana.finSiguientePunto;

            ctx.FinalizarObtenerReportesYEstados += (s, e) =>
                {
                    PuntosControl = new ObservableCollection<SiguientePuntoControl>(e.Result.PuntosReporte);
                    Estados = new ObservableCollection<EstadoVehiculo>(e.Result.Estados);

                    if (puntosControl.Count > 0)
                    {
                        if (idUltimo.HasValue && !siguiente)
                            PuntoControl = puntosControl.FirstOrDefault(w => w.finIdPuntoRuta == idUltimo);
                        else if (idSiguiente.HasValue && siguiente)
                            PuntoControl = puntosControl.FirstOrDefault(w => w.finIdPuntoRuta == idSiguiente);
                        else if (puntoControl == null)
                            PuntoControl = puntosControl.FirstOrDefault();
                    }

                    Estado = Estados.FirstOrDefault(w => w.fvcDescripcion == Contexto.CaravanaSeleccionada.Model.Caravana.fvcEstado);
                    if (estado == null)
                        pasos++;
                    VerificarCerrarCargando();
                };
            ctx.ObtenerReportesYEstados();

            if (Contexto.CaravanaSeleccionada.Model.Caravana.TipoCaravana != TipoCaravana.Nacional)
            {
                ctx.FinalizarObtenerPuntosEntregaControl += (s, e) =>
                {
                    PuntosEntrega = new ObservableCollection<PuntoEntrega2>(e.Result.PuntosEntrega);
                    foreach (var item in PuntosEntrega)
                        item.Contenedor = this;

                    VerificarCerrarCargando();
                };
                ctx.ObtenerPuntosEntregaControl();
            }
            else
                pasos++;
        }
        private void ObtenerRazonesEstados()
        {
            if (estado == null)
            {
                VerificarCerrarCargando();
                return;
            }
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerRazonesEstado += (s, e) =>
            {
                Razones = new ObservableCollection<RazonEstado>(e.Result);
                Razon = Razones[0];
                VerificarCerrarCargando();
            };
            ctx.ObtenerRazonesEstado(estado.fsmIdEstadoVehiculo);
        }
        private void VerificarCerrarCargando()
        {
            pasos++;
            if (pasos >= 3) MostrarProgreso = false;
        }
        #endregion
    }
}
