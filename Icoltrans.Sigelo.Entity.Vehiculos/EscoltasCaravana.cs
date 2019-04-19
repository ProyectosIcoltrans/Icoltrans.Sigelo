using System;
using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Vehiculos
{
    [DataContract]
    public sealed class EscoltasCaravana
    {
        #region Propiedades
        [DataMember]
        public Guid CaravanaId { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Escolta[] Escoltas { get; set; }
        #endregion
    }
}