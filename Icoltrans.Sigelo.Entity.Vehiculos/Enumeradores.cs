using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    [DataContract]
    public enum TipoCaravana
    {
        [EnumMember]
        Nacional = 0,
        [EnumMember]
        Urbana = 1,
        [EnumMember]
        Regional = 2
    }
}
