using System;
using System.Linq;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using Icoltrans.Sigelo.Win.Vehiculos.Code;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public class SeguimientoViewModel : ViewModelBase
    {
        #region Variables
        private bool marcado;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public SeguimientoViewModel()
        {
            if (Contexto.ParametrosIniciales != null && Contexto.ParametrosIniciales.PerfilesUsuario.Any(p => p.IdOpcion == new Guid("D90A5CC0-1C6D-4272-A54F-FA422AB4C446")))
            {
                ReportarActualCommand = new DelegateCommand(ReportarActual, (e) => true);
                ReportarSiguienteCommand = new DelegateCommand(ReportarSiguiente, (e) => true);
                ReportarEnMapaCommand = new DelegateCommand(ReportarEnMapa, (e) => true);
            }
        }
        public SeguimientoViewModel(CaravanaDisponible origen)
        {
            if (origen == null) throw new ArgumentNullException("origen");
            
            this.Model = new DetalleCaravana();
            this.Model.Caravana = new Seguimiento();
            this.Model.Caravana.fbtUrbana = origen.fbtUrbana;
            this.Model.Caravana.fidCaravana = origen.fidCaravana;
            this.Model.Caravana.fvcDescripcion = origen.fvcDescripcion;
        }
        #endregion

        #region Propiedades
        internal event EventHandler<EventArgs> BuscarPlacaEnMapa;

        public ICommand ReportarActualCommand { get; private set; }
        public ICommand ReportarSiguienteCommand { get; private set; }
        public ICommand ReportarEnMapaCommand { get; private set; }
        public bool Marcado
        {
            get
            {
                return marcado;
            }
            set
            {
                if (marcado != value)
                {
                    marcado = value;
                    OnPropertyChanged("Marcado");
                }
            }
        }
        public DetalleCaravana Model { get; set; }
        internal Guid IdCaravana
        {
            get
            {
                Guid retorno = Guid.Empty;
                if (this.Model != null && this.Model.Caravana != null)
                    retorno = this.Model.Caravana.fidCaravana;

                return retorno;
            }
        }
        internal bool SiguientePunto { get; set; }
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void ReportarActual(object param)
        {
            SiguientePunto = false;
            ((App)App.Current).Navegar(OpcionesNavegacion.ControlCarretera);
        }
        private void ReportarSiguiente(object param)
        {
            SiguientePunto = true;
            ((App)App.Current).Navegar(OpcionesNavegacion.ControlCarretera);
        }
        private void ReportarEnMapa(object param)
        {
            if (BuscarPlacaEnMapa != null)
                this.BuscarPlacaEnMapa(this, (EventArgs)param);
        }
        #endregion

        #region Eventos
        #endregion
    }
}
