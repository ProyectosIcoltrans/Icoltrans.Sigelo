using Icoltrans.Sigelo.Win.Vehiculos.Controles;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace Icoltrans.Sigelo.Win.Vehiculos
{
    /// <summary>
    /// Clase abtracta para unificar el comportamiento de todas las páginas;
    /// </summary>
    public abstract class PageBase : Page
    {
        #region Variables
        private Grid root;
        #endregion

        #region Definicion de Eventos
        #endregion

        #region Constructor / Destructor
        protected PageBase()
            : base()
        {
            this.Loaded += new System.Windows.RoutedEventHandler(PageBase_Loaded);
        }
        #endregion

        #region Propiedades
        #endregion

        #region Metodos
        #endregion

        #region Funciones
        private void AdicionarDialogo(UserControl control, string nombreBinding)
        {
            Binding visibleBinding = new Binding(nombreBinding) { Converter = new BooleanToVisibilityConverter(), Mode= BindingMode.TwoWay };
            control.SetBinding(ControlProgreso.VisibilityProperty, visibleBinding);
            if (root.ColumnDefinitions.Count > 0) Grid.SetColumnSpan(control, root.ColumnDefinitions.Count);
            if (root.RowDefinitions.Count > 0) Grid.SetRowSpan(control, root.RowDefinitions.Count);
            root.Children.Add(control);
        }
        #endregion

        #region Eventos
        public event EventHandler<EventArgs> OnCerrar;

        void PageBase_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            root = this.Content as Grid;

            if (OnCerrar != null)
                this.OnCerrar(null, null);

            if (root == null) return;

            AdicionarDialogo(new ControlProgreso(), "MostrarProgreso");
            AdicionarDialogo(new ControlError(), "MostrarError");
            //AdicionarDialogo(new ControlMensaje(), "MostrarMensaje");
        }
        #endregion
    }
}
