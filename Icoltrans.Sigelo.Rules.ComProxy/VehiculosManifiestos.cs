using System;
using System.Runtime.InteropServices;
using dveh_Vehiculos;
using Icoltrans.Sigelo.Data;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class VehiculosManifiestos : IDisposable
    {
        #region Variables
        //private clsManifiestos instanciaCom;
        private VehiculosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiculosManifiestos"/> class.
        /// </summary>
        public VehiculosManifiestos()
        {
            //instanciaCom = new clsManifiestos();
            modelo = new VehiculosModelo();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                //if (instanciaCom != null)
                //{
                //    Marshal.FinalReleaseComObject(instanciaCom);
                //    instanciaCom = null;
                //}
            }
            catch { }
        }
        #endregion

        #region Metodos
        public void ActualizaCopias(decimal manifiesto)
        {
            modelo.ActualizaCopias(manifiesto);
        }
        public void ActualizaCarnet(decimal manifiesto, int carnet, string trailer)
        {
            if (string.IsNullOrEmpty(trailer))
                trailer = null;
            modelo.ActualizaCarnet(manifiesto, carnet, trailer);
        }
        #endregion
    }
}
