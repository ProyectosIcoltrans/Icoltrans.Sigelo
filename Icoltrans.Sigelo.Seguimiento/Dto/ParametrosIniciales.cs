using System.Runtime.Serialization;
using Icoltrans.Sigelo.Entity.Maestros;
using Icoltrans.Sigelo.Entity.Nomina;
using Icoltrans.Sigelo.Entity.Seguridad;
using Icoltrans.Sigelo.Entity.Vehiculos;

namespace Icoltrans.Sigelo.ControlSeguimiento
{
    [DataContract]
    public sealed class ParametrosIniciales
    {
        #region Parametros
        [DataMember]
        public ParametrosPantalla Parametros
        {
            get;
            internal set;
        }
        [DataMember]
        public Usuario UsuarioActual { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public OpcionPerfil[] PerfilesUsuario { get; internal set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public CiudadRuta[] Origenes
        {
            get;
            internal set;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember]
        public CiudadRuta[] Destinos
        {
            get;
            internal set;
        }
        #endregion
    }
}