//using System;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.InteropServices;
//using Icoltrans.Sigelo.Comunes;
//using Icoltrans.Sigelo.Entity.Vehiculos;
//using rveh_Vehiculos;
//using Icoltrans.Sigelo.Entity.Produccion.ComPlus;
//using System.Collections.Generic;


using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Entity.Vehiculos;
using rveh_Vehiculos;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class VehiculosNovedades : IDisposable
    {

        #region Variables
        private clsNovedades instanciaCom;
        #endregion

        #region Constructor / Destructor
        public VehiculosNovedades()
        {
            instanciaCom = new clsNovedades();
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
            }
            catch { }
        }
        #endregion

        #region Metodos
        ///// <summary>
        ///// Iniciars the recorrido.
        ///// </summary>
        ///// <param name="vaCaravana">The va caravana.</param>
        ///// <param name="dtHora">The dt hora.</param>
        ///// <param name="vaPunto">The va punto.</param>
        //public void IniciarRecorrido(Guid vaCaravana, DateTime dtHora, Guid vaPunto)
        //{
        //    instanciaCom.IniciaRecorridos(Utilidades.AjustarGuid(vaCaravana), dtHora, Utilidades.AjustarGuid(vaPunto), Comun.Usuario);
        //}
        ///// <summary>
        ///// Finalizars the recorrido.
        ///// </summary>
        ///// <param name="vaCaravana">The va caravana.</param>
        ///// <param name="dtHora">The dt hora.</param>
        //public void FinalizarRecorrido(Guid vaCaravana, DateTime dtHora)
        //{
        //    instanciaCom.FinalizaRecorrido(Utilidades.AjustarGuid(vaCaravana), dtHora);
        //}


        ///// <summary>
        ///// Reportes the carretera.
        ///// </summary>
        ///// <param name="idCaravana">The identifier caravana.</param>
        ///// <param name="idPuntoRuta">The identifier punto ruta.</param>
        ///// <param name="horaReporte">The hora reporte.</param>
        ///// <param name="idEstadoVehiculo">The identifier estado vehiculo.</param>
        ///// <param name="idRazon">The identifier razon.</param>
        ///// <param name="nombreRazon">The nombre razon.</param>
        ///// <param name="ubicacion">The ubicacion.</param>
        ///// <param name="idPuntoEntrega">The identifier punto entrega.</param>
        //public void ReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime horaReporte, short idEstadoVehiculo, short idRazon,
        //    string nombreRazon, string ubicacion, Guid[] idPuntoEntrega)
        //{
        //    ADOR.Recordset puntosEntrega = null;
        //    if (idPuntoEntrega != null && idPuntoEntrega.Length > 0)
        //    {
        //        ADODB.Recordset puntosEntregaPaso = null;
        //        using (ProduccionBasicas interop = new ProduccionBasicas())
        //            puntosEntregaPaso = interop.PuntoEntregaCaravana(idCaravana);

        //        if (puntosEntregaPaso != null)
        //        {
        //            puntosEntrega = new ADOR.Recordset();
        //            puntosEntrega.CursorLocation = ADOR.CursorLocationEnum.adUseClient;
        //            puntosEntrega.CursorType = ADOR.CursorTypeEnum.adOpenStatic;
        //            puntosEntrega.LockType = ADOR.LockTypeEnum.adLockBatchOptimistic;

        //            puntosEntregaPaso.MoveFirst();
        //            foreach (ADODB.Field item in puntosEntregaPaso.Fields)
        //            {
        //                puntosEntrega.Fields.Append(item.Name,
        //                    (ADOR.DataTypeEnum)item.Type,
        //                    item.DefinedSize,
        //                   (ADOR.FieldAttributeEnum)item.Attributes,
        //                    Missing.Value);
        //                puntosEntrega.Fields[item.Name].Precision = item.Precision;
        //            }
        //            puntosEntrega.Open();
        //            while (!puntosEntregaPaso.EOF)
        //            {
        //                var fields = puntosEntregaPaso.Fields;
        //                object valor = fields["fidPuntoEntrega"].Value;
        //                try
        //                {
        //                    Guid puntoEntrega = new Guid(valor.ToString());
        //                    if (idPuntoEntrega.Contains(puntoEntrega))
        //                    {
        //                        fields["Unificar"].Value = true;
        //                        puntosEntregaPaso.Update();
        //                    }
        //                }
        //                catch { }
        //                puntosEntrega.AddNew(Missing.Value, Missing.Value);
        //                foreach (ADODB.Field item in fields)
        //                    puntosEntrega.Fields[item.Name].Value = item.Value;

        //                puntosEntrega.Update(Missing.Value, Missing.Value);
        //                puntosEntregaPaso.MoveNext();
        //            }
        //        }
        //    }
        //    if (ubicacion == null)
        //        ubicacion = string.Empty;

        //    instanciaCom.ReporteCarretera(Utilidades.AjustarGuid(idCaravana),
        //                idPuntoRuta,
        //                horaReporte,
        //                idEstadoVehiculo,
        //                idRazon,
        //                nombreRazon,
        //                puntosEntrega,
        //                ubicacion);
        //}


        //public void CambioCarvanas(Guid caravanaOrigen, Guid caravanaDestino, bool nuevaCaravana, bool todoOrigen, bool todoDestino,
        //    string ubicacion, VehiculoCaravana[] vehiculos)
        //{
        //    if (vehiculos == null) throw new ArgumentNullException("vehiculos");

        //    ADOR.Recordset cambiosvehiculos = new ADOR.Recordset();
        //    cambiosvehiculos.CursorLocation = ADOR.CursorLocationEnum.adUseClient;
        //    cambiosvehiculos.CursorType = ADOR.CursorTypeEnum.adOpenStatic;
        //    cambiosvehiculos.LockType = ADOR.LockTypeEnum.adLockBatchOptimistic;
        //    cambiosvehiculos.Fields.Append("Prioridad", ADOR.DataTypeEnum.adBoolean);
        //    cambiosvehiculos.Fields.Append("finNumeroCarnet", ADOR.DataTypeEnum.adInteger);
        //    cambiosvehiculos.Fields.Append("fidVehiculoCaravana", ADOR.DataTypeEnum.adGUID);
        //    cambiosvehiculos.Fields.Append("Estado", ADOR.DataTypeEnum.adVarChar, 1);
        //    cambiosvehiculos.Fields.Append("fvcPlaca", ADOR.DataTypeEnum.adVarChar, 10);
        //    cambiosvehiculos.Fields.Append("fnuManifiesto", ADOR.DataTypeEnum.adNumeric);
        //    cambiosvehiculos.Fields["fnuManifiesto"].Precision = 20;
        //    cambiosvehiculos.Fields.Append("Trailer", ADOR.DataTypeEnum.adVarChar, 10);
        //    cambiosvehiculos.Fields.Append("fnuIdentificacion", ADOR.DataTypeEnum.adNumeric);
        //    cambiosvehiculos.Fields["fnuIdentificacion"].Precision = 20;
        //    cambiosvehiculos.Fields.Append("fbtNoCambiar", ADOR.DataTypeEnum.adBoolean);
        //    cambiosvehiculos.Open();
        //    foreach (var item in vehiculos)
        //    {
        //        cambiosvehiculos.AddNew(Missing.Value, Missing.Value);
        //        cambiosvehiculos.Fields["Prioridad"].Value = item.Vehiculo.Prioridad;
        //        cambiosvehiculos.Fields["finNumeroCarnet"].Value = item.NumeroCarnet.Value;
        //        cambiosvehiculos.Fields["fidVehiculoCaravana"].Value = Utilidades.AjustarGuid(item.Id);
        //        cambiosvehiculos.Fields["Estado"].Value = item.Estado;
        //        cambiosvehiculos.Fields["fvcPlaca"].Value = item.Vehiculo.Placa;
        //        cambiosvehiculos.Fields["fnuManifiesto"].Value = item.Manifiesto.Value;
        //        cambiosvehiculos.Fields["Trailer"].Value = item.Vehiculo.Complemento;
        //        cambiosvehiculos.Fields["fnuIdentificacion"].Value = item.Conductor.Numero;
        //        cambiosvehiculos.Fields["fbtNoCambiar"].Value = item.NoCambiar;
        //        cambiosvehiculos.Update(Missing.Value, Missing.Value);
        //    }

        //    //Public Sub CambioCarvanas(ByVal vaIdCaravanaOri As Variant, ByVal rsVehiculos As ADOR.Recordset, _
        //    //              ByVal rsEscoltas As ADOR.Recordset, ByVal vaIdCaravanaDes As Variant, _
        //    //              ByVal blNuevo As Boolean, ByVal blTodoOri As Boolean, _
        //    //              ByVal blTodoDes As Boolean, ByVal dtHora As Date, _
        //    //              ByVal rsrefuerzos As ADOR.Recordset, Optional ByVal stUbicacion As String = "")

        //    instanciaCom.CambioCarvanas(
        //        Utilidades.AjustarGuid(caravanaOrigen),      //vaIdCaravanaOri
        //        cambiosvehiculos,                                    //rsVehiculos
        //        null,                                                       //rsEscoltas: Siempre nulo
        //        Utilidades.AjustarGuid(caravanaDestino),    //vaIdCaravanaDes
        //        nuevaCaravana,                                      // blNuevo: Si la caravana de destino es nueva
        //        todoOrigen,                                            // blTodoOri: Finaliza la caravana de origen
        //        todoDestino,                                           // blTodoDes: Finaliza la caravana de destino
        //        DateTime.Now,                                       // dtHora
        //        null,                                                      // rsrefuerzos: Siempre nulo
        //        ubicacion                                               // stUbicacion
        //        );
        //}
        #endregion










        //#region Variables
        //private clsNovedades instanciaCom;
        //#endregion

        //#region Constructor / Destructor
        //public VehiculosNovedades()
        //{
        //    instanciaCom = new clsNovedades();
        //}
        //public void Dispose()
        //{
        //    try
        //    {
        //        if (instanciaCom != null)
        //        {
        //            Marshal.FinalReleaseComObject(instanciaCom);
        //            instanciaCom = null;
        //        }
        //    }
        //    catch { }
        //}
        //#endregion

        //#region Metodos
        ///// <summary>
        ///// Iniciars the recorrido.
        ///// </summary>
        ///// <param name="vaCaravana">The va caravana.</param>
        ///// <param name="dtHora">The dt hora.</param>
        ///// <param name="vaPunto">The va punto.</param>
        //public void IniciarRecorrido(Guid vaCaravana, DateTime dtHora, Guid vaPunto)
        //{
        //    instanciaCom.IniciaRecorridos(Utilidades.AjustarGuid(vaCaravana), dtHora, Utilidades.AjustarGuid(vaPunto), Comun.Usuario);
        //}
        ///// <summary>
        ///// Finalizars the recorrido.
        ///// </summary>
        ///// <param name="vaCaravana">The va caravana.</param>
        ///// <param name="dtHora">The dt hora.</param>
        //public void FinalizarRecorrido(Guid vaCaravana, DateTime dtHora)
        //{
        //    instanciaCom.FinalizaRecorrido(Utilidades.AjustarGuid(vaCaravana), dtHora);
        //}
        ///// <summary>
        ///// Reportes the carretera.
        ///// </summary>
        ///// <param name="idCaravana">The identifier caravana.</param>
        ///// <param name="idPuntoRuta">The identifier punto ruta.</param>
        ///// <param name="horaReporte">The hora reporte.</param>
        ///// <param name="idEstadoVehiculo">The identifier estado vehiculo.</param>
        ///// <param name="idRazon">The identifier razon.</param>
        ///// <param name="nombreRazon">The nombre razon.</param>
        ///// <param name="ubicacion">The ubicacion.</param>
        ///// <param name="idPuntoEntrega">The identifier punto entrega.</param>
        //public void ReporteCarretera(Guid idCaravana, int idPuntoRuta, DateTime horaReporte, short idEstadoVehiculo, short idRazon,
        //    string nombreRazon, string ubicacion, Guid[] idPuntoEntrega)
        //{
        //    List<PuntoEntregaCaravana> puntosEntrega = null;
        //    if (idPuntoEntrega != null && idPuntoEntrega.Length > 0)
        //    {
        //        List<PuntoEntregaCaravana> puntosEntregaPaso = null;
        //        using (ProduccionBasicas interop = new ProduccionBasicas())
        //            puntosEntregaPaso = interop.PuntoEntregaCaravana(idCaravana);

        //        if (puntosEntregaPaso != null)
        //        {
        //            puntosEntregaPaso.ForEach(p =>
        //            {
        //                if (idPuntoEntrega.Contains(p.fidPuntoEntrega))
        //                {
        //                    p.Unificar = true;
        //                }
        //                puntosEntrega.AddRange(puntosEntregaPaso);
        //            });
        //            puntosEntrega.AddRange(puntosEntregaPaso);
        //        }
        //    }
        //    if (ubicacion == null)
        //        ubicacion = string.Empty;

        //    instanciaCom.ReporteCarretera(Utilidades.AjustarGuid(idCaravana),
        //                idPuntoRuta,
        //                horaReporte,
        //                idEstadoVehiculo,
        //                idRazon,
        //                nombreRazon,
        //                puntosEntrega,
        //                ubicacion);
        //}

        //public void CambioCarvanas(Guid caravanaOrigen, Guid caravanaDestino, bool nuevaCaravana, bool todoOrigen, bool todoDestino,
        //    string ubicacion, VehiculoCaravana[] vehiculos)
        //{
        //    if (vehiculos == null) throw new ArgumentNullException("vehiculos");

        //    ADOR.Recordset cambiosvehiculos = new ADOR.Recordset();
        //    cambiosvehiculos.CursorLocation = ADOR.CursorLocationEnum.adUseClient;
        //    cambiosvehiculos.CursorType = ADOR.CursorTypeEnum.adOpenStatic;
        //    cambiosvehiculos.LockType = ADOR.LockTypeEnum.adLockBatchOptimistic;
        //    cambiosvehiculos.Fields.Append("Prioridad", ADOR.DataTypeEnum.adBoolean);
        //    cambiosvehiculos.Fields.Append("finNumeroCarnet", ADOR.DataTypeEnum.adInteger);
        //    cambiosvehiculos.Fields.Append("fidVehiculoCaravana", ADOR.DataTypeEnum.adGUID);
        //    cambiosvehiculos.Fields.Append("Estado", ADOR.DataTypeEnum.adVarChar, 1);
        //    cambiosvehiculos.Fields.Append("fvcPlaca", ADOR.DataTypeEnum.adVarChar, 10);
        //    cambiosvehiculos.Fields.Append("fnuManifiesto", ADOR.DataTypeEnum.adNumeric);
        //    cambiosvehiculos.Fields["fnuManifiesto"].Precision = 20;
        //    cambiosvehiculos.Fields.Append("Trailer", ADOR.DataTypeEnum.adVarChar, 10);
        //    cambiosvehiculos.Fields.Append("fnuIdentificacion", ADOR.DataTypeEnum.adNumeric);
        //    cambiosvehiculos.Fields["fnuIdentificacion"].Precision = 20;
        //    cambiosvehiculos.Fields.Append("fbtNoCambiar", ADOR.DataTypeEnum.adBoolean);
        //    cambiosvehiculos.Open();
        //    foreach (var item in vehiculos)
        //    {
        //        cambiosvehiculos.AddNew(Missing.Value, Missing.Value);
        //        cambiosvehiculos.Fields["Prioridad"].Value = item.Vehiculo.Prioridad;
        //        cambiosvehiculos.Fields["finNumeroCarnet"].Value = item.NumeroCarnet.Value;
        //        cambiosvehiculos.Fields["fidVehiculoCaravana"].Value = Utilidades.AjustarGuid(item.Id);
        //        cambiosvehiculos.Fields["Estado"].Value = item.Estado;
        //        cambiosvehiculos.Fields["fvcPlaca"].Value = item.Vehiculo.Placa;
        //        cambiosvehiculos.Fields["fnuManifiesto"].Value = item.Manifiesto.Value;
        //        cambiosvehiculos.Fields["Trailer"].Value = item.Vehiculo.Complemento;
        //        cambiosvehiculos.Fields["fnuIdentificacion"].Value = item.Conductor.Numero;
        //        cambiosvehiculos.Fields["fbtNoCambiar"].Value = item.NoCambiar;
        //        cambiosvehiculos.Update(Missing.Value, Missing.Value);
        //    }

        //    //Public Sub CambioCarvanas(ByVal vaIdCaravanaOri As Variant, ByVal rsVehiculos As ADOR.Recordset, _
        //    //              ByVal rsEscoltas As ADOR.Recordset, ByVal vaIdCaravanaDes As Variant, _
        //    //              ByVal blNuevo As Boolean, ByVal blTodoOri As Boolean, _
        //    //              ByVal blTodoDes As Boolean, ByVal dtHora As Date, _
        //    //              ByVal rsrefuerzos As ADOR.Recordset, Optional ByVal stUbicacion As String = "")

        //    instanciaCom.CambioCarvanas(
        //        Utilidades.AjustarGuid(caravanaOrigen),      //vaIdCaravanaOri
        //        cambiosvehiculos,                                    //rsVehiculos
        //        null,                                                       //rsEscoltas: Siempre nulo
        //        Utilidades.AjustarGuid(caravanaDestino),    //vaIdCaravanaDes
        //        nuevaCaravana,                                      // blNuevo: Si la caravana de destino es nueva
        //        todoOrigen,                                            // blTodoOri: Finaliza la caravana de origen
        //        todoDestino,                                           // blTodoDes: Finaliza la caravana de destino
        //        DateTime.Now,                                       // dtHora
        //        null,                                                      // rsrefuerzos: Siempre nulo
        //        ubicacion                                               // stUbicacion
        //        );
        //}
        //#endregion
    }
}
