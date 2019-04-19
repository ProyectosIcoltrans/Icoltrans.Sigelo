using System.Collections.ObjectModel;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model del final de recorrido
    /// </summary>
    public sealed class InicioRecorridoViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private ObservableCollection<CaravanaDisponible> caravanasDisponibles;
        private CaravanaDisponible caravanaSeleccionada;
        private bool permitirGrabar;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public InicioRecorridoViewModel()
        {
            GrabarCommand = new DelegateCommand(Grabar, (e) => true);
            if (!base.IsInDesignMode)
                DatosIniciales();
        }
        #endregion

        #region Propiedades
        public ICommand GrabarCommand { get; private set; }
        public ObservableCollection<CaravanaDisponible> CaravanasDisponibles
        {
            get { return this.caravanasDisponibles; }
            private set
            {
                if (this.caravanasDisponibles != value)
                {
                    this.caravanasDisponibles = value;
                    OnPropertyChanged("CaravanasDisponibles");
                }
            }
        }
        public CaravanaDisponible CaravanaSeleccionada
        {
            get { return this.caravanaSeleccionada; }
            set
            {
                if (this.caravanaSeleccionada != value)
                {
                    this.caravanaSeleccionada = value;
                    OnPropertyChanged("CaravanaSeleccionada");
                    PermitirGrabar = value != null;
                }
            }
        }
        public bool PermitirGrabar
        {
            get { return this.permitirGrabar; }
            set
            {
                if (this.permitirGrabar != value)
                {
                    this.permitirGrabar = value;
                    OnPropertyChanged("PermitirGrabar");
                }
            }
        }
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void DatosIniciales()
        {
            MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerCaravanasDisponibles += (s, e) =>
                {
                    CaravanasDisponibles = new ObservableCollection<CaravanaDisponible>(e.Result);
                    if (caravanasDisponibles.Count > 0)
                    {
                        CaravanaSeleccionada = caravanasDisponibles[0];
                        PermitirGrabar = true;
                    }
                    MostrarProgreso = false;
                };
            ctx.ObtenerCaravanasDisponibles();
        }
        private void Grabar(object param)
        {
            if (this.caravanaSeleccionada == null) return;
            MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarIniciarRecorrido += (s, e) => Cancelar(null);
            ctx.IniciarRecorrido(CaravanaSeleccionada.fidCaravana);
        }
        #endregion

        #region Eventos
        #endregion
    }
}
