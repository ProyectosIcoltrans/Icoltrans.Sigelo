using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Entity.Vehiculos;
using Microsoft.Data.Extensions;
using System.Collections.Generic;
using Icoltrans.Sigelo.Data.Vehiculos;

namespace Icoltrans.Sigelo.Data
{
    public partial class VehiculosModelo
    {
        #region Métodos
        public Guid ObtenerCaravanaPlaca(string placa)
        {
            Guid idCaravana = Guid.Empty;
            using (var q = this.CreateStoreCommand("stpQryCaravanaPlaca", CommandType.StoredProcedure,
                new SqlParameter("@vcPlaca", placa)))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                {
                    var resp = q.ExecuteScalar();
                    try
                    {
                        idCaravana = (Guid)resp;
                    }
                    catch
                    {
                        idCaravana = Guid.Empty;
                    }
                }
            }
            return idCaravana;
        }
        public Seguimiento[] ObtenerSeguimientos(TipoCaravana? urbana, string idUbicacionOrigen, string idUbicacionDestino)
        {
            string mensaje = string.Empty;
            bool error = false;
            //DataTable resultados = null;
            //List<Seguimiento> seguimientos = new List<Seguimiento>();
            List<Seguimiento> list = new List<Seguimiento>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("@btUrbana", urbana);
                dictionary.Add("@vcIDUbicacionOrigen", idUbicacionOrigen);
                dictionary.Add("@vcIDUbicacionDestino", idUbicacionDestino);
                DataTable dataTable = new Datos("CadenaVehiculos").ExecuteQuery("dbo.stpQrySeguimientoNew", dictionary, ref error).Tables[0];
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Seguimiento seguimiento = new Seguimiento();
                    seguimiento.destino = dataRow["destino"].ToString();
                    seguimiento.fbtUrbana = int.Parse(dataRow["fbtUrban"].ToString());
                    seguimiento.fdtSalida = DateTime.Parse(dataRow["fdtSalida"].ToString());
                    seguimiento.fdtSiguientePunto = DateTime.Parse(dataRow["fdtSiguientePunto"].ToString());
                    seguimiento.fdtUltimoPunto = DateTime.Parse(dataRow["fdtUltimoPunto"].ToString());
                    seguimiento.fidCaravana = Guid.Parse(dataRow["fidCaravana"].ToString());
                    seguimiento.finIdRuta = int.Parse(dataRow["finIdRuta"].ToString());
                    seguimiento.finSiguientePunto = VehiculosModelo.ToNullableInt(dataRow["finSiguientePunto"].ToString());
                    seguimiento.finUltimoPunto = VehiculosModelo.ToNullableInt(dataRow["finUltimoPunto"].ToString());
                    seguimiento.frlDiferencia = (double)float.Parse(dataRow["frlDiferencia"].ToString());
                    seguimiento.ftyEstAlerta = VehiculosModelo.ToNullableShort(dataRow["ftyEstAlerta"].ToString());
                    seguimiento.ftyEstReporte = VehiculosModelo.ToNullableShort(dataRow["ftyEstReporte"].ToString());
                    seguimiento.ftyMinutosAlerta = int.Parse(dataRow["ftyMinutosAlerta"].ToString());
                    seguimiento.ftyMinutosReporte = int.Parse(dataRow["ftyMinutosReporte"].ToString());
                    seguimiento.fvcDescripcion = dataRow["fvcDescripcion"].ToString();
                    seguimiento.fvcEstado = dataRow["fvcEstado"].ToString();
                    seguimiento.fvcSiguientePunto = dataRow["fvcSiguientePunto"].ToString();
                    seguimiento.fvcUltimoPunto = dataRow["fvcUltimoPunto"].ToString();
                    seguimiento.origen = dataRow["origen"].ToString();
                    seguimiento.ruta = dataRow["ruta"].ToString();
                    seguimiento.Vehiculos = int.Parse(dataRow["Vehiculos"].ToString());
                    seguimiento.Satelital = dataRow["Satelital"].ToString();
                    seguimiento.URLSatelital = dataRow["URLSatelital"].ToString();
                    seguimiento.UsuarioSatelital = dataRow["UsuarioSatelital"].ToString();
                    seguimiento.ClaveSatelital = dataRow["ClaveSatelital"].ToString();
                    seguimiento.ftxObservacion = dataRow["ftxObservacion"].ToString();

                    //Seguimiento seguimiento2 = seguimiento;

                    seguimiento.fdtSiguientePunto = seguimiento.fdtUltimoPunto.AddMinutes((double)seguimiento.ftyMinutosReporte);
                    if (seguimiento.ftyEstReporte.HasValue && seguimiento.ftyEstReporte > 0)
                    {
                        seguimiento.fdtSiguientePunto = seguimiento.fdtUltimoPunto.AddMinutes((double)seguimiento.ftyEstReporte);
                    }
                    seguimiento.frlDiferencia = new TimeSpan(seguimiento.fdtSiguientePunto.Ticks).TotalMinutes;
                    if (seguimiento.fbtUrbana.Equals(1))
                    {
                        seguimiento.fvcSiguientePunto = string.Empty;
                        seguimiento.fvcUltimoPunto = string.Empty;
                    }
                    list.Add(seguimiento);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return list.ToArray();

            //try
            //{
            //    DbContextScope<Seguimiento>.ObtenerInstancia.DataBase = "CadenaVehiculos";

            //    var seguimiento = DbContextScope<Seguimiento>.ObtenerInstancia.QuerySP("dbo.stpQrySeguimientoNew", new
            //    {
            //        btUrbana = 0,
            //        vcIDUbicacionOrigen = urbana,
            //        vcIDUbicacionDestino = idUbicacionOrigen,
            //        fvcFactura = idUbicacionDestino
            //    });

            //    seguimiento.ForEach(item =>
            //    {
            //        item.fdtSiguientePunto = item.fdtUltimoPunto.AddMinutes(item.ftyMinutosReporte);
            //        if (item.ftyEstReporte.HasValue && item.ftyEstReporte > 0)
            //            item.fdtSiguientePunto = item.fdtUltimoPunto.AddMinutes(item.ftyEstReporte.Value);

            //        item.frlDiferencia = (new TimeSpan(item.fdtSiguientePunto.Ticks)).TotalMinutes;

            //        if (item.fbtUrbana.Equals(TipoCaravana.Urbana))
            //        {
            //            item.fvcSiguientePunto = string.Empty;
            //            item.fvcUltimoPunto = string.Empty;
            //        }
            //    });

            //    return seguimiento.ToArray();
            //try
            //{
            //    Dictionary<string, object> dicParametros = new Dictionary<string, object>();

            //    dicParametros.Add("@btUrbana", urbana);
            //    dicParametros.Add("@vcIDUbicacionOrigen", idUbicacionOrigen);
            //    dicParametros.Add("@vcIDUbicacionDestino", idUbicacionDestino);

            //    resultados = new Datos("CadenaVehiculos").ExecuteQuery("dbo.stpQrySeguimientoNew", dicParametros, ref error).Tables[0];

            //    foreach (DataRow registro in resultados.Rows)
            //    {
            //        Seguimiento item = new Seguimiento
            //        {
            //            destino = registro["destino"].ToString(), //r.Field<string>("destino"),
            //            fbtUrbana = int.Parse(registro["fbtUrbana"].ToString()),//(int)r.Field<byte>("fbtUrbana"),
            //            fdtSalida = DateTime.Parse(registro["fdtSalida"].ToString()),//r.Field<DateTime>("fdtSalida"),
            //            fdtSiguientePunto = DateTime.Parse(registro["fdtSiguientePunto"].ToString()),//r.Field<DateTime>("fdtSiguientePunto"),
            //            fdtUltimoPunto = DateTime.Parse(registro["fdtUltimoPunto"].ToString()),//r.Field<DateTime>("fdtUltimoPunto"),
            //            fidCaravana = Guid.Parse(registro["fidCaravana"].ToString()),//r.Field<Guid>("fidCaravana"),
            //            finIdRuta = int.Parse(registro["finIdRuta"].ToString()),//r.Field<int>("finIdRuta"),
            //            finSiguientePunto = ToNullableInt(registro["finSiguientePunto"].ToString()),//r.Field<int?>("finSiguientePunto"),
            //            finUltimoPunto = ToNullableInt(registro["finUltimoPunto"].ToString()),//r.Field<int?>("finUltimoPunto"),
            //            frlDiferencia = float.Parse(registro["frlDiferencia"].ToString()),//r.Field<float>("frlDiferencia"),
            //            ftyEstAlerta = ToNullableShort(registro["ftyEstAlerta"].ToString()),//r.Field<Int16?>("ftyEstAlerta"),
            //            ftyEstReporte = ToNullableShort(registro["ftyEstReporte"].ToString()),//r.Field<Int16?>("ftyEstReporte"),
            //            ftyMinutosAlerta = int.Parse(registro["ftyMinutosAlerta"].ToString()),//(int)r.Field<byte>("ftyMinutosAlerta"),
            //            ftyMinutosReporte = int.Parse(registro["ftyMinutosReporte"].ToString()),//(int)r.Field<byte>("ftyMinutosReporte"),
            //            fvcDescripcion = registro["fvcDescripcion"].ToString(),//r.Field<string>("fvcDescripcion"),
            //            fvcEstado = registro["fvcEstado"].ToString(),//r.Field<string>("fvcEstado"),
            //            fvcSiguientePunto = registro["fvcSiguientePunto"].ToString(),//r.Field<string>("fvcSiguientePunto"),
            //            fvcUltimoPunto = registro["fvcUltimoPunto"].ToString(),//r.Field<string>("fvcUltimoPunto"),
            //            origen = registro["origen"].ToString(),// r.Field<string>("origen"),
            //            ruta = registro["ruta"].ToString(),//r.Field<string>("ruta"),
            //            Vehiculos = int.Parse(registro["Vehiculos"].ToString()),//r.Field<int>("Vehiculos"),
            //            Satelital = registro["Satelital"].ToString(),//r.Field<string>("Satelital"),
            //            URLSatelital = registro["URLSatelital"].ToString(),//r.Field<string>("URLSatelital"),
            //            UsuarioSatelital = registro["UsuarioSatelital"].ToString(),//r.Field<string>("UsuarioSatelital"),
            //            ClaveSatelital = registro["ClaveSatelital"].ToString(),//r.Field<string>("ClaveSatelital")
            //            ftxObservacion = registro["ftxObservacion"].ToString()//r.Field<string>("ClaveSatelital")
            //        };
            //        item.fdtSiguientePunto = item.fdtUltimoPunto.AddMinutes(item.ftyMinutosReporte);
            //        if (item.ftyEstReporte.HasValue && item.ftyEstReporte > 0)
            //            item.fdtSiguientePunto = item.fdtUltimoPunto.AddMinutes(item.ftyEstReporte.Value);

            //        item.frlDiferencia = (new TimeSpan(item.fdtSiguientePunto.Ticks)).TotalMinutes;

            //        if (item.fbtUrbana.Equals(TipoCaravana.Urbana))
            //        {
            //            item.fvcSiguientePunto = string.Empty;
            //            item.fvcUltimoPunto = string.Empty;
            //        }

            //        seguimientos.Add(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    mensaje = ex.Message;
            //}

            //return seguimientos.ToArray();
        }
        public CaravanaDisponible[] ObtenerCaravanasDisponibles(short idCentro)
        {
            using (var q = this.CreateStoreCommand("stpCaravanas", CommandType.StoredProcedure,
                 new SqlParameter("@idCentro", idCentro),
                 new SqlParameter("@tyOperacion", (Int16)4)))
            {
                q.CommandTimeout = 60;
                return q.Materialize<CaravanaDisponible>((r) => new CaravanaDisponible
                {
                    fbtUrbana = (int)r.Field<byte>("fbtUrbana"),
                    fidCaravana = r.Field<Guid>("fidCaravana"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    Ruta = r.Field<string>("Ruta").TrimStart(' ')
                }
                ).ToArray();
            }
        }
        public CaravanaDisponible[] ObtenerCaravanasCarretera()
        {
            using (var q = this.CreateStoreCommand("stpQryCaravanasCarretera", CommandType.StoredProcedure))
            {
                q.CommandTimeout = 60;
                return q.Materialize<CaravanaDisponible>((r) => new CaravanaDisponible
               {
                   fbtUrbana = (int)r.Field<byte>("fbtUrbana"),
                   fidCaravana = r.Field<Guid>("inv.Caravana"),
                   fvcDescripcion = r.Field<string>("Descripcion"),
                   Ruta = r.Field<string>("Ruta")
               }
               ).ToArray();
            }
        }
        public UltimoReporte ObtenerUltimoReporte(Guid caravanaId)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravanaId);

