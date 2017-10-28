namespace DataAccessLayer.DataAccessObjects.Dependencies
{
    /// <summary>
    /// Limitador de elementos en lectura
    /// </summary>
    public class Limiter
    {
        /// <summary>
        /// Constructor de Limitador
        /// </summary>
        public Limiter()
        {
            ResultsOffeset = 0;
            ResultsCount = 0;
        }
        /// <summary>
        /// Cantidad de resultados deben ser leidos
        /// </summary>
        public int ResultsCount;
        /// <summary>
        /// Cantidad de elementos deben ser desplazados
        /// </summary>
        public int ResultsOffeset;
    }
}

