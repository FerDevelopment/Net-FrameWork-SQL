using DI.Proyecto.Clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using di.proyecto.clase._2024.MVVM.Base;
using System.Windows.Data;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVModeloArticulo : MVBaseCRUD<Modeloarticulo>
    {
        //Variables privadas
        /// <summary>
        /// Contexto de Entity Framwork
        /// </summary>
        private DiinventarioexamenContext contexto;
        //objeto que acccede a la tabla de modelo articulo
        private Modeloarticulo modelo;
        //Objeto que accede a la tabla de modelo articulo
        private TipoArticuloServicio tipoArticuloServicio;
        //Objeto auxiliar  donde se alamcenan los valores del diálogo 
        private ModeloArticuloServicio modeloArticuloServicio;
        //Objeto que accede a la tabla de modeloArticullo
        private ListCollectionView _listaModelos;


        private Tipoarticulo _tipoarticulo;
        //Lista de criterios 
        public IEnumerable<Tipoarticulo> listaTipos { get { return Task.Run(tipoArticuloServicio.GetAllAsync).Result; } }
        //Lista auxiliar para obtener los modelos de articulo
        public IEnumerable<Modeloarticulo> listaModelos { get { return Task.Run(modeloArticuloServicio.GetAllAsync).Result; } }

        public ListCollectionView listaModelosFiltro => _listaModelos;
        //objeto que guarda el tipo de articulo seleccioonado
        private List<Predicate<Modeloarticulo>> criterios;
        //Lista de criterios para el filtrado de tabla
        private Predicate<Modeloarticulo> criterioBusqueda;

        private Predicate<Modeloarticulo> criterioTipo;
        //Criterios de tipo de artic ulo. FIltrra por el tipo de articulo
        private Predicate<object> predicadoFiltro;
        //Objeto qeu se asoica a la propiedad filtrar de la lista y al metodo flitrado 
        private string _textoBusqueda;
        //Guarada el texto Introducido


        /// <summary>
        /// Getter y setter del objeto modeloarticulo que está asociado al dialogo de la interfaz
        /// </summary>
        public Modeloarticulo modeloArticulo
        {
            get { return modelo; }
            set { modelo = value; OnPropertyChanged(nameof(modeloArticulo)); }
        }

        public string textoBusqueda
        {
            get { return _textoBusqueda; }
            set
            {
                _textoBusqueda = value; OnPropertyChanged(nameof(textoBusqueda));
                if (_textoBusqueda == null)
                {
                    _textoBusqueda = "";
                }
            }
        }

        public Tipoarticulo tipoSeleccionado
        {
            get => _tipoarticulo;
            set { _tipoarticulo = value; OnPropertyChanged(nameof(tipoSeleccionado)); }
        }

        public bool guarda { get { return Task.Run(() => Update(modeloArticulo)).Result; } }

        public MVModeloArticulo(DiinventarioexamenContext context)

        {
            contexto = context;
        }

        public async Task Inicializa()
        {

            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            tipoArticuloServicio = new TipoArticuloServicio(contexto);

            modelo = new Modeloarticulo();
            servicio = modeloArticuloServicio;
           
            _listaModelos = new ListCollectionView(listaModelos.ToList());

            criterios = new List<Predicate<Modeloarticulo>>();
            predicadoFiltro = new Predicate<object>(FiltroCriterios);
            InicializaCriterios();

        }

        private void AddCriterios()
        {
            if (tipoSeleccionado != null)
            {
                criterios.Add(criterioTipo);

            }
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                criterios.Add(criterioBusqueda);
            }
        }
        private void InicializaCriterios()
        {
            criterioTipo = new Predicate<Modeloarticulo>(m => m.TipoNavigation != null && m.TipoNavigation.Equals(tipoSeleccionado));
            criterioBusqueda = new Predicate<Modeloarticulo>(m => m.Nombre != null && m.Nombre.ToLower().StartsWith(textoBusqueda.ToLower()));
        }

        public void Filtrar()
        {
            AddCriterios();
            listaModelosFiltro.Filter = predicadoFiltro;
        }

        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Modeloarticulo modelo = (Modeloarticulo)item;
            if (criterios != null)
            {
                correcto = criterios.TrueForAll(x => x(modelo));
            }
            return correcto;
        }
    }
}
