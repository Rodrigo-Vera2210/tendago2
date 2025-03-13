
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ER.BE; 


namespace ER.DA
{
    public partial class PantallaDataAccessCollection
    {
        //public static PantallaEntityCollection ObtenerPantallasPorPerfil(string usuario, SqlConnection connection, SqlTransaction transaction)
        //{
        //    SqlCommand mCommand = new SqlCommand();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Pantalla_ObtenerPantallasPerfil";

        //        mCommand.Parameters.AddWithValue("@usuario", usuario);

        //        if (connection.State != ConnectionState.Open) connection.Open();
        //        reader = mCommand.ExecuteReader();
        //        PantallaEntityCollection pantallaEntityCollection = new PantallaEntityCollection();
        //        PantallaEntity pantallaEntity;
                
        //        while (reader.Read())
        //        {
        //            pantallaEntity = new PantallaEntity();
                    
        //            pantallaEntity.Id = Convert.ToInt16(reader["Id"]);
        //            pantallaEntity.IdModulo = Convert.ToInt16(reader["IdModulo"]);
        //            if (reader["IdGrupo"] != DBNull.Value)
        //            {
        //                pantallaEntity.IdGrupo = Convert.ToInt16(reader["IdGrupo"]);
        //            }
        //            pantallaEntity.Nombre = Convert.ToString(reader["Nombre"]);
        //            if (reader["Descripcion"] != DBNull.Value)
        //            {
        //                pantallaEntity.Descripcion = Convert.ToString(reader["Descripcion"]);
        //            }
        //            if (reader["NombreAssembly"] != DBNull.Value)
        //            {
        //                pantallaEntity.NombreAssembly = Convert.ToString(reader["NombreAssembly"]);
        //            }
        //            if (reader["Icono"] != DBNull.Value)
        //            {
        //                pantallaEntity.Icono = Convert.ToString(reader["Icono"]);
        //            }
        //            if (reader["Ayuda"] != DBNull.Value)
        //            {
        //                pantallaEntity.Ayuda = Convert.ToString(reader["Ayuda"]).ToUpper();
        //            }
        //            pantallaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
        //            pantallaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
        //            pantallaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
        //            if (reader["IpModificacion"] != DBNull.Value)
        //            {
        //                pantallaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
        //            }
        //            if (reader["UsuarioModificacion"] != DBNull.Value)
        //            {
        //                pantallaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
        //            }
        //            if (reader["FechaModificacion"] != DBNull.Value)
        //            {
        //                pantallaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
        //            }
        //            pantallaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);


        //            pantallaEntity.SetLoadedState();
        //            pantallaEntityCollection.Add(pantallaEntity);

        //        }
        //        return pantallaEntityCollection;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        mCommand.Dispose();
        //    }
        //}
    }
}

