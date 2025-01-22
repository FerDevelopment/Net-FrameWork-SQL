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
    /// Interaction logic for DialogoModeloArticulo.xaml
    /// </summary>
    public partial class DialogoModeloArticuloMVVM : MetroWindow
    {
        private DiinventarioexamenContext contexto;
        private ModeloArticuloServicio modeloArticuloServicio;
        private TipoArticuloServicio tipoArticuloServicio;
       
        private MVModeloArticulo mvmodeloArticulo;


        public DialogoModeloArticuloMVVM(MVModeloArticulo mv)
        {
            mvmodeloArticulo = mv;
            InitializeComponent();
            DataContext = mvmodeloArticulo;
            //this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvmodeloArticulo.OnErrorEvent));
            mvmodeloArticulo.btnGuardar = btnGuardar;
        }

        protected override void OnClosed(EventArgs e)
        {
            mvmodeloArticulo.modeloArticulo= new Modeloarticulo();
            base.OnClosed(e);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (mvmodeloArticulo.IsValid(this))
            {
                if (mvmodeloArticulo.guarda)
                {
                    await this.ShowMessageAsync("Gestión modelo artículo", "El modelo artículo " + mvmodeloArticulo.modeloArticulo.Nombre + " se ha guardado correctamente");
                    DialogResult = true;
                    mvmodeloArticulo.modeloArticulo = new Modeloarticulo();

                }
                else
                {
                    await this.ShowMessageAsync("Gestión modelo artículo", "ERROR!! ALGO NO CUADRA PILLÍN");

                }
            }
            else
            {
                this.ShowMessageAsync("Gestión modelo artículo", "Tienes campos obligatorios sin rellenar correctamente");
            }
        }



        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            mvmodeloArticulo.modeloArticulo = new Modeloarticulo();

        }

    }
}

