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
/// Data access object para la tabla Tendencia mapeada
/// </summary>
public abstract class TendenciaDAO : DAO<Tendencia>{

//Methods
/// <summary>
/// Obtener instancia a la implementacion concreta de esta clase abstracta (FACTORY) bajo la premisa de 'Un DBMS especifico'.
/// </summary>
/// <returns></returns>
public static TendenciaDAO getInstance(){
return new MySQL_TendenciaDAO();
}
public abstract bool create(Tendencia dto);
public abstract void delete(int id);
public abstract Tendencia read(int id);
public abstract List<Tendencia> readALL(Limiter limiter);
public abstract bool update(Tendencia dto);
/// <summary>
///
/// </summary>
/// <param name="categoria"></param>
/// <returns></returns>
public abstract List<Tendencia> readALL(Categoria categoria);
/// <summary>
///
/// </summary>
/// <param name="patron"></param>
/// <returns></returns>
public abstract List<Tendencia> readALL(Patron patron);
}
}