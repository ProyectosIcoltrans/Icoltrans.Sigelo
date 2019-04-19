using System;
using System.Runtime.InteropServices;
using dveh_Basicas;
using Icoltrans.Sigelo.Data;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class VehiculosConductores : IDisposable
    {
        #region Variables
        //private clsConductores instanciaCom;
        private VehiculosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiculosConductores"/> class.
        /// </summary>
        public VehiculosConductores()
        {
            //instanciaCom = new clsConductores();
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
        public void CambiarEstado(decimal identificacion, short razon, short estado, string observaciones)
        {
            if (estado.Equals(9))
            {
                observaciones = "VETADO DEL SISTEMA";
            }
            modelo.CambiarEstado(identificacion, razon, estado, observaciones);
        }
        public void ActualizarUbicaConductor(decimal identificacion, string ubicacion)
        {
            if (string.IsNullOrEmpty(ubicacion))
                ubicacion = string.Empty;

            modelo.ActualizarUbicaConductor(identificacion, ubicacion);
        }
        #endregion
    }
}
