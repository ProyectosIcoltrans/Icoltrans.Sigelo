using System;
using System.Linq;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Nomina;

namespace Icoltrans.Sigelo.Rules.Nomina
{
    public sealed class ControladorNomina : IDisposable
    {
        #region Variables
        private NominaModelo modelo;
        #endregion

        #region Constructor / Destructor
        public ControladorNomina()
        {
            modelo = new NominaModelo();
        }
        public void Dispose()
        {
            if (this.modelo != null)
                this.modelo.Dispose();
        }
        #endregion

        #region Metodos
        public Escolta1[] ObtenerEscoltas()
        {
            return modelo.EmpleadosGrupoCargo(0, 0).ToArray();
        }
        public Usuario ObtenerInformacionUsuario()
        {
            return modelo.ObtenerInformacionUsuario();
        }
        #endregion
    }
}
