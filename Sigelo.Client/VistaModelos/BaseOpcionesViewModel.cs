
using System;
using System.Windows.Input;
namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model base para tpdas las opciones que funcionas como dialogos para cpaturar data
    /// </summary>
    public abstract class BaseOpcionesViewModel : ViewModelBase
    {
        #region Variables
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        protected BaseOpcionesViewModel()
            : base()
        {
            this.CancelCommand = new DelegateCommand(Cancelar, (e) => true);
        }

        #endregion

        #region Propiedades
        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>
        /// The cancel command.
        /// </value>
        public ICommand CancelCommand { get; private set; }
        #endregion

        #region Metodos
        
        #endregion

        #region Funciones
        protected void Cancelar(object param)
        {
            var actual = ((App)App.Current);
            OpcionesNavegacion opcion = OpcionesNavegacion.Caravanas;
            if (actual.OpcionAnterior.HasValue)
            {
                opcion = actual.OpcionAnterior.Value;
                actual.OpcionAnterior = null;
            }
            actual.Navegar(opcion);
        }
        #endregion

        #region Eventos
        #endregion
    }
}
