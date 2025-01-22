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
        private DiinventarioexamenContext contexto;
        private Modeloarticulo modelo;
        private TipoArticuloServicio tipoArticuloServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private IEnumerable<Modeloarticulo> _listaModelos;
        private Tipoarticulo _tipoarticulo;
        public IEnumerable<Tipoarticulo> listaTipos { get { return Task.Run(tipoArticuloServicio.GetAllAsync).Result; } }
        public IEnumerable<Modeloarticulo> listaModelos => _listaModelos;

        private List<Predicate<Modeloarticulo>> criterios;
        private Predicate<Modeloarticulo> criterioTipo;
        public Modeloarticulo modeloArticulo
        {
            get { return modelo; }
            set { modelo = value; OnPropertyChanged(nameof(modeloArticulo)); }
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
            Inicializa();
        }

        public async void Inicializa()
        {

            modeloArticuloServicio = new ModeloArticuloServicio(contexto);
            tipoArticuloServicio = new TipoArticuloServicio(contexto);
            _listaModelos = await modeloArticuloServicio.GetAllAsync();
            modelo = new Modeloarticulo();
            servicio = modeloArticuloServicio;

            criterios = new List<Predicate<Modeloarticulo>>() ;
        }

        private void InicializaCriterios()
        {
            criterioTipo = new Predicate<Modeloarticulo>(m => m.TipoNavigation !=null && m.TipoNavigation.Equals(tipoSeleccionado));
        }

        private bool FiltroCriterios(object item)
        {
            bool correcto = true;
            Modeloarticulo modelo = (Modeloarticulo)item;
            if(criterios != null)
            {
                correcto = criterios.TrueForAll( x => x(modelo));
            }
            return correcto;
        }
    }
}
