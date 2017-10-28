namespace DataAccessLayer.DataAccessObjects.Dependencies
{
    /// <summary>
    /// Interfaz de definición de operaciones comunes entre todas las estructuras de datos,
    /// que hacen la funcion de transportar datos persistentes.
    /// </summary>
    public interface DTO
    {
        /// <summary>
        /// Proceso de validación de datos, pertenecientes a el presente DTO
        /// </summary>
        /// <returns> true = valido, false = error</returns>
        bool isValid();
    }
}

