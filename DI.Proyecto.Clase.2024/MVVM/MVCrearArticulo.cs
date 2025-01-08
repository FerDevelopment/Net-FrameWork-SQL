using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using DI.Proyecto.Clase._2024.Backend.Modelo;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVCrearArticulo: MVBaseCRUD<Articulo>
    {
        private DiinventarioexamenContext contexto;
        private Articulo articulo;
        private ModeloArticuloServicio modeloArticuloServicio;
        private ArticuloServicio articuloServicio;
        private DptoServicio dptoServicio;
        private UsuarioServicio usuarioServicio;
        private EspacioServicio espacioServicio;
        private List<String> estados = new List<string> { "Operativo", "Obsoleto", "Mantenimiento" };

        public IEnumerable<Modeloarticulo> listaModelos { get { return Task.Run(modeloArticuloServicio.GetAllAsync).Result; } }
        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }
        public IEnumerable<Usuario> listaUsuarios { get { return Task.Run(usuarioServicio.GetAllAsync).Result; } }
        public IEnumerable<Espacio> listaEspacios { get { return Task.Run(espacioServicio.GetAllAsync).Result; } }
        public IEnumerable<Articulo> listaArmarios { get { return Task.Run(articuloServicio.GetAllAsync).Result; } }
        public IEnumerable<String> listaEstados { get { return estados; } }


        public Articulo crearArticulo
        {
            get { return articulo; }
            set { articulo = value; OnPropertyChanged(nameof(crearArticulo)); }
        }
        public bool guarda { get { return Task.Run(() => Add(crearArticulo)).Result; } }

        public MVCrearArticulo(DiinventarioexamenContext context)

        {
            contexto = context;
            Inicializa();
        }

        public void Inicializa()
        {
            
            dptoServicio = new DptoServicio(contexto);
            espacioServicio = new EspacioServicio(contexto);
            usuarioServicio = new UsuarioServicio(contexto);
            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            articuloServicio = new ArticuloServicio(contexto);
            articulo = new Articulo();
            servicio = articuloServicio;
        }

    }
}
