using System;
using Icoltrans.Sigelo.Entity.Vehiculos;

namespace Icoltrans.Sigelo.Rules.Vehiculos
{
    public interface IControladorVehiculos
    {
        int AgregarEscolta(Guid caravana, decimal identificacion, short funcion, DateTime inicio);
        Guid AgregarRefuerzoCaravana(Guid caravana, decimal identidad, short idFuncionEscolta, DateTime inicio);
        void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave);
        void CambiarCarnet(Guid idCaravana, string placaOrigen, VehiculoPorSalir vehiculoDestino, string ubicacion, int idPuntoRuta);
        void CambiarNombreCaravana(Guid idCaravana, string descripcion);
        void CambiarRutaCaravana(Guid idCaravana, int idRuta, string descripcion, string ubicacion);
        Guid CrearCaravana(int idRuta, string descripcion, VehiculoPorSalir vehiculo);
        void CrearSatelital(string nombre, string url);
        void Dispose();
        void EliminarCaravana(Guid[] caravanas);
        void EliminarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos);
        void FinalizarRecorrido(Guid caravana, DateTime hora);
        void GrabarReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime reporte, short idEstadoVehiculo, short idRazon, string observacion, Guid[] idPuntosEntrega);
        void IniciarRecorrido(Guid caravana, DateTime hora, Guid punto);
        void InsertarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos);
        Guid ObtenerCaravanaPlaca(string placa);
        CaravanaDisponible[] ObtenerCaravanasCarretera();
        CaravanaDisponible[] ObtenerCaravanasDisponibles(short idCentro);
        CiudadRuta[] ObtenerCiudadesRutaDestino();
        CiudadRuta[] ObtenerCiudadesRutaOrigen();
        Conductor[] ObtenerConductoresCaravana(Guid caravana);
        Escolta[] ObtenerEscoltasCaravana(Guid caravana);
        EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana);
        EstadoVehiculo[] ObtenerEstadosVehiculo();
        FuncionEscolta[] ObtenerFuncionesEscoltas();
        Ruta[] ObtenerListaRutas(TipoCaravana urbana, short idSucursal);
        RazonEstado[] ObtenerRazonesEstado(int idEstadoVehiculo);
        Refuerzo[] ObtenerRefuerzosDisponibles();
        RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana);
        Reporte[] ObtenerReportesCaravana(Guid caravana);
        Ruta[] ObtenerRutasTipo(TipoCaravana urbana);
        Satelital[] ObtenerSatelitales();
        VehiculoSatelital[] ObtenerSatelitalesAsignados();
        Satelital ObtenerSatelitalPorId(int id);
        Seguimiento[] ObtenerSeguimientos(TipoCaravana? tipo, string origen, string destino);
        VehiculoSatelital ObtenerVehiculoSatelitalPorPlaca(string placa);
        Vehiculo[] ObtenerVehiculosCaravana(Guid caravana);
        VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio);
        SiguientePuntoControl[] PuntosReporte(int idRuta);
        void RetirarEscolta(int[] escoltas, DateTime final);
        void RetirarRefuerzoCaravana(Guid[] refuerzos, DateTime final);
        void UnirCaravanas(Guid idCaravanaOrigen, Guid idCaravanaDestino);
        VehiculoPorSalir[] VehiculosPorSalir(short idCentro);
    }
}