using System;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public sealed class EscoltaYFuncionViewModel : ViewModelBase
    {
        #region Variables
        private EscoltasViewModel padre;
        #endregion

        #region Constructor / Destructor
        public EscoltaYFuncionViewModel()
        {
        }
        public EscoltaYFuncionViewModel(EscoltasViewModel padre)
            : this()
        {
            this.padre = padre;
        }
        #endregion

        #region Propiedades
        public ICommand RetirarCommand { get { return padre.RetirarCommand; } }
        public FuncionEscolta Funcion { get; set; }
        public Escolta1 Escolta { get; set; }
        public Guid CaravanaId { get; set; }
        public EscoltaCaravana EscoltaCaravana { get; set; }
        #endregion
    }
}

