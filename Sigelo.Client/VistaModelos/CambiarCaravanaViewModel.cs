
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model de Cambiar Caravanas
    /// </summary>
    public sealed class CambiarCaravanaViewModel : BaseOpcionesViewModel
    {
        enum Operacion
        {
            Nada,
            Ruta,
            Dividir,
            Unir,
            Carnet,
            Nombre
        }

        #region Variables
        private Ruta rutaSeleccionada;
        private CaravanaDisponible caravanaDestino;
        private VehiculoPorSalir vehiculoCambio;
        private ObservableCollection<PuntoEntrega> carga;
        private ObservableCollection<Ruta> rutas;
        private ObservableCollection<CaravanaDisponible> caravanasDisponibles;
        private ObservableCollection<VehiculoPorSalir> vehiculosCambio;
        private bool habilitarGrabar;
        private Operacion operacionActual;
        private TipoCaravana tipoCaravanaSeleccionada;
        #endregion

        #region Constructor / Destructor
        public CambiarCaravanaViewModel()
        {
            GrabarCommand = new DelegateCommand(Grabar, (e) => true);
            CambiarRutaCommand = new DelegateCommand(CambiarRuta, (e) => true);
            DividirCommand = new DelegateCommand(Dividir, (e) => true);
            UnirCommand = new DelegateCommand(Unir, (e) => true);
            CambiarCarnetCommand = new DelegateCommand(CambiarCarnet, (e) => true);
            CambiarNombreCommand = new DelegateCommand(CambiarNombre, (e) => true);
            CambioNombre = new CambioNombreViewModel(true);
            operacionActual = Operacion.Nada;
            if (!IsInDesignMode)
                ObtenerDatosIniciales();
        }
        #endregion

        #region Propiedades
        public ICommand GrabarCommand { get; private set; }
        public ICommand CambiarRutaCommand { get; private set; }
        public ICommand DividirCommand { get; private set; }
        public ICommand UnirCommand { get; private set; }
        public ICommand CambiarCarnetCommand { get; private set; }
        public ICommand CambiarNombreCommand { get; private set; }
        public bool NacionalSeleccionada
        {
            get { return this.tipoCaravanaSeleccionada == TipoCaravana.Nacional; }
            set
            {
                if (this.tipoCaravanaSeleccionada != TipoCaravana.Nacional)
                {
                    if (value)
                    {
                        this.tipoCaravanaSeleccionada = TipoCaravana.Nacional;
                        ObtenerListaRutas();
                    }

                    OnPropertyChanged("NacionalSeleccionada");
                    OnPropertyChanged("RegionalSeleccionada");
                    OnPropertyChanged("UrbanaSeleccionada");
                }
            }
        }
        public bool RegionalSeleccionada
        {
            get { return this.tipoCaravanaSeleccionada == TipoCaravana.Regional; }
            set
            {
                if (this.tipoCaravanaSeleccionada != TipoCaravana.Regional)
                {
                    if (value)
                    {
                        this.tipoCaravanaSeleccionada = TipoCaravana.Regional;
                        ObtenerListaRutas();
                    }
                    OnPropertyChanged("NacionalSeleccionada");
                    OnPropertyChanged("RegionalSeleccionada");
                    OnPropertyChanged("UrbanaSeleccionada");
                }
            }
        }
        public bool UrbanaSeleccionada
        {
            get { return this.tipoCaravanaSeleccionada == TipoCaravana.Urbana; }
            set
            {
                if (this.tipoCaravanaSeleccionada != TipoCaravana.Urbana)
                {
                    if (value)
                    {
                        this.tipoCaravanaSeleccionada = TipoCaravana.Urbana;
                        ObtenerListaRutas();
                    }

                    OnPropertyChanged("NacionalSeleccionada");
                    OnPropertyChanged("RegionalSeleccionada");
                    OnPropertyChanged("UrbanaSeleccionada");
                }
            }
        }
        public bool HabilitarGrabar
        {
            get { return habilitarGrabar; }
            set
            {
                if (habilitarGrabar != value)
                {
                    habilitarGrabar = value;
                    OnPropertyChanged("HabilitarGrabar");
                }
            }
        }
        public bool HabilitarCambiarCarnet
        {
            get
            {
                //
                // Solo descarngando mercancia y propios de la compañía
                //
                return (operacionActual == Operacion.Nada || operacionActual == Operacion.Carnet) &&
                    CaravanaSeleccionada.Model.Caravana.fvcEstado.Equals("DESCARGANDO MERCANCIA", StringComparison.OrdinalIgnoreCase)
                && CaravanaSeleccionada.Model.Vehiculos.Any(p => p.VinculacionId == 1);
            }
        }
        public bool HabilitarCambiarRuta
        {
            get
            {
                return Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == new Guid("3C350273-41D7-44F8-81F4-CC45D8ED1EFC")) &&
                    (operacionActual == Operacion.Nada || operacionActual == Operacion.Ruta);
            }
        }
        public bool HabilitarDividir
        {
            get
            {
                if (!CaravanaSeleccionada.Model.HasVehiculos)
                    return false;

                return Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == new Guid("3C350273-41D7-44F8-81F4-CC45D8ED1EFC")) &&
                    (operacionActual == Operacion.Nada || operacionActual == Operacion.Dividir) &&
                    CaravanaSeleccionada.Model.Vehiculos.Length > 1;
            }
        }
        public bool HabilitarUnir
        {
            get
            {
                return Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == new Guid("3C350273-41D7-44F8-81F4-CC45D8ED1EFC")) &&
                    (operacionActual == Operacion.Nada || operacionActual == Operacion.Unir);
            }
        }
        public bool HabilitarCambiaNombre
        {
            get
            {
                return Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == new Guid("B381B7EA-F3AF-50B6-BC86-BF12B7A0714F")) &&
                    (operacionActual == Operacion.Nada || operacionActual == Operacion.Nombre);
            }
        }
        public bool MostrarRuta
        {
            get { return operacionActual == Operacion.Ruta || operacionActual == Operacion.Dividir; }
        }
        public bool MostrarCambiarNombre { get { return operacionActual == Operacion.Nombre || operacionActual == Operacion.Dividir; } }
        public bool MostrarListaCaravanas { get { return operacionActual == Operacion.Unir; } }
        public bool MostrarListaCarnets { get { return operacionActual == Operacion.Carnet; } }
        public string MensajeOperacion
        {
            get
            {
                switch (operacionActual)
                {
                    case Operacion.Ruta:
                        return Mensajes.CambioRuta;
                    case Operacion.Dividir:
                        return Mensajes.DividirCaravana;
                    case Operacion.Unir:
                        return Mensajes.UnirCaravana;
                    case Operacion.Carnet:
                        return Mensajes.CanbioCarnet;
                    case Operacion.Nombre:
                        return Mensajes.CambioNombre;
                    default:
                        return string.Empty;
                }
            }
        }
        public CambioNombreViewModel CambioNombre { get; private set; }
        public SeguimientoViewModel CaravanaSeleccionada { get; private set; }
        public CaravanaDisponible CaravanaDestino
        {
            get { return this.caravanaDestino; }
            set
            {
                if (this.caravanaDestino != value)
                {
                    this.caravanaDestino = value;
                    OnPropertyChanged("CaravanaDestino");
                }
            }
        }
        public VehiculoPorSalir VehiculoCambio
        {
            get { return this.vehiculoCambio; }
            set
            {
                if (this.vehiculoCambio != value)
                {
                    this.vehiculoCambio = value;
                    OnPropertyChanged("VehiculoCambio");
                }
            }
        }
        public Ruta RutaSeleccionada
        {
            get { return this.rutaSeleccionada; }
            set
            {
                if (this.rutaSeleccionada != value)
                {
                    this.rutaSeleccionada = value;
                    OnPropertyChanged("RutaSeleccionada");
                }
            }
        }
        public ObservableCollection<PuntoEntrega> Carga
        {
            get { return this.carga; }
            private set
            {
                if (this.carga != value)
                {
                    this.carga = value;
                    OnPropertyChanged("Carga");
                }
            }
        }
        public ObservableCollection<Ruta> Rutas
        {
            get { return this.rutas; }
            private set
            {
                if (this.rutas != value)
                {
                    this.rutas = value;
                    OnPropertyChanged("Rutas");
                }
            }
        }
        public ObservableCollection<CaravanaDisponible> CaravanasDisponibles
        {
            get { return this.caravanasDisponibles; }
            private set
            {
                if (this.caravanasDisponibles != value)
                {
                    this.caravanasDisponibles = value;
                    OnPropertyChanged("CaravanasDisponibles");
                }
            }
        }
        public ObservableCollection<VehiculoPorSalir> VehiculosCambio
        {
            get { return this.vehiculosCambio; }
            set
            {
                if (this.vehiculosCambio != value)
                {
                    this.vehiculosCambio = value;
                    OnPropertyChanged("VehiculosCambio");
                }
            }
        }
        #endregion

        #region Funciones
        private void ObtenerDatosIniciales()
        {
            CaravanaSeleccionada = Contexto.CaravanaSeleccionada;

            tipoCaravanaSeleccionada = CaravanaSeleccionada.Model.Caravana.TipoCaravana;

            Contexto ctx = new Contexto(base.MostarVentaError);
            if (!CaravanaSeleccionada.Model.HasVehiculos)
            {
                ctx.FinalizarObtenerVehiculosCaravana += (s, e) =>
                {
                    CaravanaSeleccionada.Model.Vehiculos = e.Result.Vehiculos;
                    CambioNombre.VehiculoSeleccionado = e.Result.Vehiculos[0];
                    OnPropertyChanged("HabilitarCambiarCarnet");
                    OnPropertyChanged("HabilitarDividir");
                    MostrarProgreso = false;
                };
                ctx.ObtenerVehiculosCaravana();
            }
            else
                CambioNombre.VehiculoSeleccionado = CaravanaSeleccionada.Model.Vehiculos[0];


            if (!CaravanaSeleccionada.Model.HasConductores)
            {
                ctx.FinalizarObtenerConductoresCaravana += (s, e) =>
                {
                    CaravanaSeleccionada.Model.Conductores = e.Result.Conductores;
                    MostrarProgreso = false;
                };
                ctx.ObtenerConductoresCaravana();
            }

            if (!CaravanaSeleccionada.Model.HasPuntosEntrega)
            {
                ctx.FinalizarObtenerPuntosEntregaCaravana += (s, e) =>
                {
                    List<PuntoEntrega> temp = App.TranformarPuntosEntrega(e.Result.PuntosEntregas);
                    CaravanaSeleccionada.Model.PuntosEntrega = temp.ToArray();
                    Carga = new ObservableCollection<PuntoEntrega>(temp);
                    MostrarProgreso = false;
                };
                ctx.ObtenerPuntosEntregaCaravana();
            }
            else
                Carga = new ObservableCollection<PuntoEntrega>(CaravanaSeleccionada.Model.PuntosEntrega);

            ObtenerListaRutas();
            MostrarProgreso = !(CaravanaSeleccionada.Model.HasPuntosEntrega && CaravanaSeleccionada.Model.HasVehiculos);
        }
        private void Grabar(object param)
        {
            MostrarProgreso = true;
            Contexto ctx = new Contexto(base.MostarVentaError);
            switch (operacionActual)
            {
                case Operacion.Ruta:
                    ctx.FinalizarCambiarRutaCaravana += (s, e) => { MostrarProgreso = false; base.Cancelar(null); };
                    ctx.CambiarRutaCaravana
                        (
                        CaravanaSeleccionada.IdCaravana,
                        rutaSeleccionada.Id,
                        CaravanaSeleccionada.Model.Caravana.fvcDescripcion,
                        rutaSeleccionada.IdUbicacionOrigen
                        );
                    break;
                case Operacion.Dividir:
                    break;
                case Operacion.Unir:
                    if (caravanaDestino == null)
                    {
                        MostrarProgreso = false;
                        MessageBox.Show("Debe seleccionar una caravana de destino.", Mensajes.TituloAlerta, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    ctx.FinalizarUnirCaravanas += (s, e) => { MostrarProgreso = false; base.Cancelar(null); };
                    ctx.UnirCaravanas(CaravanaSeleccionada.IdCaravana, caravanaDestino.fidCaravana);
                    break;
                case Operacion.Carnet:
                    if (vehiculoCambio != null)
                    {
                        MostrarProgreso = false;
                        MessageBox.Show("Debe seleccionar un carnet de destino.", Mensajes.TituloAlerta, MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    ctx.FinalizarCambiarCarnet += (s, e) => { MostrarProgreso = false; base.Cancelar(null); };
                    ctx.CambiarCarnet(CambioNombre.VehiculoSeleccionado.Placa, vehiculoCambio);
                    break;
                case Operacion.Nombre:
                    ctx.FinalizarCambiarNombreCaravana += (s, e) => { MostrarProgreso = false; base.Cancelar(null); };
                    ctx.CambiarNombreCaravana(CambioNombre.NuevoNombreCaravana);
                    break;
            }
        }
        private void CambiarRuta(object param)
        {
            operacionActual = Operacion.Ruta;
            AjustarOperacion();
        }
        private void Dividir(object param)
        {
            operacionActual = Operacion.Dividir;
            AjustarOperacion();
            MessageBox.Show("No Implementado");
        }
        private void Unir(object param)
        {
            operacionActual = Operacion.Unir;
            AjustarOperacion();
            Contexto ctx = new Contexto(base.MostarVentaError);
            ctx.FinalizarObtenerCaravanasCarretera += (s, e) =>
            {
                CaravanasDisponibles = new ObservableCollection<CaravanaDisponible>(e.Result);
                MostrarProgreso = false;
            };
            MostrarProgreso = true;
            ctx.ObtenerCaravanasCarretera();
        }
        private void CambiarNombre(object param)
        {
            operacionActual = Operacion.Nombre;
            AjustarOperacion();
            if (string.IsNullOrEmpty(CambioNombre.VehiculoSeleccionado.Placa))
                CambioNombre.VehiculoSeleccionado = CaravanaSeleccionada.Model.Vehiculos[0];
            if (string.IsNullOrEmpty(CambioNombre.ConductorSeleccionado.Nombre))
                CambioNombre.ConductorSeleccionado = CaravanaSeleccionada.Model.Conductores[0];
        }
        private void CambiarCarnet(object param)
        {
            operacionActual = Operacion.Carnet;
            AjustarOperacion();
            Contexto ctx = new Contexto(base.MostarVentaError);
            ctx.FinalizarObtenerVehiculosParaCambio += (s, e) =>
                {
                    VehiculosCambio = new ObservableCollection<VehiculoPorSalir>(e.Result);
                    if (vehiculosCambio.Count > 0)
                        VehiculoCambio = vehiculosCambio[0];
                    else
                    {
                        HabilitarGrabar = false;
                        MessageBox.Show("No existen otros carnets asociados a este vehículo");
                    }
                    MostrarProgreso = false;
                };
            MostrarProgreso = true;
            ctx.ObtenerVehiculosParaCambio(CambioNombre.VehiculoSeleccionado.Placa);
        }
        private void AjustarOperacion()
        {
            OnPropertyChanged("MensajeOperacion");
            OnPropertyChanged("HabilitarCambiarCarnet");
            OnPropertyChanged("HabilitarCambiarRuta");
            OnPropertyChanged("HabilitarDividir");
            OnPropertyChanged("HabilitarUnir");
            OnPropertyChanged("HabilitarCambiaNombre");
            OnPropertyChanged("MostrarRuta");
            OnPropertyChanged("MostrarCambiarNombre");
            OnPropertyChanged("MostrarListaCaravanas");
            OnPropertyChanged("MostrarListaCarnets");

            HabilitarGrabar = true;
        }
        private void ObtenerListaRutas()
        {
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerRutasTipo += (s, e) =>
            {
                Rutas = new ObservableCollection<Ruta>(e.Result);
                if (rutas.Count > 0)
                {
                    RutaSeleccionada = rutas.FirstOrDefault(p => Convert.ToInt32(p.Id, CultureInfo.InvariantCulture) == CaravanaSeleccionada.Model.Caravana.finIdRuta);
                    if (rutaSeleccionada == null)
                        RutaSeleccionada = rutas[0];
                }
            };
            ctx.ObtenerRutasTipo(tipoCaravanaSeleccionada);
        }
        #endregion
    }
}
