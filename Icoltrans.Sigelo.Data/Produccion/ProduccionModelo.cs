using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Icoltrans.Sigelo.Entity.Produccion;
using Microsoft.Data.Extensions;
using Icoltrans.Sigelo.Entity.Produccion.ComPlus;
using System.Collections.Generic;

namespace Icoltrans.Sigelo.Data
{
    public partial class ProduccionModelo 
    {
        #region Métodos
        /// <summary>
        /// Obteners the punto entrega.
        /// </summary>
        /// <param name="caravana">The caravana.</param>
        /// <returns></returns>
        public PuntoEntrega[] ObtenerPuntoEntrega(Guid caravana)
        {
            PuntoEntrega[] t = null;
            Novedad[] n = null;
            using (var q = this.CreateStoreCommand("stpQryOperaCaravanaManifiesto", CommandType.StoredProcedure,
                new SqlParameter("@idCaravana", caravana)))
            {
                q.CommandTimeout = 60;
                t = q.Materialize<PuntoEntrega>((r) => new PuntoEntrega
               {
                   fnuManifiesto = r.Field<string>("fnuManifiesto"),
                   fvcPlaca = r.Field<string>("fvcPlaca"),
                   fvcPlacaComplemento = r.Field<string>("fvcPlacaComplemento"),
                   Conductor = r.Field<string>("Conductor"),
                   Origen = r.Field<string>("Origen"),
                   Destino = r.Field<string>("Destino"),
                   idCentro = r.Field<Int16?>("idCentro"),
                   fvcNombre = r.Field<string>("fvcNombre"),
                   fvcIDUbicacion = r.Field<string>("fvcIDUbicacion"),
                   CiudadDpto = r.Field<string>("CiudadDpto"),
                   fidOperacion = r.Field<Guid?>("fidOperacion"),
                   fvcDescripcion = r.Field<string>("fvcDescripcion"),
                   fvcDescripcionNovedad = r.Field<string>("fvcDescripcionNovedad"),
                   fvcCartaPorte = r.Field<string>("fvcCartaPorte"),
                   fvcFactura = r.Field<string>("fvcFactura"),
                   fdtCompromisoCliente = r.Field<DateTime?>("fdtCompromisoCliente"),
                   Punto = r.Field<string>("Punto"),
                   fvcDireccion = r.Field<string>("fvcDireccion"),
                   frlPeso = r.Field<double?>("frlPeso"),
                   frlVolumen = r.Field<double?>("frlVolumen"),
                   finUnidades = r.Field<Int32?>("finUnidades"),
                   Cant = r.Field<Int32?>("Cant"),
                   fdtEntrega = r.Field<DateTime?>("fdtEntrega"),
                   SiglaNovedad = r.Field<string>("SiglaNovedad")
               }
               ).ToArray();
            }
            using (var g = this.CreateStoreCommand("stpQryTiposNovedades"))
            {
                g.CommandTimeout = 60;
                n = g.Materialize<Novedad>((r) => new Novedad
               {
                   Id = r.Field<Int32>("finTipoNovedad"),
                   Descripcion = r.Field<string>("fvcDescripcion")
               }).ToArray();
            }
            SqlParameter idOperacion = new SqlParameter("@idOperacion", SqlDbType.UniqueIdentifier);
            SqlParameter idPuntoEntrega = new SqlParameter("@idPuntoEntrega", SqlDbType.UniqueIdentifier);
            using (var x = this.CreateStoreCommand("stpOperaciones", CommandType.StoredProcedure,
                idOperacion,
                new SqlParameter("@tyOperacion", 3)))
            using (var h = this.CreateStoreCommand("stpHorariosPunto", CommandType.StoredProcedure,
                idPuntoEntrega,
                new SqlParameter("@tyOperacion", 4)))
            {
                x.CommandTimeout = 60;
                h.CommandTimeout = 60;
                foreach (var item in t.Where(w => w.fidOperacion.HasValue))
                {
                    idOperacion.Value = item.fidOperacion;

                    var y = x.Materialize<Operacion>((r) => new Operacion
                    {
                        fdtCitaEntrega = r.Field<DateTime?>("fdtCitaEntrega"),
                        fidPuntoEntregaDestino = r.Field<Guid>("fidPuntoEntregaDestino"),
                        finTipoNovedad = r.Field<Int32?>("finTipoNovedad")
                    }).FirstOrDefault();

                    if (y != null)
                    {
                        item.fdtCompromisoCliente = y.fdtCitaEntrega;
                        if (item.finTipoNovedad.HasValue && n.FirstOrDefault(w => w.Id == item.finTipoNovedad) != null)
                            item.fvcDescripcionNovedad = n.FirstOrDefault(w => w.Id == item.finTipoNovedad).Descripcion;

                        if (!item.fdtCompromisoCliente.HasValue)
                        {
                            idPuntoEntrega.Value = y.fidPuntoEntregaDestino;

                            var u = h.Materialize<PuntoEntrega>((r) => new PuntoEntrega
                            {
                                fdtHoraFinal = r.Field<DateTime?>("fdtHoraFinal"),
                                fdtHoraInicial = r.Field<DateTime?>("fdtHoraInicial")
                            }).FirstOrDefault();

                            if (u != null)
                            {
                                item.fdtHoraInicial = u.fdtHoraInicial;
                                item.fdtHoraFinal = u.fdtHoraFinal;
                            }
                        }
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// Obteners the punto entrega para control.
        /// </summary>
        /// <param name="caravana">The caravana.</param>
        /// <returns></returns>
        public PuntoEntrega2[] ObtenerPuntoEntregaParaControl(Guid caravana)
        {
            SqlParameter idCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryPuntosEntregaCaravana", CommandType.StoredProcedure, idCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<PuntoEntrega2>((r) => new PuntoEntrega2
                {
                    fidPuntoEntrega = r.Field<Guid>("fidPuntoEntrega"),
                    fnuManifiesto = r.Field<decimal>("fnuManifiesto").ToString(),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcDireccion = r.Field<string>("fvcDireccion"),
                    fvcTelefono = r.Field<string>("fvcTelefono"),
                    fvcCodigoEDI = r.Field<string>("fvcCodigoEDI"),
                    fsmOrden = r.Field<Int16>("fsmOrden"),
                    frlKilos = r.Field<double>("frlKilos"),
                    finUnidades = r.Field<Int32?>("finUnidades")
                }
                ).ToArray();
            }
        }
        /// <summary>
        /// Obteners the sucursal.
        /// </summary>
        /// <param name="idSucursal">The id sucursal.</param>
        /// <returns></returns>
        public Sucursal ObtenerSucursal(Int16 idSucursal)
        {
            SqlParameter smIdSucursal = new SqlParameter("@smIdSucursal", idSucursal);

            using (var q = this.CreateStoreCommand("stpQryPuntoSucursal", CommandType.StoredProcedure, smIdSucursal))
            {
                q.CommandTimeout = 60;
                return q.Materialize<Sucursal>((r) => new Sucursal
                {
                    fidPuntoEntrega = r.Field<Guid>("fidPuntoEntrega"),
                    fvcCodigoEDI = r.Field<string>("fvcCodigoEDI"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcDireccion = r.Field<string>("fvcDireccion"),
                    fvcIDUbicacion = r.Field<string>("fvcIDUbicacion"),
                    fvcTelefono = r.Field<string>("fvcTelefono")
                }
                ).FirstOrDefault();
            }
        }
        #endregion
        #region MIGRACION COM+
        public List<PuntoEntregaCaravana> PuntoEntregaCaravana(Guid caravana)
        {

            SqlParameter smIdCaravana = new SqlParameter("@idCaravana", caravana);

            using (var q = this.CreateStoreCommand("stpQryPuntosEntregaCaravana", CommandType.StoredProcedure, smIdCaravana))
            {
                q.CommandTimeout = 60;
                return q.Materialize<PuntoEntregaCaravana>((r) => new PuntoEntregaCaravana
                {
                    Unificar = r.Field<bool>("Unificar"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcDireccion = r.Field<string>("fvcDireccion"),
                    fvcTelefono = r.Field<string>("fvcTelefono"),
                    fvcCodigoEDI = r.Field<string>("fvcCodigoEDI"),
                    fnuManifiesto = r.Field<int>("fnuManifiesto"),
                    fidPuntoEntrega = r.Field<Guid>("fidPuntoEntrega"),
                    fsmOrden = r.Field<int>("fsmOrden"),
                    frlKilos = r.Field<decimal>("frlKilos"),
                    finUnidades = r.Field<int>("finUnidades")
                }).ToList();
            }
        }

        internal void ReporteCarretera(Guid vaCaravana, int lnIdPuntoRuta, DateTime dtReporte,
                               short inIdEstadoVehiculo, short inRazon, string stRazon,
                               List<PuntoEntregaCaravana> lsOpera, string stUbicacion = "")
        {
            if (dtReporte > DateTime.Now)
                dtReporte = DateTime.Now;
            
        }
        internal void ControlCarreteraInsertar(Guid vaCaravana, Guid lnIdPuntoRuta, DateTime dtReporte,
                                               short inIdEstadoVehiculo, string stObservacion, Nullable<Guid> lnIdPuntoEntrega = null)
        {
            SqlParameter smIdCaravana = new SqlParameter("@idCaravana", null);

            using (var q = this.CreateStoreCommand("stpQryPuntosEntregaCaravana", CommandType.StoredProcedure, smIdCaravana))
            {
                q.CommandTimeout = 60;
                q.Materialize<PuntoEntregaCaravana>((r) => new PuntoEntregaCaravana
                {
                    Unificar = r.Field<bool>("Unificar"),
                    fvcDescripcion = r.Field<string>("fvcDescripcion"),
                    fvcDireccion = r.Field<string>("fvcDireccion"),
                    fvcTelefono = r.Field<string>("fvcTelefono"),
                    fvcCodigoEDI = r.Field<string>("fvcCodigoEDI"),
                    fnuManifiesto = r.Field<int>("fnuManifiesto"),
                    fidPuntoEntrega = r.Field<Guid>("fidPuntoEntrega"),
                    fsmOrden = r.Field<int>("fsmOrden"),
                    frlKilos = r.Field<decimal>("frlKilos"),
                    finUnidades = r.Field<int>("finUnidades")
                }).ToList();
            }
        }


        #endregion
    }
}
