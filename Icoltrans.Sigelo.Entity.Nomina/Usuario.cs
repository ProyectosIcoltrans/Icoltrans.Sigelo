//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Icoltrans.Sigelo.Entity.Nomina
{
    using System;
    using System.Runtime.Serialization;
    
    [DataContract]
    public sealed partial class Usuario
    {
        [DataMember]
    	public short fsmIdSucursal { get; set; }
        [DataMember]
    	public string DesSucursal { get; set; }
        [DataMember]
    	public System.Guid fidRol { get; set; }
        [DataMember]
    	public short fsmIdArea { get; set; }
        [DataMember]
    	public string DesArea { get; set; }
        [DataMember]
    	public short ftyIdCompania { get; set; }
        [DataMember]
    	public string DesCompania { get; set; }
        [DataMember]
    	public short fsmIdCargo { get; set; }
        [DataMember]
    	public string DesCargo { get; set; }
        [DataMember]
    	public short fsmCentroCosto { get; set; }
        [DataMember]
    	public string DesCentro { get; set; }
        [DataMember]
    	public string NomEmpleado { get; set; }
        [DataMember]
    	public string fvcIDUbicacion { get; set; }
        [DataMember]
    	public string CiudadDpto { get; set; }
        [DataMember]
    	public short IdCOperativo { get; set; }
        [DataMember]
    	public System.Guid IdPunto { get; set; }
        [DataMember]
    	public decimal fnuIdentidad { get; set; }
    }
}
