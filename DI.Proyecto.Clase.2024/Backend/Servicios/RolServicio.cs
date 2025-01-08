using di.proyecto.clase._2024.Backend.Servicios;
using DI.Proyecto.Clase._2024.Backend.Modelo;

namespace di.proyecto.clase.ribbon._2023.Backend.Servicios
{
    public class RolServicio : ServicioGenerico<Rol>
    {
        public RolServicio(DiinventarioexamenContext context) : base(context)
        {
        }
    }
}
