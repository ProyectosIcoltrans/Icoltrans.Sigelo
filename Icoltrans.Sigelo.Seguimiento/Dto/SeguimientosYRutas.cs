using System.Runtime.Serialization;
using Icoltrans.Sigelo.Data;

namespace Icoltrans.Sigelo.Web.Host
{
    [DataContract]
    public sealed class ParametrosIniciales
    {
        #region Parametros
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Ruta[] Origenes
        {
            get;
            internal set;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public Ruta[] Destinos
        {
            get;
            internal set;
        }
        [DataMember]
        public ParametrosPantalla Parametros
        {
            get;
            internal set;
        }
        #endregion
    }
}