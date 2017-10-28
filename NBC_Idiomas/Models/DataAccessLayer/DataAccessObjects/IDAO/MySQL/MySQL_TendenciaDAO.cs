///
/// Copyright (C) Miguel Ángel Campos - All Rights Reserved
/// Unauthorized copying of this file, via any medium is strictly prohibited
/// Proprietary and confidential
/// Written by Miguel Ángel Campos <camposmiguelangel3010@gmail.com>  twitter: @Miguel_Angel_30,
/// January 2017
/// 
namespace DataAccessLayer.DataAccessObjects.IDAO.MySQL
{
    using DataAccessLayer.DataAccessObjects;
    using Domain_Layer;
    using DataAccessLayer.Database.DBConnections;
    using DataAccessLayer.Database;
    using DataAccessLayer.Database.DBConnections.DBParameters;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using DataAccessLayer.DataAccessObjects.Dependencies;
    using General;
    public class MySQL_TendenciaDAO : TendenciaDAO
    {

        //Methods
        public override bool create(Tendencia dto)
        {
            if (dto != null && dto.isValid())
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                string query = "INSERT INTO `tendencia` ( `id`, `Categoria_id`, `Patron_id`, `medida`) VALUES ( NULL, @Categoria_id, @Patron_id, @medida);";
                Value values = new Value();
                values.add("@Categoria_id", dto.getCategoria_id().ToString());
                values.add("@Patron_id", dto.getPatron_id().ToString());
                values.add("@medida", dto.getmedida().ToString());
                if (db.ExecSQL(query, values))
                {
                    dto.setid((int)db.getLastInsertedID());
                    return true;
                }
            }
            return false;
        }
        public override void delete(int id)
        {
            MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
            string query = "DELETE FROM `tendencia` WHERE `tendencia`.`id` = @identifier;";
            Value values = new Value();
            values.add("@identifier", id.ToString());
            db.ExecSQL(query, values);
        }
        public override Tendencia read(int id)
        {
            Tendencia dto = null;
            if (id > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                string query = "SELECT `Categoria_id`, `Patron_id`, `medida` FROM `tendencia` WHERE `id` = @identifier";
                Value values = new Value();
                values.add("@identifier", id.ToString());
                MySqlDataReader reader = db.ExceuteSQL(query, values);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        dto = new Tendencia();
                        dto.setid(id);
                        if (!reader.IsDBNull(0))
                        {
                            dto.setCategoria_id(reader.GetInt32(0));
                        }
                        if (!reader.IsDBNull(1))
                        {
                            dto.setPatron_id(reader.GetInt32(1));
                        }
                        dto.setmedida(reader.GetInt32(2));
                    }
                    reader.Close();
                }
                db.close();
            }
            return dto;
        }
        public override List<Tendencia> readALL(Limiter limiter)
        {
            List<Tendencia> list = null;
            MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
            string query = "SELECT id FROM tendencia";
            if (limiter != null)
            {
                query += " LIMIT " + limiter.ResultsOffeset + "," + limiter.ResultsCount + ";";
            }
            else
            {
                query += ";";
            }
            MySqlDataReader reader = db.ExceuteSQL(query);
            if (reader != null)
            {
                list = new List<Tendencia>();
                while (reader.Read())
                {
                    Tendencia dto = read(reader.GetInt32(0));
                    if (dto != null)
                    {
                        list.Add(dto);

                    }
                }
                reader.Close();
            }
            db.close();
            return list;
        }
        public override bool update(Tendencia dto)
        {
            if (dto != null && dto.isValid() && dto.getid() > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                string query = "UPDATE `tendencia` SET `Categoria_id` = @Categoria_id, `Patron_id` = @Patron_id, `medida` = @medida WHERE `tendencia`.`id` = @identifier;";
                Value values = new Value();
                values.add("@Categoria_id", dto.getCategoria_id().ToString());
                values.add("@Patron_id", dto.getPatron_id().ToString());
                values.add("@medida", dto.getmedida().ToString());
                values.add("@identifier", dto.getid().ToString());
                return db.ExecSQL(query, values);
            }
            return false;
        }
        public override List<Tendencia> readALL(Categoria categoria)
        {
            List<Tendencia> list = null;
            if (categoria.getid() > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                string query = "SELECT E2.id FROM tendencia E2 JOIN categoria E1 ON E1.id = E2.Categoria_id WHERE E1.id = @Identifier";
                Value values = new Value();
                values.add("@identifier", categoria.getid().ToString());
                MySqlDataReader reader = db.ExceuteSQL(query, values);
                if (reader != null)
                {
                    list = new List<Tendencia>();
                    while (reader.Read())
                    {
                        Tendencia dto = read(reader.GetInt32(0));
                        if (dto != null)
                        {
                            list.Add(dto);
                        }
                    }
                    reader.Close();
                }
                db.close();
            }
            return list;
        }
        public override List<Tendencia> readALL(Patron patron)
        {
            List<Tendencia> list = null;
            if (patron.getid() > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Tendencia)));
                string query = "SELECT E2.id FROM tendencia E2 JOIN patron E1 ON E1.id = E2.Patron_id WHERE E1.id = @Identifier";
                Value values = new Value();
                values.add("@identifier", patron.getid().ToString());
                MySqlDataReader reader = db.ExceuteSQL(query, values);
                if (reader != null)
                {
                    list = new List<Tendencia>();
                    while (reader.Read())
                    {
                        Tendencia dto = read(reader.GetInt32(0));
                        if (dto != null)
                        {
                            list.Add(dto);
                        }
                    }
                    reader.Close();
                }
                db.close();
            }
            return list;
        }
    }
}