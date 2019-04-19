using System;
using System.Runtime.InteropServices;
using dveh_Vehiculos;
using Icoltrans.Sigelo.Data;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class VehiculosControlCarretera : IDisposable
    {
        #region Variables
        //private clsControlCarretera instanciaCom;
        private VehiculosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiculosControlCarretera"/> class.
        /// </summary>
        public VehiculosControlCarretera()
        {
            //instanciaCom = new clsControlCarretera();
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
        public void Insertar(Guid caravana, int idPuntoRuta, short idEstadoVehiculo, string observacion, Guid? idPuntoEntrega)
        {
            modelo.VehiculosControlCarreteraInsertar(caravana, idPuntoRuta, idEstadoVehiculo, observacion, idPuntoEntrega);
        }
        #endregion
    }
}
