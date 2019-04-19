using System.Collections.Generic;
using System.Runtime.Serialization;
using Icoltrans.Sigelo.Entity.Produccion;
using Icoltrans.Sigelo.Entity.Vehiculos;

namespace Icoltrans.Sigelo.Web.Host
{
    [DataContract]
    public sealed class DetalleCaravana
    {
        #region Propiedades
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Conductor[] Conductores { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Escolta[] Escoltas { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Vehiculo[] Vehiculos { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Reporte[] Reportes { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public PuntoEntrega[] PuntosEntrega { get; internal set; }
        #endregion
    }
}