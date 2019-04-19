using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Microsoft.Win32;
using System.DirectoryServices;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Vista modelo para la lista principal de caravanas
    /// </summary>
    /// <remarks><code>
    /// *----------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 Michael Guerrero Ltda, Todos los Derechos Reservados
    /// * http://www.michaelguerrero.com - mailto:gerencia@michaelguerrero.com
    /// *
    /// * Archivo:      CaravanasViewModel.cs
    /// * Tipo:         Vista Modelo
    /// * Autor:        jguerrero
    /// * Fecha:        2012 Oct 25
    /// * Propósito:    Vista modelo para la lista principal de caravanas
    /// *----------------------------------------------------------------------------------*
    /// </code></remarks>
    public sealed class CaravanasViewModel : ViewModelBase, IFiltrosCaravana
    {
        #region Variables
        private ObservableCollection<SeguimientoViewModel> seguimientos;
        private SeguimientoViewModel[] seguimientosOriginales;
        private ObservableCollection<PuntoEntrega> puntosEntrega;
        private ObservableCollection<PuntoEntrega> carga;
        private bool filtroRojo;
        private bool filtroAmarillo;
        private bool filtroVerde;
        private bool filtroMarcados;
        private bool filtroNacional;
        private bool filtroRegional;
        private bool filtroUrbana;
        private SeguimientoViewModel descripcionBuscada;
        private CiudadRuta filtroOrigen;
        private CiudadRuta filtroDestino;
        private DispatcherTimer timer;
        private string actualizarStoryboard;
        private bool primeraVez;
        private bool recuperarFiltros;
        private bool noRefrescar;
        private bool desdeCaravana;
        private bool mostrarMapa;
        private string placaMapa;
        private bool mostrarDetalle;
        private Key keyPressed;
        #endregion

        #region Constructor / Destructor
        public CaravanasViewModel()
        {
            filtroRojo = true;
            filtroAmarillo = true;
            filtroVerde = true;
            filtroNacional = true;
            filtroRegional = true;
            filtroUrbana = true;
            primeraVez = true;
            Marcados = new Guid[0];
            recuperarFiltros = ((App)App.Current).EstadoFiltros != null;
            seguimientos = new ObservableCollection<SeguimientoViewModel>();
            puntosEntrega = new ObservableCollection<PuntoEntrega>();
            seguimientosOriginales = new SeguimientoViewModel[0];

            NavgationCommand = new DelegateCommand((param) => ((App)App.Current).Navegar((OpcionesNavegacion)param), (e) => true);
            AccionesToolBarCommand = new DelegateCommand(AccionesToolbar, (e) => true);
            ClearFilterCommand = new DelegateCommand(ClearFilter, (e) => true);
            ExportarExcelCommand = new DelegateCommand(ExportarExcel, (e) => true);
            RefrescarListaCommand = new DelegateCommand((param) => ObtenerSeguimientos(), (e) => true);

            timer = new DispatcherTimer();
            timer.Tick += (s, e) => ObtenerSeguimientos();
            timer.Interval = new TimeSpan(0, 5, 0);
            if (!base.IsInDesignMode)
                ObtenerSeguimientos();
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Gets the navgation command.
        /// </summary>
        /// <value>
        /// The navgation command.
        /// </value>
        public ICommand NavgationCommand { get; private set; }
        public ICommand AccionesToolBarCommand { get; private set; }
        public ICommand ClearFilterCommand { get; private set; }
        public ICommand ExportarExcelCommand { get; private set; }
        public ICommand RefrescarListaCommand { get; private set; }

        public ObservableCollection<SeguimientoViewModel> Seguimientos
        {
            get { return this.seguimientos; }
            private set
            {
                if (this.seguimientos != value)
                {
                    this.seguimientos = value;
                    OnPropertyChanged("Seguimientos");
                }
            }
        }
        public ObservableCollection<PuntoEntrega> PuntosEntrega
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ObservableCollection<CiudadRuta> Origenes
        {
            get { return Contexto.ParametrosIniciales != null ? new ObservableCollection<CiudadRuta>(Contexto.ParametrosIniciales.Origenes) : null; }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ObservableCollection<CiudadRuta> Destinos
        {
            get { return Contexto.ParametrosIniciales != null ? new ObservableCollection<CiudadRuta>(Contexto.ParametrosIniciales.Destinos) : null; }
        }
        public SeguimientoViewModel CaravanaSeleccionada
        {
            get { return Contexto.CaravanaSeleccionada; }
            set
            {
                if (Contexto.CaravanaSeleccionada != value)
                {
                    Contexto.CaravanaSeleccionada = value;
                    OnPropertyChanged("CaravanaSeleccionada");
                    MostrarDetalle = false;
                    if (value != descripcionBuscada && descripcionBuscada != null)
                    {
                        desdeCaravana = true;
                        DescripcionBuscada = null;
                    }
                    if (value != null)
                        ObtenerVehiculosCaravana();
                }
            }
        }
        #region IFiltrosCaravana
        public bool FiltroRojo
        {
            get { return filtroRojo; }
            set
            {
                if (filtroRojo != value)
                {
                    filtroRojo = value;
                    Refrescar();
                    base.OnPropertyChanged("FiltroRojo");
                }
            }
        }
        public bool FiltroAmarillo
        {
            get { return filtroAmarillo; }
            set
            {
                if (filtroAmarillo != value)
                {
                    filtroAmarillo = value;
                    Refrescar();
                    base.OnPropertyChanged("FiltroAmarillo");
                }
            }
        }
        public bool FiltroVerde
        {
            get { return filtroVerde; }
            set
            {
                if (filtroVerde != value)
                {
                    filtroVerde = value;
                    Refrescar();
                    base.OnPropertyChanged("FiltroVerde");
                }
            }
        }
        public bool FiltroNacional
        {
            get { return this.filtroNacional; }
            set
            {
                if (this.filtroNacional != value)
                {
                    this.filtroNacional = value;
                    Refrescar();
                    OnPropertyChanged("FiltroNacional");
                }
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
                    Refrescar();
                    OnPropertyChanged("FiltroRegional");
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
                    Refrescar();
                    OnPropertyChanged("FiltroUrbana");
                }
            }
        }
        public bool FiltroMarcados
        {
            get { return this.filtroMarcados; }
            set
            {
                if (this.filtroMarcados != value)
                {
                    this.filtroMarcados = value;
                    Refrescar();
                    OnPropertyChanged("FiltroMarcados");
                }
            }
        }
        public CiudadRuta FiltroOrigen
        {
            get { return filtroOrigen; }
            set
            {
                bool cambio = false;
                if (value == null)
                {
                    if (filtroOrigen != null) // Cambio
                    {
                        filtroOrigen = null;
                        cambio = true;
                    }
                }
                else
                {
                    if (filtroOrigen == null || value.Descripcion != filtroOrigen.Descripcion) // Cambio
                    {
                        filtroOrigen = value;
                        cambio = true;
                    }
                }
                if (cambio)
                {
                    Refrescar();
                    base.OnPropertyChanged("FiltroOrigen");
                }
            }
        }
        public CiudadRuta FiltroDestino
        {
            get
            {
                return filtroDestino;
            }
            set
            {
                bool cambio = false;
                if (value == null)
                {
                    if (filtroDestino != null) // Cambio
                    {
                        filtroDestino = null;
                        cambio = true;
                    }
                }
                else
                {
                    if (filtroDestino == null || value.Descripcion != filtroDestino.Descripcion) // Cambio
                    {
                        filtroDestino = value;
                        cambio = true;
                    }
                }
                if (cambio)
                {
                    Refrescar();
                    base.OnPropertyChanged("FiltroDestino");
                }
            }
        }
        public IEnumerable<Guid> Marcados { get; private set; }
        #endregion
        public SeguimientoViewModel DescripcionBuscada
        {
            get { return this.descripcionBuscada; }
            set
            {
                bool cambio = false;
                if (value == null)
                {
                    if (descripcionBuscada != null) // Cambio
                    {
                        descripcionBuscada = null;
                        cambio = true;
                    }
                }
                else
                {
                    if (descripcionBuscada == null || value.IdCaravana != descripcionBuscada.IdCaravana) // Cambio
                    {
                        descripcionBuscada = value;
                        cambio = true;
                    }
                }
                if (cambio)
                {
                    if (desdeCaravana)
                        desdeCaravana = false;
                    else
                        this.CaravanaSeleccionada = value;

                    base.OnPropertyChanged("DescripcionBuscada");
                }
            }
        }
        public Int32 TotalCaravanas
        {
            get { return seguimientosOriginales.Length; }
        }
        public Int32 TotalVehiculos
        {
            get { return seguimientos.Sum(w => w.Model.Caravana.Vehiculos); }
        }
        public Int32 CaravanasFiltradas
        {
            get { return seguimientos.Count; }
        }
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
        public bool MostrarMapa
        {
            get { return this.mostrarMapa; }
            set
            {
                if (this.mostrarMapa != value)
                {
                    this.mostrarMapa = value;
                    OnPropertyChanged("MostrarMapa");
                }
            }
        }
        public string PlacaMapa
        {
            get { return this.placaMapa; }
            set
            {
                bool cambio = false;
                if (string.IsNullOrEmpty(value))
                {
                    if (!string.IsNullOrEmpty(placaMapa)) // Cambio
                    {
                        placaMapa = null;
                        cambio = true;
                    }
                }
                else
                {
                    if (value != placaMapa) // Cambio
                    {
                        placaMapa = value;
                        cambio = true;
                    }
                }
                if (cambio)
                {
                    if (desdeCaravana)
                        desdeCaravana = false;
                    else if (!string.IsNullOrEmpty(value))
                        BuscarPlaca(value);
                    else
                        this.CaravanaSeleccionada = null;

                    //base.OnPropertyChanged("PlacaMapa");
                }
            }
        }
        public bool MostrarDetalle
        {
            get { return this.mostrarDetalle; }
            set
            {
                if (this.mostrarDetalle != value)
                {
                    this.mostrarDetalle = value;
                    OnPropertyChanged("MostrarDetalle");
                }
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Uri RutaReportarNovedades
        {
            get { return Contexto.ParametrosIniciales != null ? Contexto.ParametrosIniciales.Parametros.CapturaNovedades : null; }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Uri RutaInformeNovedadesManifiesto
        {
            get { return Contexto.ParametrosIniciales != null ? Contexto.ParametrosIniciales.Parametros.InformeNovedadesManifiesto : null; }
        }
        public Uri RutaInformeOperaciones
        {
            get
            {
                if (Contexto.ParametrosIniciales == null || CaravanaSeleccionada == null)
                    return null;
                string basico = Contexto.ParametrosIniciales.Parametros.InformeOperaciones.ToString();
                string caravana = string.Format(CultureInfo.CurrentCulture, "{{{0}}}", CaravanaSeleccionada.IdCaravana);
                basico = string.Format(CultureInfo.CurrentCulture, basico, caravana);
                return new Uri(basico);
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public Usuario UsuarioActual
        {
            get { return Contexto.ParametrosIniciales.UsuarioActual; }
        }
        public override bool MostrarError
        {
            get
            {
                return base.MostrarError;
            }
            set
            {
                if (base.MostrarError != value)
                {
                    base.MostrarError = value;
                    if (!value && !mostrarMapa)
                        this.MostrarMapa = true;
                }
            }
        }
        public Key KeyPressed
        {
            get { return this.keyPressed; }
            set
            {
                if (this.keyPressed != value)
                {
                    Contexto.CaravanaKeyPressed = value;
                    this.keyPressed = value;
                    OnPropertyChanged("KeyPressed");
                }
            }
        }
        #endregion

        #region Metodos
        internal void TieneFoco(bool valor)
        {
            if (valor)
                RecuperarFiltros();
            else
            {
                timer.Stop();
                Marcados = (from p in this.seguimientosOriginales
                            where p.Marcado
                            select p.IdCaravana).ToArray();
                ((App)App.Current).EstadoFiltros = this;
            }

            MostrarMapa = !valor;
        }
        #endregion

        #region Funciones
        //
        // Comandos
        //
        private void AccionesToolbar(object param)
        {
            OpcionesBarra valor = (OpcionesBarra)param;
            switch (valor)
            {
                case OpcionesBarra.EstadoRojo:
                    this.FiltroRojo = !filtroRojo;
                    break;
                case OpcionesBarra.EstadoAmarillo:
                    this.FiltroAmarillo = !filtroAmarillo;
                    break;
                case OpcionesBarra.EstadoVerde:
                    this.FiltroVerde = !filtroVerde;
                    break;
                case OpcionesBarra.EstadoRegional:
                    this.FiltroRegional = !filtroRegional;
                    break;
                case OpcionesBarra.EstadoNacional:
                    this.FiltroNacional = !filtroNacional;
                    break;
                case OpcionesBarra.EstadoUrbana:
                    this.FiltroUrbana = !filtroUrbana;
                    break;
                case OpcionesBarra.EstadoMarcada:
                    this.FiltroMarcados = !filtroMarcados;
                    break;
                case OpcionesBarra.CambiarMarca:
                    if (this.CaravanaSeleccionada != null)
                        CaravanaSeleccionada.Marcado = !CaravanaSeleccionada.Marcado;
                    break;
                case OpcionesBarra.MostrarDetalle:
                    MostrarDetalleCaravana();
                    break;
            }
        }
        private void ClearFilter(object param)
        {
            noRefrescar = true;
            FiltroRojo = true;
            FiltroAmarillo = true;
            FiltroVerde = true;
            FiltroNacional = true;
            FiltroRegional = true;
            FiltroUrbana = true;
            DescripcionBuscada = null;
            FiltroDestino = null;
            FiltroOrigen = null;
            noRefrescar = false;
            Refrescar();
        }
        private void ExportarExcel(object param)
        {
            if (!CaravanaSeleccionada.Model.HasPuntosEntrega)
                return;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(CultureInfo.CurrentCulture, "Factura\tCliente\tPunto de entrega\tCompromiso\tUnidades\tPeso\tVolumen\r\n");
            foreach (var item in CaravanaSeleccionada.Model.PuntosEntrega)
            {
                string compromiso = item.fdtCompromisoCliente.HasValue ? item.fdtCompromisoCliente.Value.ToString("m", CultureInfo.CurrentCulture) : "";
                sb.AppendFormat(CultureInfo.CurrentCulture, "{0}\t{1}\t{2}\t{3}\t{4}\t{5:n}\t{6:n}\r\n",
                    item.fvcFactura,
                    item.fvcDescripcion,
                    item.Punto,
                    compromiso,
                    item.finUnidades,
                    item.frlPeso.Value,
                    item.frlVolumen.Value
                    );
            }
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Ms Excel|*.xls";
            saveFileDialog1.Title = "Grabar archivo Microsoft Excel";
            saveFileDialog1.FileName = "Reporte de carga.xls";
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                File.WriteAllText(saveFileDialog1.FileName, sb.ToString());
        }
        //
        // Datos
        // 
        public void ObtenerSeguimientos()
        {
            timer.Stop();
            ActualizarStoryboard = "Actualizando";
            Contexto ctx = new Contexto(MostrarMensajeError);
            if (primeraVez)
            {
                base.MostrarProgreso = true;
                primeraVez = false;
                ctx.FinalizarObtenerParametrosIniciales += (s, e) =>
                    {
                        base.OnPropertyChanged("Origenes");
                        base.OnPropertyChanged("Destinos");
                        base.OnPropertyChanged("RutaReportarNovedades");
                        base.OnPropertyChanged("RutaInformeNovedadesManifiesto");
                    };
                ctx.ObtenerParametrosIniciales();
            }
            ctx.FinalizarObtenerSeguimientos += (s, e) =>
                {
                    if (e.Result != null)
                    {
                        if (seguimientosOriginales.Length > 0)
                            Marcados = (from p in this.seguimientosOriginales
                                        where p.Marcado
                                        select p.IdCaravana).ToArray();
                        this.seguimientosOriginales =
                            (
                                from p in e.Result
                                select new SeguimientoViewModel()
                                                {
                                                    Model = new DetalleCaravana { Caravana = p },
                                                    Marcado = Marcados.Contains(p.fidCaravana)
                                                }
                              ).ToArray();
                        ActualizarStoryboard = DateTime.Now.ToLongTimeString();
                    }
                    Refrescar();

                    foreach (SeguimientoViewModel seg in this.seguimientosOriginales.Where(x => x.Model.Caravana.TipoCaravana.Equals(TipoCaravana.Urbana)))
                    {
                        seg.Model.Caravana.fvcSiguientePunto = string.Empty;
                        seg.Model.Caravana.fvcUltimoPunto = string.Empty;
                        seg.BuscarPlacaEnMapa += seg_BuscarPlacaEnMapa;
                    }

                    foreach (SeguimientoViewModel so in this.seguimientosOriginales)
                    {
                        so.BuscarPlacaEnMapa += seg_BuscarPlacaEnMapa;
                    }

                };
            ctx.ObtenerSeguimientos();
        }

        private void seg_BuscarPlacaEnMapa(object sender, EventArgs e)
        {
            base.OnPropertyChanged("PlacaMapa");
        }
        private void ObtenerVehiculosCaravana()
        {
            if (this.CaravanaSeleccionada == null)
                return;
            if (!CaravanaSeleccionada.Model.HasVehiculos)
            {
                Contexto ctx = new Contexto(MostrarMensajeError);
                ctx.FinalizarObtenerVehiculosCaravana += (s, e) =>
                    {
                        if (this.CaravanaSeleccionada == null ||
                            e.Result.CaravanaId != this.CaravanaSeleccionada.IdCaravana)
                            return;
                        this.CaravanaSeleccionada.Model.Vehiculos = e.Result.Vehiculos;
                        AsigarPlacaCaravana();
                    };
                ctx.ObtenerVehiculosCaravana();
            }
            else
                AsigarPlacaCaravana();
        }
        //
        // Utilidades
        //
        private void MostrarMensajeError(Exception error)
        {
            ActualizarStoryboard = "Error: No actualizado";
            Refrescar();
            MostarVentaError(error);
            this.MostrarMapa = false;
        }
        private void Refrescar()
        {
            if (noRefrescar) return;
            Guid seleccionada = Guid.Empty;
            if (this.CaravanaSeleccionada != null)
                seleccionada = this.CaravanaSeleccionada.IdCaravana;

            foreach (SeguimientoViewModel seg in this.seguimientosOriginales.Where(x => x.Model.Caravana.TipoCaravana.Equals(TipoCaravana.Urbana)))
            {
                seg.Model.Caravana.fvcSiguientePunto = string.Empty;
                seg.Model.Caravana.fvcUltimoPunto = string.Empty;
            }

            var retorno = seguimientosOriginales.Where(p => ValidarEstado(p.Model.Caravana));

            if (filtroMarcados)
                retorno = retorno.Where(p => p.Marcado);

            retorno = retorno.Where(p =>
                (filtroNacional && p.Model.Caravana.TipoCaravana == TipoCaravana.Nacional) ||
                (filtroRegional && p.Model.Caravana.TipoCaravana == TipoCaravana.Regional) ||
                (filtroUrbana && p.Model.Caravana.TipoCaravana == TipoCaravana.Urbana)
                );

            if (this.filtroOrigen != null)
                retorno = retorno.Where(p => p.Model.Caravana.origen == this.filtroOrigen.Descripcion);
            if (this.filtroDestino != null)
                retorno = retorno.Where(p => p.Model.Caravana.destino == this.filtroDestino.Descripcion);

            this.Seguimientos = new ObservableCollection<SeguimientoViewModel>(retorno.OrderBy(p => p.Model.Caravana.frlDiferencia));
            if (seleccionada != Guid.Empty)
            {
                var primera = (from p in this.seguimientos where p.IdCaravana == seleccionada select p).FirstOrDefault();
                if (primera != null)
                {
                    primera.Model.Caravana.fvcUltimoPunto = string.Empty;
                    primera.Model.Caravana.fvcSiguientePunto = string.Empty;
                    this.CaravanaSeleccionada = primera;
                }
                //var primera = (from p in this.seguimientos 
                //               where p.IdCaravana == seleccionada
                //               &&(filtroNacional && p.Model.Caravana.TipoCaravana == TipoCaravana.Nacional) ||
                //                (filtroRegional && p.Model.Caravana.TipoCaravana == TipoCaravana.Regional) ||
                //                (filtroUrbana && p.Model.Caravana.TipoCaravana == TipoCaravana.Urbana)
                //               select p).FirstOrDefault();

                //if (primera != null)
                //{
                //    primera.Model.Caravana.fvcUltimoPunto = string.Empty;
                //    primera.Model.Caravana.fvcSiguientePunto = string.Empty;
                //    this.CaravanaSeleccionada = primera;
                //}
                //var primera = (from p in this.seguimientos where p.IdCaravana == seleccionada select p).FirstOrDefault();
                //if (primera != null)
                //    this.CaravanaSeleccionada = primera;


                //if (this.seguimientos.Count > 0)
                //{
                //    this.CaravanaSeleccionada = this.seguimientos[0];
                //}
            }
            base.MostrarProgreso = false;
            this.MostrarMapa = !this.MostrarError;
            base.OnPropertyChanged("TotalCaravanas");
            base.OnPropertyChanged("TotalVehiculos");
            base.OnPropertyChanged("CaravanasFiltradas");
            this.timer.Start();
        }
        private bool ValidarEstado(Seguimiento item)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
            double t = item.frlDiferencia - ts.TotalMinutes;

            if (t >= 0)
                return filtroVerde;
            else
            {
                t = Math.Abs(t);

                if (item.ftyEstAlerta != 0 && item.ftyEstReporte != 0)
                    return (t > item.ftyEstAlerta ? filtroRojo : filtroAmarillo);
                else
                    return (t > item.ftyMinutosAlerta ? filtroRojo : filtroAmarillo);
            }
        }
        private void AsigarPlacaCaravana()
        {
            if (!this.CaravanaSeleccionada.Model.HasVehiculos)
                return;
            desdeCaravana = true;
            var vehiculo = this.CaravanaSeleccionada.Model.Vehiculos.FirstOrDefault(p => p.GPS.HasValue);
            if (vehiculo != null)
                PlacaMapa = vehiculo.Placa;
            else
                PlacaMapa = this.CaravanaSeleccionada.Model.Vehiculos[0].Placa;
        }
        private void MostrarDetalleCaravana()
        {
            this.MostrarMapa = false;
            if (CaravanaSeleccionada == null)
                return;

            FinalizarMostrarDetalle();

            Contexto ctx = new Contexto(MostrarMensajeError);
            if (!CaravanaSeleccionada.Model.HasPuntosEntrega)
            {
                ctx.FinalizarObtenerPuntosEntregaCaravana += new EventHandler<ObtenerPuntosEntregaCaravanaCompletedEventArgs>(ctx_FinalizarObtenerPuntosEntregaCaravana);
                ctx.ObtenerPuntosEntregaCaravana();
            }
            if (!CaravanaSeleccionada.Model.HasConductores)
            {
                ctx.FinalizarObtenerConductoresCaravana += (s, e) =>
                {
                    if (CaravanaSeleccionada != null && CaravanaSeleccionada.IdCaravana == e.Result.CaravanaId)
                        CaravanaSeleccionada.Model.Conductores = e.Result.Conductores;
                    FinalizarMostrarDetalle();
                };
                ctx.ObtenerConductoresCaravana();
            }
            if (!CaravanaSeleccionada.Model.HasEscoltas)
            {
                ctx.FinalizarObtenerEscoltasCaravana += (s, e) =>
                    {
                        if (CaravanaSeleccionada != null && CaravanaSeleccionada.IdCaravana == e.Result.CaravanaId)
                            CaravanaSeleccionada.Model.Escoltas = e.Result.Escoltas;
                        FinalizarMostrarDetalle();
                    };
                ctx.ObtenerEscoltasCaravana();
            }
            ctx.FinalizarObtenerReportesCaravana += (s, e) =>
                {
                    if (CaravanaSeleccionada != null && CaravanaSeleccionada.IdCaravana == e.Result.CaravanaId)
                        CaravanaSeleccionada.Model.Reportes = e.Result.Reportes;
                    FinalizarMostrarDetalle();
                };
            ctx.ObtenerReportesCaravana();

            this.MostrarDetalle = true;
        }
        private void FinalizarMostrarDetalle()
        {
            //if (CaravanaSeleccionada == null)
            //{
            //    base.MostrarProgreso = false;
            //    this.MostrarMapa = true;
            //}
            //else
            //{
            //    base.MostrarProgreso = !CaravanaSeleccionada.Model.HasAll;
            //    this.MostrarMapa = !base.MostrarProgreso;
            //}
        }
        private void BuscarPlaca(string placa)
        {
            if (placa.Length < 6) return;
            this.CaravanaSeleccionada = seguimientos.FirstOrDefault(w => w.Model.Caravana.fvcDescripcion.IndexOf(placa, StringComparison.OrdinalIgnoreCase) >= 0);
            if (this.CaravanaSeleccionada == null)
            {
                Contexto ctx = new Contexto(MostrarMensajeError);
                ctx.FinalizarObtenerCaravanaPlaca += (s, e) =>
                        this.CaravanaSeleccionada = seguimientos.FirstOrDefault(w => w.Model.Caravana.fidCaravana == e.Result);
                ctx.ObtenerCaravanaPlaca(placa);
            }
        }
        private void RecuperarFiltros()
        {
            try
            {
                if (recuperarFiltros)
                {
                    IFiltrosCaravana estado = ((App)App.Current).EstadoFiltros;
                    FiltroRojo = estado.FiltroRojo;
                    FiltroAmarillo = estado.FiltroAmarillo;
                    FiltroVerde = estado.FiltroVerde;
                    FiltroNacional = estado.FiltroNacional;
                    FiltroRegional = estado.FiltroRegional;
                    FiltroUrbana = estado.FiltroUrbana;
                    FiltroMarcados = estado.FiltroMarcados;
                    FiltroOrigen = estado.FiltroOrigen;
                    FiltroDestino = estado.FiltroDestino;
                    Marcados = estado.Marcados;
                    foreach (var item in seguimientosOriginales.Where(p => estado.Marcados.Contains(p.IdCaravana)))
                        item.Marcado = true;

                    desdeCaravana = false;
                    PlacaMapa = estado.PlacaMapa;
                }
                else
                    ObtenerSeguimientos();
            }
            catch (Exception) { }
            finally
            {
                recuperarFiltros = false;
                ((App)App.Current).EstadoFiltros = null;
            }
        }
        public void BuscarPlacaView()
        {
            base.OnPropertyChanged("PlacaMapa");
        }
        #endregion

        #region Eventos
        private void ctx_FinalizarObtenerPuntosEntregaCaravana(object sender, ObtenerPuntosEntregaCaravanaCompletedEventArgs e)
        {
            if (CaravanaSeleccionada == null || CaravanaSeleccionada.IdCaravana != e.Result.CaravanaId)
            {
                //base.MostrarProgreso = !CaravanaSeleccionada.Model.HasAll;
                this.MostrarMapa = !base.MostrarProgreso;
                return;
            }
            List<PuntoEntrega> temp = App.TranformarPuntosEntrega(e.Result.PuntosEntregas);
            CaravanaSeleccionada.Model.PuntosEntrega = temp.ToArray();
            Carga = new ObservableCollection<PuntoEntrega>(temp);
            PuntosEntrega = new ObservableCollection<PuntoEntrega>(temp);
            //
            // Funciones de las grillas
            //
            base.OnPropertyChanged("RutaInformeOperaciones");
            //base.MostrarProgreso = !CaravanaSeleccionada.Model.HasAll;
            this.MostrarMapa = !base.MostrarProgreso;
        }
        #endregion
    }
}
