﻿using di.proyecto.clase._2024.Backend.Servicios;
using di.proyecto.clase._2024.MVVM.Base;
using di.proyecto.clase.ribbon._2023.Backend.Servicios;
using DI.Proyecto.Clase._2024.Backend.Modelo;
using DI.Proyecto.Clase._2024.Frontend.ControlUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
        private List<Predicate<Usuario>> criterios;
        private Predicate<Usuario> criterioBusquedaNombre;
        private Predicate<Usuario> criterioGrupo;
        private Predicate<Usuario> criterioSalidas;
        private Predicate<object> predicadoFiltro;
        private string _textoBusqueda ="";
        private ListCollectionView _listaUsuarios;
        public IEnumerable<Departamento> listaDepartamentos { get { return Task.Run(dptoServicio.GetAllAsync).Result; } }

        public IEnumerable<Grupo> listaGrupos { get { return Task.Run(grupoServicio.GetAllAsync).Result; } }
        public IEnumerable<Rol> listaRoles { get { return Task.Run(rolServicio.GetAllAsync).Result; } }
        public IEnumerable<Tipousuario> listaTipos { get { return Task.Run(tipoUsuario.GetAllAsync).Result; } }


        public ListCollectionView listaUsuarioFiltro => _listaUsuarios;
        public Usuario usuario
        {
            get { return usu; }
            set { usu = value; OnPropertyChanged(nameof(usuario)); }
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

        private Grupo _grupoSeleccionado;
        public Grupo grupoSeleccionado
        {
            get { return _grupoSeleccionado; }
            set
            {
                _grupoSeleccionado = value; OnPropertyChanged(nameof(grupoSeleccionado));
            }
        }
        private Boolean _check = false;

        public Boolean check
        {
            get { return _check; }
            set
            {
                _check = value; OnPropertyChanged(nameof(check));
            }
        }


        public bool guarda { get { return Task.Run(() => Update(usuario)).Result; } }
        public bool borrar { get { return Task.Run(() => Delete(usuario)).Result; } }

        public MVUsuario(DiinventarioexamenContext context)

        {
            contexto = context;

        }

        public async Task Inicializa()
        {
            usu = new Usuario();
            usuarioServicio = new UsuarioServicio(contexto);
            rolServicio = new RolServicio(contexto);
            dptoServicio = new DptoServicio(contexto);
            grupoServicio = new GrupoServicio(contexto);
            tipoUsuario = new ServicioGenerico<Tipousuario>(contexto);
            servicio = usuarioServicio;
            criterios = new List<Predicate<Usuario>>();
            predicadoFiltro = new Predicate<object>(FiltroCriterios);
            _listaUsuarios = new ListCollectionView((await usuarioServicio.GetAllAsync()).ToList());
            InicializaCriterios();


        }

        private void AddCriterios()
        {

            if (String.IsNullOrEmpty(textoBusqueda))
            {
                criterios.Add(criterioBusquedaNombre);

            }

            if (grupoSeleccionado != null)
            {
                criterios.Add(criterioGrupo);

            }

            if (!criterios.Contains(criterioSalidas) && check)
            {
                criterios.Add(criterioSalidas);
            }
                else
                {
                if (!check)
                {
                    criterios.Remove(criterioSalidas);
                }
                    
                }


        }

        public void Filtrar()
        {
            AddCriterios();
            listaUsuarioFiltro.Filter = predicadoFiltro;
        }
        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Usuario usuario = (Usuario)item;
            if (criterios != null)
            {
                correcto = criterios.TrueForAll(x => x(usuario));
            }
            return correcto;
        }
        private void InicializaCriterios()
        {

            criterioBusquedaNombre = new Predicate<Usuario>(u => u.Nombre != null && ((u.Nombre.ToLower() + " " + u.Apellido1.ToLower()).Contains(textoBusqueda.ToLower())));
            criterioGrupo = new Predicate<Usuario>(u => u.Grupo != null && u.GrupoNavigation == grupoSeleccionado);
            criterioSalidas = new Predicate<Usuario>(u => u.Salida != null && u.Salida.Count >= 10);
        }
    }
}
