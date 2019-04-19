using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Vehiculos;
using Icoltrans.Sigelo.Rules.ComProxy;
using Icoltrans.Sigelo.Data.Vehiculos;

namespace Icoltrans.Sigelo.Rules.Vehiculos
{
    public sealed class ControladorVehiculos : IDisposable
        //, IControladorVehiculos
    {
        #region Variables
        private VehiculosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ControladorVehiculos"/> class.
        /// </summary>
        public ControladorVehiculos()
        {
            modelo = new VehiculosModelo();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.modelo != null)
                this.modelo.Dispose();
        }
        #endregion

        #region Metodos
        public Seguimiento[] ObtenerSeguimientos(TipoCaravana? tipo, string origen, string destino)
        {
            return VehiculosModeloDapper.ObtenerInstancia.ObtenerSeguimientos(tipo, origen, destino);
        }
        public CiudadRuta[] ObtenerCiudadesRutaDestino()
        {
            //return modelo.ObtenerCiudadesRutaDestino();
            return VehiculosModeloDapper.ObtenerInstancia.ObtenerCiudadesRuta();
        }
        public CiudadRuta[] ObtenerCiudadesRutaOrigen()
        {
            return VehiculosModeloDapper.ObtenerInstancia.ObtenerCiudadesRuta();
        }
        public Vehiculo[] ObtenerVehiculosCaravana(Guid caravana)
        {
            return modelo.ObtenerVehiculosCaravana(caravana);
        }
        public Conductor[] ObtenerConductoresCaravana(Guid caravana)
        {
            return modelo.ObtenerCoductoresCaravana(caravana);
        }
        public Escolta[] ObtenerEscoltasCaravana(Guid caravana)
        {
            return modelo.ObtenerEscoltasCaravana(caravana);
        }
        public Reporte[] ObtenerReportesCaravana(Guid caravana)
        {
            return modelo.ObtenerReportesCaravana(caravana);
        }
        public FuncionEscolta[] ObtenerFuncionesEscoltas()
        {
            return VehiculosModeloDapper.ObtenerInstancia.ObtenerFuncionesEscolta();
        }
        public Int32 AgregarEscolta(Guid caravana, decimal identificacion, Int16 funcion, DateTime inicio)
        {
            return modelo.AgregarEscolta(caravana, identificacion, funcion, inicio);
        }
        public void RetirarEscolta(Int32[] escoltas, DateTime final)
        {
            Parallel.ForEach(escoltas, escolta =>
            {
                modelo.RetirarEscolta(escolta, final);
            });
        }
        public RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana)
        {
            return modelo.ObtenerRefuerzosPorCaravana(caravana);
        }
        public Refuerzo[] ObtenerRefuerzosDisponibles()
        {
            return modelo.ObtenerRefuerzosDisponibles();
        }
        public EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana)
        {
            return modelo.ObtenerEscoltasPorCaravana(caravana);
        }
        public Guid AgregarRefuerzoCaravana(Guid caravana, decimal identidad, Int16 idFuncionEscolta, DateTime inicio)
        {
            return modelo.AgregarRefuerzoCaravana(caravana, identidad, idFuncionEscolta, inicio);
        }
        public void RetirarRefuerzoCaravana(Guid[] refuerzos, DateTime final)
        {
            Parallel.ForEach(refuerzos, refuerzo =>
            {
                modelo.RetirarRefuerzoCaravana(refuerzo, final);
            });
        }
        public void CambiarNombreCaravana(Guid idCaravana, string descripcion)
        {
            modelo.CambiarNombreCaravana(idCaravana, descripcion);
        }
        public RazonEstado[] ObtenerRazonesEstado(Int32 idEstadoVehiculo)
        {
            return modelo.ObtenerRazonesEstado(idEstadoVehiculo);
        }
        public EstadoVehiculo[] ObtenerEstadosVehiculo()
        {
            return modelo.ObtenerEstadosVehiculo();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void GrabarReporteCarretera(Guid idCaravana, Int32 idPuntoRuta, DateTime reporte, Int16 idEstadoVehiculo, Int16 idRazon, string observacion, Guid[] idPuntosEntrega)
        {
            //observacion = string.Format(CultureInfo.InvariantCulture, "{0}. Operador: {1}", observacion, Comun.Usuario);
            //using (VehiculosControlCarretera model = new VehiculosControlCarretera())
            //{
            //    foreach (var pe in idPuntosEntrega)
            //    {
            //        modelo.GrabarReporteCarretera(idCaravana, idPuntoRuta, reporte, idEstadoVehiculo, observacion, pe, Comun.Usuario);
            //    }

            //    VehiculoCaravana[] vehiculos = modelo.ObtenerVehiculosPorCaravana(idCaravana);

            //    foreach (var vehiculo in vehiculos)
            //    {
            //        modelo.ObtenerEstadosVehiculo();
            //    }
            //}

            //using (VehiculosNovedades interop = new VehiculosNovedades()) , reporte , Comun.Usuario
            //modelo.GrabarReporteCarretera(idCaravana, idPuntoRuta, reporte, idEstadoVehiculo, observacion, idPuntosEntrega, Comun.Usuario);
        }
        public SiguientePuntoControl[] PuntosReporte(Int32 idRuta)
        {
            return VehiculosModeloDapper.ObtenerInstancia.PuntosReporte(idRuta);
        }
        public CaravanaDisponible[] ObtenerCaravanasDisponibles(Int16 idCentro)
        {
            return modelo.ObtenerCaravanasDisponibles(idCentro);
        }
        public CaravanaDisponible[] ObtenerCaravanasCarretera()
        {
            return modelo.ObtenerCaravanasCarretera();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void IniciarRecorrido(Guid caravana, DateTime hora, Guid punto)
        {
            //using (VehiculosNovedades interop = new VehiculosNovedades())
            //    interop.IniciarRecorrido(caravana, hora, punto);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void FinalizarRecorrido(Guid caravana, DateTime hora)
        {
            //using (VehiculosNovedades interop = new VehiculosNovedades())
            //    interop.FinalizarRecorrido(caravana, hora);
        }
        public VehiculoPorSalir[] VehiculosPorSalir(Int16 idCentro)
        {
            return modelo.ObtenerVehiculosPorSalir(idCentro);
        }
        public void InsertarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos)
        {
            Parallel.ForEach(vehiculos, vehiculo =>
            {
                modelo.InsertarVehiculoCaravana(caravana, vehiculo.finNumeroCarnet, vehiculo.fnuManifiesto);
            });
        }
        public void EliminarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos)
        {
            Parallel.ForEach(vehiculos, vehiculo =>
            {
                modelo.EliminarVehiculoCaravana(caravana, vehiculo.finNumeroCarnet);
            });
        }
        public Guid CrearCaravana(Int32 idRuta, string descripcion, VehiculoPorSalir vehiculo)
        {
            Guid caravanaId = VehiculosModeloDapper.ObtenerInstancia.CrearCaravana(idRuta, descripcion);

            InsertarVehiculoCaravana(caravanaId, new VehiculoPorSalir[] { vehiculo });

            return caravanaId;
        }
        public void EliminarCaravana(Guid[] caravanas)
        {
            Parallel.ForEach(caravanas, caravana =>
            {
                modelo.EliminarCaravana(caravana);
            });
        }
        public Guid ObtenerCaravanaPlaca(string placa)
        {
            return modelo.ObtenerCaravanaPlaca(placa);
        }
        public Ruta[] ObtenerListaRutas(TipoCaravana urbana, Int16 idSucursal)
        {
            return modelo.ObtenerRutasCentroOperativo(urbana, idSucursal);
        }
        public Ruta[] ObtenerRutasTipo(TipoCaravana urbana)
        {
            return modelo.ObtenerRutasTipo(urbana);
        }
        public VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio)
        {
            if (string.IsNullOrEmpty(placaCambio))
                placaCambio = null;
            return modelo.ObtenerVehiculosParaCambio(placaCambio);
        }
        public void CambiarRutaCaravana(Guid idCaravana, int idRuta, string descripcion, string ubicacion)
        {
            VehiculoCaravana[] vehiculos = modelo.ObtenerVehiculosPorCaravana(idCaravana);
            if (vehiculos == null || vehiculos.Length == 0)
                return;
            foreach (var item in vehiculos)
            {
                item.Estado = "B";
                item.NoCambiar = true;
            }

            Guid caravanaNuevaId = modelo.CrearCaravana(idRuta, descripcion);
            //using (VehiculosNovedades interop = new VehiculosNovedades())
            //    interop.CambioCarvanas(idCaravana, caravanaNuevaId, true, true, false, ubicacion, vehiculos);
        }
        public void UnirCaravanas(Guid idCaravanaOrigen, Guid idCaravanaDestino)
        {
            VehiculoCaravana[] vehiculos = null;
            vehiculos = modelo.ObtenerVehiculosPorCaravana(idCaravanaOrigen);
            if (vehiculos == null || vehiculos.Length == 0)
                return;
            foreach (var item in vehiculos)
            {
                item.Estado = "B";
                item.NoCambiar = true;
            }

            //using (VehiculosNovedades interop = new VehiculosNovedades())
            //    interop.CambioCarvanas(idCaravanaOrigen, idCaravanaDestino, false, true, false, string.Empty, vehiculos);
        }
        public void CambiarCarnet(Guid idCaravana, string placaOrigen, VehiculoPorSalir vehiculoDestino, string ubicacion, int idPuntoRuta)
        {
            VehiculoCaravana[] vehiculos = modelo.ObtenerVehiculosPorCaravana(idCaravana);
            if (vehiculos == null || vehiculos.Length == 0)
                return;

            VehiculoCaravana vehiculo = vehiculos.FirstOrDefault(p => p.Vehiculo.Placa == placaOrigen);
            if (vehiculo == null)
                return;

            string mensaje = string.Format("Se Genero Cambio de Carnet en Tiempo de Caravana Nuevo Carnet: {0}", vehiculoDestino.finNumeroCarnet);
            VehiculosManifiestos interopManifiestos = null;
            VehiculosVehiculo interopVehiculo = null;
            VehiculosConductores interopConductores = null;
            VehiculosControlCarretera interopControlCarretera = null;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    /*---------------------------------------------*/
                    interopManifiestos = new VehiculosManifiestos();
                    interopManifiestos.ActualizaCopias(vehiculo.Manifiesto.Value);
                    interopManifiestos.ActualizaCarnet(vehiculo.Manifiesto.Value, vehiculoDestino.finNumeroCarnet, vehiculo.Vehiculo.Complemento);
                    /*---------------------------------------------*/
                    interopVehiculo = new VehiculosVehiculo();
                    if (!string.IsNullOrEmpty(vehiculo.Vehiculo.Complemento))
                        interopVehiculo.ActualizaEstado(vehiculo.Vehiculo.Complemento, 15, 1, mensaje, null, ubicacion);

                    interopVehiculo.ActualizaEstado(vehiculo.Vehiculo.Placa, 15, 1, mensaje, null, ubicacion);
                    /*---------------------------------------------*/
                    interopConductores = new VehiculosConductores();
                    interopConductores.CambiarEstado(vehiculo.Conductor.Numero, 1, 15, mensaje);
                    interopConductores.ActualizarUbicaConductor(vehiculo.Conductor.Numero, ubicacion);

                    interopConductores.CambiarEstado(vehiculoDestino.IdentidadConductor.Value, 1, 15, mensaje);
                    interopConductores.ActualizarUbicaConductor(vehiculoDestino.IdentidadConductor.Value, ubicacion);
                    /*---------------------------------------------*/
                    interopControlCarretera = new VehiculosControlCarretera();
                    interopControlCarretera.Insertar(idCaravana, idPuntoRuta, 15, mensaje, null);
                    /*---------------------------------------------*/
                    scope.Complete();
                }
            }
            finally
            {
                if (interopManifiestos != null)
                    interopManifiestos.Dispose();
                if (interopVehiculo != null)
                    interopVehiculo.Dispose();
                if (interopConductores != null)
                    interopConductores.Dispose();
                if (interopControlCarretera != null)
                    interopControlCarretera.Dispose();

                interopManifiestos = null;
                interopVehiculo = null;
                interopConductores = null;
                interopControlCarretera = null;
            }
        }

        public VehiculoSatelital[] ObtenerSatelitalesAsignados()
        {
            return modelo.ObtenerSatelitalesAsignados();
        }

        public Satelital[] ObtenerSatelitales()
        {
            return modelo.ObtenerSatelitales();
        }

        public void CrearSatelital(string nombre, string url)
        {
            modelo.CrearSatelital(nombre, url);
        }

        public void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave)
        {
            modelo.AsignarSatelitalAVehiculo(placa, idSatelital, usuario, clave);
        }
        public VehiculoSatelital ObtenerVehiculoSatelitalPorPlaca(string placa)
        {
            return modelo.ObtenerVehiculoSatelitalPorPlaca(placa);
        }
        public Satelital ObtenerSatelitalPorId(int id)
        {
            return modelo.ObtenerSatelitalPorId(id);
        }
        #endregion
    }
}
