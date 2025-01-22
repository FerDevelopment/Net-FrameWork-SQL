using di.proyecto.clase._2024.Backend.Servicios;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.Backend.Utiles;
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
        private Articulo articuloAux;
        private MVArticulo mvCrearArticulo;
        private bool _editar;

        public ArticuloCrear(MVArticulo ca, bool editar)
        {
            InitializeComponent();
            mvCrearArticulo = ca;
            DataContext = mvCrearArticulo;
            _editar = editar;
            articuloAux = new Articulo();
            //this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvCrearArticulo.OnErrorEvent));
            PropertyCopier<Articulo>.CopyProperties(mvCrearArticulo.crearArticulo, articuloAux);
            mvCrearArticulo.btnGuardar = btnGuardar;
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            PropertyCopier<Articulo>.CopyProperties(articuloAux, mvCrearArticulo.crearArticulo);

            DialogResult = false;

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (mvCrearArticulo.IsValid(this))
            {
                if (_editar == false)
                {
                    getId();
                }
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

        private void getId()
        {
            mvCrearArticulo.crearArticulo.Idarticulo = (mvCrearArticulo.artserv.GetLastId() + 1);

        }
    }
}
