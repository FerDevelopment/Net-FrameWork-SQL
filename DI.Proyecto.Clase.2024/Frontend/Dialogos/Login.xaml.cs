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
using Microsoft.EntityFrameworkCore;

namespace DI.Proyecto.Clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        public Login()
        {

            if (ConectarBD())
            {
                InitializeComponent();
                usuarioServicio = new UsuarioServicio(contexto);
                txtUsername.Focus();
            }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await usuarioServicio.Login(txtUsername.Text.ToLower(), passClaveAcceso.Password))
            {

                MainWindow ventanaPrincipal = new MainWindow(contexto,usuarioServicio.usuLogin);
              
                ventanaPrincipal.Show();
                this.Close();
            }
            else
            {
                this.ShowMessageAsync("INICIO DE SESIÓN", "El usuario y/o contraseña son incorrectos");
            }
        }

        private bool ConectarBD()
        {
            bool correcto = true;
            contexto = new DiinventarioexamenContext();
            try
            {
                contexto.Database.OpenConnection();
            }
            catch (Exception ex)
            {
                correcto = false;
                this.ShowMessageAsync("CONEXIÓN DE LA BASE DE DATOS",
                    "Upps!!! No podemos conectar con la BD. Contacte con el administrador");
            }
            return correcto;
        }
    }
}
