///
/// Copyright (C) Miguel Ángel Campos - All Rights Reserved
/// Unauthorized copying of this file, via any medium is strictly prohibited
/// Proprietary and confidential
/// Written by Miguel Ángel Campos <camposmiguelangel3010@gmail.com>  twitter: @Miguel_Angel_30,
/// January 2017
/// 
namespace DataAccessLayer.DataAccessObjects 
{
using DataAccessLayer.DataAccessObjects.Dependencies;
using Domain_Layer;
using DataAccessLayer.DataAccessObjects.IDAO.MySQL;
using System.Collections.Generic;
/// <summary>
/// Data access object para la tabla Categoria mapeada
/// </summary>
public abstract class CategoriaDAO : DAO<Categoria>{

//Methods
/// <summary>
/// Obtener instancia a la implementacion concreta de esta clase abstracta (FACTORY) bajo la premisa de 'Un DBMS especifico'.
/// </summary>
/// <returns></returns>
public static CategoriaDAO getInstance(){
return new MySQL_CategoriaDAO();
}
public abstract bool create(Categoria dto);
public abstract void delete(int id);
public abstract Categoria read(int id);
public abstract List<Categoria> readALL(Limiter limiter);
public abstract bool update(Categoria dto);
/// <summary>
/// Metodo de lectura de un elemento del conjunto Categoria producto de una relación con la tabla Tendencia
/// </summary>
/// <param name="tendencia"></param>
/// <returns></returns>
public abstract Categoria read(Tendencia tendencia);
}
}