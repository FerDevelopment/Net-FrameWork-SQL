using di.proyecto.clase._2024.Backend.Servicios;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.MVVM;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Controls;


namespace DI.Proyecto.Clase._2024.Frontend.Dialogos
{
    /// <summary>
    /// Interaction logic for ArticuloCrear.xaml
    /// </summary>

    public partial class ArticuloCrear : MetroWindow
    {

        private MVCrearArticulo mvCrearArticulo;

        public ArticuloCrear(MVCrearArticulo ca)
        {
            mvCrearArticulo = ca;
            InitializeComponent();
            DataContext = mvCrearArticulo;
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvCrearArticulo.OnErrorEvent));
            mvCrearArticulo.btnGuardar = btnGuardar;

        }
       

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (mvCrearArticulo.IsValid(this))
            {
                if (mvCrearArticulo.guarda)
                {
                    await this.ShowMessageAsync("Gestión crear" +
                        " artículo", "El modelo artículo " + mvCrearArticulo.crearArticulo.Numserie + " se ha guardado correctamente");
                    DialogResult = true;
                }
                else
                {
                    await this.ShowMessageAsync("Gestión crear artículo", "ERROR!! ALGO NO CUADRA PILLIN");

                }
            }
            else
            {
                this.ShowMessageAsync("Gestión crear artículo", "Tienes campos obligatorios sin rellenar correctamente");

            }

        }
       

    }
}
