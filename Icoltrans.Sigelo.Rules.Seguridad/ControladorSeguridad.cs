using System;
using System.Linq;
using Icoltrans.Sigelo.Comunes;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Seguridad;

namespace Icoltrans.Sigelo.Rules.Seguridad
{
    public sealed class ControladorSeguridad : IDisposable
    {
        #region Variables
        private SeguridadModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ControladorMaestros"/> class.
        /// </summary>
        public ControladorSeguridad()
        {
            this.modelo = new SeguridadModelo();
        }
        public void Dispose()
        {
            if (this.modelo != null)
                modelo.Dispose();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtener los parametros necesario para la pantalla de control de seguimiento.
        /// </summary>
        /// <returns>Los parametros en un objeto de tipo <see cref="ParametrosPantalla"/></returns>
        public OpcionPerfil[] ObtenerOpcionesUsuario(Modulo modulo)
        {
            return modelo.ObtenerOpcionesUsuario(Comun.Usuario, (byte)modulo).ToArray();
        }
        #endregion

    }
}
