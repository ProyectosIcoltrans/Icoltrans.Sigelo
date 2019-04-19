using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Icoltrans.Sigelo.Win.Vehiculos.VistaModelos
{
    public sealed class SatelitalViewModel : BaseOpcionesViewModel
    {
        #region Variables
        //Pantalla creacion de Satelitales
        private bool mostrarNombreSatelital;
        private bool mostrarURLSatelital;
        private bool activarGuardarSatelital;
        private string descripcion;
        private string url;
        private string validacionSatelital;
        //Pantalla asignacion Satelital a Vehiculo
        private ObservableCollection<Satelital> satelitales;
        private ObservableCollection<VehiculoSatelital> vehiculoSatelitales;
        private string placa;
        private Satelital satelital;
        private VehiculoSatelital vehiculoSatelital;
        private string usuario;
        private string clave;
        private bool mostrarVehiculoSatelital;
        private bool activarGuardarVehiculoSatelital;
        private string validacionVehiculosSatelital;
        #endregion

        #region Propiedades
        //Pantalla creacion de Satelitales
        public ICommand CancelarCrearSatelitalCommand { get; private set; }
        public ICommand GuardarCrearSatelitalCommand { get; private set; }
        public ICommand CrearSatelitalCommand { get; private set; }
        //Pantalla asignacion Satelital a Vehiculo
        public ICommand RefrescarListaCommand { get; private set; }
        public ICommand GuardarVehiculoSatelitalCommand { get; private set; }
        //Pantalla creacion de Satelitales
        public bool ActivarGuardarSatelital
        {
            get { return this.activarGuardarSatelital; }
            set
            {
                if (this.activarGuardarSatelital != value)
                {
                    this.activarGuardarSatelital = value;
                    OnPropertyChanged("ActivarGuardarSatelital");
                }
            }
        }
        //Pantalla asignacion Satelital a Vehiculo
        public bool MostrarVehiculoSatelital
        {
            get { return this.mostrarVehiculoSatelital; }
            set
            {
                if (this.mostrarVehiculoSatelital != value)
                {
                    this.mostrarVehiculoSatelital = value;
                    OnPropertyChanged("MostrarVehiculoSatelital");
                }
            }
        }
        public ObservableCollection<Satelital> Satelitales
        {
            get { return this.satelitales; }
            private set
            {
                if (this.satelitales != value)
                {
                    this.satelitales = value;
                    OnPropertyChanged("Satelitales");
                }
            }
        }
        public ObservableCollection<VehiculoSatelital> VehiculoSatelitales
        {
            get { return this.vehiculoSatelitales; }
            private set
            {
                if (this.vehiculoSatelitales != value)
                {
                    this.vehiculoSatelitales = value;
                    OnPropertyChanged("VehiculoSatelitales");
                }
            }
        }
        public string Placa
        {
            get { return this.placa; }
            private set
            {
                if (this.placa != value)
                {
                    this.placa = value;
                    OnPropertyChanged("Placa");
                    this.BuscarPlaca(value);
                }
            }
        }
        public Satelital Satelital
        {
            get { return this.satelital; }
            private set
            {
                if (this.satelital != value)
                {
                    this.satelital = value;
                    OnPropertyChanged("Satelital");
                }
            }
        }
        public string Usuario
        {
            get { return this.usuario; }
            private set
            {
                if (this.usuario != value)
                {
                    this.usuario = value;
                    OnPropertyChanged("Usuario");
                }
            }
        }
        public string Clave
        {
            get { return this.clave; }
            private set
            {
                if (this.clave != value)
                {
                    this.clave = value;
                    OnPropertyChanged("Clave");
                }
            }
        }
        //
        public string Descripcion
        {
            get { return this.descripcion; }
            private set
            {
                if (this.descripcion != value)
                {
                    this.descripcion = value;
                    OnPropertyChanged("Descripcion");
                }
            }
        }
        public string URL
        {
            get { return this.url; }
            private set
            {
                if (this.url != value)
                {
                    this.url = value;
                    OnPropertyChanged("URL");
                }
            }
        }
        public string ValidacionSatelital
        {
            get { return this.validacionSatelital; }
            private set
            {
                if (this.validacionSatelital != value)
                {
                    this.validacionSatelital = value;
                    OnPropertyChanged("ValidacionSatelital");
                }
            }
        }
        public string ValidacionVehiculosSatelital
        {
            get { return this.validacionVehiculosSatelital; }
            private set
            {
                if (this.validacionVehiculosSatelital != value)
                {
                    this.validacionVehiculosSatelital = value;
                    OnPropertyChanged("ValidacionVehiculosSatelital");
                }
            }
        }

        #endregion

        public SatelitalViewModel()
        {
            this.InstaciarComandos();
            this.EstadoCrearVehiculoSatelital(true);
        }

        #region Funciones

        private void ActualizarInformacion()
        {
        }

        private void CancelarCrearSatelital()
        {
            ObtenerDatosIniciales();
            this.EstadoCrearVehiculoSatelital(true);
            this.Descripcion = string.Empty;
            //this.URL = string.Empty;
        }

        private void GuardarCrearSatelital()
        {
            MostrarProgreso = true;
            bool crear = true;
            StringBuilder error = new StringBuilder();

            Contexto ctx = new Contexto(MostarVentaError);

            if (!Uri.IsWellFormedUriString(this.URL, UriKind.Absolute))
            {
                crear = false;
                error.AppendLine("Por favor ingresar la URL valida");
            }

            if (string.IsNullOrEmpty(this.Descripcion))
            {
                error.AppendLine("Por favor ingresar el nombre del Satelital");
                crear = false;
            }

            if (crear)
            {
                this.ValidacionSatelital = string.Empty;

                ctx.FinalizarCrearSatelital += (s, e) =>
                {
                    MostrarProgreso = false;
                    this.ObtenerSatelitales();
                    this.EstadoCrearVehiculoSatelital(true);
                };

                ctx.CrearSatelital(this.descripcion, this.url);
            }
            else
            {
                MostrarProgreso = false;
                this.ValidacionSatelital = error.ToString();
            }
        }

        private void CrearSatelital()
        {
            this.EstadoCrearVehiculoSatelital(false);
        }

        private void GuardarVehiculoSatelital()
        {
            MostrarProgreso = true;
            bool crear = true;
            StringBuilder error = new StringBuilder();

            Contexto ctx = new Contexto(MostarVentaError);

            if (string.IsNullOrEmpty(this.placa))
            {
                crear = false;
                error.AppendLine("Por favor ingresar una placa valida");
            }

            if (this.satelital == null)
            {
                crear = false;
                error.AppendLine("Por favor seleccionar una empresa Satelital");
            }

            if (string.IsNullOrEmpty(this.usuario))
            {
                error.AppendLine("Por favor ingresar el usuario");
                crear = false;
            }

            if (string.IsNullOrEmpty(this.clave))
            {
                error.AppendLine("Por favor ingresar la clave");
                crear = false;
            }

            if (crear)
            {
                ctx.FinalizarAsignarSatelitalAVehiculo += (s, e) =>
                {
                    MostrarProgreso = false;
                    base.Cancelar(null);
                };

                ctx.AsignarSatelitalAVehiculo(this.placa, this.satelital.fidSatelital, this.usuario, this.clave);
            }
            else
            {
                MostrarProgreso = false;
                this.ValidacionVehiculosSatelital = error.ToString();
            }
        }

        private void InstaciarComandos()
        {
            RefrescarListaCommand = new DelegateCommand((param) => ActualizarInformacion(), (e) => true);
            CancelarCrearSatelitalCommand = new DelegateCommand((param) => CancelarCrearSatelital(), (e) => true);
            GuardarCrearSatelitalCommand = new DelegateCommand((param) => GuardarCrearSatelital(), (e) => true);
            CrearSatelitalCommand = new DelegateCommand((param) => CrearSatelital(), (e) => true);
            GuardarVehiculoSatelitalCommand = new DelegateCommand((param) => GuardarVehiculoSatelital(), (e) => true);
            this.ObtenerDatosIniciales();
        }

        private void EstadoCrearVehiculoSatelital(bool estado)
        {
            this.MostrarVehiculoSatelital = estado;
            this.ActivarGuardarSatelital = !estado;
            //this.ActivarGuardarVehiculoSatelital = estado;
            //this.MostrarNombreSatelital = !estado;
            //this.MostrarURLSatelital = !estado;
            //this.ActivarGuardarSatelital = !estado;

            //if (estado)
            //    timer.Stop();
            //else
            //    timer.Start();
        }

        private void ObtenerDatosIniciales()
        {
            this.ObtenerSatelitales();
            this.ObtenerSatelitalesAsignados();
        }

        private void ObtenerSatelitales()
        {
            //MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);

            ctx.FinalizarObtenerSatelitales += (s, e) =>
            {
                this.Satelitales = new ObservableCollection<Satelital>(e.Result);
            };

            ctx.ObtenerSatelitales();
        }

        private void ObtenerSatelitalesAsignados()
        {
            //MostrarProgreso = true;
            Contexto ctx = new Contexto(MostarVentaError);

            ctx.FinalizarObtenerSatelitalesAsignados += (s, e) =>
            {
                this.VehiculoSatelitales = new ObservableCollection<VehiculoSatelital>(e.Result);
                base.OnPropertyChanged("VehiculoSatelitales");
            };

            ctx.ObtenerSatelitalesAsignados();
        }

        private void BuscarPlaca(string placa)
        {
            this.vehiculoSatelital = this.vehiculoSatelitales.FirstOrDefault(w => w.fvcPlaca.ToUpper().Equals(placa.ToUpper()));


            if (this.vehiculoSatelital != null)
            {
                this.Placa = this.vehiculoSatelital.fvcPlaca;
                this.Satelital = this.satelitales.FirstOrDefault(w => w.fidSatelital.Equals(this.vehiculoSatelital.fidSatelital));
                this.Usuario = this.vehiculoSatelital.fvcUsuario;
                this.Clave = this.vehiculoSatelital.fvcClave;
            }

            //MostrarProgreso = false;
            //Contexto ctx = new Contexto(MostarVentaError);

            //ctx.FinalizarObtenerVehiculoSatelitalPorPlaca += (s, e) =>
            //{
            //    VehiculoSatelital vehiculo = (VehiculoSatelital)e.Result;

            //    if (vehiculo != null)
            //    {
            //        this.Satelital = BuscarSatelitalPorId(vehiculo.fidSatelital);
            //        this.placa = vehiculo.fvcPlaca;
            //        this.Descripcion = vehiculo.fvcDescripcion;
            //        this.Usuario = vehiculo.fvcUsuario;
            //        this.Clave = vehiculo.fvcClave;
            //    }

            //    base.OnPropertyChanged("VehiculoSatelitales");
            //};

            //ctx.ObtenerVehiculoSatelitalPorPlaca(placa);
        }

        private Satelital BuscarSatelitalPorId(int id)
        {
            MostrarProgreso = false;
            Contexto ctx = new Contexto(MostarVentaError);
            Satelital idSatelital = new Satelital();

            ctx.FinalizarObtenerSatelitalPorId += (s, e) =>
            {
                Satelital satelital = (Satelital)e.Result;

                if (satelital != null)
                {
                    idSatelital = satelital;
                }

                base.OnPropertyChanged("VehiculoSatelitales");
            };

            ctx.ObtenerVehiculoSatelitalPorPlaca(placa);

            return idSatelital;
        }
        #endregion
    }
}