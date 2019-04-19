
namespace Icoltrans.Sigelo.Win.Vehiculos.Servicio.Vehiculos
{
    public partial class DetalleCaravana
    {

        public Seguimiento Caravana { get; set; }
        internal bool HasPuntosEntrega { get { return this.PuntosEntrega != null && this.PuntosEntrega.Length > 0; } }
        internal bool HasConductores { get { return this.Conductores != null && this.Conductores.Length > 0; } }
        internal bool HasEscoltas { get { return this.Escoltas != null && this.Escoltas.Length > 0; } }
        internal bool HasReportes { get { return this.Reportes != null && this.Reportes.Length > 0; } }
        internal bool HasVehiculos { get { return this.Vehiculos != null && this.Vehiculos.Length > 0; } }
        internal bool HasAll
        {
            get
            {
                // No se incluye HasEscoltas porque no es obligatorio que una caravana tenga Escoltas
                return HasPuntosEntrega && HasConductores && HasReportes && HasVehiculos;
            }
        }
    }

    public partial class Seguimiento
    {
        public TipoCaravana TipoCaravana
        {
            get { return (TipoCaravana)this.fbtUrbana; }
        }
        public string OrigenDestino
        {
            get { return string.Format("{0} - {1}", origen, destino); }
        }
    }
    public partial class Conductor
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} {1} {2} - {3}", Nombre, PrimerApellido, SegundoApellido, Numero);
            }
        }
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1} {2}", Nombre, PrimerApellido, SegundoApellido);
            }
        }
        public string LicenciaCompleta
        {
            get
            {
                return string.Format("{0} ({1})", Licencia, Categoria);
            }
        }
        public string DireccionCompleta
        {
            get { return string.Format("{0} ({1})", Direccion, Ciudad); }
        }
    }
    public partial class Vehiculo
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} {1}", Placa, De);
            }
        }
        public string PlacaCompleta
        {
            get { return string.Format("{0} ({1})", Placa, De); }
        }
    }
    public partial class EstadoVehiculo
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} - {1}", fvcDescripcion, fsmIdEstadoVehiculo);
            }
        }
    }
    public partial class RazonEstado
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} - {1}", fvcDescripcion, fsmIdRazonEstadoV);
            }
        }
    }
    public partial class Escolta
    {
        public string NombreCompleto
        {
            get { return string.Format("{0} {1} {2}", Nombre, PrimerApellido, SegundoApellido); }
        }
        public string DireccionCompleta
        {
            get { return string.Format("{0} ({1})", Direccion, Ciudad); }
        }
    }
    public partial class Escolta1
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} - {1}", Nombre, Cedula);
            }
        }
    }
    public partial class Refuerzo
    {
        public string Descripcion
        {
            get
            {
                return string.Format("{0} - {1}", Nombre, Identificacion);
            }
        }
    }
    public partial class PuntoEntrega2
    {
        private bool seleccionado;
        internal ControlCarreteraViewModel Contenedor { get; set; }
        public bool Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                if (this.seleccionado != value)
                {
                    this.seleccionado = value;
                    Contenedor.AjustarObservacion();
                    RaisePropertyChanged("Seleccionado");
                }
            }
        }
    }
    public partial class CaravanaDisponible
    {
        public TipoCaravana TipoCaravana
        {
            get { return (TipoCaravana)this.fbtUrbana; }
        }
        public string Descripcion
        {
            get
            {
                return string.Format("{0} {1}", fvcDescripcion, Ruta);
            }
        }
    }
    public partial class VehiculoPorSalir
    {
        private bool marcado;
        internal CrearCaravanaViewModel Contenedor { get; set; }
        public bool Marcado
        {
            get { return this.marcado; }
            set
            {
                if (this.marcado != value)
                {
                    this.marcado = value;
                    Contenedor.AjustarSeleccion();
                    RaisePropertyChanged("Marcado");
                }
            }
        }
    }
}