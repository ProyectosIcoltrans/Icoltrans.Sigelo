//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public sealed partial class Reporte
    {
        [DataMember]
    	public System.DateTime HoraSistema { get; set; }
        [DataMember]
    	public System.DateTime HoraReporte { get; set; }
        [DataMember]
    	public string PuntoRuta { get; set; }
        [DataMember]
    	public string Municipio { get; set; }
        [DataMember]
    	public string Operador { get; set; }
        [DataMember]
    	public string Estado { get; set; }
        [DataMember]
    	public string Peaje { get; set; }
        [DataMember]
    	public string Observacion { get; set; }
        [DataMember]
    	public string Caravanas { get; set; }
    }
}