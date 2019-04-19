
using System.Runtime.Serialization;
namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    public sealed partial class Ruta
    {
        [DataMember]
        public string CiudadOrigen { get; set; }
        [DataMember]
        public string CiudadDestino { get; set; }
        [DataMember]
        public int CantidadPuntos { get; set; }
        [DataMember]
        public TipoCaravana Clase { get; set; }
    }
}
