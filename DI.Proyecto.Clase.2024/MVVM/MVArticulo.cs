using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using Mysqlx.Cursor;
using System;
using System.Windows.Data;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVArticulo : MVBaseCRUD<Articulo>
    {
        private DiinventarioexamenContext contexto;
        private Articulo articulo;
        private ModeloArticuloServicio modeloArticuloServicio;
        private ArticuloServicio articuloServicio;
        private DptoServicio dptoServicio;
        private UsuarioServicio usuarioServicio;
        private EspacioServicio espacioServicio;
        private List<String> estados = new List<string> { "operativo", "obsoleto", "mantenimiento" };
        private String _nombreBuscado;
        private ListCollectionView _listaArticulos;

        private DateOnly _fechaInicial;
        private DateOnly _fechaFinal;
        private Espacio _espacioSeleccionado;


        private List<Predicate<Articulo>> criterios;
        //Lista de criterios para el filtrado de tabla
        private Predicate<Articulo> criterioFechas;
        private Predicate<Articulo> criterioEspacio;


        //Criterios de tipo de artic ulo. FIltrra por el tipo de articulo
        private Predicate<object> predicadoFiltro;

        public ListCollectionView listaArticulosFiltro => _listaArticulos;
        public IEnumerable<Modeloarticulo> listaModelos { get { return Task.Run(modeloArticuloServicio.GetAllAsync).Result; } }
        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }
        public IEnumerable<Usuario> listaUsuarios { get { return Task.Run(usuarioServicio.GetAllAsync).Result; } }
        public IEnumerable<Espacio> listaEspacios { get { return Task.Run(espacioServicio.GetAllAsync).Result; } }
        public IEnumerable<Articulo> listaArmarios { get { return Task.Run(articuloServicio.GetAllAsync).Result; } }
        public IEnumerable<String> listaEstados { get { return estados; } }

        public IEnumerable<Tipoarticulo> listaTipos
        {
            get { return Task.Run(new TipoArticuloServicio(contexto).GetAllAsync).Result; }
        }

        public DateOnly fechaInicial
        {
            get { return _fechaInicial; }
            set
            {
                _fechaInicial = value; OnPropertyChanged(nameof(fechaInicial));

            }
        }
        public DateOnly fechaFinal
        {
            get { return _fechaFinal; }
            set
            {
                _fechaFinal = value; OnPropertyChanged(nameof(fechaFinal));   

            }
        }


        public Espacio espacioSeleccionado
        {
            get { return _espacioSeleccionado; }
            set
            {
                _espacioSeleccionado = value; OnPropertyChanged(nameof(espacioSeleccionado));
                
            }
        }

        public string nombreBuscado

        {
            get { return _nombreBuscado; }
            set
            {
                _nombreBuscado = value; OnPropertyChanged(nameof(nombreBuscado));
                if (_nombreBuscado == null)
                {
                    _nombreBuscado = "";
                }
            }
        }
        public Articulo crearArticulo
        {
            get { return articulo; }
            set { articulo = value; OnPropertyChanged(nameof(crearArticulo)); }
        }


        public ArticuloServicio artserv
        {
            get { return articuloServicio; }
            set { articuloServicio = value; OnPropertyChanged(nameof(artserv)); }
        }

        public Articulo Clonar { get { return (Articulo)articulo.Clone(); } }

        public bool guarda { get { return Task.Run(() => Update(crearArticulo)).Result; } }

        public bool borrar { get { return Task.Run(() => Delete(crearArticulo)).Result; } }
        public MVArticulo(DiinventarioexamenContext context)

        {
            contexto = context;


        }

        public async Task Inicializa()
        {
            dptoServicio = new DptoServicio(contexto);
            espacioServicio = new EspacioServicio(contexto);
            usuarioServicio = new UsuarioServicio(contexto);
            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            articuloServicio = new ArticuloServicio(contexto);
            _listaArticulos = new ListCollectionView( (await articuloServicio.GetAllAsync()).ToList() );
            articulo = new Articulo();
            articulo.Fechaalta = DateTime.Now;
            servicio = articuloServicio;
            criterios = new List<Predicate<Articulo>>();
            predicadoFiltro = new Predicate<object>(FiltroCriterios);
            InicializaCriterios();


        }

        public void Filtrar()
        {
            AddCriterios();
            listaArticulosFiltro.Filter = predicadoFiltro;
        }

        public void LimpiarFiltros()
        {
            criterios.Clear();
            listaArticulosFiltro.Filter= predicadoFiltro;
            InicializaCriterios();
        }
        private void InicializaCriterios()
        {
            espacioSeleccionado = null;
            _fechaFinal = 
               DateOnly.FromDateTime( articuloServicio.GetFechaFinal());

            _fechaInicial = DateOnly.FromDateTime(articuloServicio.GetFechaInicial
                ());

            criterioFechas = new Predicate<Articulo>(a => a.Fechaalta!= null && fechaInicial.ToDateTime(TimeOnly.MinValue)<=a.Fechaalta && a.Fechaalta <= fechaFinal.ToDateTime(TimeOnly.MinValue));
            criterioEspacio = new Predicate<Articulo>(a => a.EspacioNavigation != null && a.EspacioNavigation == espacioSeleccionado);
        }
        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Articulo articulo = (Articulo)item;
            if (criterios != null)
            {
                correcto = criterios.TrueForAll(x => x(articulo));
            }
            return correcto;
        }
        private void AddCriterios()
        {
            
            criterios.Add(criterioFechas);
            
            criterios.Add(criterioEspacio);
            
        }

    }
}
