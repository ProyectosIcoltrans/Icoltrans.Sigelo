using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Seguridad
{
    [DataContract]
    public enum Modulo
    {
        [EnumMember]
        Maestros = 0,
        [EnumMember]
        Seguridad = 1,
        [EnumMember]
        Vehiculos = 2,
        [EnumMember]
        Nomina = 3,
        [EnumMember]
        Produccion = 4
    }
}
