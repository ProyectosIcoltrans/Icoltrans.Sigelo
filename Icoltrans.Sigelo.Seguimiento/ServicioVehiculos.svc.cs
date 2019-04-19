using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Entity.Nomina;
using Icoltrans.Sigelo.Entity.Produccion;
using Icoltrans.Sigelo.Entity.Seguridad;
using Icoltrans.Sigelo.Entity.Vehiculos;
using Icoltrans.Sigelo.Rules.Maestros;
using Icoltrans.Sigelo.Rules.Nomina;
using Icoltrans.Sigelo.Rules.Produccion;
using Icoltrans.Sigelo.Rules.Seguridad;
using Icoltrans.Sigelo.Rules.Vehiculos;

namespace Icoltrans.Sigelo.ControlSeguimiento
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class ServicioVehiculos : IServicioVehiculos
    {
        #region IServicioVehiculos Members
        public ParametrosIniciales ObtenerParametrosIniciales()
        {
            try
            {
                ParametrosIniciales retorno = new ParametrosIniciales();

                Parallel.Invoke(
                    () =>
                    {
                        using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                        {
                            retorno.Destinos = vehiculos.ObtenerCiudadesRutaDestino();
                            retorno.Origenes = vehiculos.ObtenerCiudadesRutaOrigen();
                        }
                    },
                    () =>
                    {
                        using (ControladorMaestros maestros = new ControladorMaestros())
                            retorno.Parametros = maestros.ObtenerParametros();
                    },
                     () =>
                     {
                         using (ControladorSeguridad seguridad = new ControladorSeguridad())
                             retorno.PerfilesUsuario = seguridad.ObtenerOpcionesUsuario(Modulo.Vehiculos);
                     },
                    () =>
                    {
                        using (ControladorNomina nomina = new ControladorNomina())
                            retorno.UsuarioActual = nomina.ObtenerInformacionUsuario();
                    }
                    );

                return retorno;
            }
            catch (AggregateException error)
            {
                using (EventLog registro = new EventLog())
                    foreach (var item in error.InnerExceptions)
                        registro.LogError(item);

                error = error.Flatten();
                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Seguimiento[] ObtenerSeguimientos(TipoCaravana? tipo, string origen, string destino)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerSeguimientos(tipo, origen, destino);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public DetalleCaravana ObtenerDetalleCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                using (ControladorProduccion produccion = new ControladorProduccion())
                {
                    return new DetalleCaravana
                    {
                        Conductores = vehiculos.ObtenerConductoresCaravana(caravana),
                        Escoltas = vehiculos.ObtenerEscoltasCaravana(caravana),
                        Reportes = vehiculos.ObtenerReportesCaravana(caravana),
                        Vehiculos = vehiculos.ObtenerVehiculosCaravana(caravana),
                        PuntosEntrega = produccion.ObtenerPuntosEntregaPorCaravana(caravana)
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public ConductoresCaravana ObtenerConductoresCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                {
                    return new ConductoresCaravana()
                    {
                        Conductores = vehiculos.ObtenerConductoresCaravana(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public EscoltasCaravana ObtenerEscoltasCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                {
                    return new EscoltasCaravana()
                    {
                        Escoltas = vehiculos.ObtenerEscoltasCaravana(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public ReporteCaravana ObtenerReportesCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                {
                    return new ReporteCaravana()
                    {
                        Reportes = vehiculos.ObtenerReportesCaravana(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public VehiculosCaravana ObtenerVehiculosCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                {
                    return new VehiculosCaravana()
                    {
                        Vehiculos = vehiculos.ObtenerVehiculosCaravana(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public PuntosEntregasCaravana ObtenerPuntosEntregaCaravana(Guid caravana)
        {
            try
            {
                using (ControladorProduccion produccion = new ControladorProduccion())
                {
                    return new PuntosEntregasCaravana()
                    {
                        PuntosEntregas = produccion.ObtenerPuntosEntregaPorCaravana(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public PuntosEntregaCaravana2 ObtenerPuntosEntregaControl(Guid caravana)
        {
            try
            {
                using (ControladorProduccion produccion = new ControladorProduccion())
                {
                    return new PuntosEntregaCaravana2()
                    {
                        PuntosEntrega = produccion.ObtenerPuntoEntregaParaControl(caravana),
                        CaravanaId = caravana
                    };
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public FuncionEscolta[] ObtenerFuncionesEscolta()
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerFuncionesEscoltas();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public int AgregarEscolta(Guid caravana, decimal identificacion, short funcion, DateTime inicio)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.AgregarEscolta(caravana, identificacion, funcion, inicio);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void RetirarEscolta(int[] escoltaCaravana, DateTime final)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.RetirarEscolta(escoltaCaravana, final);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerRefuerzosPorCaravana(caravana);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Refuerzo[] ObtenerRefuerzosDisponibles()
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerRefuerzosDisponibles();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Guid AgregarRefuerzoCaravana(Guid caravana, decimal identidad, short idFuncionEscolta, DateTime inicio)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.AgregarRefuerzoCaravana(caravana, identidad, idFuncionEscolta, inicio);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void RetirarRefuerzoCaravana(Guid[] idRefuerzoCaravana, DateTime final)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.RetirarRefuerzoCaravana(idRefuerzoCaravana, final);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Escolta1[] ObtenerEscoltas()
        {
            try
            {
                using (ControladorNomina nomina = new ControladorNomina())
                    return nomina.ObtenerEscoltas();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerEscoltasPorCaravana(caravana);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void CambiarNombreCaravana(Guid idCaravana, string descripcion)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.CambiarNombreCaravana(idCaravana, descripcion);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public ReportesYEstados ObtenerReportesYEstados(int idRuta)
        {
            try
            {
                ReportesYEstados reportesYEstados = new ReportesYEstados();

                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                {
                    Parallel.Invoke(
                    () => {
                        reportesYEstados.PuntosReporte = vehiculos.PuntosReporte(idRuta);
                    },
                    () => {
                        reportesYEstados.Estados = vehiculos.ObtenerEstadosVehiculo();
                    });
                }
                return reportesYEstados;
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void GrabarReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime reporte, short idEstadoVehiculo, short idRazon, string observacion, Guid[] idPuntoEntrega)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.GrabarReporteCarretera(idCaravana, idPuntoRuta, reporte, idEstadoVehiculo, idRazon, observacion, idPuntoEntrega);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public RazonEstado[] ObtenerRazonesEstado(int idEstadoVehiculo)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerRazonesEstado(idEstadoVehiculo);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public CaravanaDisponible[] ObtenerCaravanasDisponibles()
        {
            try
            {
                Usuario t = null;
                using (ControladorNomina nomina = new ControladorNomina())
                    t = nomina.ObtenerInformacionUsuario();

                if (t != null)
                    using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                        return vehiculos.ObtenerCaravanasDisponibles(t.IdCOperativo);
                else
                    return null;
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void IniciarRecorrido(Guid caravana, DateTime hora)
        {
            try
            {
                Usuario t = null;

                using (ControladorNomina nomina = new ControladorNomina())
                    t = nomina.ObtenerInformacionUsuario();

                if (t != null)
                {
                    Sucursal k = null;
                    using (ControladorProduccion produccion = new ControladorProduccion())
                        k = produccion.ObtenerSucursal(t.fsmIdSucursal);

                    if (k != null)
                        using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                            vehiculos.IniciarRecorrido(caravana, hora, k.fidPuntoEntrega);
                }
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void FinalizarRecorrido(Guid caravana, DateTime hora)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.FinalizarRecorrido(caravana, hora);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public VehiculoPorSalir[] VehiculosPorSalir()
        {
            try
            {
                Usuario t = null;
                using (ControladorNomina nomina = new ControladorNomina())
                    t = nomina.ObtenerInformacionUsuario();
                if (t != null)
                    using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                        return vehiculos.VehiculosPorSalir(t.IdCOperativo);
                else
                    return null;
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void InsertarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos)
        {
            try
            {
                using (ControladorVehiculos controlador = new ControladorVehiculos())
                    controlador.InsertarVehiculoCaravana(caravana, vehiculos);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void EliminarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculosPorSalir)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.EliminarVehiculoCaravana(caravana, vehiculosPorSalir);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Guid CrearCaravana(int idRuta, string descripcion, VehiculoPorSalir vehiculo)
        {
            try
            {
                using (ControladorVehiculos controlador = new ControladorVehiculos())
                    return controlador.CrearCaravana(idRuta, descripcion, vehiculo);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void EliminarCaravana(Guid[] idCaravana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.EliminarCaravana(idCaravana);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Ruta[] ObtenerListaRutas(TipoCaravana urbana)
        {
            try
            {
                Usuario t = null;
                using (ControladorNomina nomina = new ControladorNomina())
                    t = nomina.ObtenerInformacionUsuario();

                if (t != null)
                    using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                        return vehiculos.ObtenerListaRutas(urbana, t.IdCOperativo);
                else
                    return null;
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Guid ObtenerCaravanaPlaca(string placa)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerCaravanaPlaca(placa);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void GenerarError()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception error)
            {
                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Ruta[] ObtenerRutasTipo(TipoCaravana urbana)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerRutasTipo(urbana);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    return vehiculos.ObtenerVehiculosParaCambio(placaCambio);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void CambiarRutaCaravana(Guid idCaravana, int idRuta, string descripcion, string ubicacion)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.CambiarRutaCaravana(idCaravana, idRuta, descripcion, ubicacion);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void UnirCaravanas(Guid idCaravanaOrigen, Guid idCaravanaDestino)
        {
            try
            {
                using (ControladorVehiculos vehiculos = new ControladorVehiculos())
                    vehiculos.UnirCaravanas(idCaravanaOrigen, idCaravanaDestino);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public CaravanaDisponible[] ObtenerCaravanasCarretera()
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    return rule.ObtenerCaravanasCarretera();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void CambiarCarnet(Guid idCaravana, string ubicacion, int idPuntoRuta, string placaOrigen, VehiculoPorSalir vehiculoDestino)
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    rule.CambiarCarnet(idCaravana, placaOrigen, vehiculoDestino, ubicacion, idPuntoRuta);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public VehiculoSatelital[] ObtenerSatelitalesAsignados()
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    return rule.ObtenerSatelitalesAsignados();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Satelital[] ObtenerSatelitales()
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    return rule.ObtenerSatelitales();
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void CrearSatelital(string nombre, string url)
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    rule.CrearSatelital(nombre, url);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave)
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    rule.AsignarSatelitalAVehiculo(placa, idSatelital, usuario, clave);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public VehiculoSatelital ObtenerVehiculoSatelitalPorPlaca(string placa)
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    return rule.ObtenerVehiculoSatelitalPorPlaca(placa);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        public Satelital ObtenerSatelitalPorId(int id)
        {
            try
            {
                using (ControladorVehiculos rule = new ControladorVehiculos())
                    return rule.ObtenerSatelitalPorId(id);
            }
            catch (Exception error)
            {
                using (EventLog registro = new EventLog())
                    registro.LogError(error);

                throw new FaultException<ServerErrorWrapper>(new ServerErrorWrapper(error), error.Message);
            }
        }
        #endregion
    }
}
