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
    public sealed partial class Escolta
    {
        [DataMember]
    	public decimal Identificacion { get; set; }
        [DataMember]
    	public string Clase { get; set; }
        [DataMember]
    	public string Nombre { get; set; }
        [DataMember]
    	public string PrimerApellido { get; set; }
        [DataMember]
    	public string SegundoApellido { get; set; }
        [DataMember]
    	public string Telefonos { get; set; }
        [DataMember]
    	public string Email { get; set; }
        [DataMember]
    	public string Ciudad { get; set; }
        [DataMember]
    	public string Direccion { get; set; }
    }
}
