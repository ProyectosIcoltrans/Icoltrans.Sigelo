using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public class RefuerzoYFuncionViewModel : ViewModelBase
    {
        #region Variables
        private RefuerzosViewModel padre;
        #endregion

        #region Constructor / Destructor
        public RefuerzoYFuncionViewModel()
        {
        }
        public RefuerzoYFuncionViewModel(RefuerzosViewModel padre)
            : this()
        {
            this.padre = padre;
        }
        #endregion

        #region Propiedades
        public ICommand RetirarCommand { get { return padre.RetirarCommand; } }
        public FuncionEscolta Funcion { get; set; }
        public Refuerzo Refuerzo { get; set; }
        internal RefuerzoCaravana RefuerzoCaravana { get; set; }
        #endregion
    }
}
