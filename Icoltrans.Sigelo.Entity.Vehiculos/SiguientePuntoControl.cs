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
    public sealed partial class SiguientePuntoControl
    {
        [DataMember]
    	public int Id { get; set; }
        [DataMember]
    	public int finIdPuntoRuta { get; set; }
        [DataMember]
    	public Nullable<short> fsmIdTipoPeaje { get; set; }
        [DataMember]
    	public short fsmOrden { get; set; }
        [DataMember]
    	public Nullable<short> ftyMinutosPuntoAnterior { get; set; }
        [DataMember]
    	public string fvcDescripcion { get; set; }
        [DataMember]
    	public string fvcIDUbicacion { get; set; }
        [DataMember]
    	public Nullable<int> finIdRuta { get; set; }
        [DataMember]
    	public Nullable<bool> fbtControl { get; set; }
    }
}
