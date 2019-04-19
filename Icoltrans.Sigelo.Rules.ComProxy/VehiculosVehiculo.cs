using System;
using System.Runtime.InteropServices;
using dveh_Vehiculos;
using Icoltrans.Sigelo.Data;

namespace Icoltrans.Sigelo.Rules.ComProxy
{
    public sealed class VehiculosVehiculo : IDisposable
    {
        #region Variables
        //private clsVehiculo instanciaCom;
        private VehiculosModelo modelo;
        #endregion

        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiculosVehiculo"/> class.
        /// </summary>
        public VehiculosVehiculo()
        {
            //instanciaCom = new clsVehiculo();
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
        public void ActualizaEstado(string placa, short estado, short razonEstadoVehiculo, string razon, Guid? caravana, string ultimaUbicacion)
        {
            if (string.IsNullOrEmpty(ultimaUbicacion))
                ultimaUbicacion = string.Empty;

            object caravanaFinal = Type.Missing;
            if (caravana.HasValue)
                caravanaFinal = Utilidades.AjustarGuid(caravana.Value);

            modelo.ActualizaEstado(placa, estado, razonEstadoVehiculo, razon, caravana, ultimaUbicacion);
        }
        #endregion
    }
}
