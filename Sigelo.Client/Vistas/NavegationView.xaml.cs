using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for NavegationView.xaml
    /// </summary>
    public partial class NavegationView : NavigationWindow
    {
        #region Constructor / Destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NavegationView" /> class.
        /// </summary>
        public NavegationView()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Loaded event of the NavigationWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void NavigationWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CO");
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.Name);
            this.Title = string.Format(CultureInfo.CurrentCulture, "Control de Seguimiento {0} – Icoltrans SAS", Assembly.GetExecutingAssembly().GetName().Version);
        }
        #endregion

    }
}
