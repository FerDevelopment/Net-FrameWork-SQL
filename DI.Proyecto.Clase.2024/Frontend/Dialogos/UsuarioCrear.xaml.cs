using DI.Proyecto.Clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using System.Windows.Shapes;
using di.proyecto.clase.ribbon._2023.Backend.Servicios;
using DI.Proyecto.Clase._2024.MVVM;
using DI.Proyecto.Clase._2024.Frontend.ControlUsuario;

namespace DI.Proyecto.Clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for UsuarioCrear.xaml
    /// </summary>
    public partial class UsuarioCrear : MetroWindow
    {

        MVCrearUsuario mvCrearUsuario;
        public UsuarioCrear()
        {
            InitializeComponent();
        }

        public UsuarioCrear(MVCrearUsuario mvc)
        {
            mvCrearUsuario = mvc;
            InitializeComponent();
            DataContext = mvCrearUsuario;
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvCrearUsuario.OnErrorEvent));
            mvCrearUsuario.btnGuardar = btnGuardar;
        }




        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            if (mvCrearUsuario.IsValid(this))
            {
                if (mvCrearUsuario.guarda)
                {
                    await this.ShowMessageAsync("Gestión crear" +
                        " usuario", "El usuario " + mvCrearUsuario.usuario.Nombre + " se ha guardado correctamente");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("Gestión crear usuario", "ERROR!! ALGO NO CUADRA PILLIN");

                }
            }
            else
            {
                this.ShowMessageAsync("Gestión crear usuario", "Tienes campos obligatorios sin rellenar correctamente");

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void cbTipoUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String tipoUsu = ((Tipousuario)cbTipoUser.SelectedItem).Nombre;
            if (tipoUsu.Equals("Alumno"))
            {
                cbGrupo.IsEnabled = true;
                cbDepartamento.IsEnabled = false;

            }
            else if (tipoUsu.Equals("Profesor"))
            {
                cbDepartamento.IsEnabled = true;
                cbGrupo.IsEnabled = false;

            }
            else
            {
                cbGrupo.IsEnabled = false;
                cbDepartamento.IsEnabled = false;
            }
        }
    }
}
