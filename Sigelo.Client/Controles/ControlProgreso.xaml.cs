using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for ControlProgreso.xaml
    /// </summary>
    public partial class ControlProgreso : UserControl
    {
        public ControlProgreso()
        {
            InitializeComponent();
        }
        private void CancelarTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
