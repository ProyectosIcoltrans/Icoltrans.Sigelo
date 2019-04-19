using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    public partial class ControlError : UserControl
    {
        public ControlError()
        {
            InitializeComponent();
        }
        private void ContinuarTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
