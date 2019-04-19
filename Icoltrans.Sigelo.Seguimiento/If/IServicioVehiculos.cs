using System;
using System.ServiceModel;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Entity.Nomina;
using Icoltrans.Sigelo.Entity.Produccion;
using Icoltrans.Sigelo.Entity.Vehiculos;

namespace Icoltrans.Sigelo.ControlSeguimiento
{
    [ServiceContract]
    public interface IServicioVehiculos
    {
        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        ParametrosIniciales ObtenerParametrosIniciales();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Seguimiento[] ObtenerSeguimientos(TipoCaravana? tipo, string origen, string destino);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        DetalleCaravana ObtenerDetalleCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        ConductoresCaravana ObtenerConductoresCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        EscoltasCaravana ObtenerEscoltasCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        ReporteCaravana ObtenerReportesCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        VehiculosCaravana ObtenerVehiculosCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        PuntosEntregasCaravana ObtenerPuntosEntregaCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        PuntosEntregaCaravana2 ObtenerPuntosEntregaControl(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        FuncionEscolta[] ObtenerFuncionesEscolta();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        int AgregarEscolta(Guid caravana, decimal identificacion, short funcion, DateTime inicio);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void RetirarEscolta(int[] escoltaCaravana, DateTime final);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Refuerzo[] ObtenerRefuerzosDisponibles();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Guid AgregarRefuerzoCaravana(Guid caravana, decimal identidad, short idFuncionEscolta, DateTime inicio);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void RetirarRefuerzoCaravana(Guid[] idRefuerzoCaravana, DateTime final);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Escolta1[] ObtenerEscoltas();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void CambiarNombreCaravana(Guid idCaravana, string descripcion);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        ReportesYEstados ObtenerReportesYEstados(int idRuta);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void GrabarReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime reporte, short idEstadoVehiculo, short idRazon, string observacion, Guid[] idPuntoEntrega);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        RazonEstado[] ObtenerRazonesEstado(int idEstadoVehiculo);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        CaravanaDisponible[] ObtenerCaravanasDisponibles();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        CaravanaDisponible[] ObtenerCaravanasCarretera();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void IniciarRecorrido(Guid caravana, DateTime hora);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void FinalizarRecorrido(Guid caravana, DateTime hora);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        VehiculoPorSalir[] VehiculosPorSalir();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void InsertarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculos);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void EliminarVehiculoCaravana(Guid caravana, VehiculoPorSalir[] vehiculosPorSalir);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Guid CrearCaravana(int idRuta, string descripcion, VehiculoPorSalir vehiculo);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void EliminarCaravana(Guid[] idCaravana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Ruta[] ObtenerListaRutas(TipoCaravana urbana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Guid ObtenerCaravanaPlaca(string placa);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Ruta[] ObtenerRutasTipo(TipoCaravana urbana);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio);
        
        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void CambiarRutaCaravana(Guid idCaravana, int idRuta, string descripcion, string ubicacion);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void UnirCaravanas(Guid idCaravanaOrigen, Guid idCaravanaDestino);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void CambiarCarnet(Guid idCaravana, string ubicacion, int idPuntoRuta, string placaOrigen, VehiculoPorSalir vehiculoDestino);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        VehiculoSatelital[] ObtenerSatelitalesAsignados();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Satelital[] ObtenerSatelitales();

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void CrearSatelital(string nombre, string url);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        VehiculoSatelital ObtenerVehiculoSatelitalPorPlaca(string placa);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        Satelital ObtenerSatelitalPorId(int id);

        [OperationContract]
        [FaultContract(typeof(ServerErrorWrapper))]
        void GenerarError();
    }
}
