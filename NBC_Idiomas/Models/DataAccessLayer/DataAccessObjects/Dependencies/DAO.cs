using System.Collections.Generic;
namespace DataAccessLayer.DataAccessObjects.Dependencies
{
    /// <summary>
    /// Interfaz de definición de operaciones CRUD básicas aplicables a cualquier DTO Persistente
    /// </summary>
    /// <typeparam name="T">Data Transfer Object</typeparam>
    public interface DAO<T>
    {
        /// <summary>
        /// Metodo de Creación (registro) de entidad (DTO)
        /// </summary>
        /// <param name="entity">Data Transfer Object</param>
        /// <returns></returns>
        bool create(T entity);
        /// <summary>
        /// Metodo de lectura de entidad (DTO) persistente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T read(int id);
        /// <summary>
        /// Lectura de Todos los Elementos T (DTO) que pertenencen al conjunto (DTO) en base de datos, 
        /// tal que cumplan con las condiciones de limit 
        /// </summary>
        /// <param name="limit">Limitador de elementos y desplazamiento</param>
        /// <returns>Lista de T, donde T es elemento de DTO</returns>
        List<T> readALL(Limiter limit);
        /// <summary>
        /// Proceso de eliminacion de elemento que pertenece al conjunto (DTO),
        /// tal que sea identificable por un id = ID
        /// </summary>
        /// <param name="id">numero de identificación de elemento</param>
        void delete(int id);
        /// <summary>
        /// Proceso de actualizacion de entidad T,
        /// tal que t es elemento del conjunto DTO
        /// </summary>
        /// <param name="entity">Data Transfer Object</param>
        /// <returns>true = correcto, false = error</returns>
        bool update(T entity);
    }
}

