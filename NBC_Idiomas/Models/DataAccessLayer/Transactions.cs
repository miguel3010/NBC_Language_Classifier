using DataAccessLayer.Database;
using DataAccessLayer.Database.DBConnections;
using DataAccessLayer.Database.DBConnections.DBParameters;
using Domain_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBC_Idiomas.Models.DataAccessLayer
{
    public class Transactions
    {
        public void setKnowledge(Patron patron, Categoria cat)
        {
            if (patron != null && cat != null)
            {
                if (!String.IsNullOrEmpty(patron.getnombre()) && cat.getid() > 0)
                {
                    MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                    string query = "CALL `setKnowledge`(@p0, @p1);";
                    Value values = new Value();
                    values.add("@p0", patron.getnombre());
                    values.add("@p1", cat.getid().ToString());
                    db.ExecSQL(query, values);
                }
            }
        }
    }
}