            using (var u = this.CreateStoreCommand("stpQryUltimoReporte", CommandType.StoredProcedure, idCaravana))
            {
                u.CommandTimeout = 60;
                return u.Materialize<UltimoReporte>((r) => new UltimoReporte
                    {
                        EstAlerta = (Int16)r.Field<byte>("EstAlerta"),
                        EstReporte = (Int16)r.Field<byte>("EstReporte"),
                        fdtReporte = r.Field<DateTime>("fdtReporte"),
                        fidControlCarretera = r.Field<Guid>("fidControlCarretera"),
                        finIdPuntoRuta = r.Field<int?>("finIdPuntoRuta"),
                        fsmIdEstadoVehiculo = r.Field<Int16>("fsmIdEstadoVehiculo"),
                        fvcDescripcion = r.Field<string>("fvcDescripcion"),
                        fvcEstado = r.Field<string>("fvcEstado"),
                        ftxObservacion = r.Field<string>("ftxObservacion")
                    }
                ).FirstOrDefault();
            }
        }
        public SiguientePuntoControl ObtenerSiguienteReporte(int idRuta, Nullable<int> idPuntoRuta)
        {
            string mensaje = string.Empty;
            bool error = false;
            DataTable resultados = null;
            List<SiguientePuntoControl> seguimientos = new List<SiguientePuntoControl>();
            Dictionary<string, object> dicParametros = new Dictionary<string, object>();

            dicParametros.Add("@inIdRuta", idRuta);
            dicParametros.Add("@inIdPuntoRuta", idPuntoRuta);
            resultados = new Datos("CadenaVehiculos").ExecuteQuery("dbo.stpQrySiguientePuntoControl", dicParametros, ref error).Tables[0];
            foreach (DataRow fila in resultados.Rows)
            {
                seguimientos.Add(new SiguientePuntoControl
                {
                    finIdPuntoRuta = int.Parse(fila["finIdPuntoRuta"].ToString()),
                    //fsmIdTipoPeaje = r.Field<Int16?>("fsmIdTipoPeaje"),
                    //fsmOrden = r.Field<Int16>("fsmOrden"),
                    //ftyMinutosPuntoAnterior = r.Field<Int16>("ftyMinutosPuntoAnterior"),
                    fvcDescripcion = fila["fvcDescripcion"].ToString(),
                    fvcIDUbicacion = fila["fvcIDUbicacion"].ToString()
                });
            }

            return seguimientos.FirstOrDefault();
        }
        public EstadoVehiculo[] ObtenerEstadosVehiculo()
        {
            using (var q = this.CreateStoreCommand("stpQryIdEstadosVehiculos", CommandType.StoredProcedure))
            {
                q.CommandTimeout = 60;
                return q.Materialize<EstadoVehiculo>((r) => new EstadoVehiculo
                {
                    fsmIdEstadoVehiculo = r.Field<Int16>("fsmIdEstadoVehiculo"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fbtProgramable = r.Field<bool>("fbtProgramable"),
                    fmsIdRazonEstadoVehiculo = r.Field<int>("fsmIdRazonEstadoVehiculo"),
                    Razon = r.Field<int?>("Razon")
                }).ToArray();
            }
        }
        public RazonEstado[] ObtenerRazonesEstado(int idEstadoVehiculo)
        {
            SqlParameter m_smIdEstadoVehiculo = new SqlParameter();
            m_smIdEstadoVehiculo.Value = idEstadoVehiculo;
            m_smIdEstadoVehiculo.ParameterName = "@smIdEstadoVehiculo";
            m_smIdEstadoVehiculo.Direction = ParameterDirection.Input;

            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpRazonEstados", CommandType.StoredProcedure, m_smIdEstadoVehiculo, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<RazonEstado>((r) => new RazonEstado
                {
                    fsmIdRazonEstadoV = r.Field<Int16>("fsmIdRazonEstadoVehiculo"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion")
                }).ToArray();
            }
        }
        public SiguientePuntoControl[] PuntosReporte(int idRuta)
        {
            SqlParameter m_inIdRuta = new SqlParameter();
            m_inIdRuta.Value = idRuta;
            m_inIdRuta.ParameterName = "@inIdRuta";
            m_inIdRuta.Direction = ParameterDirection.Input;

            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpGrdRutasNacionales", CommandType.StoredProcedure, m_inIdRuta, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<SiguientePuntoControl>((r) => new SiguientePuntoControl
                    {
                        finIdRuta = r.Field<int>("finIdRuta"),
                        finIdPuntoRuta = r.Field<int>("finIdPuntoRuta"),
                        fvcDescripcion = r.Field<string>("fvcDescripcion"),
                        fsmOrden = r.Field<Int16>("fsmOrden"),
                        fvcIDUbicacion = r.Field<string>("fvcIDUbicacion"),
                        ftyMinutosPuntoAnterior = (Int16?)r.Field<byte?>("ftyMinutosPuntoAnterior"),
                        fbtControl = r.Field<bool>("fbtControl")
                    }).ToArray();
            }
        }
        public void GrabarReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime reporte, Int16 idEstadoVehiculo, string observacion, Guid? idPuntoEntrega, string usuario)
        {
            SqlParameter m_idControlCarretera = new SqlParameter();
            m_idControlCarretera.ParameterName = "@idControlCarretera";
            m_idControlCarretera.Direction = ParameterDirection.Output;
            m_idControlCarretera.DbType = DbType.Guid;
            m_idControlCarretera.IsNullable = true;

            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", idCaravana);
            SqlParameter m_inIdPuntoRuta = new SqlParameter("@inIdPuntoRuta", idPuntoRuta);
            SqlParameter m_dtReporte = new SqlParameter("@dtReporte", reporte);
            SqlParameter m_smIdEstadoVehiculo = new SqlParameter("@smIdEstadoVehiculo", (Int16)idEstadoVehiculo);
            SqlParameter m_txObservacion = new SqlParameter("@txObservacion", observacion);
            SqlParameter m_inIdPuntoEntrega = new SqlParameter("@inIdPuntoEntrega", idPuntoEntrega);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", usuario);
            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", (byte?)0);
            using (var q = this.CreateStoreCommand("stpControlCarretera", CommandType.StoredProcedure, m_idControlCarretera, m_idCaravana, m_inIdPuntoRuta, m_inIdPuntoEntrega, m_dtReporte, m_smIdEstadoVehiculo, m_txObservacion, m_vcUsuario, tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public CiudadRuta[] ObtenerCiudadesRutaOrigen()
        {
            using (var q = this.CreateStoreCommand("stpQryOrigenesRutas"))
            {
                q.CommandTimeout = 60;
                return q.Materialize<CiudadRuta>((r) => new CiudadRuta
                    {
                        Id = r.Field<string>("fvcIDUbicacionOrigen"),
                        Descripcion = r.Field<string>("fvcDescripcion")
                    }
                ).ToArray();
            }
        }
        public CiudadRuta[] ObtenerCiudadesRutaDestino()
        {
            using (var q = this.CreateStoreCommand("stpQryDestinosRutas"))
            {
                q.CommandTimeout = 60;
                return q.Materialize<CiudadRuta>((r) => new CiudadRuta
                    {
                        Id = r.Field<string>("fvcIDUbicacionDestino"),
                        Descripcion = r.Field<string>("fvcDescripcion")
                    }
                    ).ToArray();
            }
        }
        public Ruta[] ObtenerRutasCentroOperativo(TipoCaravana urbana, short idSucursal)
        {
            SqlParameter m_btUrbana = new SqlParameter("@btUrbana", (byte)urbana);
            SqlParameter m_smIdSucursal = new SqlParameter("@smIdSucursal", idSucursal);

            using (var q = this.CreateStoreCommand("stpQryRutasTipoCOperativo", CommandType.StoredProcedure, m_btUrbana, m_smIdSucursal))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Ruta>((r) => new Ruta
                    {
                        Id = r.Field<int>("inv.Id"),
                        Descripcion = r.Field<string>("Nombre"),
                        MinutosReporte = r.Field<byte>("Minutos"),
                        CiudadOrigen = r.Field<string>("Origen"),
                        CiudadDestino = r.Field<string>("Destino"),
                        CantidadPuntos = r.Field<int>("Puntos"),
                        Clase = (TipoCaravana)r.Field<byte>("inv.Urbana")
                    }
                    ).ToArray();
            }
        }
        public Ruta[] ObtenerRutasTipo(TipoCaravana clase)
        {
            SqlParameter operacion = new SqlParameter("@tyOperacion", (byte)clase);

            using (var q = this.CreateStoreCommand("stpQryRutasTipo", CommandType.StoredProcedure, operacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Ruta>((r) => new Ruta
                {
                    Id = r.Field<int>("inv.Id"),
                    Descripcion = r.Field<string>("Nombre"),
                    MinutosReporte = r.Field<byte>("Minutos"),
                    IdUbicacionOrigen = r.Field<string>("inv.IDOrigen"),
                    CiudadOrigen = r.Field<string>("Origen"),
                    CiudadDestino = r.Field<string>("Destino"),
                    Clase = (TipoCaravana)r.Field<byte>("inv.Urbana")
                }
                    ).ToArray();
            }
        }
        public VehiculoPorSalir[] ObtenerVehiculosPorSalir(Int16 idCentro)
        {
            SqlParameter m_idCentro = new SqlParameter("@smIdSucursal", idCentro);

            using (var q = this.CreateStoreCommand("stpQryVehiculosPorSalir", CommandType.StoredProcedure, m_idCentro))
            {
                q.CommandTimeout = 60;
                return q.Materialize<VehiculoPorSalir>((r) => new VehiculoPorSalir
                 {
                     CDestino = r.Field<string>("CDestino"),
                     Centro = r.Field<string>("Centro"),
                     Completa = r.Field<string>("Completa"),
                     Conductor = r.Field<string>("Conductor").ToUpper(),
                     COrigen = r.Field<string>("COrigen"),
                     Destinos = r.Field<string>("Destinos"),
                     fbtEnCaravana = r.Field<int>("fbtEnCaravana"),
                     fdtLastUpd = r.Field<DateTime>("fdtLastUpd"),
                     fidCaravana = r.Field<Guid?>("fidCaravana"),
                     finNumeroCarnet = r.Field<int>("finNumeroCarnet"),
                     fnuManifiesto = r.Field<decimal>("fnuManifiesto"),
                     fsmModelo = r.Field<Int16>("fsmModelo"),
                     fvcDescripcion = r.Field<string>("fvcDescripcion"),
                     fvcNombre = r.Field<string>("fvcNombre"),
                     fvcPlaca = r.Field<string>("fvcPlaca"),
                     fvcUsuario = r.Field<string>("fvcUsuario"),
                     Prioridad = r.Field<bool>("Prioridad"),
                     Trailer = r.Field<string>("Trailer"),
                     Telefono = r.Field<string>("Telefonos")
                 }
                 ).ToArray();
            }
        }
        public VehiculoPorSalir[] ObtenerVehiculosParaCambio(string placaCambio)
        {
            using (var sqlCmd = this.CreateStoreCommand("stpQryCambioVehDestPlc", CommandType.StoredProcedure,
                new SqlParameter("@vcPlaca", placaCambio)))
            {
                sqlCmd.CommandTimeout = 60;
                return sqlCmd.Materialize<VehiculoPorSalir>((r) => new VehiculoPorSalir
                {

                    finNumeroCarnet = r.Field<int>("Carnet"),
                    fvcPlaca = r.Field<string>("Placa"),
                    Conductor = r.Field<string>("Conductor"),
                    fvcDescripcion = r.Field<string>("Tipo Vehiculo"),
                    Destinos = r.Field<string>("inv.Mostrar"),
                    Trailer = r.Field<string>("Trailer"),
                    IdentidadConductor = r.Field<decimal>("fnuIdentificacion")
                }
                 ).ToArray();
            }
        }
        public Vehiculo[] ObtenerVehiculosCaravana(Guid caravana)
        {
            Vehiculo[] vehiculos = null;
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryVehiculosCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                vehiculos = q.Materialize<Vehiculo>((r) => new Vehiculo
                    {
                        Carroceria = r.Field<string>("Carroceria"),
                        Chasis = r.Field<string>("Chasis"),
                        Color = r.Field<string>("Color"),
                        Complemento = r.Field<string>("Complemento"),
                        De = r.Field<string>("De"),
                        Ejes = r.Field<byte>("Ejes"),
                        GPS = r.Field<byte?>("GPS"),
                        Manifiesto = r.Field<decimal?>("Manifiesto"),
                        Marca = r.Field<string>("Marca"),
                        Modelo = r.Field<Int16>("Modelo"),
                        Motor = r.Field<string>("Motor"),
                        Peso = r.Field<float>("Peso"),
                        Placa = r.Field<string>("Placa"),
                        Prioridad = r.Field<bool>("Prioridad"),
                        Tipo = r.Field<string>("Tipo"),
                        Vinculacion = r.Field<string>("Vinculación").ToUpper(),
                        VinculacionId = r.Field<byte>("ftyIdVinculacion"),
                        Volumen = r.Field<float>("Volumen")
                    }
                    ).ToArray();
            }
            SqlParameter Placa = new SqlParameter("@vcPlaca", SqlDbType.VarChar, 6);
            using (var v = this.CreateStoreCommand("stpQryPropietariosVehiculo", CommandType.StoredProcedure, Placa))
            {
                v.CommandTimeout = 60;
                foreach (var item in vehiculos.Where(p => p.VinculacionId != 1))
                {
                    Placa.Value = item.Placa;
                    var t = v.Materialize<Propietario>((r) => new Propietario
                    {
                        Nombre = r.Field<string>("fvcNombre")
                    }).ToArray();

                    if (t.LastOrDefault() != null)
                    {
                        item.Vinculacion = t.LastOrDefault().Nombre;
                    }
                }
            }
            return vehiculos;
        }
        public VehiculoCaravana[] ObtenerVehiculosPorCaravana(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryVehiculosPorCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<VehiculoCaravana>((r) => new VehiculoCaravana
                {
                    Id = r.Field<Guid>("fidVehiculoCaravana"),
                    NombreCaravana = r.Field<string>("Caravana"),
                    NumeroCarnet = r.Field<int?>("finNumeroCarnet"),
                    CiudadesRecorrido = r.Field<string>("Destinos"),
                    Origen = r.Field<string>("Origen"),
                    Destino = r.Field<string>("Destino"),
                    NoCambiar = r.Field<bool>("fbtNoCambiar"),
                    Manifiesto = r.Field<decimal?>("fnuManifiesto"),
                    Prioridad = r.Field<bool>("Prioridad"),
                    Vehiculo = new Vehiculo
                    {
                        Prioridad = r.Field<bool>("Prioridad"),
                        Placa = r.Field<string>("fvcPlaca"),
                        Tipo = r.Field<string>("fvcDescripcion"),
                        Complemento = r.Field<string>("Trailer"),
                        VinculacionId = r.Field<byte>("inv.Vinculacion"),
                        Manifiesto = r.Field<decimal?>("fnuManifiesto")
                    },
                    Conductor = new Conductor
                    {
                        Nombre = r.Field<string>("Conductor"),
                        Numero = r.Field<decimal>("fnuIdentificacion")
                    }
                }
                    ).ToArray();
            }
        }
        public Conductor[] ObtenerCoductoresCaravana(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryConductoresCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Conductor>((r) => new Conductor
                    {
                        Documento = r.Field<string>("Documento"),
                        Numero = r.Field<decimal>("Número"),
                        Banco = r.Field<string>("Banco"),
                        Categoria = r.Field<string>("Categoria"),
                        Ciudad = r.Field<string>("Ciudad"),
                        CuentaAhorros = r.Field<string>("Cuenta Ahorros"),
                        Direccion = r.Field<string>("Direccion"),
                        Email = r.Field<string>("E-Mail"),
                        Licencia = r.Field<string>("Licencia"),
                        Nombre = r.Field<string>("Nombre"),
                        PrimerApellido = r.Field<string>("Primer Apellido"),
                        SegundoApellido = r.Field<string>("Segundo Apellido"),
                        Telefonos = r.Field<string>("Telefonos"),
                        Vence = r.Field<DateTime?>("Vence")
                    }
                    ).ToArray();
            }
        }
        public Escolta[] ObtenerEscoltasCaravana(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryEscoltasCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Escolta>((r) => new Escolta
                    {
                        Clase = r.Field<string>("Clase"),
                        Ciudad = r.Field<string>("Ciudad"),
                        Direccion = r.Field<string>("Direccion"),
                        Email = r.Field<string>("E-Mail"),
                        Identificacion = r.Field<decimal>("Identificacion"),
                        Nombre = r.Field<string>("Nombre"),
                        PrimerApellido = r.Field<string>("Primer Apellido"),
                        SegundoApellido = r.Field<string>("Segundo Apellido"),
                        Telefonos = r.Field<string>("Telefonos")
                    }
                    ).ToArray();
            }
        }
        public Reporte[] ObtenerReportesCaravana(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryReportesCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Reporte>((r) => new Reporte
                    {

                        HoraSistema = r.Field<DateTime>("Hora Sistema"),
                        HoraReporte = r.Field<DateTime>("Hora Reporte"),
                        Caravanas = r.Field<string>("Caravanas"),
                        Estado = r.Field<string>("Estado"),
                        Municipio = r.Field<string>("Municipio"),
                        Observacion = r.Field<string>("Observacion"),
                        Operador = r.Field<string>("Operador"),
                        Peaje = r.Field<string>("Peaje"),
                        PuntoRuta = r.Field<string>("Punto Ruta")
                    }
                    ).OrderByDescending(w => w.HoraReporte).ToArray();
            }
        }
        public Refuerzo[] ObtenerRefuerzosDisponibles()
        {
            SqlParameter Usuario = new SqlParameter("@vcUsuario", null);
            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpRefuerzos", CommandType.StoredProcedure, Usuario, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Refuerzo>((r) => new Refuerzo
                    {
                        Identificacion = r.Field<decimal>("Identificacion"),
                        Nombre = r.Field<string>("Nombre"),
                        Email = r.Field<string>("E-Mail"),
                        Direccion = r.Field<string>("Direccion"),
                        Ubicacion = r.Field<string>("Ubicacion"),
                        UltimaModificacion = r.Field<DateTime>("Ult.Modifi.")
                    }).ToArray();
            }
        }
        public RefuerzoCaravana[] ObtenerRefuerzosPorCaravana(Guid caravana)
        {
            Guid fidRefuerzoCaravana = Guid.Empty;

            SqlParameter m_fidRefuerzoCaravana = new SqlParameter("@fidRefuerzoCaravana", fidRefuerzoCaravana);
            m_fidRefuerzoCaravana.Direction = ParameterDirection.InputOutput;

            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpRefuerzosCaravana", CommandType.StoredProcedure, m_fidRefuerzoCaravana, idCaravana, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<RefuerzoCaravana>((r) => new RefuerzoCaravana
                    {
                        fidRefuerzoCaravana = r.Field<Guid>("fidRefuerzoCaravana"),
                        ftyIdFuncionEscolta = Convert.ToInt16(r.Field<byte?>("ftyIdFuncionEscolta")),
                        fnuIdentidad = r.Field<decimal?>("fnuIdentidad")
                    }).ToArray();
            }
        }
        public Guid AgregarRefuerzoCaravana(Guid? caravana, decimal? identidad, Int16? idFuncionEscolta, DateTime? inicio)
        {
            SqlParameter m_fidRefuerzoCaravana = new SqlParameter();
            m_fidRefuerzoCaravana.ParameterName = "@fidRefuerzoCaravana";
            m_fidRefuerzoCaravana.Direction = ParameterDirection.Output;
            m_fidRefuerzoCaravana.DbType = DbType.Guid;
            m_fidRefuerzoCaravana.IsNullable = true;
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);
            SqlParameter nuIdentidad = new SqlParameter("@nuIdentidad", identidad);
            SqlParameter tyIdFuncionEscolta = new SqlParameter("@tyIdFuncionEscolta", (byte?)idFuncionEscolta);
            SqlParameter dtInicio = new SqlParameter("@dtInicio", inicio);
            SqlParameter m_usuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", (byte?)0);

            using (var q = this.CreateStoreCommand("stpRefuerzosCaravana", CommandType.StoredProcedure, m_fidRefuerzoCaravana, idCaravana, nuIdentidad, tyIdFuncionEscolta, dtInicio, m_usuario, tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
                return (Guid)m_fidRefuerzoCaravana.Value;
            }
        }
        public void RetirarRefuerzoCaravana(Guid idRefuerzoCaravana, DateTime? final)
        {
            SqlParameter m_fidRefuerzoCaravana = new SqlParameter();
            m_fidRefuerzoCaravana.ParameterName = "@fidRefuerzoCaravana";
            m_fidRefuerzoCaravana.Direction = ParameterDirection.InputOutput;
            m_fidRefuerzoCaravana.Value = idRefuerzoCaravana;
            m_fidRefuerzoCaravana.DbType = DbType.Guid;
            m_fidRefuerzoCaravana.IsNullable = true;
            SqlParameter dtFinal = new SqlParameter("@dtFinal", final);
            SqlParameter m_usuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", (byte?)3);

            using (var q = this.CreateStoreCommand("stpRefuerzosCaravana", CommandType.StoredProcedure, m_fidRefuerzoCaravana, dtFinal, m_usuario, tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public FuncionEscolta[] ObtenerFuncionesEscolta()
        {
            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpFuncionesEscolta", CommandType.StoredProcedure, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<FuncionEscolta>((r) => new FuncionEscolta
                    {
                        IdFuncionEscolta = (int)r.Field<byte>("inv.IdFuncionEscolta"),
                        Descripcion = r.Field<string>("Descripcion"),
                        Usuario = r.Field<string>("Usuario"),
                        UltimaModificacion = r.Field<DateTime>("Ult. Modi.")
                    }).ToArray();
            }
        }
        public EscoltaCaravana[] ObtenerEscoltasPorCaravana(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            SqlParameter tyOperacion = new SqlParameter("@tyOperacion", 4);

            using (var q = this.CreateStoreCommand("stpEscoltasCaravana", CommandType.StoredProcedure, idCaravana, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<EscoltaCaravana>((r) => new EscoltaCaravana
                    {
                        finIdEscoltaCaravana = r.Field<int>("finIdEscoltaCaravana"),
                        ftyIdFuncionEscolta = Convert.ToInt16(r.Field<byte?>("ftyIdFuncionEscolta")),
                        fnuIdentidad = r.Field<decimal?>("fnuIdentidad")
                    }).ToArray();
            }
        }
        public int AgregarEscolta(Guid idCaravana, decimal identidad, Int16 idFuncionEscolta, DateTime inicio)
        {
            SqlParameter m_inIdEscoltaCaravana = new SqlParameter();
            m_inIdEscoltaCaravana.ParameterName = "@inIdEscoltaCaravana";
            m_inIdEscoltaCaravana.Direction = ParameterDirection.Output;
            m_inIdEscoltaCaravana.DbType = DbType.Int32;
            m_inIdEscoltaCaravana.IsNullable = true;
            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", idCaravana);
            SqlParameter m_nuIdentidad = new SqlParameter("@nuIdentidad", identidad);
            SqlParameter m_tyIdFuncionEscolta = new SqlParameter("@tyIdFuncionEscolta", idFuncionEscolta);
            SqlParameter m_dtInicio = new SqlParameter("@dtInicio", inicio);
            SqlParameter m_usuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", (byte?)0);

            using (var q = this.CreateStoreCommand("stpEscoltasCaravana", CommandType.StoredProcedure, m_inIdEscoltaCaravana, m_idCaravana, m_nuIdentidad, m_tyIdFuncionEscolta, m_dtInicio, m_usuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
            return (int)m_inIdEscoltaCaravana.Value;
        }
        public void RetirarEscolta(int idEscoltaCaravana, DateTime final)
        {
            SqlParameter m_inIdEscoltaCaravana = new SqlParameter();
            m_inIdEscoltaCaravana.ParameterName = "@inIdEscoltaCaravana";
            m_inIdEscoltaCaravana.Direction = ParameterDirection.InputOutput;
            m_inIdEscoltaCaravana.Value = idEscoltaCaravana;
            m_inIdEscoltaCaravana.DbType = DbType.Int32;
            m_inIdEscoltaCaravana.IsNullable = true;

            SqlParameter m_dtFinal = new SqlParameter("@dtFinal", final);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", (byte?)3);

            using (var q = this.CreateStoreCommand("stpEscoltasCaravana", CommandType.StoredProcedure, m_inIdEscoltaCaravana, m_dtFinal, m_vcUsuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void InsertarVehiculoCaravana(Guid idCaravana, int numero, decimal manifiesto)
        {
            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", idCaravana);
            SqlParameter m_inNumero = new SqlParameter("@inNumero", numero);
            SqlParameter m_nuManifiesto = new SqlParameter("@nuManifiesto", manifiesto);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", (byte?)0);

            using (var q = this.CreateStoreCommand("stpVehiculosCaravana", CommandType.StoredProcedure, m_idCaravana, m_inNumero, m_nuManifiesto, m_vcUsuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void EliminarVehiculoCaravana(Guid idCaravana, int numero)
        {
            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", idCaravana);
            SqlParameter m_inNumero = new SqlParameter("@inNumero", numero);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", (byte?)3);

            using (var q = this.CreateStoreCommand("stpVehiculosCaravana", CommandType.StoredProcedure, m_idCaravana, m_inNumero, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void CambiarNombreCaravana(Guid idCaravana, string descripcion)
        {
            if ((!string.IsNullOrEmpty(descripcion)) && descripcion.Length > 150)
                descripcion = descripcion.Substring(0, 150);
            SqlParameter m_idCaravana = new SqlParameter();
            m_idCaravana.ParameterName = "@idCaravana";
            m_idCaravana.Direction = ParameterDirection.InputOutput;
            m_idCaravana.Value = idCaravana;
            m_idCaravana.DbType = DbType.Guid;
            m_idCaravana.IsNullable = true;

            SqlParameter m_vcDescripcion = new SqlParameter("@vcDescripcion", descripcion);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 7);

            using (var q = this.CreateStoreCommand("stpCaravanas", CommandType.StoredProcedure, m_idCaravana, m_vcDescripcion, m_vcUsuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public Guid CrearCaravana(int idRuta, string descripcion)
        {
            if ((!string.IsNullOrEmpty(descripcion)) && descripcion.Length > 150)
                descripcion = descripcion.Substring(0, 150);

            SqlParameter m_idCaravana = new SqlParameter();
            m_idCaravana.ParameterName = "@idCaravana";
            m_idCaravana.Direction = ParameterDirection.Output;
            m_idCaravana.DbType = DbType.Guid;
            m_idCaravana.IsNullable = true;

            SqlParameter m_inIdRuta = new SqlParameter("@inIdRuta", idRuta);
            SqlParameter m_vcDescripcion = new SqlParameter("@vcDescripcion", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[0]);
            SqlParameter m_vcPlaca = new SqlParameter("@vcPlaca", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[1]);
            SqlParameter m_vcTelefono = new SqlParameter("@vcTelefono", descripcion.Split('|').Length == 1 ? descripcion : descripcion.Split('|')[2]);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", (byte?)0);

            using (var q = this.CreateStoreCommand("stpCaravanas", CommandType.StoredProcedure, m_idCaravana, m_inIdRuta, m_vcDescripcion, m_vcUsuario, m_tyOperacion, m_vcPlaca, m_vcTelefono))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
                return (Guid)m_idCaravana.Value;
            }
        }
        public void EliminarCaravana(Guid idCaravana)
        {
            using (this.Database.Connection.CreateConnectionScope())
            {
                using (IDbCommand escoltaCmd = this.CreateStoreCommand("stpEscoltasCaravana", CommandType.StoredProcedure,
                    new SqlParameter("@idCaravana", idCaravana),
                    new SqlParameter("@tyOperacion", 2)))
                {
                    escoltaCmd.CommandTimeout = 60;
                    escoltaCmd.ExecuteNonQuery();
                }
                using (IDbCommand refuerzoCmd = this.CreateStoreCommand("stpRefuerzosCaravana", CommandType.StoredProcedure,
                    new SqlParameter("@idCaravana", idCaravana),
                    new SqlParameter("@tyOperacion", 2)))
                {
                    refuerzoCmd.CommandTimeout = 60;
                    refuerzoCmd.ExecuteNonQuery();
                }
                using (IDbCommand vehiculoCmd = this.CreateStoreCommand("stpVehiculosCaravana", CommandType.StoredProcedure,
                    new SqlParameter("@idCaravana", idCaravana),
                    new SqlParameter("@tyOperacion", 2)))
                {
                    vehiculoCmd.CommandTimeout = 60;
                    vehiculoCmd.ExecuteNonQuery();
                }
                using (IDbCommand caravanaCmd = this.CreateStoreCommand("stpCaravanas", CommandType.StoredProcedure,
                    new SqlParameter("@idCaravana", idCaravana),
                    new SqlParameter("@tyOperacion", 2)))
                {
                    caravanaCmd.CommandTimeout = 60;
                    caravanaCmd.ExecuteNonQuery();
                }
            }
        }

        public VehiculoSatelital[] ObtenerSatelitalesAsignados()
        {
            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure))
            {
                q.CommandTimeout = 60;
                return q.Materialize<VehiculoSatelital>((r) => new VehiculoSatelital
                {
                    fidVehiculosSatelital = r.Field<int>("fidVehiculosSatelital"),
                    fvcPlaca = r.Field<string>("fvcPlaca"),
                    fidSatelital = r.Field<int>("fidSatelital"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcURL = r.Field<string>("fvcURL"),
                    fvcUsuario = r.Field<string>("fvcUsuario"),
                    fvcClave = r.Field<string>("fvcClave")
                }).ToArray();
            }
        }

        public Satelital[] ObtenerSatelitales()
        {
            SqlParameter tyOperacion = new SqlParameter("@TipoConsulta", 5);

            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure, tyOperacion))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Satelital>((r) => new Satelital
                {
                    fidSatelital = r.Field<int>("fidSatelital"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcURL = r.Field<string>("fvcURL")
                }).ToArray();
            }
        }

        public void CrearSatelital(string nombre, string url)
        {
            SqlParameter tyOperacion = new SqlParameter("@TipoConsulta", 6);
            SqlParameter vcDescripcion = new SqlParameter("@fvcDescripcion", nombre);
            SqlParameter vcURL = new SqlParameter("@fvcURL", url);

            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure, tyOperacion, vcDescripcion, vcURL))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                {
                    q.ExecuteNonQuery();
                }
            }
        }

        public void AsignarSatelitalAVehiculo(string placa, int idSatelital, string usuario, string clave)
        {
            SqlParameter tyOperacion = new SqlParameter("@TipoConsulta", 2);
            SqlParameter vcPlaca = new SqlParameter("@fvcPlaca", placa);
            SqlParameter vcUsuario = new SqlParameter("@fvcUsuario", usuario);
            SqlParameter vcClave = new SqlParameter("@fvcClave", clave);
            SqlParameter fidSatelital = new SqlParameter("@fidSatelital", idSatelital);

            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure, tyOperacion, vcPlaca, vcUsuario, vcClave, fidSatelital))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                {
                    q.ExecuteNonQuery();
                }
            }
        }

        public VehiculoSatelital ObtenerVehiculoSatelitalPorPlaca(string placa)
        {
            SqlParameter tyOperacion = new SqlParameter("@TipoConsulta", 9);
            SqlParameter vcPlaca = new SqlParameter("@fvcPlaca", placa);

            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure, tyOperacion, vcPlaca))
            {
                q.CommandTimeout = 60;

                try
                {
                    return q.Materialize<VehiculoSatelital>((r) => new VehiculoSatelital
                    {
                        fidVehiculosSatelital = r.Field<int>("fidVehiculosSatelital"),
                        fidSatelital = r.Field<int>("fidSatelital"),
                        fvcPlaca = r.Field<string>("fvcPlaca"),
                        fvcDescripcion = r.Field<string>("fvcDescripcion"),
                        fvcURL = r.Field<string>("fvcURL"),
                        fvcUsuario = r.Field<string>("fvcUsuario"),
                        fvcClave = r.Field<string>("fvcClave")
                    }).ToArray()[0];
                }
                catch
                {
                    throw;
                }
            }
        }

        public Satelital ObtenerSatelitalPorId(int id)
        {
            SqlParameter tyOperacion = new SqlParameter("@TipoConsulta", 10);
            SqlParameter vcSatelital = new SqlParameter("@fidSatelital", id);

            using (var q = this.CreateStoreCommand("stpSatelital", CommandType.StoredProcedure, tyOperacion, vcSatelital))
            {
                q.CommandTimeout = 60;

                try
                {
                    return q.Materialize<Satelital>((r) => new Satelital
                    {
                        fidSatelital = r.Field<int>("fidSatelital"),
                        fvcDescripcion = r.Field<string>("fvcDescripcion"),
                        fvcURL = r.Field<string>("fvcURL")
                    }).ToArray()[0];
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion

        #region MIGRACION COM+
        public void CambiarEstado(decimal identificacion, short razon, short estado, string observaciones)
        {
            SqlParameter m_nuIdentificacion = new SqlParameter("@nuIdentificacion", identificacion);
            SqlParameter m_smIdEstadoConductor = new SqlParameter("@smIdEstadoConductor", estado);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 2);
            //---------------------------------------------------------------------
            SqlParameter m_dtFecha = new SqlParameter("@dtFecha", DateTime.Now);
            SqlParameter m_txComentario = new SqlParameter("@txComentario", observaciones);
            SqlParameter m_btRazon = new SqlParameter("@btUltimo", razon);
            SqlParameter m_tyOperacionLog = new SqlParameter("@tyOperacion", 0);

            using (var q = this.CreateStoreCommand("stpConductores", CommandType.StoredProcedure, m_nuIdentificacion, m_smIdEstadoConductor, m_vcUsuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }

            using (var l = this.CreateStoreCommand("stpLogConductores", CommandType.StoredProcedure, m_dtFecha, m_nuIdentificacion, m_smIdEstadoConductor, m_txComentario,
                                                                                                    m_btRazon, m_vcUsuario, m_tyOperacionLog))
            {
                l.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    l.ExecuteNonQuery();
            }
        }
        public void ActualizarUbicaConductor(decimal identificacion, string ubicacion)
        {
            SqlParameter m_nuIdentificacion = new SqlParameter("@nuIdentificacion", identificacion);
            SqlParameter m_stCiudad = new SqlParameter("@stCiudad", ubicacion);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 5);

            using (var q = this.CreateStoreCommand("stpConductores", CommandType.StoredProcedure, m_nuIdentificacion, m_stCiudad, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void VehiculosControlCarreteraInsertar(Guid caravana, int idPuntoRuta, short idEstadoVehiculo,
                                                      string observacion, Guid? idPuntoEntrega)
        {
            SqlParameter m_idControlCarretera = new SqlParameter("@idControlCarretera", null);
            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", caravana);
            SqlParameter m_inIdPuntoRuta = new SqlParameter("@inIdPuntoRuta", idPuntoRuta);
            SqlParameter m_dtReporte = new SqlParameter("@dtReporte", DateTime.Now);
            SqlParameter m_smIdEstadoVehiculo = new SqlParameter("@smIdEstadoVehiculo", idEstadoVehiculo);
            SqlParameter m_txObservacion = new SqlParameter("@txObservacion", observacion);
            SqlParameter m_inIdPuntoEntrega = new SqlParameter("@inIdPuntoEntrega", idPuntoEntrega);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 0);

            using (var q = this.CreateStoreCommand("stpConductores", CommandType.StoredProcedure, m_idControlCarretera, m_idCaravana, m_inIdPuntoRuta, m_dtReporte,
                                                                                                  m_smIdEstadoVehiculo, m_txObservacion, m_inIdPuntoEntrega, m_vcUsuario,
                                                                                                  m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void ActualizaCopias(decimal manifiesto)
        {
            SqlParameter m_nuManifiesto = new SqlParameter("@nuManifiesto", manifiesto);
            SqlParameter m_btImpreso = new SqlParameter("@btImpreso", 0);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 8);

            using (var q = this.CreateStoreCommand("stpManifiestos", CommandType.StoredProcedure, m_nuManifiesto, m_btImpreso, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void ActualizaCarnet(decimal manifiesto, int carnet, string trailer)
        {
            SqlParameter m_nuManifiesto = new SqlParameter("@nuManifiesto", manifiesto);
            SqlParameter m_inNumeroCarnet = new SqlParameter("@inNumeroCarnet", carnet);
            SqlParameter m_vcPlacaComplemento = new SqlParameter("@vcPlacaComplemento", trailer);
            SqlParameter m_tyOperacion = new SqlParameter("@vcPlacaComplemento", 11);

            using (var q = this.CreateStoreCommand("stpManifiestos", CommandType.StoredProcedure, m_nuManifiesto, m_inNumeroCarnet,
                                                                                                  m_vcPlacaComplemento, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
        }
        public void ActualizaEstado(string placa, short estado, short razonEstadoVehiculo, string razon, Guid? caravana, string ultimaUbicacion)
        {
            SqlParameter m_dtFecha = new SqlParameter("@dtFecha", DateTime.Now);
            SqlParameter m_vcPlaca = new SqlParameter("@vcPlaca", placa);
            SqlParameter m_smIdEstadoVehiculo = new SqlParameter("@smIdEstadoVehiculo", estado);
            SqlParameter m_smIdRazonEstadoVehiculo = new SqlParameter("@smIdRazonEstadoVehiculo", razonEstadoVehiculo);
            SqlParameter m_txCometario = new SqlParameter("@txCometario", razon);
            SqlParameter m_btUltimo = new SqlParameter("@btUltimo", 1);
            SqlParameter m_idCaravana = new SqlParameter("@idCaravana", caravana);
            SqlParameter m_vcUsuario = new SqlParameter("@vcUsuario", Comun.Usuario);
            SqlParameter m_tyOperacion = new SqlParameter("@tyOperacion", 0);
            SqlParameter m_tyOperacionVeh = new SqlParameter("@tyOperacion", 2);

            using (var q = this.CreateStoreCommand("stpLogVehiculo", CommandType.StoredProcedure, m_dtFecha, m_vcPlaca, m_smIdEstadoVehiculo, m_smIdRazonEstadoVehiculo,
                                                                                                  m_txCometario, m_btUltimo, m_idCaravana, m_vcUsuario, m_tyOperacion))
            {
                q.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    q.ExecuteNonQuery();
            }
            using (var v = this.CreateStoreCommand("stpVehiculos", CommandType.StoredProcedure, m_vcPlaca, m_smIdEstadoVehiculo, ultimaUbicacion,
                                                                                                  m_txCometario, m_btUltimo, m_idCaravana, m_vcUsuario, m_tyOperacionVeh))
            {
                v.CommandTimeout = 60;
                using (this.Database.Connection.CreateConnectionScope())
                    v.ExecuteNonQuery();
            }
        }
        #endregion

        public static int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static short? ToNullableShort(string s)
        {
            short i;
            if (short.TryParse(s, out i)) return i;
            return null;
        }
    }
}
