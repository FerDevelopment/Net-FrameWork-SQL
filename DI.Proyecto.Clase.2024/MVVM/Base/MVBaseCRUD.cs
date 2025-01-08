using di.proyecto.clase._2024.Backend.Servicios;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace di.proyecto.clase._2024.MVVM.Base
{
    public class MVBaseCRUD<T> : MVBase
        where T : class
    {
        public ServicioGenerico<T> servicio { get; set; }
        private static Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Realiza una inserción en la base de datos y captura la excepción
        /// </summary>
        /// <param name="entity">Objeto a guardar</param>
        /// <returns></returns>
        public async Task<bool> Add(T entity)
        {
            bool correcto = true;
            try
            {
                await servicio.AddAsync(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Insertando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
            }
            return correcto;
        }
        /// <summary>
        /// Realiza una actualización de una tupla de la base de datos
        /// </summary>
        /// <param name="entity">Objeto que se actualiza</param>
        /// <returns></returns>
        public async Task<bool> Update(T entity)
        {
            bool correcto = true;
            try
            {
                await servicio.UpdateAsync(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Insertando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
            }
            return correcto;
        }
        /// <summary>
        /// Borra una fila de la tabla correspondiente
        /// </summary>
        /// <param name="entity">Objeto que se borra</param>
        /// <returns></returns>
        public async Task<bool> Delete(T entity)
        {
            bool correcto = true;
            try
            {
                await servicio.DeleteAsync(entity);
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                // Guardamos en el Log el error
                log.Error("\n" + "Insertando un nuevo objeto ..." + entity.GetType() + "\n" + dbex.Message + "\n" + dbex.StackTrace);
            }
            return correcto;
        }
    }
}
