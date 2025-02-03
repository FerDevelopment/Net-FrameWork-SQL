using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.Backend.Utiles;
using DI.Proyecto.Clase._2024.Frontend.Dialogos;
using DI.Proyecto.Clase._2024.MVVM;
using System.Windows;
using System.Windows.Controls;


namespace DI.Proyecto.Clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Interaction logic for UCArticulo.xaml
    /// </summary>
    public partial class UCArticulo : UserControl
    {
        private MVArticulo _crearArticulo;
        public UCArticulo()
        {
            InitializeComponent();
        }
        public UCArticulo(MVArticulo mvc)
        {
            InitializeComponent();
            _crearArticulo = mvc;
            DataContext = mvc;
            
        }

        private async void mItemBorrar_Click(object sender, RoutedEventArgs e)
        {
            _crearArticulo.crearArticulo = (Backend.Modelo.Articulo)dgCrearArticulo.SelectedItem;

            if (_crearArticulo.borrar)
            {

                MessageBox.Show("Articulo eliminado correctamente", "Gestión articulos");
            }
            else
            {
                MessageBox.Show("Error al intentar eliminar articulo", "Gestión articulos");
            }

            dgCrearArticulo.Items.Refresh();
            _crearArticulo.crearArticulo = new Articulo();
        }

        private void mItemEditar_Click(object sender, RoutedEventArgs e)
        {
            _crearArticulo.crearArticulo = (Backend.Modelo.Articulo)dgCrearArticulo.SelectedItem;

            Articulo articuloAux = _crearArticulo.Clonar;
            ArticuloCrear ac = new ArticuloCrear(_crearArticulo,true);
            ac.ShowDialog();

            if (ac.DialogResult.Equals(true))
            {
                dgCrearArticulo.Items.Refresh();
                _crearArticulo.crearArticulo = new Articulo();
            }
            else
            {
                _crearArticulo.crearArticulo = articuloAux;
                dgCrearArticulo.SelectedItem = articuloAux;

            }
        }

        private void TextBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            _crearArticulo.Filtrar();
        }



      

    }
}
