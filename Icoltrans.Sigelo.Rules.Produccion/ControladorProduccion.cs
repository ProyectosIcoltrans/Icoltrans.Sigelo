using System;
using Icoltrans.Sigelo.Data;
using Icoltrans.Sigelo.Entity.Produccion;

namespace Icoltrans.Sigelo.Rules.Produccion
{
    public sealed class ControladorProduccion : IDisposable
    {
        #region Variables
        ProduccionModelo modelo;
        #endregion

        #region Constructor / Destructor
        public ControladorProduccion()
        {
            modelo = new ProduccionModelo();
        }
        public void Dispose()
        {
            if (this.modelo != null)
                this.modelo.Dispose();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obteners the puntos entrega por caravana.
        /// </summary>
        /// <param name="caravana">The caravana.</param>
        /// <returns></returns>
        public PuntoEntrega[] ObtenerPuntosEntregaPorCaravana(Guid caravana)
        {

            return modelo.ObtenerPuntoEntrega(caravana);
        }
        /// <summary>
        /// Obteners the punto entrega para control.
        /// </summary>
        /// <param name="caravana">The caravana.</param>
        /// <returns></returns>
        public PuntoEntrega2[] ObtenerPuntoEntregaParaControl(Guid caravana)
        {
            return modelo.ObtenerPuntoEntregaParaControl(caravana);
        }
        /// <summary>
        /// Obteners the sucursal.
        /// </summary>
        /// <param name="idSucursal">The id sucursal.</param>
        /// <returns></returns>
        public Sucursal ObtenerSucursal(Int16 idSucursal)
        {
            return modelo.ObtenerSucursal(idSucursal);
        }

        public PuntoEntrega PuntoEntregaCaravana(Guid caravana)
        {
            return new PuntoEntrega();
        }
        #endregion
    }
}
