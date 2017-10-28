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
public class MySQL_CategoriaDAO : CategoriaDAO{

//Methods
public override bool create(Categoria dto){
if(dto != null && dto.isValid())
{
MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Categoria)));
string query = "INSERT INTO `categoria` ( `id`, `nombre`) VALUES ( NULL, @nombre);";
Value values = new Value();
values.add("@nombre", dto.getnombre());
if(db.ExecSQL(query, values))
{
 dto.setid( (int) db.getLastInsertedID() );
return true;
}
}
return false;}
public override void delete(int id){
MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Categoria)));
string query = "DELETE FROM `categoria` WHERE `categoria`.`id` = @identifier;";
Value values = new Value();
values.add("@identifier", id.ToString());
db.ExecSQL(query, values);}
public override Categoria read(int id){
Categoria dto = null;
if (id > 0)
{
MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Categoria)));
string query = "SELECT `nombre` FROM `categoria` WHERE `id` = @identifier";
Value values = new Value();
values.add("@identifier", id.ToString());
 MySqlDataReader reader = db.ExceuteSQL(query,values);
if (reader != null)
{
if (reader.Read())
{
dto = new Categoria();
dto.setid(id);
 if (!reader.IsDBNull(0))
{
dto.setnombre(reader.GetString(0));
}
}
reader.Close();
}
db.close();}
return dto;
}
public override List<Categoria> readALL(Limiter limiter){
List<Categoria> list = null;
MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Categoria)));
string query = "SELECT id FROM categoria";
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
list = new List<Categoria>();
while (reader.Read())
{
Categoria dto = read(reader.GetInt32(0));
if(dto != null )
{
list.Add(dto);

}
}
reader.Close();
}
db.close();
return list;
}
public override bool update(Categoria dto){
if(dto != null && dto.isValid() && dto.getid() > 0)
{
MySQL_DBManager db = new MySQL_DBManager(DBCredentials_Factory.getCredentials(typeof(Categoria)));
string query = "UPDATE `categoria` SET `nombre` = @nombre WHERE `categoria`.`id` = @identifier;";
Value values = new Value();
values.add("@nombre", dto.getnombre());
values.add("@identifier", dto.getid().ToString());
return db.ExecSQL(query, values);
}
return false;
}
public override Categoria read(Tendencia tendencia){
if(tendencia.getCategoria_id() > 0)
{
return read(tendencia.getCategoria_id());
}
return null;
}
}
}