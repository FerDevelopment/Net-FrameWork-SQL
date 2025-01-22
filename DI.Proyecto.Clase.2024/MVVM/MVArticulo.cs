using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using Mysqlx.Cursor;
using System;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVArticulo: MVBaseCRUD<Articulo>
    {
        private DiinventarioexamenContext contexto;
        private Articulo articulo;
        private ModeloArticuloServicio modeloArticuloServicio;
        private ArticuloServicio articuloServicio;
        private DptoServicio dptoServicio;
        private UsuarioServicio usuarioServicio;
        private EspacioServicio espacioServicio;
        private List<String> estados = new List<string> { "operativo", "obsoleto", "mantenimiento" };

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

        
        public ArticuloServicio artserv
        {
            get { return articuloServicio; }
            set { articuloServicio = value; OnPropertyChanged(nameof(artserv)); }
        }

        public Articulo Clonar {  get { return (Articulo)articulo.Clone(); } }

        public bool guarda { get { return Task.Run(() => Update(crearArticulo)).Result; } }

        public bool borrar { get  { return Task.Run(() => Delete(crearArticulo)).Result; } }
        public MVArticulo(DiinventarioexamenContext context)

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
            articulo.Fechaalta = DateTime.Now;
            servicio = articuloServicio;
            

        }

    }
}
