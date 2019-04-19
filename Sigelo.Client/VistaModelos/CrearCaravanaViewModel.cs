using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model de crear caravanas
    /// </summary>
    public sealed class CrearCaravanaViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private string actualizarStoryboard;
        private bool mostrarNombreCaravana;
        private bool mostrarListaNombres;
        private bool mostrarVehiculos;
        private bool mostrarCaravanas;
        private bool activarGuardarCaravana;
        private bool filtoNacional;
        private bool filtroUrbana;
        private bool filtroRegional;
        private bool? seleccionarTodos;
        private DispatcherTimer timer;
        private TipoCaravana tipoCaravanaSeleccionada;
        private VehiculoPorSalir vehiculoPorSalirSeleccionado;
        private CaravanaDisponible caravanaSeleccionada;
        private Ruta rutaSeleccionada;
        private ObservableCollection<CaravanaDisponible> caravanas;
        private ObservableCollection<CaravanaDisponible> caravanasFiltradas;
        private ObservableCollection<VehiculoPorSalir> vehiculosPorSalir;
        private ObservableCollection<Ruta> rutas;
        #endregion

        #region Constructor / Destructor
        public CrearCaravanaViewModel()
        {
            InstaciarComandos();

            filtoNacional = true;
            filtroRegional = true;
            filtroUrbana = true;
            seleccionarTodos = false;
            tipoCaravanaSeleccionada = TipoCaravana.Nacional;
            CambioNombre = new CambioNombreViewModel(true);

            timer = new DispatcherTimer();
            timer.Tick += (s, e) => ActualizarInformacion();
            timer.Interval = new TimeSpan(0, 1, 0);
            if (!base.IsInDesignMode)
            {
                MostrarProgreso = true;
                EstadoCrearCaravana(false);
                ActualizarInformacion();
            }
        }
        #endregion

        #region Propiedades
        public ICommand RefrescarListaCommand { get; private set; }
        public ICommand CancelarCrearCaravanaCommand { get; private set; }
        public ICommand CrearCaravanaCommand { get; private set; }
        public ICommand EliminarCaravanaCommand { get; private set; }
        public ICommand EscoltasCaravanaCommand { get; private set; }
        public ICommand RefuerzosCaravanaCommand { get; private set; }
        public ICommand GuardarVehiculoCommand { get; private set; }
        public ICommand GuardarCrearCaravanaCommand { get; private set; }
        public ICommand IniciarCaravanaCommand { get; private set; }
        public string ActualizarStoryboard
        {
            get { return this.actualizarStoryboard; }
            set
            {
                if (this.actualizarStoryboard != value)
                {
                    this.actualizarStoryboard = value;
                    OnPropertyChanged("ActualizarStoryboard");
                }
            }
        }
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
        public bool MostrarNombreCaravana
        {
            get { return this.mostrarNombreCaravana; }
            set
            {
                if (this.mostrarNombreCaravana != value)
                {
                    this.mostrarNombreCaravana = value;
                    OnPropertyChanged("MostrarNombreCaravana");
                }
            }
        }
        public bool MostrarListaNombres
        {
            get { return this.mostrarListaNombres; }
            set
            {
                if (this.mostrarListaNombres != value)
                {
                    this.mostrarListaNombres = value;
                    OnPropertyChanged("MostrarListaNombres");
                }
            }
        }
        public bool MostrarVehiculos
        {
            get { return this.mostrarVehiculos; }
            set
            {
                if (this.mostrarVehiculos != value)
                {
                    this.mostrarVehiculos = value;
                    OnPropertyChanged("MostrarVehiculos");
                }
            }
        }
        public bool MostrarCaravanas
        {
            get { return this.mostrarCaravanas; }
            set
            {
                if (this.mostrarCaravanas != value)
                {
                    this.mostrarCaravanas = value;
                    OnPropertyChanged("MostrarCaravanas");
                }
            }
        }
        public bool ActivarSeleccionCaravana
        {
            get { return caravanaSeleccionada != null; }
        }
        public bool ActivarGuardarCaravana
        {
            get { return this.activarGuardarCaravana; }
            set
            {
                if (this.activarGuardarCaravana != value)
                {
                    this.activarGuardarCaravana = value;
                    OnPropertyChanged("ActivarGuardarCaravana");
                }
            }
        }
        public bool ActivarGuardarVehiculo
        {
            get
            {
                return caravanaSeleccionada != null &&
                    (!seleccionarTodos.HasValue || (seleccionarTodos.HasValue && seleccionarTodos.Value));
            }
        }
        public bool FiltroRegional
        {
            get { return this.filtroRegional; }
            set
            {
                if (this.filtroRegional != value)
                {
                    this.filtroRegional = value;
                    OnPropertyChanged("FiltroRegional");
                    AplicarFiltrosCaravana();
                }
            }
        }
        public bool FiltroUrbana
        {
            get { return this.filtroUrbana; }
            set
            {
                if (this.filtroUrbana != value)
                {
                    this.filtroUrbana = value;
                    OnPropertyChanged("FiltroUrbana");
                    AplicarFiltrosCaravana();
                }
            }
        }
        public bool FiltoNacional
        {
            get { return this.filtoNacional; }
            set
            {
                if (this.filtoNacional != value)
                {
                    this.filtoNacional = value;
                    OnPropertyChanged("FiltoNacional");
                    AplicarFiltrosCaravana();
                }
            }
        }
        public bool? SeleccionarTodos
        {
            get { return seleccionarTodos; }
            set
            {
                if (value.HasValue && seleccionarTodos != value)
                {
                    seleccionarTodos = value;
                    foreach (var item in vehiculosPorSalir)
                        item.Marcado = value.Value;
                }
            }
        }
        public ObservableCollection<CaravanaDisponible> Caravanas
        {
            get { return this.caravanasFiltradas; }
            private set
            {
                if (this.caravanas != value)
                {
                    this.caravanas = value;
                    AplicarFiltrosCaravana();
                }
            }
        }
        public ObservableCollection<VehiculoPorSalir> VehiculosPorSalir
        {
            get { return this.vehiculosPorSalir; }
            private set
            {
                if (this.vehiculosPorSalir != value)
                {
                    this.vehiculosPorSalir = value;
                    if (value != null)
                        foreach (var item in value)
                            item.Contenedor = this;
                    OnPropertyChanged("VehiculosPorSalir");
                    AjustarSeleccion();
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
        public CaravanaDisponible CaravanaSeleccionada
        {
            get { return this.caravanaSeleccionada; }
            set
            {
                if (this.caravanaSeleccionada != value)
                {
                    this.caravanaSeleccionada = value;
                    OnPropertyChanged("CaravanaSeleccionada");
                    EditarCaravana();
                }
            }
        }
        public VehiculoPorSalir VehiculoPorSalirSeleccionado
        {
            get { return this.vehiculoPorSalirSeleccionado; }
            set
            {
                if (this.vehiculoPorSalirSeleccionado != value)
                {
                    this.vehiculoPorSalirSeleccionado = value;
                    ActualizarCambioNombre();
                    OnPropertyChanged("VehiculoPorSalirSeleccionado");
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
        public CambioNombreViewModel CambioNombre { get; private set; }
        #endregion

        #region Metodos
        internal void AjustarSeleccion()
        {
            seleccionarTodos = false;
            if (vehiculosPorSalir != null && vehiculosPorSalir.Count > 0)
            {
                seleccionarTodos = vehiculosPorSalir[0].Marcado;
                if (vehiculosPorSalir.Any(p => p.Marcado != seleccionarTodos.Value))
                    seleccionarTodos = null;
            }

            OnPropertyChanged("SeleccionarTodos");
            OnPropertyChanged("ActivarGuardarVehiculo");
        }
        #endregion

        #region Funciones
        private void CancelarCrearCaravana()
        {
            this.VehiculoPorSalirSeleccionado = null;
            EstadoCrearCaravana(false);
        }
        private void CrearCaravana()
        {
            base.MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            if (this.vehiculosPorSalir.Count == 0)
            {
                ctx.FinalizarVehiculosPorSalir += (s, e) =>
                {
                    this.VehiculosPorSalir = new ObservableCollection<VehiculoPorSalir>(e.Result);
                    EstadoCrearCaravana(true);
                    base.MostrarProgreso = false;

                };
                ctx.VehiculosPorSalir();
            }
            else
            {
                EstadoCrearCaravana(true);
                base.MostrarProgreso = false;
            }
        }
        private void EliminarCaravana()
        {
            if (this.caravanaSeleccionada == null) return;
            Guid[] marcadas = new Guid[] { this.caravanaSeleccionada.fidCaravana };
            base.MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarEliminarCaravana += (s, e) => ActualizarInformacion();
            ctx.EliminarCaravana(marcadas);
        }
        private void EditarCaravana()
        {
            if (this.caravanaSeleccionada == null) return;
            bool[] finalizo = new bool[4];
            for (int i = 0; i < finalizo.Length; i++)
                finalizo[i] = true;
            finalizo[2] = false;

            base.MostrarProgreso = true;
            Contexto ctx = new Contexto((e) =>
            {
                MostarVentaError(e);
                ActualizarStoryboard = "Error Actualizando!";
                timer.Start();
            });
            ctx.FinalizarVehiculosPorSalir += (s, e) => FinalizarVehiculosPorSalir(finalizo, ctx, e);
            ctx.VehiculosPorSalir();
        }
        private void EscoltasCaravana()
        {
            if (this.caravanaSeleccionada != null)
            {
                var actual = ((App)App.Current);
                actual.OpcionAnterior = OpcionesNavegacion.CrearCaravana;
                Contexto.CaravanaSeleccionada = new SeguimientoViewModel(this.caravanaSeleccionada);
                actual.Navegar(OpcionesNavegacion.Escoltas);
            }
        }
        private void RefuerzosCaravana()
        {
            if (this.caravanaSeleccionada != null)
            {
                var actual = ((App)App.Current);
                actual.OpcionAnterior = OpcionesNavegacion.CrearCaravana;
                Contexto.CaravanaSeleccionada = new SeguimientoViewModel(this.caravanaSeleccionada);
                actual.Navegar(OpcionesNavegacion.Refuerzos);
            }
        }
        private void IniciarCaravana()
        {
            if (this.caravanaSeleccionada != null)
            {
                var actual = ((App)App.Current);
                Contexto.CaravanaSeleccionada = new SeguimientoViewModel(this.caravanaSeleccionada);
                actual.Navegar(OpcionesNavegacion.InicioRecorrido);
            }
        }
        private void GuardarVehiculo()
        {
            if (caravanaSeleccionada == null) return;

            bool[] finalizo = new bool[2];
            base.MostrarProgreso = true;

            Contexto ctx = new Contexto(MostarVentaError);

            ctx.FinalizarEliminarVehiculoCaravana += (s, e) => { finalizo[0] = true; VerificarFinalizacion(finalizo); };
            ctx.FinalizarInsertarVehiculoCaravana += (s, e) => { finalizo[1] = true; VerificarFinalizacion(finalizo); };

            VehiculoPorSalir[] temp = (from p in vehiculosPorSalir where !p.Marcado select p).ToArray();
            if (temp.Length > 0)
                ctx.EliminarVehiculoCaravana(caravanaSeleccionada.fidCaravana, temp);
            else
                finalizo[0] = true;

            temp = (from p in vehiculosPorSalir where p.Marcado select p).ToArray();
            if (temp.Length > 0)
                ctx.InsertarVehiculoCaravana(caravanaSeleccionada.fidCaravana, temp);
            else
                finalizo[1] = true;

            VerificarFinalizacion(finalizo);
        }
        private void GuardarCrearCaravana()
        {
            if (this.rutaSeleccionada == null || this.vehiculoPorSalirSeleccionado == null) return;
            base.MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarCrearCaravana += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        this.MostarVentaError(e.Error, "El vehiculo ya tiene una caravana asignada.");
                    }
                    else
                    {
                        MostrarProgreso = true;
                        EstadoCrearCaravana(false);
                        ActualizarInformacion();
                    }
                };
            
            ctx.CrearCaravana(
                Convert.ToInt32(this.rutaSeleccionada.Id, CultureInfo.InvariantCulture),
                this.CambioNombre.NuevoNombreCaravana + "|" + 
                this.CambioNombre.Placa.Split()[0] + "|" +
                (this.CambioNombre.Placa.Split().Length==2 ? this.CambioNombre.Placa.Split()[1] + "|": " ") +
                this.CambioNombre.Celular,
                this.vehiculoPorSalirSeleccionado
                );

        }
        private void ActualizarInformacion()
        {
            timer.Stop();
            ActualizarStoryboard = "Actualizando";
            bool[] finalizo = new bool[4];
            Contexto ctx = new Contexto((e) =>
            {
                MostarVentaError(e, "El vehiculo ya tiene una caravana asignada.");
                ActualizarStoryboard = "Error Actualizando!";
                timer.Start();
            });
            ctx.FinalizarObtenerCaravanasDisponibles += (s, e) =>
            {
                this.Caravanas = new ObservableCollection<CaravanaDisponible>(e.Result);
                finalizo[0] = true;
                VerificarFinalizacion(finalizo);
            };
            ctx.FinalizarVehiculosPorSalir += (s, e) => FinalizarVehiculosPorSalir(finalizo, ctx, e);
            ctx.FinalizarObtenerListaRutas += (s, e) =>
                {
                    this.Rutas = new ObservableCollection<Ruta>(e.Result);
                    if (rutas.Count > 0)
                        this.RutaSeleccionada = rutas[0];
                    finalizo[3] = true;
                    VerificarFinalizacion(finalizo);
                };
            ctx.ObtenerCaravanasDisponibles();
            ctx.VehiculosPorSalir();
            ctx.ObtenerListaRutas(this.tipoCaravanaSeleccionada);
        }
        private void FinalizarVehiculosPorSalir(bool[] finalizo, Contexto ctx, VehiculosPorSalirCompletedEventArgs e)
        {
            this.VehiculosPorSalir = new ObservableCollection<VehiculoPorSalir>(e.Result);
            if (this.caravanaSeleccionada != null)
            {
                ctx.FinalizarObtenerVehiculosCaravana += (source, arg) =>
                {
                    foreach (var item in from p in this.vehiculosPorSalir
                                         join q in arg.Result.Vehiculos
                                         on p.fnuManifiesto equals q.Manifiesto.Value
                                         select p)
                    {
                        item.Marcado = true;
                    }
                    finalizo[2] = true;
                    VerificarFinalizacion(finalizo);
                };
                ctx.ObtenerVehiculosCaravana(this.caravanaSeleccionada.fidCaravana);
            }
            else
                finalizo[2] = true;

            finalizo[1] = true;
            VerificarFinalizacion(finalizo);
            OnPropertyChanged("ActivarSeleccionCaravana");
        }
        private void VerificarFinalizacion(bool[] finalizo)
        {
            if (finalizo.Contains(false)) return;

            base.MostrarProgreso = false;
            base.MostrarError = false;
            ActualizarStoryboard = DateTime.Now.ToLongTimeString();
            timer.Start();
        }
        private void EstadoCrearCaravana(bool estado)
        {
            this.MostrarNombreCaravana = estado;
            this.MostrarListaNombres = estado;
            this.MostrarVehiculos = !estado;
            this.MostrarCaravanas = !estado;
            if (estado)
                timer.Stop();
            else
                timer.Start();
        }
        private void AplicarFiltrosCaravana()
        {
            this.caravanasFiltradas = new ObservableCollection<CaravanaDisponible>(from p in this.caravanas
                                                                                   where (this.filtoNacional && p.TipoCaravana == TipoCaravana.Nacional) ||
                                                                                   (this.filtroRegional && p.TipoCaravana == TipoCaravana.Regional) ||
                                                                                   (this.filtroUrbana && p.TipoCaravana == TipoCaravana.Urbana)
                                                                                   select p);
            OnPropertyChanged("Caravanas");
        }
        private void ObtenerListaRutas()
        {
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerListaRutas += (s, e) =>
            {
                this.Rutas = new ObservableCollection<Ruta>(e.Result);
                if (rutas.Count > 0)
                    this.RutaSeleccionada = rutas[0];
            };
            ctx.ObtenerListaRutas(this.tipoCaravanaSeleccionada);
        }
        private void ActualizarCambioNombre()
        {
            if (this.vehiculoPorSalirSeleccionado == null)
            {
                ActivarGuardarCaravana = false;
                CambioNombre.VehiculoSeleccionado = null;
                CambioNombre.ConductorSeleccionado = null;
            }
            else
            {
                CambioNombre.VehiculoSeleccionado = new Vehiculo
                {
                    Placa =this.vehiculoPorSalirSeleccionado.Completa
                };
                var conductor = new Conductor
                {
                    Telefonos = this.vehiculoPorSalirSeleccionado.Telefono
                };
                if (!string.IsNullOrEmpty(this.vehiculoPorSalirSeleccionado.Conductor))
                {
                    string[] t = this.vehiculoPorSalirSeleccionado.Conductor.Split(' ');
                    conductor.PrimerApellido = t[0];
                }
                CambioNombre.ConductorSeleccionado = conductor;
                ActivarGuardarCaravana = true;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private void InstaciarComandos()
        {
            RefrescarListaCommand = new DelegateCommand((param) => ActualizarInformacion(), (e) => true);
            CancelarCrearCaravanaCommand = new DelegateCommand((param) => CancelarCrearCaravana(), (e) => true);
            CrearCaravanaCommand = new DelegateCommand((param) => CrearCaravana(), (e) => true);
            EliminarCaravanaCommand = new DelegateCommand((param) => EliminarCaravana(), (e) => true);
            EscoltasCaravanaCommand = new DelegateCommand((param) => EscoltasCaravana(), (e) => true);
            RefuerzosCaravanaCommand = new DelegateCommand((param) => RefuerzosCaravana(), (e) => true);
            GuardarVehiculoCommand = new DelegateCommand((param) => GuardarVehiculo(), (e) => true);
            GuardarCrearCaravanaCommand = new DelegateCommand((param) => GuardarCrearCaravana(), (e) => true);
            IniciarCaravanaCommand = new DelegateCommand((param) => IniciarCaravana(), (e) => true);
        }
        #endregion
    }
}