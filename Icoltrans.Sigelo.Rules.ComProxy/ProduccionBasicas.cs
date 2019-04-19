using System;
using System.Runtime.InteropServices;
using dprod_Consultas;
using Icoltrans.Sigelo.Entity.Produccion;
using System.Data.SqlClient;
using System.Data;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Produccion.ComPlus;
using System.Collections.Generic;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class ProduccionBasicas : IDisposable
    {
        #region Variables
        private clsBasicas instanciaCom;
        private ProduccionModelo modelo;
        #endregion

        #region Constructor / Destructor
        public ProduccionBasicas()
        {
            instanciaCom = new clsBasicas();
            modelo = new ProduccionModelo();
        }
        public void Dispose()
        {
            try
            {
                if (instanciaCom != null)
                {
                    Marshal.FinalReleaseComObject(instanciaCom);
                    instanciaCom = null;
                }

                if (this.modelo != null)
                    this.modelo.Dispose();
            }
            catch { }
        }
        #endregion

        #region Metodos
        //internal ADODB.Recordset PuntoEntregaCaravana(Guid idCaravana)
        //{
        //    return instanciaCom.PuntoEntregaCaravana(Utilidades.AjustarGuid(idCaravana));
        //}
        #endregion
        //#region MIGRACION COM+
        //internal List<PuntoEntregaCaravana> PuntoEntregaCaravana(Guid idCaravana)
        //{
        //    return modelo.PuntoEntregaCaravana(idCaravana);
        //}

        //internal void ReporteCarretera(Guid vaCaravana , int lnIdPuntoRuta, DateTime dtReporte,
        //                               short inIdEstadoVehiculo, short inRazon, string stRazon, 
        //                               List<PuntoEntregaCaravana> lsOpera, string stUbicacion = "")
        //{
        //    if (dtReporte > DateTime.Now)
        //        dtReporte = DateTime.Now;


        //    //modelo.ReporteCarretera(vaCaravana);
        //}
        //#endregion
    }
}