using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    [DataContract]
    public class Satelital
    {
        [DataMember]
        public int fidSatelital { get; set; }
        [DataMember]
        public string fvcDescripcion { get; set; }
        [DataMember]
        public string fvcURL { get; set; }
    }
}
