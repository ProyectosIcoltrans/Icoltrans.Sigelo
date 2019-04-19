using System;
using System.ComponentModel;
using System.ServiceModel;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using System.Windows.Input;
namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Centraliza toda la comunicación con la capa de servicios.  Adicionalmente deja un cahce local para evitar llamados continuos
    /// </summary>
    internal sealed class Contexto
    {
        #region Variables
        private static FuncionEscolta[] funcionesEscolta;
        private static Escolta1[] escoltasDisponibles;
        private static Refuerzo[] refuerzosDisponibles;
        #endregion

        #region Definicion de Eventos
        /// <summary>
        /// Ocurre cuando se genera un error lectura.
        /// </summary>
        /// 01/12/2010 By Jaimir Guerrero
        internal event EventHandler<AsyncCompletedEventArgs> ErrorLectura;
        internal event EventHandler<ObtenerFuncionesEscoltaCompletedEventArgs> FinalizarObtenerFuncionesEscolta;
        internal event EventHandler<ObtenerSeguimientosCompletedEventArgs> FinalizarObtenerSeguimientos;
        internal event EventHandler<ObtenerParametrosInicialesCompletedEventArgs> FinalizarObtenerParametrosIniciales;
        internal event EventHandler<ObtenerVehiculosCaravanaCompletedEventArgs> FinalizarObtenerVehiculosCaravana;
        internal event EventHandler<ObtenerEscoltasCaravanaCompletedEventArgs> FinalizarObtenerEscoltasCaravana;
        internal event EventHandler<ObtenerPuntosEntregaCaravanaCompletedEventArgs> FinalizarObtenerPuntosEntregaCaravana;
        internal event EventHandler<ObtenerReportesCaravanaCompletedEventArgs> FinalizarObtenerReportesCaravana;
        internal event EventHandler<ObtenerConductoresCaravanaCompletedEventArgs> FinalizarObtenerConductoresCaravana;
        internal event EventHandler<ObtenerReportesYEstadosCompletedEventArgs> FinalizarObtenerReportesYEstados;
        internal event EventHandler<ObtenerRazonesEstadoCompletedEventArgs> FinalizarObtenerRazonesEstado;
        internal event EventHandler<ObtenerPuntosEntregaControlCompletedEventArgs> FinalizarObtenerPuntosEntregaControl;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarGrabarReporteCarretera;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarCambiarNombreCaravana;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarFinalizarRecorrido;
        internal event EventHandler<ObtenerEscoltasCompletedEventArgs> FinalizarObtenerEscoltas;
        internal event EventHandler<ObtenerEscoltasPorCaravanaCompletedEventArgs> FinalizarObtenerEscoltasPorCaravana;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarRetirarEscolta;
        internal event EventHandler<AgregarEscoltaCompletedEventArgs> FinalizarAgregarEscolta;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarRetirarRefuerzoCaravana;
        internal event EventHandler<AgregarRefuerzoCaravanaCompletedEventArgs> FinalizarAgregarRefuerzoCaravana;
        internal event EventHandler<ObtenerRefuerzosDisponiblesCompletedEventArgs> FinalizarObtenerRefuerzosDisponibles;
        internal event EventHandler<ObtenerRefuerzosPorCaravanaCompletedEventArgs> FinalizarObtenerRefuerzosPorCaravana;
        internal event EventHandler<ObtenerCaravanasDisponiblesCompletedEventArgs> FinalizarObtenerCaravanasDisponibles;
        internal event EventHandler<ObtenerCaravanaPlacaCompletedEventArgs> FinalizarObtenerCaravanaPlaca;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarIniciarRecorrido;
        internal event EventHandler<VehiculosPorSalirCompletedEventArgs> FinalizarVehiculosPorSalir;
        internal event EventHandler<ObtenerListaRutasCompletedEventArgs> FinalizarObtenerListaRutas;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarEliminarCaravana;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarEliminarVehiculoCaravana;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarInsertarVehiculoCaravana;
        internal event EventHandler<CrearCaravanaCompletedEventArgs> FinalizarCrearCaravana;
        internal event EventHandler<ObtenerRutasTipoCompletedEventArgs> FinalizarObtenerRutasTipo;
        internal event EventHandler<ObtenerVehiculosParaCambioCompletedEventArgs> FinalizarObtenerVehiculosParaCambio;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarCambiarRutaCaravana;
        internal event EventHandler<ObtenerCaravanasCarreteraCompletedEventArgs> FinalizarObtenerCaravanasCarretera;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarUnirCaravanas;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarCambiarCarnet;

        internal event EventHandler<ObtenerSatelitalesAsignadosCompletedEventArgs> FinalizarObtenerSatelitalesAsignados;
        internal event EventHandler<ObtenerSatelitalesCompletedEventArgs> FinalizarObtenerSatelitales;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarCrearSatelital;
        internal event EventHandler<AsyncCompletedEventArgs> FinalizarAsignarSatelitalAVehiculo;
        internal event EventHandler<ObtenerVehiculoSatelitalPorPlacaCompletedEventArgs> FinalizarObtenerVehiculoSatelitalPorPlaca;
        internal event EventHandler<ObtenerSatelitalPorIdCompletedEventArgs> FinalizarObtenerSatelitalPorId;
        //internal event EventHandler<AsyncCompletedEventArgs> FinalizarGenerarError;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Contexto" /> class.
        /// </summary>
        internal Contexto() { }
        internal Contexto(Action<Exception> action)
        {
            this.ErrorLectura += (s, e) => action(e.Error);
        }
        #endregion

        #region Propiedades
        internal static SeguimientoViewModel CaravanaSeleccionada { get; set; }
        internal static Key CaravanaKeyPressed { get; set; }
        internal static ParametrosIniciales ParametrosIniciales;
        #endregion

        #region Metodos
        //internal void GenerarError()
        //{
        //    var cliente = new ServicioVehiculosClient();
        //    cliente.GenerarErrorCompleted += (s, e) =>
        //    {
        //        if (e.Error != null)
        //            OnErrorLectura(s, e);
        //        else if (this.FinalizarGenerarError != null)
        //            this.FinalizarGenerarError(this, e);
        //        TryClientClose(s);
        //    };
        //    cliente.GenerarErrorAsync();
        //}
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerParametrosIniciales()
        {
            if (ParametrosIniciales != null)
            {
                if (this.FinalizarObtenerParametrosIniciales != null)
                    this.FinalizarObtenerParametrosIniciales(this,
                        new ObtenerParametrosInicialesCompletedEventArgs(new object[] { ParametrosIniciales }, null, false, null));
            }
            else
            {
                var cliente = new ServicioVehiculosClient();
                cliente.ObtenerParametrosInicialesCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        OnErrorLectura(s, e);
                    else if (this.FinalizarObtenerParametrosIniciales != null)
                    {
                        ParametrosIniciales = e.Result;
                        this.FinalizarObtenerParametrosIniciales(this, e);
                    }
                    TryClientClose(s);
                };
                cliente.ObtenerParametrosInicialesAsync();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerFuncionesEscolta()
        {
            if (funcionesEscolta != null && funcionesEscolta.Length > 0)
            {
                if (this.FinalizarObtenerFuncionesEscolta != null)
                    this.FinalizarObtenerFuncionesEscolta
                        (
                        this,
                        new ObtenerFuncionesEscoltaCompletedEventArgs
                            (
                            new object[] { funcionesEscolta },
                            null, false, null
                            )
                        );
                return;
            }
            else
            {
                var cliente = new ServicioVehiculosClient();
                cliente.ObtenerFuncionesEscoltaCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        OnErrorLectura(s, e);
                    else if (this.FinalizarObtenerFuncionesEscolta != null)
                    {
                        funcionesEscolta = e.Result;
                        this.FinalizarObtenerFuncionesEscolta(this, e);
                    }
                    TryClientClose(s);
                };
                cliente.ObtenerFuncionesEscoltaAsync();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerSeguimientos()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerSeguimientosCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerSeguimientos != null)
                    this.FinalizarObtenerSeguimientos(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerSeguimientosAsync(null, null, null);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerVehiculosCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerVehiculosCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerVehiculosCaravana != null)
                    this.FinalizarObtenerVehiculosCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerVehiculosCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerVehiculosCaravana(Guid idCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerVehiculosCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerVehiculosCaravana != null)
                    this.FinalizarObtenerVehiculosCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerVehiculosCaravanaAsync(idCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerEscoltasCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerEscoltasCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerEscoltasCaravana != null)
                    this.FinalizarObtenerEscoltasCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerEscoltasCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerPuntosEntregaCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerPuntosEntregaCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerPuntosEntregaCaravana != null)
                    this.FinalizarObtenerPuntosEntregaCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerPuntosEntregaCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerReportesCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerReportesCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerReportesCaravana != null)
                    this.FinalizarObtenerReportesCaravana(this, e);
                TryClientClose(s);

            };
            cliente.ObtenerReportesCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerConductoresCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerConductoresCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerConductoresCaravana != null)
                    this.FinalizarObtenerConductoresCaravana(this, e);
                TryClientClose(s);

            };
            cliente.ObtenerConductoresCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerReportesYEstados()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerReportesYEstadosCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerReportesYEstados != null)
                    this.FinalizarObtenerReportesYEstados(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerReportesYEstadosAsync(CaravanaSeleccionada.Model.Caravana.finIdRuta);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerRazonesEstado(int idEstadoVehiculo)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerRazonesEstadoCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerRazonesEstado != null)
                    this.FinalizarObtenerRazonesEstado(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerRazonesEstadoAsync(idEstadoVehiculo);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerPuntosEntregaControl()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerPuntosEntregaControlCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerPuntosEntregaControl != null)
                    this.FinalizarObtenerPuntosEntregaControl(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerPuntosEntregaControlAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void GrabarReporteCarretera(int idPuntoRuta, short idEstadoVehiculo, short idRazon, string observacion, Guid[] idPuntosEntrega)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.GrabarReporteCarreteraCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarGrabarReporteCarretera != null)
                    this.FinalizarGrabarReporteCarretera(this, e);
                TryClientClose(s);
            };
            cliente.GrabarReporteCarreteraAsync(
                        CaravanaSeleccionada.IdCaravana,
                        idPuntoRuta,
                        DateTime.Now,
                        idEstadoVehiculo,
                        idRazon,
                        observacion,
                        CaravanaSeleccionada.Model.Caravana.TipoCaravana == TipoCaravana.Urbana ? idPuntosEntrega : null
                );
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void CambiarNombreCaravana(string nuevaDescripcion)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.CambiarNombreCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarCambiarNombreCaravana != null)
                    this.FinalizarCambiarNombreCaravana(this, e);
                TryClientClose(s);
            };
            cliente.CambiarNombreCaravanaAsync(CaravanaSeleccionada.IdCaravana,
                        nuevaDescripcion
                );
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void FinalizarRecorrido()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.FinalizarRecorridoCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarFinalizarRecorrido != null)
                    this.FinalizarFinalizarRecorrido(this, e);
                TryClientClose(s);

            };
            cliente.FinalizarRecorridoAsync(CaravanaSeleccionada.IdCaravana, DateTime.Now);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerEscoltas()
        {
            if (escoltasDisponibles != null && escoltasDisponibles.Length > 0)
            {
                if (this.FinalizarObtenerEscoltas != null)
                    this.FinalizarObtenerEscoltas(this,
                        new ObtenerEscoltasCompletedEventArgs
                            (
                            new object[] { escoltasDisponibles },
                            null, false, null
                            ));
                return;
            }
            else
            {
                var cliente = new ServicioVehiculosClient();
                cliente.ObtenerEscoltasCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        OnErrorLectura(s, e);
                    else if (this.FinalizarObtenerEscoltas != null)
                    {
                        escoltasDisponibles = e.Result;
                        this.FinalizarObtenerEscoltas(this, e);
                    }
                    TryClientClose(s);
                };
                cliente.ObtenerEscoltasAsync();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerEscoltasPorCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerEscoltasPorCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerEscoltasPorCaravana != null)
                    this.FinalizarObtenerEscoltasPorCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerEscoltasPorCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void RetirarEscolta(int[] escoltaCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.RetirarEscoltaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarRetirarEscolta != null)
                    this.FinalizarRetirarEscolta(this, e);
                TryClientClose(s);

            };
            cliente.RetirarEscoltaAsync(escoltaCaravana, DateTime.Now);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void AgregarEscolta(decimal identificacion, short funcion)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.AgregarEscoltaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarAgregarEscolta != null)
                    this.FinalizarAgregarEscolta(this, e);
                TryClientClose(s);
            };
            cliente.AgregarEscoltaAsync(CaravanaSeleccionada.IdCaravana, identificacion, funcion, DateTime.Now);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void RetirarRefuerzoCaravana(Guid[] idRefuerzosCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.RetirarRefuerzoCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarRetirarRefuerzoCaravana != null)
                    this.FinalizarRetirarRefuerzoCaravana(this, e);
                TryClientClose(s);

            };
            cliente.RetirarRefuerzoCaravanaAsync
                (
                idRefuerzosCaravana,
                DateTime.Now
                );
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void AgregarRefuerzoCaravana(decimal identidad, short idFuncionEscolta)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.AgregarRefuerzoCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarAgregarRefuerzoCaravana != null)
                    this.FinalizarAgregarRefuerzoCaravana(this, e);
                TryClientClose(s);

            };
            cliente.AgregarRefuerzoCaravanaAsync
                (
                    CaravanaSeleccionada.IdCaravana,
                    identidad,
                    idFuncionEscolta,
                    DateTime.Now
                );
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerRefuerzosDisponibles()
        {
            if (refuerzosDisponibles != null && refuerzosDisponibles.Length > 0)
            {
                if (this.FinalizarObtenerRefuerzosDisponibles != null)
                    this.FinalizarObtenerRefuerzosDisponibles(this,
                        new ObtenerRefuerzosDisponiblesCompletedEventArgs
                            (
                            new object[] { refuerzosDisponibles },
                            null, false, null
                            ));
            }
            else
            {
                var cliente = new ServicioVehiculosClient();
                cliente.ObtenerRefuerzosDisponiblesCompleted += (s, e) =>
                {
                    if (e.Error != null)
                        OnErrorLectura(s, e);
                    else if (this.FinalizarObtenerRefuerzosDisponibles != null)
                    {
                        refuerzosDisponibles = e.Result;
                        this.FinalizarObtenerRefuerzosDisponibles(this, e);
                    }
                    TryClientClose(s);

                };
                cliente.ObtenerRefuerzosDisponiblesAsync();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerRefuerzosPorCaravana()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerRefuerzosPorCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerRefuerzosPorCaravana != null)
                    this.FinalizarObtenerRefuerzosPorCaravana(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerRefuerzosPorCaravanaAsync(CaravanaSeleccionada.IdCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerCaravanasDisponibles()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerCaravanasDisponiblesCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerCaravanasDisponibles != null)
                    this.FinalizarObtenerCaravanasDisponibles(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerCaravanasDisponiblesAsync();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerCaravanaPlaca(string placa)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerCaravanaPlacaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerCaravanaPlaca != null)
                    this.FinalizarObtenerCaravanaPlaca(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerCaravanaPlacaAsync(placa);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void IniciarRecorrido(Guid idCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.IniciarRecorridoCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarIniciarRecorrido != null)
                    this.FinalizarIniciarRecorrido(this, e);
                TryClientClose(s);

            };
            cliente.IniciarRecorridoAsync(
                    idCaravana,
                    DateTime.Now
                );
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void VehiculosPorSalir()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.VehiculosPorSalirCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarVehiculosPorSalir != null)
                    this.FinalizarVehiculosPorSalir(this, e);
                TryClientClose(s);
            };
            cliente.VehiculosPorSalirAsync();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerListaRutas(TipoCaravana tipoCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerListaRutasCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerListaRutas != null)
                    this.FinalizarObtenerListaRutas(this, e);
                TryClientClose(s);
            };
            cliente.ObtenerListaRutasAsync(tipoCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void EliminarCaravana(Guid[] idCaravana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.EliminarCaravanaCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarEliminarCaravana != null)
                    this.FinalizarEliminarCaravana(this, e);
                TryClientClose(s);

            };
            cliente.EliminarCaravanaAsync(idCaravana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void EliminarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.EliminarVehiculoCaravanaCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarEliminarVehiculoCaravana != null)
                       this.FinalizarEliminarVehiculoCaravana(this, e);
                   TryClientClose(s);
               };
            cliente.EliminarVehiculoCaravanaAsync(caravana, vehiculos);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void InsertarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.InsertarVehiculoCaravanaCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarInsertarVehiculoCaravana != null)
                       this.FinalizarInsertarVehiculoCaravana(this, e);
                   TryClientClose(s);
               };
            cliente.InsertarVehiculoCaravanaAsync(caravana, vehiculos);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void CrearCaravana(Int32 inIdRuta, string vcDescripcion, VehiculoPorSalir vehiculo)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.CrearCaravanaCompleted += (s, e) =>
               {
                   //if (e.Error != null)
                   //{
                   //    OnErrorLectura(s, e);
                   //}
                   //else 
                   if (this.FinalizarCrearCaravana != null)
                       this.FinalizarCrearCaravana(this, e);
                   TryClientClose(s);
               };
            cliente.CrearCaravanaAsync(inIdRuta, vcDescripcion, vehiculo);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerRutasTipo(TipoCaravana urbana)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerRutasTipoCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarObtenerRutasTipo != null)
                       this.FinalizarObtenerRutasTipo(this, e);
                   TryClientClose(s);

               };
            cliente.ObtenerRutasTipoAsync(urbana);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void CambiarRutaCaravana(Guid idCaravana, int idRuta, string descripcion, string ubicacion)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.CambiarRutaCaravanaCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarCambiarRutaCaravana != null)
                       this.FinalizarCambiarRutaCaravana(this, e);
                   TryClientClose(s);

               };
            cliente.CambiarRutaCaravanaAsync(idCaravana, idRuta, descripcion, ubicacion);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void UnirCaravanas(Guid idCaravanaOrigen, Guid idCaravanaDestino)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.UnirCaravanasCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarUnirCaravanas != null)
                       this.FinalizarUnirCaravanas(this, e);
                   TryClientClose(s);
               };
            cliente.UnirCaravanasAsync(idCaravanaOrigen, idCaravanaDestino);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerCaravanasCarretera()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerCaravanasCarreteraCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarObtenerCaravanasCarretera != null)
                       this.FinalizarObtenerCaravanasCarretera(this, e);
                   TryClientClose(s);

               };
            cliente.ObtenerCaravanasCarreteraAsync();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerVehiculosParaCambio(string placaCambio)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerVehiculosParaCambioCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarObtenerVehiculosParaCambio != null)
                       this.FinalizarObtenerVehiculosParaCambio(this, e);
                   TryClientClose(s);

               };
            cliente.ObtenerVehiculosParaCambioAsync(placaCambio);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void CambiarCarnet(string placaOrigen, VehiculoPorSalir vehiculoDestino)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.CambiarCarnetCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarCambiarCarnet != null)
                       this.FinalizarCambiarCarnet(this, e);
                   TryClientClose(s);
               };
            cliente.CambiarCarnetAsync
                (
                CaravanaSeleccionada.IdCaravana,
                ParametrosIniciales.UsuarioActual.fvcIDUbicacion,
                CaravanaSeleccionada.Model.Caravana.finUltimoPunto.Value,
                placaOrigen,
                vehiculoDestino);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerSatelitalesAsignados()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerSatelitalesAsignadosCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerSatelitalesAsignados != null)
                    this.FinalizarObtenerSatelitalesAsignados(this, e);
                TryClientClose(s);

            };
            cliente.ObtenerSatelitalesAsignadosAsync();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerSatelitales()
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerSatelitalesCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarObtenerSatelitales != null)
                    this.FinalizarObtenerSatelitales(this, e);
                TryClientClose(s);

            };
            cliente.ObtenerSatelitalesAsync();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void CrearSatelital(string nombre, string url)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.CrearSatelitalCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarCrearSatelital != null)
                    this.FinalizarCrearSatelital(this, e);
                TryClientClose(s);

            };
            cliente.CrearSatelitalAsync(nombre, url);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.AsignarSatelitalAVehiculoCompleted += (s, e) =>
            {
                if (e.Error != null)
                    OnErrorLectura(s, e);
                else if (this.FinalizarAsignarSatelitalAVehiculo != null)
                    this.FinalizarAsignarSatelitalAVehiculo(this, e);
                TryClientClose(s);

            };
            cliente.AsignarSatelitalAVehiculoAsync(placa, idSatelital, usuario, clave);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerVehiculoSatelitalPorPlaca(string placa)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerVehiculoSatelitalPorPlacaCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarObtenerVehiculoSatelitalPorPlaca != null)
                       this.FinalizarObtenerVehiculoSatelitalPorPlaca(this, e);
                   TryClientClose(s);

               };
            cliente.ObtenerVehiculoSatelitalPorPlacaAsync(placa);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        internal void ObtenerSatelitalPorId(int id)
        {
            var cliente = new ServicioVehiculosClient();
            cliente.ObtenerSatelitalPorIdCompleted += (s, e) =>
               {
                   if (e.Error != null)
                       OnErrorLectura(s, e);
                   else if (this.FinalizarObtenerSatelitalPorId != null)
                       this.FinalizarObtenerSatelitalPorId(this, e);
                   TryClientClose(s);

               };
            cliente.ObtenerSatelitalPorIdAsync(id);
        }

        #endregion

        #region Funciones
        /// <summary>
        /// Se llama para informar de un error de lectura.
        /// </summary>
        /// <param name="s">La fuente del error.</param>
        /// <param name="e">La instancia de <see cref="System.ComponentModel.AsyncCompletedEventArgs"/> que contiene los datos del evento.</param>
        /// 01/12/2010 By Jaimir Guerrero
        private void OnErrorLectura(object s, AsyncCompletedEventArgs e)
        {
            if (this.ErrorLectura != null)
                this.ErrorLectura(this, e);

            ServicioVehiculosClient source = s as ServicioVehiculosClient;
            if (source != null)
            {
                try
                {
                    source.Abort();
                }
                catch { }
            }
        }
        /// <summary>
        /// Trata de cerrar el cliente de forma normal ante fallos en el canal de comunicación.
        /// </summary>
        /// <param name="cliente">El cliente que generó el fallo.</param>
        /// 01/12/2010 By Jaimir Guerrero
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void TryClientClose(object cliente)
        {
            ServicioVehiculosClient client = cliente as ServicioVehiculosClient;
            try
            {
                if (client != null && client.State != CommunicationState.Closed)
                    client.Close();
            }
            catch { }

            try
            {
                if (client != null && client.State == CommunicationState.Faulted)
                    client.Abort();
            }
            catch { }
        }
        #endregion
    }
}
