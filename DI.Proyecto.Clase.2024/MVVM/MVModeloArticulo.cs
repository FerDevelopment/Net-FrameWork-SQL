using DI.Proyecto.Clase._2024.Backend.Modelo;
using di.proyecto.clase._2024.Backend.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using di.proyecto.clase._2024.MVVM.Base;

namespace DI.Proyecto.Clase._2024.MVVM
{
    public class MVModeloArticulo :MVBaseCRUD <Modeloarticulo>
    {
        private DiinventarioexamenContext contexto;
        private Modeloarticulo modelo;
        private TipoArticuloServicio tipoArticuloServicio;
        private ModeloArticuloServicio modeloArticuloServicio;
        private IEnumerable<Modeloarticulo> _listaModelos;
        public IEnumerable<Tipoarticulo> listaTipos { get { return Task.Run(tipoArticuloServicio.GetAllAsync).Result; } }
        public IEnumerable<Modeloarticulo> listaModelos => _listaModelos;
        public Modeloarticulo modeloArticulo
        {
             get { return modelo; }
             set { modelo = value; OnPropertyChanged(nameof(modeloArticulo)); }
        }
        public bool guarda { get { return Task.Run(() => Add(modeloArticulo)).Result; } }
        
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
        }

    }
}
