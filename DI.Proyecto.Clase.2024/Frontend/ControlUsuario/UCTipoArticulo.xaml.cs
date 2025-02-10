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
        public UCTipoArticulo(MVModeloArticulo mv)
        {
            _mvma = mv;
            DataContext = _mvma;
            InitializeComponent();
        }
    }
}
