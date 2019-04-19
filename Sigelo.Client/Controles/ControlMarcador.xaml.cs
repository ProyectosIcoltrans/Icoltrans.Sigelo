using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Interaction logic for ControlMarcador.xaml
    /// </summary>
    public partial class ControlMarcador : UserControl
    {
        public ControlMarcador()
        {
            InitializeComponent();
            this.Estrella.MouseLeftButtonDown += (s, e) => this.Marcado = !this.Marcado;
        }
        #region Propiedad Dependiente Marcado
        /// <summary>
        /// Obtener o asignar el Marcado.
        /// </summary>
        /// <value>El Marcado.</value>
        public bool Marcado
        {
            get { return (bool)GetValue(MarcadoProperty); }
            set { SetValue(MarcadoProperty, value); }
        }
        /// <summary>
        /// DependencyProperty como el alamcenamiento de Marcado.  
        /// </summary>
        public static readonly DependencyProperty MarcadoProperty =
            DependencyProperty.Register("Marcado", typeof(bool), typeof(ControlMarcador),
            new PropertyMetadata(false, new PropertyChangedCallback(OnMarcadoChanged)));

        private static void OnMarcadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ControlMarcador instancia = d as ControlMarcador;
            bool valor = (bool)e.NewValue;
            if (valor)
            {
                instancia.Estrella.Fill = new SolidColorBrush(Colors.Orange);
                instancia.Estrella.Stroke = null;
            }
            else
            {
                instancia.Estrella.Fill = (Brush)Application.Current.Resources["BackgroundBrush"];
                instancia.Estrella.Stroke = new SolidColorBrush(Colors.DarkGray);
            }
        }
        #endregion
    }
}