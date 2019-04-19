using System;
using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Maestros
{

    [DataContract]
    public sealed class ParametrosPantalla
    {
        [DataMember]
        public Uri InformeOperaciones { get; set; }
        [DataMember]
        public Uri InformeNovedadesManifiesto { get; set; }
        [DataMember]
        public Uri CapturaNovedades { get; set; }
        [DataMember]
        public Uri InformeSatelital { get; set; }
        [DataMember]
        public string ParametrosSatelital { get; set; }
    }
}
