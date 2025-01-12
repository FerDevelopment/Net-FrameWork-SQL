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
using DI.Proyecto.Clase._2024.MVVM;

namespace DI.Proyecto.Clase._2024.Frontend.ControlUsuario
{
    /// <summary>
    /// Interaction logic for UCCrearUsuario.xaml
    /// </summary>
    public partial class UCCrearUsuario : UserControl
    {
        MVCrearUsuario _mvCrearUsuario;
        public UCCrearUsuario()
        {
            InitializeComponent();
        }

        public UCCrearUsuario(MVCrearUsuario mv)
        {
            InitializeComponent();
            _mvCrearUsuario = mv;
            DataContext = _mvCrearUsuario;
            
        }
    }
}
