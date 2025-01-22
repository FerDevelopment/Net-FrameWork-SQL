using Castle.Components.DictionaryAdapter.Xml;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.Frontend.Dialogos;
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
    /// Interaction logic for UCModeloArtiuclo.xaml
    /// </summary>
    public partial class UCModeloArticulo : UserControl
    {
        private MVModeloArticulo _mvModeloArticulo;
        
        public UCModeloArticulo()
        {
            InitializeComponent();
        }

        public UCModeloArticulo(MVModeloArticulo mv)
        {
            InitializeComponent();
            _mvModeloArticulo = mv;
            DataContext = _mvModeloArticulo;
        }

        private void mItemEditar_Click(object sender, RoutedEventArgs e)
        {
            _mvModeloArticulo.modeloArticulo = (Backend.Modelo.Modeloarticulo)dgModeloArticulo.SelectedItem;
            DialogoModeloArticuloMVVM dialogoModeloArticuloMVVM = new DialogoModeloArticuloMVVM(_mvModeloArticulo);
            dialogoModeloArticuloMVVM.ShowDialog();

            if (dialogoModeloArticuloMVVM.DialogResult.Equals(true))
            {
                dgModeloArticulo.Items.Refresh();
                _mvModeloArticulo.modeloArticulo = new Modeloarticulo();
                
            }
        }

        private void cbTipoArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
