using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Icoltrans.Sigelo.Win.Vehiculos.Controles
{
    /// <summary>
    /// Interaction logic for ControlMensaje.xaml
    /// </summary>
    public partial class ControlMensaje : UserControl
    {
        public ControlMensaje()
        {
            InitializeComponent();
        }

        private void ContinuarTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
