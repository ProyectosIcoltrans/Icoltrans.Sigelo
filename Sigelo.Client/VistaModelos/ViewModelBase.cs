using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.Windows;
using Icoltrans.Sigelo.Comunes;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Clase base para implementar el pratron de vista modelo
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Variables
        private bool mostrarProgreso;
        private bool mostrarError;
        private static bool? _isInDesignMode;
        private string detalleError;
        private string mensajeError = "El servidor no pudo culminar exitosamente el proceso solicitado. Por favor intentelo de nuevo.";
        #endregion

        #region Definicion de Eventos
        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor / Destructor
        #endregion

        #region Propiedades
        public string DetalleError
        {
            get { return this.detalleError; }
            set
            {
                if (this.detalleError != value)
                {
                    this.detalleError = value;
                    OnPropertyChanged("DetalleError");
                }
            }
        }
        public string MensajeError
        {
            get { return this.mensajeError; }
            set
            {
                if (this.mensajeError != value)
                {
                    this.mensajeError = value;
                    OnPropertyChanged("MensajeError");
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether the control is in design mode (running in Blend
        /// or Visual Studio).
        /// </summary>
        public static bool IsInDesignModeStatic
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
#if SILVERLIGHT
            _isInDesignMode = DesignerProperties.IsInDesignTool;
#else
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    _isInDesignMode
                        = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;
#endif
                }

                return _isInDesignMode.Value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [mostrar progreso].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mostrar progreso]; otherwise, <c>false</c>.
        /// </value>
        public bool MostrarProgreso
        {
            get { return this.mostrarProgreso; }
            set
            {
                if (this.mostrarProgreso != value)
                {
                    this.mostrarProgreso = value;
                    OnPropertyChanged("MostrarProgreso");
                }
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [mostrar error].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [mostrar error]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool MostrarError
        {
            get { return this.mostrarError; }
            set
            {
                if (this.mostrarError != value)
                {
                    this.mostrarError = value;
                    OnPropertyChanged("MostrarError");
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether the control is in design mode (running under Blend
        /// or Visual Studio).
        /// </summary>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "Non static member needed for data binding")]
        public bool IsInDesignMode
        {
            get
            {
                return IsInDesignModeStatic;
            }
        }
        #endregion

        #region Metodos
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void InvokeEventHandler<T>(EventHandler<T> eventHandler, T eventArgs) where T : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(this, eventArgs);
            }
        }
        protected void MostarVentaError(Exception error)
        {
            var errorServicio = error as FaultException<ServerErrorWrapper>;
            if (errorServicio != null)
            {
                mensajeError = errorServicio.Detail.Message;
                DetalleError = errorServicio.Detail.ToString();
            }
            else if (error != null)
            {
                DetalleError = error.ToFormatString();
                do
                {
                    mensajeError = error.Message;
                    error = error.InnerException;
                } while (error != null);
            }
            else
            {
                mensajeError = "El servidor no pudo culminar exitosamente el proceso solicitado. Por favor intentelo de nuevo.";
                detalleError = string.Empty;
            }
            OnPropertyChanged("MensajeError");
            this.MostrarProgreso = false;
            this.MostrarError = true;
        }
        protected void MostarVentaError(Exception error, string mensaje)
        {
            var errorServicio = error as FaultException<ServerErrorWrapper>;
            if (errorServicio != null)
            {
                mensajeError = mensaje;
                DetalleError = errorServicio.Detail.ToString();
            }
            else if (error != null)
            {
                DetalleError = error.ToFormatString();
                do
                {
                    mensajeError = error.Message;
                    error = error.InnerException;
                } while (error != null);
            }
            else
            {
                mensajeError = "El servidor no pudo culminar exitosamente el proceso solicitado. Por favor intentelo de nuevo.";
                detalleError = string.Empty;
            }
            OnPropertyChanged("MensajeError");
            this.MostrarProgreso = false;
            this.MostrarError = true;
        }
        #endregion

        #region Funciones
        #endregion

        #region Eventos
        #endregion
    }
}
