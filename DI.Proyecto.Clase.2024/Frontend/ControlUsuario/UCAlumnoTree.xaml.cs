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
    /// Interaction logic for UCUsuarioTree.xaml
    /// </summary>
    public partial class UCAlumnoTree : UserControl
    {
        private MVUsuario _mvuser;
        public UCAlumnoTree( MVUsuario mv)
        {
            _mvuser = mv;
            DataContext = mv;
            InitializeComponent();
        }
    }
}
