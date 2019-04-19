using System.Globalization;
using System.Text;
using Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// View model del cambio de nombre de caravanas
    /// </summary>
    public sealed class CambioNombreViewModel : BaseOpcionesViewModel
    {
        #region Variables
        private string nuevoNombreCaravana;
        private string nombreCaravana;
        private Vehiculo vehiculoSeleccionado;
        private Conductor conductorSeleccionado;
        private bool opcionTrailer;
        private bool opcionCabezote;
        private string placa;
        private string conductor;
        private string celular;
        #endregion

        #region Constructor / Destructor
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "interno")]
        public CambioNombreViewModel(bool interno)
        {
            opcionCabezote = true;
            this.vehiculoSeleccionado = new Vehiculo();
            this.conductorSeleccionado = new Conductor();
        }
        #endregion

        #region Propiedades
        public string NuevoNombreCaravana
        {
            get { return this.nuevoNombreCaravana; }
            set
            {
                if (this.nuevoNombreCaravana != value)
                {
                    this.nuevoNombreCaravana = value;
                    OnPropertyChanged("NuevoNombreCaravana");
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
        public Vehiculo VehiculoSeleccionado
        {
            get { return this.vehiculoSeleccionado; }
            set
            {
                if (this.vehiculoSeleccionado != value)
                {
                    this.vehiculoSeleccionado = value;
                    OnPropertyChanged("VehiculoSeleccionado");
                    this.OpcionCabezote = true;
                    AsignarPlaca();
                }
            }
        }
        public Conductor ConductorSeleccionado
        {
            get { return this.conductorSeleccionado; }
            set
            {
                if (this.conductorSeleccionado != value)
                {
                    this.conductorSeleccionado = value;
                    OnPropertyChanged("ConductorSeleccionado");
                    if (value == null)
                    {
                        Conductor = string.Empty;
                        Celular = string.Empty;
                    }
                    else
                    {
                        Conductor = value.PrimerApellido;
                        Celular = value.Telefonos;
                    }
                }
            }
        }
        public bool OpcionTrailer
        {
            get { return this.opcionTrailer; }
            set
            {
                if (this.opcionTrailer != value)
                {
                    this.opcionTrailer = value;
                    OnPropertyChanged("OpcionTrailer");
                    AsignarPlaca();
                }
            }
        }
        public bool OpcionCabezote
        {
            get { return this.opcionCabezote; }
            set
            {
                if (this.opcionCabezote != value)
                {
                    this.opcionCabezote = value;
                    OnPropertyChanged("OpcionCabezote");
                    AsignarPlaca();
                }
            }
        }
        public string Placa
        {
            get { return this.placa; }
            set
            {
                if (this.placa != value)
                {
                    this.placa = value;
                    OnPropertyChanged("Placa");
                    GenerarDescripcion();
                }
            }
        }
        public string Conductor
        {
            get { return this.conductor; }
            set
            {
                if (this.conductor != value)
                {
                    this.conductor = value;
                    OnPropertyChanged("Conductor");
                    GenerarDescripcion();
                }
            }
        }
        public string Celular
        {
            get { return this.celular; }
            set
            {
                if (this.celular != value)
                {
                    this.celular = value;
                    OnPropertyChanged("Celular");
                    GenerarDescripcion();
                }
            }
        }
        #endregion

        #region Funciones
        private void AsignarPlaca()
        {
            if (vehiculoSeleccionado == null || string.IsNullOrEmpty(vehiculoSeleccionado.Placa))
                Placa = string.Empty;
            else
            {
                Placa = vehiculoSeleccionado.Placa.Substring(0, 6);
                if (opcionTrailer)
                {
                    if (vehiculoSeleccionado.Placa.Length >= 13)
                        Placa = vehiculoSeleccionado.Placa.Substring(0, 13);
                    else if (!string.IsNullOrEmpty(vehiculoSeleccionado.Complemento))
                        Placa = string.Format(CultureInfo.CurrentCulture, "{0}-{1}", 
                            vehiculoSeleccionado.Placa.Substring(0, 6),
                            vehiculoSeleccionado.Complemento);
                }
            }
        }
        private void GenerarDescripcion()
        {

            if (string.IsNullOrEmpty(placa))
                AsignarPlaca();
            if (conductorSeleccionado != null)
            {
                if (string.IsNullOrEmpty(conductor))
                    Conductor = conductorSeleccionado.PrimerApellido;
                if (string.IsNullOrEmpty(celular))
                    Celular = conductorSeleccionado.Telefonos;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(
                "{0} {1} {2}",
                string.IsNullOrEmpty(placa) ? "/" : placa,
                string.IsNullOrEmpty(conductor) ? "/" : conductor,
                string.IsNullOrEmpty(celular) ? "/" : celular
                );
            NuevoNombreCaravana = sb.ToString();
        }
        #endregion

    }
}
