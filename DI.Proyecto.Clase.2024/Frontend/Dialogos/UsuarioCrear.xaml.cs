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

namespace DI.Proyecto.Clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for UsuarioCrear.xaml
    /// </summary>
    public partial class UsuarioCrear : MetroWindow
    {
        DiinventarioexamenContext contexto;
        UsuarioServicio usuarioServicio;
        ServicioGenerico<Tipousuario> tipoUsuarioServicio;
        RolServicio rolServicio;
        DptoServicio dptServicio;
        GrupoServicio grupoServicio;
        Usuario usu;

        public UsuarioCrear()
        {
            InitializeComponent();
        }

        public UsuarioCrear(DiinventarioexamenContext context)
        {
            InitializeComponent();
            contexto = context;
            Inicializa();
        }

        private async void Inicializa()
        {
            usuarioServicio = new UsuarioServicio(contexto);
            rolServicio = new RolServicio(contexto);
            tipoUsuarioServicio = new ServicioGenerico<Tipousuario>(contexto);
            dptServicio = new DptoServicio(contexto);
            grupoServicio = new GrupoServicio(contexto);
            usu = new Usuario();

            cbTipoUser.ItemsSource = await tipoUsuarioServicio.GetAllAsync();
            cbDepartamento.ItemsSource = await dptServicio.GetAllAsync();
            cbGrupo.ItemsSource = await grupoServicio.GetAllAsync();
            cbRolUser.ItemsSource = await rolServicio.GetAllAsync();

        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            recogerDatos();

            if (usuarioServicio.UsuarioUnico(usu.Username))
            {


                if (await usuarioServicio.AddAsync(usu))
                {
                    await this.ShowMessageAsync("Gestión de usuarios", "Usuario guardado correctamente");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("Gestión de usuarios", "ERROR!! PROBLEMAS AL INSERTAR EL USUARIO");
                }
            }
            else
            {
                await this.ShowMessageAsync("Gestión de usuarios", "Nombre de usuario ya existente");

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void recogerDatos()
        {

            usu.Nombre = txtNombre.Text;
            usu.Apellido1 = txtApellido1.Text;
            usu.Apellido2 = txtApellido2.Text;
            usu.Email = txtEmail.Text;
            usu.Domicilio = txtDomicilio.Text;
            usu.Poblacion = txtPoblacion.Text;
            usu.Telefono = txtTelefono.Text;
            usu.Codpostal = txtCp.Text;
            usu.Username = txtNombreUser.Text;
            usu.Password = txtContraseña.Text;

            usu.RolNavigation = (Rol)cbRolUser.SelectedItem;
            usu.TipoNavigation = (Tipousuario)cbTipoUser.SelectedItem;
            usu.DepartamentoNavigation = (Departamento)cbDepartamento.SelectedItem;
            usu.GrupoNavigation = (Grupo)cbGrupo.SelectedItem;

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
