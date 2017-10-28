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
    using System;

    public class MySQL_PatronDAO : PatronDAO
    {

        //Methods
        public override bool create(Patron dto)
        {
            if (dto != null && dto.isValid())
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
                string query = "INSERT INTO `patron` ( `id`, `nombre`) VALUES ( NULL, @nombre);";
                Value values = new Value();
                values.add("@nombre", dto.getnombre());
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
            MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
            string query = "DELETE FROM `patron` WHERE `patron`.`id` = @identifier;";
            Value values = new Value();
            values.add("@identifier", id.ToString());
            db.ExecSQL(query, values);
        }
        public override Patron read(int id)
        {
            Patron dto = null;
            if (id > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
                string query = "SELECT `nombre` FROM `patron` WHERE `id` = @identifier";
                Value values = new Value();
                values.add("@identifier", id.ToString());
                MySqlDataReader reader = db.ExceuteSQL(query, values);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        dto = new Patron();
                        dto.setid(id);
                        if (!reader.IsDBNull(0))
                        {
                            dto.setnombre(reader.GetString(0));
                        }
                    }
                    reader.Close();
                }
                db.close();
            }
            return dto;
        }
        public override List<Patron> readALL(Limiter limiter)
        {
            List<Patron> list = null;
            MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
            string query = "SELECT id FROM patron";
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
                list = new List<Patron>();
                while (reader.Read())
                {
                    Patron dto = read(reader.GetInt32(0));
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
        public override bool update(Patron dto)
        {
            if (dto != null && dto.isValid() && dto.getid() > 0)
            {
                MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
                string query = "UPDATE `patron` SET `nombre` = @nombre WHERE `patron`.`id` = @identifier;";
                Value values = new Value();
                values.add("@nombre", dto.getnombre());
                values.add("@identifier", dto.getid().ToString());
                return db.ExecSQL(query, values);
            }
            return false;
        }
        public override Patron read(Tendencia tendencia)
        {
            if (tendencia.getPatron_id() > 0)
            {
                return read(tendencia.getPatron_id());
            }
            return null;
        }
        public override Patron read(string item)
        {
            Patron patron = null;
            MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Patron)));
            string query = "SELECT patron.id FROM `patron` WHERE patron.nombre =  @nombre";
            Value values = new Value();
            values.add("@nombre", item);
            MySqlDataReader reader = db.ExceuteSQL(query, values);
            if (reader != null)
            {
                if (reader.Read())
                {
                    patron = read(reader.GetInt32(0));
                }
                reader.Close();
            }
            db.close();
            return patron;
        }
    }
}