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
    /// View model del cambio de escoltas
    /// </summary>
    public sealed class EscoltasViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private ObservableCollection<FuncionEscolta> funcionesEscolta;
        private ObservableCollection<Escolta1> escoltasDisponibles;
        private ObservableCollection<EscoltaYFuncionViewModel> escoltasCaravana;
        private List<EscoltaYFuncionViewModel> paraAgregar = new List<EscoltaYFuncionViewModel>();
        private List<EscoltaYFuncionViewModel> paraEliminar = new List<EscoltaYFuncionViewModel>();
        private FuncionEscolta funcionEscoltaSeleccionada;
        private Escolta1 escoltaSeleccionado;
        private string nombreCaravana;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        public EscoltasViewModel()
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
        public ObservableCollection<FuncionEscolta> FuncionesEscolta
        {
            get { return this.funcionesEscolta; }
            private set
            {
                if (this.funcionesEscolta != value)
                {
                    this.funcionesEscolta = value;
                    OnPropertyChanged("FuncionesEscolta");
                }
            }
        }
        public ObservableCollection<Escolta1> EscoltasDisponibles
        {
            get { return this.escoltasDisponibles; }
            private set
            {
                if (this.escoltasDisponibles != value)
                {
                    this.escoltasDisponibles = value;
                    OnPropertyChanged("EscoltasDisponibles");
                }
            }
        }
        public ObservableCollection<EscoltaYFuncionViewModel> EscoltasCaravana
        {
            get { return this.escoltasCaravana; }
            private set
            {
                if (this.escoltasCaravana != value)
                {
                    this.escoltasCaravana = value;
                    OnPropertyChanged("EscoltasCaravana");
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
        public Escolta1 EscoltaSeleccionado
        {
            get { return this.escoltaSeleccionado; }
            set
            {
                if (this.escoltaSeleccionado != value)
                {
                    this.escoltaSeleccionado = value;
                    OnPropertyChanged("EscoltaSeleccionado");
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
            if (EscoltasCaravana.Any(p => p.Escolta.Cedula == escoltaSeleccionado.Cedula))
                return;

            EscoltaYFuncionViewModel newEscolta = new EscoltaYFuncionViewModel(this)
            {
                Funcion = funcionEscoltaSeleccionada,
                Escolta = escoltaSeleccionado,
                EscoltaCaravana = new EscoltaCaravana()
                {
                    fnuIdentidad = escoltaSeleccionado.Cedula,
                    ftyIdFuncionEscolta = (short)funcionEscoltaSeleccionada.IdFuncionEscolta
                },
                CaravanaId = Contexto.CaravanaSeleccionada.IdCaravana
            };
            EscoltasCaravana.Add(newEscolta);
            paraAgregar.Add(newEscolta);
            var elimininar = paraEliminar.FirstOrDefault(p => p.Escolta.Cedula == newEscolta.Escolta.Cedula);
            if (elimininar != null)
                paraEliminar.Remove(elimininar);
        }
        private void Retirar(object param)
        {
            decimal? cedula = Convert.ToDecimal(param, CultureInfo.InvariantCulture);
            var eliminar = EscoltasCaravana.FirstOrDefault(p => p.Escolta.Cedula == cedula);
            if (eliminar != null)
            {
                EscoltasCaravana.Remove(eliminar);
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
            ctx.FinalizarRetirarEscolta += (s, e) => {
                lock (this)
                {
                    finalizo[finalizo.Length - 1] = true;
                }
                FinalizarEspera(finalizo, true);
            };
            ctx.FinalizarAgregarEscolta += (s, e) =>
            {
                lock (this)
                {
                    finalizo[contador] = true;
                    contador++;
                }
                FinalizarEspera(finalizo, true);
            };

            ctx.RetirarEscolta(paraEliminar.Select(p => p.EscoltaCaravana.finIdEscoltaCaravana).ToArray());

            foreach (var p in paraAgregar)
            {
                ctx.AgregarEscolta(p.Escolta.Cedula, (short)p.Funcion.IdFuncionEscolta);
            }
            //Parallel.ForEach(paraAgregar, p => ctx.AgregarEscolta(p.Escolta.Cedula, (short)p.Funcion.IdFuncionEscolta));
            //Parallel.Invoke
            //    (
            //    () => ctx.RetirarEscolta(paraEliminar.Select(p => p.EscoltaCaravana.finIdEscoltaCaravana).ToArray()),
            //    () => Parallel.ForEach(paraAgregar, p => ctx.AgregarEscolta(p.Escolta.Cedula, (short)p.Funcion.IdFuncionEscolta))
            //    );
        }
        private void ObtenerDatosIniciales()
        {
            MostrarProgreso = true;
            bool[] finalizo = new bool[3];
            NombreCaravana = Contexto.CaravanaSeleccionada.Model.Caravana.fvcDescripcion;
            Contexto ctx = new Contexto(MostarVentaError);
            ctx.FinalizarObtenerFuncionesEscolta += (s, e) =>
                {
                    FuncionesEscolta = new ObservableCollection<FuncionEscolta>(e.Result.OrderBy(p => p.Descripcion));
                    FuncionEscoltaSeleccionada = funcionesEscolta[0];
                    finalizo[0] = true;
                    FinalizarEspera(finalizo);
                };
            ctx.FinalizarObtenerEscoltas += (s, e) =>
                {
                    EscoltasDisponibles = new ObservableCollection<Escolta1>(e.Result);
                    EscoltaSeleccionado = escoltasDisponibles[0];
                    finalizo[1] = true;
                    FinalizarEspera(finalizo);
                };
            ctx.FinalizarObtenerEscoltasPorCaravana += (s, e) =>
                {
                    EscoltasCaravana = new ObservableCollection<EscoltaYFuncionViewModel>
                        (
                            from w in e.Result
                            select new EscoltaYFuncionViewModel(this)
                            {
                                EscoltaCaravana = w,
                                Funcion = funcionesEscolta.FirstOrDefault(p => p.IdFuncionEscolta == w.ftyIdFuncionEscolta),
                                Escolta = escoltasDisponibles.FirstOrDefault(p => p.Cedula == w.fnuIdentidad)
                            }
                        );
                    finalizo[2] = true;
                    FinalizarEspera(finalizo);
                };

            ctx.ObtenerFuncionesEscolta();
            ctx.ObtenerEscoltas();
            ctx.ObtenerEscoltasPorCaravana();
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
