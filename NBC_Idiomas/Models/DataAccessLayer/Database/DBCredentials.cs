namespace DataAccessLayer.Database
{
    /// <summary>
    /// Credenciales de inicio y configuracion de base de datos
    /// </summary>
    public class DBCredentials
    {
        /// <summary>
        /// La dirección URL fisica a la base de datos
        /// </summary>
        public string Url;
        /// <summary>
        /// El usuario de base de datos
        /// </summary>
        public string Login;
        /// <summary>
        /// El nombre de la base de datos seleccionada
        /// </summary>
        public string DBName;
        /// <summary>
        /// La Contraseña para el usuario especifico de base de datos
        /// </summary>
        public string Password;
    }
}

