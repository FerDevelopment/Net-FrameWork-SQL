using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using di.proyecto.clase.ribbon._2023.Backend.Servicios;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.Frontend.ControlUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVUsuario : MVBaseCRUD<Usuario>
    {
        private DiinventarioexamenContext contexto;
        private UsuarioServicio usuarioServicio;
        private RolServicio rolServicio;
        private DptoServicio dptoServicio;
        private GrupoServicio grupoServicio;
        private ServicioGenerico<Tipousuario> tipoUsuario;
        private Usuario usu;

        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }
        public IEnumerable<Usuario> listaUsuarios { get { return Task.Run(usuarioServicio.GetAllAsync).Result; } }
        public IEnumerable<Grupo> listaGrupos { get { return Task.Run(grupoServicio.GetAllAsync).Result; } }
        public IEnumerable<Rol> listaRoles { get { return Task.Run(rolServicio.GetAllAsync).Result; } }
        public IEnumerable<Tipousuario> listaTipos { get { return Task.Run(tipoUsuario.GetAllAsync).Result; } }

        public Usuario usuario
        {
            get { return usu; }
            set { usu = value; OnPropertyChanged(nameof(usuario)); }
        }

        public bool guarda { get { return Task.Run(() => Update(usuario)).Result; } }
        public bool borrar { get { return Task.Run(() => Delete(usuario)).Result; } }

        public MVUsuario(DiinventarioexamenContext context)

        {
            contexto = context;
            Inicializa();
        }

        public void Inicializa()
        {
            usu = new Usuario();
            usuarioServicio = new UsuarioServicio(contexto);
            rolServicio = new RolServicio(contexto);
            dptoServicio = new DptoServicio(contexto);  
            grupoServicio = new GrupoServicio(contexto);
            tipoUsuario = new ServicioGenerico<Tipousuario>(contexto);
            servicio = usuarioServicio;
            
        }
    }
}
