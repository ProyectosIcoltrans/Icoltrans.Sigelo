using System;
using System.Runtime.Serialization;

namespace Icoltrans.Sigelo.Entity.Produccion
{
    [DataContract]
    public sealed class PuntosEntregaCaravana2
    {
        #region Propiedades
        [DataMember]
        public Guid CaravanaId { get;  set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public PuntoEntrega2[] PuntosEntrega { get;  set; }
        #endregion
    }
}