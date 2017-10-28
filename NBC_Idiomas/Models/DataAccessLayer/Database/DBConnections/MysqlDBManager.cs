namespace DataAccessLayer.Database.DBConnections
{
    using DataAccessLayer.Database.DBConnections.DBParameters;
    using General;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;

    public class MySQL_DBManager
    {
        //Atributes
        private MySqlConnection conn;
        private Boolean valid = false;
        private string url;
        private string db;
        private string pass;
        private string id;
        private long lasinsertid = 0;


        //Methods
        public MySQL_DBManager(string url, string db, string id, string pass)
        {
            configDB(url, db, id, pass);
        }

        public MySQL_DBManager(DBCredentials dBCredentials)
        {
            if (dBCredentials != null)
            {
                configDB(dBCredentials.Url, dBCredentials.DBName, dBCredentials.Login, dBCredentials.Password);
            }
        }

        private void configDB(string url, string db, string id, string pass)
        {
            if (Util.validateString(url) &&
                Util.validateString(db) &&
                Util.validateString(id) &&
                pass != null)
            {

                this.url = url;
                this.db = db;
                this.id = id;
                this.pass = pass;
                string connetionString = "server=" + url + ";database=" + db + ";uid=" + id + ";pwd=" + pass + ";Pooling=true;";
                conn = new MySqlConnection(connetionString);
                valid = true;
            }
            else
            {
                valid = false;
            }
        }

        public long getLastInsertedID()
        {
            return lasinsertid;
        }

        public string Db
        {
            get { return db; }

        }

        public string Id
        {
            get { return id; }

        }

        public string Pass
        {
            get { return pass; }

        }

        public string Url
        {
            get { return url; }
        }

        public Boolean ExecSQL(string query)
        {
            if (valid && Util.validateString(query))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    getLastId(command);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }

        public Boolean ExecSQL(string query, Value valores)
        {
            if (valid && Util.validateString(query) && valores != null)
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(query, conn);
                    prepareStatement(command, valores);
                    command.ExecuteNonQuery();
                    getLastId(command);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Data);
                }
                finally
                {
                    conn.Close();
                }

            }
            return false;
        }

        private void getLastId(MySqlCommand command)
        {
            try
            {
                object ores = MySqlHelper.ExecuteScalar(conn, "SELECT LAST_INSERT_ID();");
                if (ores != null)
                {
                    lasinsertid = Convert.ToInt64(ores);
                }
            }
            catch (Exception)
            {

            }

        }

        public MySqlDataReader ExceuteSQL(string query)
        {
            if (valid && Util.validateString(query))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    return Reader;
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        public MySqlDataReader ExceuteSQL(string query, Value valores)
        {
            if (valid && Util.validateString(query))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                try
                {
                    conn.Open();
                    prepareStatement(cmd, valores);
                    MySqlDataReader Reader = cmd.ExecuteReader();
                    return Reader;
                }
                catch (Exception)
                {
                }
            }
            return null;
        }

        private MySqlCommand prepareStatement(MySqlCommand comand, Value valores)
        {
            if (comand != null && valores != null && valores.getList() != null && valores.getList().Count > 0)
            {
                List<node> values = valores.getList();
                if (values != null)
                {
                    foreach (node prime in values)
                    {
                        comand.Parameters.AddWithValue(prime.key, prime.value);
                    }
                }

            }

            return comand;
        }

        public void close()
        {
            if (conn != null)
            {
                conn.Close();
            }

        }
    }
}