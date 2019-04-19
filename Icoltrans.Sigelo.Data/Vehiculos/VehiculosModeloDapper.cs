namespace Icoltrans.Sigelo.Data.Vehiculos
{
    using Dapper;
    using DapperExtensions;
    using Icoltrans.Sigelo.Data.Common;
    using Icoltrans.Sigelo.Data.Common.Interface;
    using Icoltrans.Sigelo.Entity.Vehiculos;
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.InteropServices;
    /// <summary>
    /// Clase que maneja el contexto de la base de datos.
    /// </summary>
    /// <typeparam name="T">Clase del contexto</typeparam>
    /// <remarks>
    /// Autor:                  Alex Mauricio Paredes Celorio.
    /// Fecha:                  23/09/2016
    /// Ultima Modificacion:    23/09/2016
    /// </remarks>
    public class VehiculosModeloDapper : IDisposable
    {
        #region Atributos
        private static readonly Lazy<VehiculosModeloDapper> instancia = new Lazy<VehiculosModeloDapper>(() => new VehiculosModeloDapper());
        private string connectionString = string.Empty;
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public string DataBase
        {
            set
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[value].ToString();
            }
        }
        #endregion
        #region Constructor
        public VehiculosModeloDapper()
        {
            DbContextScope<Seguimiento>.ObtenerInstancia.DataBase = "Vehiculos";
        }
        #endregion
        #region Singleton
        public static VehiculosModeloDapper ObtenerInstancia
        {
            get
            {
                return instancia.Value;
            }
        }
        #endregion
        #region Métodos
        public Guid ObtenerCaravanaPlaca(string placa)
        {
            Guid idCaravana = Guid.Empty;
            try
            {
                idCaravana = DbContextScope<Seguimiento>.ObtenerInstancia.QuerySP<Guid>(
                    "dbo.stpQryCaravanaPlaca", new
                    {
                        vcPlaca = placa
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return idCaravana;
        }
        public Seguimiento[] ObtenerSeguimientos(TipoCaravana? urbana, string idUbicacionOrigen, string idUbicacionDestino)
        {
            Seguimiento[] seguimiento;
            try
            {
                seguimiento = DbContextScope<Seguimiento>.ObtenerInstancia.QuerySPMapped(
                    "dbo.stpQrySeguimientoNew", new
                    {
                        btUrbana = urbana,
                        vcIDUbicacionOrigen = idUbicacionOrigen,
                        vcIDUbicacionDestino = idUbicacionDestino
                    }).Select(obj => new Seguimiento
                    {
                        ftxObservacion = (string)obj.ftxObservacion,
                        destino = (string)obj.Destino,
                        fbtUrbana = (int)obj.fbtUrbana,
                        fdtSalida = (DateTime)obj.fdtSalida,
                        fdtSiguientePunto = this.DiferenciaFecha((DateTime)obj.fdtUltimoPunto, (int)obj.ftyMinutosReporte, VehiculosModelo.ToNullableShort(((Nullable<byte>)obj.ftyEstReporte).ToString())),//((DateTime)obj.fdtUltimoPunto).AddMinutes((int)obj.ftyMinutosReporte),
                        fdtUltimoPunto = (DateTime)obj.fdtUltimoPunto,
                        fidCaravana = (Guid)obj.fidCaravana,
                        finIdRuta = (int)obj.finIdRuta,
                        finSiguientePunto = VehiculosModelo.ToNullableInt(((Nullable<int>)obj.finSiguientePunto).ToString()),
                        finUltimoPunto = VehiculosModelo.ToNullableInt(((Nullable<int>)obj.finUltimoPunto).ToString()),
                        frlDiferencia = (new TimeSpan(this.DiferenciaFecha((DateTime)obj.fdtUltimoPunto, (int)obj.ftyMinutosReporte, VehiculosModelo.ToNullableShort(((Nullable<byte>)obj.ftyEstReporte).ToString())).Ticks)).TotalMinutes,
                        ftyEstAlerta = VehiculosModelo.ToNullableShort(((Nullable<byte>)obj.ftyEstAlerta).ToString()),
                        ftyEstReporte = VehiculosModelo.ToNullableShort(((Nullable<byte>)obj.ftyEstReporte).ToString()),
                        ftyMinutosAlerta = (int)obj.ftyMinutosAlerta,
                        ftyMinutosReporte = (int)obj.ftyMinutosReporte,
                        fvcDescripcion = (string)obj.fvcDescripcion,
                        fvcEstado = (string)obj.fvcEstado,
                        fvcSiguientePunto = (string)obj.fvcSiguientePunto,
                        fvcUltimoPunto = (string)obj.fvcUltimoPunto,
                        origen = (string)obj.Origen,
                        ruta = (string)obj.RUTA,
                        Vehiculos = (int)obj.Vehiculos,
                        Satelital = (string)obj.Satelital,
                        URLSatelital = (string)obj.URLSatelital,
                        UsuarioSatelital = (string)obj.UsuarioSatelital,
                        ClaveSatelital = (string)obj.ClaveSatelital
                    }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return seguimiento;
        }
        private DateTime DiferenciaFecha(DateTime ultimoReporte, int minutosReporte, short? esReporte)
        {
            DateTime fechaARetornar;

            fechaARetornar = ultimoReporte.AddMinutes(minutosReporte);

            if (esReporte.HasValue && esReporte > 0)
                fechaARetornar = ultimoReporte.AddMinutes(esReporte.Value);

            return fechaARetornar;
        }
        public CaravanaDisponible[] ObtenerCaravanasDisponibles(short idCentro)
        {
            CaravanaDisponible[] caravanaDisponible;
            try
            {
                DbContextScope<CaravanaDisponible>.ObtenerInstancia.DataBase = "Vehiculos";
                caravanaDisponible = DbContextScope<CaravanaDisponible>.ObtenerInstancia.QuerySP(
                    "dbo.stpCaravanas", new
                    {
                        idCentro = idCentro,
                        tyOperacion = (Int16)4
                    })
                    .Select(obj => new CaravanaDisponible
                    {
                        fbtUrbana = (int)obj.fbtUrbana,
                        fidCaravana = (Guid)obj.fidCaravana,
                        fvcDescripcion = (string)obj.fvcDescripcion,
                        Ruta = ((string)obj.Ruta).TrimStart(' ')
                    }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return caravanaDisponible;
        }
        public CaravanaDisponible[] ObtenerCaravanasCarretera()
        {
            CaravanaDisponible[] caravanaDisponible;
            try
            {
                DbContextScope<CaravanaDisponible>.ObtenerInstancia.DataBase = "Vehiculos";
                caravanaDisponible = DbContextScope<CaravanaDisponible>.ObtenerInstancia.QuerySP(
                    "dbo.stpQryCaravanasCarretera").ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return caravanaDisponible;
        }
        public EstadoVehiculo[] ObtenerEstadosVehiculo()
        {
            try
            {
                DbContextScope<EstadoVehiculo>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<EstadoVehiculo>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryIdEstadosVehiculos").ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public RazonEstado[] ObtenerRazonesEstado(int idEstadoVehiculo)
        {
            try
            {
                DbContextScope<RazonEstado>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<RazonEstado>.ObtenerInstancia.QuerySP(
                                   "dbo.stpRazonEstados",
                                   new
                                   {
                                       smIdEstadoVehiculo = idEstadoVehiculo,
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public SiguientePuntoControl[] PuntosReporte(int idRuta)
        {
            try
            {
                DbContextScope<SiguientePuntoControl>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<SiguientePuntoControl>.ObtenerInstancia.QuerySP(
                                   "dbo.stpGrdRutasNacionales",
                                   new
                                   {
                                       inIdRuta = idRuta,
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void GrabarReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime reporte, int idEstadoVehiculo, 
            string observacion, Guid? idPuntoEntrega, string usuario)
        {
            DbContextScope<RazonEstado>.ObtenerInstancia.DataBase = "Vehiculos";
            DbContextScope<RazonEstado>.ObtenerInstancia.QuerySP("stpControlCarretera",
                new
                {
                    idCaravana = idCaravana,
                    inIdPuntoRuta = idPuntoRuta,
                    dtReporte = reporte,
                    smIdEstadoVehiculo = idEstadoVehiculo,
                    txObservacion = observacion,
                    inIdPuntoEntrega = idPuntoEntrega,
                    vcUsuario = usuario,
                    tyOperacion = (byte)0
                });
        }
        public Ruta[] ObtenerRutasCentroOperativo(TipoCaravana urbana, short idSucursal)
        {
            try
            {
                DbContextScope<Ruta>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Ruta>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryRutasTipoCOperativo",
                                   new
                                   {
                                       btUrbana = (byte)urbana,
                                       smIdSucursal = idSucursal
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Ruta[] ObtenerRutasTipo(TipoCaravana clase)
        {
            try
            {
                DbContextScope<Ruta>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Ruta>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryRutasTipo",
                                   new
                                   {
                                       tyOperacion = (byte)clase
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public VehiculoPorSalir[] ObtenerVehiculosPorSalir(Int16 idCentro)
        {
            try
            {
                DbContextScope<VehiculoPorSalir>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<VehiculoPorSalir>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryVehiculosPorSalir",
                                   new
                                   {
                                       smIdSucursal = (byte)idCentro
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio)
        {
            try
            {
                DbContextScope<VehiculoPorSalir>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<VehiculoPorSalir>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryCambioVehDestPlc",
                                   new
                                   {
                                       vcPlaca = placaCambio
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Vehiculo[] ObtenerVehiculosCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<Vehiculo>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Vehiculo>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryVehiculosCaravana",
                                   new
                                   {
                                       idCaravana = caravana
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Conductor[] ObtenerCoductoresCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<Conductor>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Conductor>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryConductoresCaravana",
                                   new
                                   {
                                       idCaravana = caravana
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Escolta[] ObtenerEscoltasCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<Escolta>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Escolta>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryEscoltasCaravana",
                                   new
                                   {
                                       idCaravana = caravana
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Reporte[] ObtenerReportesCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<Reporte>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Reporte>.ObtenerInstancia.QuerySP(
                                   "dbo.stpQryReportesCaravana",
                                   new
                                   {
                                       idCaravana = caravana
                                   }).OrderByDescending(w => w.HoraReporte).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public CiudadRuta[] ObtenerCiudadesRuta()
        {
            try
            {
                DbContextScope<CiudadRuta>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<CiudadRuta>.ObtenerInstancia.QuerySP("dbo.stpQryDestinosRutasNew").ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public VehiculoCaravana[] ObtenerVehiculosPorCaravana(Guid caravana)
        {
            try
            {
                List<VehiculoCaravana> vehiculosCaravana = new List<VehiculoCaravana>();
                DbContextScope<VehiculoCaravanaTemp>.ObtenerInstancia.DataBase = "Vehiculos";
                List<VehiculoCaravanaTemp> temp = DbContextScope<VehiculoCaravanaTemp>.ObtenerInstancia.QuerySP("stpQryVehiculosPorCaravana",
                    new
                    {
                        idCaravana = caravana
                    }).ToList();

                temp.ForEach(x =>
                {
                    vehiculosCaravana.Add(new VehiculoCaravana
                    {
                        CiudadesRecorrido = x.CiudadesRecorrido,
                        NumeroCarnet = x.NumeroCarnet,
                        Manifiesto = x.Manifiesto,
                        Prioridad = x.Prioridad,
                        Id = x.Id,
                        NombreCaravana = x.NombreCaravana,
                        Origen = x.Origen,
                        Destino = x.Destino,
                        NoCambiar = x.NoCambiar,
                        Estado = x.Estado,
                        Conductor = new Conductor
                        {
                            Documento = x.Documento,
                            Numero = x.Numero,
                            Nombre = x.Nombre,
                            PrimerApellido = x.PrimerApellido,
                            SegundoApellido = x.SegundoApellido,
                            Direccion = x.Direccion,
                            Ciudad = x.Ciudad,
                            Telefonos = x.Telefonos,
                            Email = x.Email,
                            Licencia = x.Licencia,
                            Categoria = x.Categoria,
                            Vence = x.Vence,
                            CuentaAhorros = x.CuentaAhorros,
                            Banco = x.Banco
                        },
                        Vehiculo = new Vehiculo
                        {
                            Placa = x.Placa,
                            Carroceria = x.Carroceria,
                            Chasis = x.Chasis,
                            Color = x.Color,
                            Complemento = x.Complemento,
                            De = x.De,
                            Ejes = x.Ejes,
                            GPS = x.GPS,
                            Manifiesto = x.Manifiesto,
                            Marca = x.Marca,
                            Modelo = x.Modelo,
                            Motor = x.Motor,
                            Peso = x.Peso,
                            Prioridad = x.Prioridad,
                            Tipo = x.Tipo,
                            Vinculacion = x.Vinculacion,
                            Volumen = x.Volumen,
                            VinculacionId = x.VinculacionId
                        }
                    });
                });

                return vehiculosCaravana.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Refuerzo[] ObtenerRefuerzosDisponibles()
        {
            try
            {
                DbContextScope<Refuerzo>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Refuerzo>.ObtenerInstancia.QuerySP(
                                   "dbo.stpRefuerzos",
                                   new
                                   {
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP(
                                   "dbo.stpRefuerzosCaravana",
                                   new
                                   {
                                       idCaravana = caravana,
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Guid AgregarRefuerzoCaravana(Guid? caravana, decimal? identidad, Int16? idFuncionEscolta, DateTime? inicio)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<Guid>(
                                   "dbo.stpRefuerzosCaravana",
                                   new
                                   {
                                       idCaravana = caravana,
                                       nuIdentidad = identidad,
                                       tyIdFuncionEscolta = idFuncionEscolta,
                                       dtInicio = inicio,
                                       vcUsuario = "",//TODO:Usuario desde Utilidades
                                       tyOperacion = (byte?)0,
                                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void RetirarRefuerzoCaravana(Guid idRefuerzoCaravana, DateTime? final)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<Guid>(
                                   "dbo.stpRefuerzosCaravana",
                                   new
                                   {
                                       dtFinal = final,
                                       vcUsuario = "",//TODO:Usuario desde Utilidades
                                       tyOperacion = (byte?)3,
                                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public FuncionEscolta[] ObtenerFuncionesEscolta()
        {
            try
            {
                DbContextScope<FuncionEscolta>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<FuncionEscolta>.ObtenerInstancia.QuerySP(
                                   "dbo.stpFuncionesEscolta",
                                   new
                                   {
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana)
        {
            try
            {
                DbContextScope<EscoltaCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<EscoltaCaravana>.ObtenerInstancia.QuerySP(
                                   "dbo.stpEscoltasCaravana",
                                   new
                                   {
                                       idCaravana = caravana,
                                       tyOperacion = 4
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public int AgregarEscolta(Guid idCaravana, decimal identidad, Int16 idFuncionEscolta, DateTime inicio)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("idCaravana", idCaravana);
                p.Add("nuIdentidad", identidad);
                p.Add("tyIdFuncionEscolta", idFuncionEscolta);
                p.Add("dtInicio", inicio);
                p.Add("vcUsuario", "");//TODO:Usuario desde Utilidades
                p.Add("tyOperacion", (byte?)0);
                p.Add("inIdEscoltaCaravana", dbType: DbType.Int32, direction: ParameterDirection.Output);

                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpEscoltasCaravana", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void RetirarEscolta(int idEscoltaCaravana, DateTime final)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("dtFinal", final);
                p.Add("vcUsuario", "");//TODO:Usuario desde Utilidades
                p.Add("tyOperacion", (byte?)3);
                p.Add("inIdEscoltaCaravana", dbType: DbType.Int32, direction: ParameterDirection.Output);

                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpEscoltasCaravana", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void InsertarVehiculoCaravana(Guid idCaravana, int numero, decimal manifiesto)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpVehiculosCaravana",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       inNumero = numero,
                                       nuManifiesto = manifiesto,
                                       vcUsuario = "",//TODO:Usuario desde Utilidades
                                       tyOperacion = (byte?)0,
                                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void EliminarVehiculoCaravana(Guid idCaravana, int numero)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpVehiculosCaravana",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       inNumero = numero,
                                       tyOperacion = (byte?)3,
                                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void CambiarNombreCaravana(Guid idCaravana, string descripcion)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("vcDescripcion", descripcion);
                p.Add("vcUsuario", "");//TODO:Usuario desde Utilidades
                p.Add("tyOperacion", (byte?)7);
                p.Add("idCaravana", value: idCaravana, dbType: DbType.Guid, direction: ParameterDirection.InputOutput);

                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<Guid>(
                                   "dbo.stpCaravanas", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Guid CrearCaravana(int idRuta, string descripcion)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("inIdRuta", idRuta);
                p.Add("vcDescripcion", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[0]);
                p.Add("vcPlaca", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[1]);
                p.Add("vcTelefono", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[2]);
                p.Add("vcUsuario", "");//TODO:Usuario desde Utilidades
                p.Add("tyOperacion", (byte?)0);
                p.Add("inIdEscoltaCaravana", dbType: DbType.Int32, direction: ParameterDirection.Output);

                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<Guid>(
                                   "dbo.stpCaravanas", p);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void EliminarCaravana(Guid idCaravana)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpEscoltasCaravana",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       tyOperacion = (byte?)2,
                                   });
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpRefuerzosCaravana",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       tyOperacion = (byte?)2,
                                   });
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpVehiculosCaravana",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       tyOperacion = (byte?)2,
                                   });
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpCaravanas",
                                   new
                                   {
                                       idCaravana = idCaravana,
                                       tyOperacion = (byte?)2,
                                   });

                //TODO: PARALELISMO EXCEPTO STPCARAVANAS
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public VehiculoSatelital[] ObtenerSatelitalesAsignados()
        {
            try
            {
                DbContextScope<VehiculoSatelital>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<VehiculoSatelital>.ObtenerInstancia.QuerySP(
                                   "dbo.stpSatelital").ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public Satelital[] ObtenerSatelitales()
        {
            try
            {
                DbContextScope<Satelital>.ObtenerInstancia.DataBase = "Vehiculos";
                return DbContextScope<Satelital>.ObtenerInstancia.QuerySP(
                                   "dbo.stpSatelital",
                                   new
                                   {
                                       TipoConsulta = 5
                                   }).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void CrearSatelital(string nombre, string url)
        {
            try
            {
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.DataBase = "Vehiculos";
                DbContextScope<RefuerzoCaravana>.ObtenerInstancia.QuerySP<int>(
                                   "dbo.stpEscoltasCaravana",
                                   new
                                   {
                                       fvcDescripcion = nombre,
                                       fvcURL = url,
                                       TipoConsulta = 6
                                   });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        #endregion
        #region Disposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }
        #endregion
    }
}
