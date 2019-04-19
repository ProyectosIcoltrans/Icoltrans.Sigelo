using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model del cambio de refuerzos
    /// </summary>
    public sealed class RefuerzosViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private ObservableCollection<FuncionEscolta> funcionesRefuerzo;
        private ObservableCollection<Refuerzo> refuerzosDisponibles;
        private ObservableCollection<RefuerzoYFuncionViewModel> refuerzosCaravana;
        private List<RefuerzoYFuncionViewModel> paraAgregar = new List<RefuerzoYFuncionViewModel>();
        private List<RefuerzoYFuncionViewModel> paraEliminar = new List<RefuerzoYFuncionViewModel>();
        private FuncionEscolta funcionEscoltaSeleccionada;
        private Refuerzo refuerzoSeleccionado;
        private string nombreCaravana;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public RefuerzosViewModel()
        {
            AdicionarCommand = new DelegateCommand(Adicionar, (e) => true);
            RetirarCommand = new DelegateCommand(Retirar, (e) => true);
            GrabarCommand = new DelegateCommand(Grabar, (e) => true);
            if (!IsInDesignMode)
                ObtenerDatosIniciales();
        }
        #endregion

        #region Propiedades
        public ICommand AdicionarCommand { get; private set; }
        public ICommand RetirarCommand { get; private set; }
        public ICommand GrabarCommand { get; private set; }
        public ObservableCollection<FuncionEscolta> FuncionesRefuerzo
        {
            get { return this.funcionesRefuerzo; }
            private set
            {
                if (this.funcionesRefuerzo != value)
                {
                    this.funcionesRefuerzo = value;
                    OnPropertyChanged("FuncionesRefuerzo");
                }
            }
        }
        public ObservableCollection<Refuerzo> RefuerzosDisponibles
        {
            get { return this.refuerzosDisponibles; }
            private set
            {
                if (this.refuerzosDisponibles != value)
                {
                    this.refuerzosDisponibles = value;
                    OnPropertyChanged("RefuerzosDisponibles");
                }
            }
        }
        public ObservableCollection<RefuerzoYFuncionViewModel> RefuerzosCaravana
        {
            get { return this.refuerzosCaravana; }
            private set
            {
                if (this.refuerzosCaravana != value)
                {
                    this.refuerzosCaravana = value;
                    OnPropertyChanged("RefuerzosCaravana");
                }
            }
        }
        public FuncionEscolta FuncionEscoltaSeleccionada
        {
            get { return this.funcionEscoltaSeleccionada; }
            set
            {
                if (this.funcionEscoltaSeleccionada != value)
                {
                    this.funcionEscoltaSeleccionada = value;
                    OnPropertyChanged("FuncionEscoltaSeleccionada");
                }
            }
        }
        public Refuerzo RefuerzoSeleccionado
        {
            get { return this.refuerzoSeleccionado; }
            set
            {
                if (this.refuerzoSeleccionado != value)
                {
                    this.refuerzoSeleccionado = value;
                    OnPropertyChanged("RefuerzoSeleccionado");
                }
            }
        }
        public string NombreCaravana
        {
            get { return this.nombreCaravana; }
            set
            {
                if (this.nombreCaravana != value)
                {
                    this.nombreCaravana = value;
                    OnPropertyChanged("NombreCaravana");
                }
            }
        }
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void Adicionar(object param)
        {
            if (refuerzosCaravana.Any(p => p.Refuerzo.Identificacion == refuerzoSeleccionado.Identificacion))
                return;

            RefuerzoYFuncionViewModel newRefuerzo = new RefuerzoYFuncionViewModel(this)
            {
                Funcion = funcionEscoltaSeleccionada,
                Refuerzo = refuerzoSeleccionado,
                RefuerzoCaravana = new RefuerzoCaravana()
                {
                    fnuIdentidad = refuerzoSeleccionado.Identificacion,
                    ftyIdFuncionEscolta = (short)funcionEscoltaSeleccionada.IdFuncionEscolta
                },
            };
            refuerzosCaravana.Add(newRefuerzo);
            paraAgregar.Add(newRefuerzo);
            var elimininar = paraEliminar.FirstOrDefault(p => p.Refuerzo.Identificacion == newRefuerzo.Refuerzo.Identificacion);
            if (elimininar != null)
                paraEliminar.Remove(elimininar);
        }
        private void Retirar(object param)
        {
            decimal? cedula = Convert.ToDecimal(param, CultureInfo.InvariantCulture);
            var eliminar = refuerzosCaravana.FirstOrDefault(p => p.Refuerzo.Identificacion == cedula);
            if (eliminar != null)
            {
                refuerzosCaravana.Remove(eliminar);
                paraAgregar.Remove(eliminar);
                paraEliminar.Add(eliminar);
            }
        }
        private void Grabar(object param)
        {
            if (paraAgregar.Count == 0 && paraEliminar.Count == 0)
                return;
            MostrarProgreso = true;
            bool[] finalizo = new bool[paraAgregar.Count + 1];
            int contador = 0;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarRetirarRefuerzoCaravana += (s, e) => { finalizo[finalizo.Length - 1] = true; FinalizarEspera(finalizo, true); };
            ctx.FinalizarAgregarRefuerzoCaravana += (s, e) =>
            {
                lock (this)
                {
                    finalizo[contador] = true;
                    contador++;
                }
                FinalizarEspera(finalizo, true);
            };
            Parallel.Invoke
                (
                () => ctx.RetirarRefuerzoCaravana(paraEliminar.Select(p => p.RefuerzoCaravana.fidRefuerzoCaravana).ToArray()),
                () => Parallel.ForEach(paraAgregar, p => ctx.AgregarRefuerzoCaravana(p.Refuerzo.Identificacion, (short)p.Funcion.IdFuncionEscolta))
                );
        }
        private void ObtenerDatosIniciales()
        {
            MostrarProgreso = true;
            bool[] finalizo = new bool[3];
            NombreCaravana = Contexto.CaravanaSeleccionada.Model.Caravana.fvcDescripcion;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerFuncionesEscolta += (s, e) =>
                {
                    FuncionesRefuerzo = new ObservableCollection<FuncionEscolta>(e.Result.OrderBy(p => p.Descripcion));
                    FuncionEscoltaSeleccionada = funcionesRefuerzo[0];
                    finalizo[0] = true;
                    FinalizarEspera(finalizo);
                };
            ctx.FinalizarObtenerRefuerzosDisponibles += (s, e) =>
                {
                    RefuerzosDisponibles = new ObservableCollection<Refuerzo>(e.Result);
                    refuerzoSeleccionado = refuerzosDisponibles[0];
                    finalizo[1] = true;
                    FinalizarEspera(finalizo);
                };
            ctx.FinalizarObtenerRefuerzosPorCaravana += (s, e) =>
                {
                    RefuerzosCaravana = new ObservableCollection<RefuerzoYFuncionViewModel>
                        (
                            from w in e.Result
                            select new RefuerzoYFuncionViewModel(this)
                            {
                                RefuerzoCaravana = w,
                                Funcion = funcionesRefuerzo.FirstOrDefault(p => p.IdFuncionEscolta == w.ftyIdFuncionEscolta),
                                Refuerzo = refuerzosDisponibles.FirstOrDefault(p => p.Identificacion == w.fnuIdentidad)
                            }
                        );
                    finalizo[2] = true;
                    FinalizarEspera(finalizo);
                };

            ctx.ObtenerFuncionesEscolta();
            ctx.ObtenerRefuerzosDisponibles();
            ctx.ObtenerRefuerzosPorCaravana();
        }
        private void FinalizarEspera(bool[] finalizo, bool final = false)
        {
            if (!finalizo.Contains(false))
                if (final)
                    base.Cancelar(null);
                else
                    MostrarProgreso = false;
        }
        #endregion

        #region Eventos
        #endregion

    }
}
