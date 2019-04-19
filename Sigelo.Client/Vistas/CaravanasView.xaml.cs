using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for CaravanasView.xaml.
    /// El manejo del mapa se hace a través de eventos por no tener dependenci properties
    /// </summary>
    public partial class CaravanasView : PageBase
    {
        CaravanasViewModel caravanasViewModel;
        public CaravanasView()
        {
            this.caravanasViewModel = new CaravanasViewModel();
            InitializeComponent();
            this.MapWebBrowser.FinalizaCargaMapa += MostrarAlarmaGrupoUsuario;
            this.OnCerrar += SubirScroll;
            this.gridCaravanas.SourceUpdated += SubirScroll;
        }
        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            CaravanasAutoCompleteBox.Focus();

            var textBox = CaravanasAutoCompleteBox.Template.FindName("Text", CaravanasAutoCompleteBox) as TextBox;
            if (textBox != null)
                textBox.Focus();
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (Uri.IsWellFormedUriString(e.Uri.ToString(), UriKind.RelativeOrAbsolute))
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
        }
        private void PlacaTextBox_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.caravanasViewModel.KeyPressed = e.Key;
            string cambio = string.Empty;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                cambio = this.PlacaTextBox.Text;

                this.PlacaTextBox.Text += " ";
                //this.caravanasViewModel.BuscarPlacaView();
            }
        }
        private void gridCaravanas_RecordUpdated(object sender, Infragistics.Windows.DataPresenter.Events.RecordUpdatedEventArgs e)
        {
            var scrollViewer = e.OriginalSource as ScrollViewer;
            scrollViewer.ScrollToTop();
        }
        private void cbdg1_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
        }
        public void MostrarAlarmaGrupoUsuario(object sender, EventArgs e)
        {
            try
            {
                this.AlarmaCheckBox.IsChecked = this.MapWebBrowser.ActivaMapa;
                this.AlarmaCheckBox.IsEnabled = this.MapWebBrowser.ActivaMapa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SubirScroll(object sender, EventArgs e)
        {
            this.gridCaravanas.ScrollInfo.ScrollOwner.ScrollToTop();
            //this.caravanasViewModel.ObtenerSeguimientos();
        }
    }
}