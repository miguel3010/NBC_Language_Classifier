///
/// Copyright (C) Miguel Ángel Campos - All Rights Reserved
/// Unauthorized copying of this file, via any medium is strictly prohibited
/// Proprietary and confidential
/// Written by Miguel Ángel Campos <camposmiguelangel3010@gmail.com>  twitter: @Miguel_Angel_30,
/// January 2017
/// 
namespace Domain_Layer 
{
using DataAccessLayer.DataAccessObjects.Dependencies;
using System.Collections.Generic;
using General;
public class Patron : DTO{
//Atributes
private int id;
private string nombre;
public List<Tendencia> tendencias;

//Methods
public bool isValid(){
if( Util.validateString(nombre)){
return true;
}
return false;
}
/// <summary>
///
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
public void setid(int id){
this.id = id;
}
/// <summary>
///
/// </summary>
/// <returns></returns>
public int getid(){
return this.id;
}
/// <summary>
///
/// </summary>
/// <param name="nombre"></param>
/// <returns></returns>
public void setnombre(string nombre){
this.nombre = nombre;
}
/// <summary>
///
/// </summary>
/// <returns></returns>
public string getnombre(){
return this.nombre;
}
}
}