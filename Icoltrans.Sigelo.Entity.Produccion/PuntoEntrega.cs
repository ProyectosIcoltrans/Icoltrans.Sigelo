//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Icoltrans.Sigelo.Entity.Produccion
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections.Generic;
    
    [DataContract]
    public sealed partial class PuntoEntrega
    {
        [DataMember]
    	public int Id { get; set; }
        [DataMember]
    	public string fnuManifiesto { get; set; }
        [DataMember]
    	public string fvcPlaca { get; set; }
        [DataMember]
    	public string fvcPlacaComplemento { get; set; }
        [DataMember]
    	public string Conductor { get; set; }
        [DataMember]
    	public string Origen { get; set; }
        [DataMember]
    	public string Destino { get; set; }
        [DataMember]
    	public Nullable<short> idCentro { get; set; }
        [DataMember]
    	public string fvcNombre { get; set; }
        [DataMember]
    	public string fvcIDUbicacion { get; set; }
        [DataMember]
    	public string CiudadDpto { get; set; }
        [DataMember]
    	public Nullable<System.Guid> fidOperacion { get; set; }
        [DataMember]
    	public string fvcDescripcion { get; set; }
        [DataMember]
    	public string fvcCartaPorte { get; set; }
        [DataMember]
    	public string fvcFactura { get; set; }
        [DataMember]
    	public Nullable<System.DateTime> fdtCompromisoCliente { get; set; }
        [DataMember]
    	public string Punto { get; set; }
        [DataMember]
    	public string fvcDireccion { get; set; }
        [DataMember]
    	public Nullable<double> frlPeso { get; set; }
        [DataMember]
    	public Nullable<double> frlVolumen { get; set; }
        [DataMember]
    	public Nullable<int> finUnidades { get; set; }
        [DataMember]
    	public Nullable<int> Cant { get; set; }
        [DataMember]
    	public Nullable<System.DateTime> fdtHoraInicial { get; set; }
        [DataMember]
    	public Nullable<System.DateTime> fdtHoraFinal { get; set; }
        [DataMember]
    	public Nullable<int> finTipoNovedad { get; set; }
        [DataMember]
    	public string fvcDescripcionNovedad { get; set; }
        [DataMember]
    	public Nullable<System.DateTime> fdtEntrega { get; set; }
        [DataMember]
    	public string SiglaNovedad { get; set; }
    }
}