using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    [DataContract]
    public class VehiculoSatelital
    {
        [DataMember]
        public int fidVehiculosSatelital { get; set; }
        [DataMember]
        public int fidSatelital { get; set; }
        [DataMember]
        public Satelital Satelital { get; set; }
        [DataMember]
        public string fvcPlaca { get; set; }
        [DataMember]
        public string fvcDescripcion { get; set; }
        [DataMember]
        public string fvcURL { get; set; }
        [DataMember]
        public string fvcUsuario { get; set; }
        [DataMember]
        public string fvcClave { get; set; }
    }
}
