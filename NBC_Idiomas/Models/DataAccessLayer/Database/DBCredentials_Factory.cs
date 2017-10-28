namespace DataAccessLayer.Database
{
    using Domain_Layer;
    using System;

    /// <summary>
    /// Factory de Credenciales de Base de datos,
    /// control centralizado de usuarios de base de datos. 
    /// </summary>
    public class DBCredentials_Factory
    {
        public static DBCredentials getCredentials(Type T)
        {
            DBCredentials dc = new DBCredentials()
            {
                DBName = "nbc",
                Url = "localhost",
                Login = "root",
                Password = ""
            };
            //if (T.GetType() == typeof(table1))
            //{
            //    dc.Login = "root"; dc.Password = "";
            //    return dc;
            //}
            return dc;
        }
    }
}

