using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Icoltrans.Sigelo.Win.Vehiculos.Resources;
using System.DirectoryServices;
using System.Text;
using System.Windows.Navigation;
using System.Threading.Tasks;
using System.Threading;
using Icoltrans.Sigelo.Win.Vehiculos.Code;
using System.ComponentModel;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for ControlMapa.xaml.  Como el mapa es un control especial, se aisla todo lo referente al uso del mismo
    /// </summary>
    public partial class ControlMapa : UserControl
    {
        #region Variables
        bool activaMapa = false;
        //private AsyncWebBrowser map = new AsyncWebBrowser();
        BackgroundWorker work = new BackgroundWorker();
        #endregion

        #region Definicion de Eventos
        internal event EventHandler<EventArgs> FinalizaCargaMapa;
        #endregion

        #region Constructor / Destructor
        public ControlMapa()
        {
            InitializeComponent();

            //map.SetBrowser(this.MapWebBrowser, new LoadCompletedEventHandler(MostrarAlarmaGrupoUsuario));
            this.MapWebBrowser.LoadCompleted += MostrarAlarmaGrupoUsuario;
        }
        #endregion

        #region Propiedades
        public bool ActivaMapa
        {
            get { return activaMapa; }
            set { activaMapa = value; }
        }
        #region Propiedad Dependiente MostarAlarmas
        /// <summary>
        /// Obtener o asignar el MostarAlarmas.
        /// </summary>
        /// <value>El MostarAlarmas.</value>
        public bool MostarAlarmas
        {
            get { return (bool)GetValue(MostarAlarmasProperty); }
            set { SetValue(MostarAlarmasProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de MostarAlarmas.  
        /// </summary>
        public static readonly DependencyProperty MostarAlarmasProperty =
            DependencyProperty.Register("MostarAlarmas", typeof(bool), typeof(ControlMapa),
            new PropertyMetadata(false, new PropertyChangedCallback(OnMostarAlarmasChanged)));
        #endregion
        #region Propiedad Dependiente MostrarPuntosControl
        /// <summary>
        /// Obtener o asignar el MostrarPuntosControl.
        /// </summary>
        /// <value>El MostrarPuntosControl.</value>
        public bool MostrarPuntosControl
        {
            get { return (bool)GetValue(MostrarPuntosControlProperty); }
            set { SetValue(MostrarPuntosControlProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de MostrarPuntosControl.  
        /// </summary>
        public static readonly DependencyProperty MostrarPuntosControlProperty =
            DependencyProperty.Register("MostrarPuntosControl", typeof(bool), typeof(ControlMapa),
            new PropertyMetadata(false, new PropertyChangedCallback(OnMostrarPuntosControlChanged)));
        #endregion
        #region Propiedad Dependiente Placa
        /// <summary>
        /// Obtener o asignar el Placa.
        /// </summary>
        /// <value>El Placa.</value>
        string placa = string.Empty;
        public string Placa
        {
            get { return (string)GetValue(PlacaProperty); }
            set { SetValue(PlacaProperty, value);
                    placa = value;
            }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de Placa.  
        /// </summary>
        public static readonly DependencyProperty PlacaProperty =
            DependencyProperty.Register("Placa", typeof(string), typeof(ControlMapa),
            new PropertyMetadata(null, new PropertyChangedCallback(OnPlacaChanged)));

        public bool Alarma { get; set; }


        #endregion
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void Refrescar()
        {
            //work.RunWorkerAsync();

            //Thread oThread = new Thread(new ThreadStart(RefreshMapa));
            //oThread.Start();

            ///RefreshMapa();

            if (ViewModelBase.IsInDesignModeStatic) return;
            //SmartDispatcher.BeginInvoke(() =>
            //{
                try
                {
                    if (string.IsNullOrEmpty(Placa) || Contexto.ParametrosIniciales == null)
                    {
                        MostraColombia();
                        return;
                    }
                    try
                    {
                        MostraPlaca();
                    }
                    catch (Exception)
                    {
                        MostraColombia();
                    }
                }
                catch { }
            //});
        }
        private void RefreshMapa()
        {
            if (ViewModelBase.IsInDesignModeStatic) return;
            try
            {
                //SmartDispatcher.BeginInvoke(() =>
                //{
                    try
                    {
                        //MostraColombia();
                        if (string.IsNullOrEmpty(Placa) || Contexto.ParametrosIniciales == null)
                        {
                            MostraColombia();
                            return;
                        }
                        try
                        {
                            MostraPlaca();
                        }
                        catch (Exception)
                        {
                            MostraColombia();
                        }
                    }
                    catch { }
                //});
            }
            catch { }
        }

        private void MostraColombia()
        {
            string urlMapa = string.Format(GoogleMapsResource.IFrame2, FormarURLAlarmaGrupoUsuario().ToString().ToLower());
            //map.NavigateAsync(urlMapa);
            MapWebBrowser.Navigate(urlMapa);
        }
        private void MostraPlaca()
        {
            if (Contexto.CaravanaSeleccionada != null)
            {
                if (this.Placa.Length >= 6)
                {
                    if (Contexto.CaravanaSeleccionada.Model.Caravana.Satelital.Equals("SPECTRUM"))
                    {
                        var param = Contexto.ParametrosIniciales.Parametros;
                        string queryString = string.Format(CultureInfo.InvariantCulture, param.ParametrosSatelital, this.Placa.Trim(), MostarAlarmas);

                        //map.InvokeScript(this.Placa);
                        this.MapWebBrowser.InvokeScript("showInMap", new object[] { this.Placa, "flota" });

                    }

                    //&& (Contexto.CaravanaKeyPressed == System.Windows.Input.Key.Enter)
                }
            }
            else
            {
                if ((Contexto.CaravanaKeyPressed == System.Windows.Input.Key.Enter))
                {
                    var param = Contexto.ParametrosIniciales.Parametros;
                    string queryString = string.Format(CultureInfo.InvariantCulture, param.ParametrosSatelital, this.Placa, MostarAlarmas);

                    this.MapWebBrowser.InvokeScript("showInMap", new object[] { this.Placa.Trim(), "flota" });

                    Contexto.CaravanaKeyPressed = System.Windows.Input.Key.Home;
                }
            }
        }
        public void MostrarAlarmaGrupoUsuario(object sender, NavigationEventArgs e)
        {
            try
            {
                if (this.FinalizaCargaMapa != null)
                {
                    this.FinalizaCargaMapa(sender, e);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void OnMostarAlarmasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlMapa instancia = d as ControlMapa;
            instancia.MapWebBrowser.InvokeScript("setAlerts", new object[] { instancia.MostarAlarmas });
        }
        private static void OnMostrarPuntosControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlMapa instancia = d as ControlMapa;
            instancia.Refrescar();
        }
        private static void OnPlacaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlMapa instancia = d as ControlMapa;
            instancia.Refrescar();
        }

        public bool FormarURLAlarmaGrupoUsuario()
        {
            // Creamos un objeto DirectoryEntry para conectarnos al directorio activo
            DirectoryEntry adsRoot = new DirectoryEntry("LDAP://" + Environment.GetEnvironmentVariable("USERDOMAIN"));
            // Creamos un objeto DirectorySearcher para hacer una búsqueda en el directorio activo
            DirectorySearcher adsSearch = new DirectorySearcher(adsRoot);

            bool activaAlarma = false;

            try
            {
                // Ponemos como filtro que busque el usuario actual
                adsSearch.Filter = "samAccountName=" + Environment.GetEnvironmentVariable("USERNAME");

                // Extraemos la primera coincidencia
                SearchResult oResult;
                oResult = adsSearch.FindOne();

                // Obtenemos el objeto de ese usuario
                DirectoryEntry usuario = oResult.GetDirectoryEntry();

                // Obtenemos la lista de SID de los grupos a los que pertenece
                usuario.RefreshCache(new string[] { "tokenGroups" });

                // Creamos una variable StringBuilder donde ir añadiendo los SID para crear un filtro de búsqueda
                StringBuilder sids = new StringBuilder();
                sids.Append("(|");
                foreach (byte[] sid in usuario.Properties["tokenGroups"])
                {
                    sids.Append("(objectSid=");
                    for (int indice = 0; indice < sid.Length; indice++)
                    {
                        sids.AppendFormat("\\{0}", sid[indice].ToString("X2"));
                    }
                    sids.AppendFormat(")");
                }
                sids.Append(")");

                // Creamos un objeto DirectorySearcher con el filtro antes generado y buscamos todas la coincidencias
                DirectorySearcher ds = new DirectorySearcher(adsRoot, sids.ToString());
                SearchResultCollection src = ds.FindAll();

                // Recorremos toda la lista de grupos devueltos
                foreach (SearchResult sr in src)
                {
                    String sGrupo = (String)sr.Properties["samAccountName"][0];

                    // A partir de aquí hacer lo que corresponda con cada grupo
                    if (sGrupo.Equals("SIGELO Seguridad Alarmas"))
                    {
                        activaAlarma = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.ActivaMapa = activaAlarma;

            return activaAlarma;
        }

        #endregion

        #region Eventos
        private void MapWebBrowser_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                this.Refrescar();
            }
        }
        #endregion

    }
}
