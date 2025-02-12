using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.MVVM;
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

namespace DI.Proyecto.Clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Interaction logic for UCTipoArticulo.xaml
    /// </summary>
    public partial class UCTipoArticulo : UserControl
    {
        private MVModeloArticulo _mvma;
        public MVArticulo _mvArticulo;
        public Articulo art;
        public UCTipoArticulo(MVModeloArticulo mv, MVArticulo mVArticulo)
        {
            InitializeComponent();
            _mvma = mv;
            _mvArticulo = mVArticulo;
            DataContext = _mvArticulo;
           
            
        }

     


        private void treeTipoArticulo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeTipoArticulo.SelectedItem != null && treeTipoArticulo.SelectedItem is Articulo)
            {
                 art = (Articulo)treeTipoArticulo.SelectedItem;

                _= _mvArticulo.crearArticulo;
                _mvArticulo.crearArticulo = art;
                _ = _mvArticulo.crearArticulo;
            }
        }
    }
}
