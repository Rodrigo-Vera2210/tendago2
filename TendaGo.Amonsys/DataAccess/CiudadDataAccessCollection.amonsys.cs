    
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Amonsys Software Factory.
//     Template: Speed Developer FrameWork Version 1.0 (For Windows Applications)
//     Web Site: http://www.amonsys.com/SDF
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ER.BE; 


namespace ER.DA
{
    public partial class CiudadDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static CiudadEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "Ciudad_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                CiudadEntityCollection ciudadEntityCollection = new CiudadEntityCollection();
                CiudadEntity ciudadEntity;
                
                while (reader.Read())
                {
                    ciudadEntity = new CiudadEntity();
                    
					ciudadEntity.Id = Convert.ToInt32(reader["Id"]);
					ciudadEntity.IdProvincia = Convert.ToInt32(reader["IdProvincia"]);
					ciudadEntity.Ciudad = Convert.ToString(reader["Ciudad"]);
					if (reader["Codigo"] != DBNull.Value)
					{
						ciudadEntity.Codigo = Convert.ToString(reader["Codigo"]).ToUpper();
					}
					ciudadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					ciudadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					ciudadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						ciudadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						ciudadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						ciudadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					ciudadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    ciudadEntity.SetLoadedState();
                    ciudadEntityCollection.Add(ciudadEntity);
                    
                }

                return ciudadEntityCollection;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (reader != null) reader.Close();
                mCommand.Dispose();
            }

        }
        */
     
    //    public static CiudadEntityCollection FindByAll(CiudadFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static CiudadEntityCollection FindByAll(CiudadFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
    //    {
    //        SqlCommand mCommand = new SqlCommand();
    //        SqlDataReader reader = null;
    //        try
    //        {
    //            mCommand.Connection = conexion;
    //            mCommand.CommandType = CommandType.StoredProcedure;
    //            mCommand.Transaction = transaction;
    //            if (deepLoadLevel >= 1)
		  //   	{
    //            	mCommand.CommandText = "Ciudad_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "Ciudad_FindByAll";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdProvincia != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProvincia", findParameter.IdProvincia);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProvincia",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Ciudad))
				//{
				//	mCommand.Parameters.AddWithValue("@Ciudad", findParameter.Ciudad );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Ciudad",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Codigo))
				//{
				//	mCommand.Parameters.AddWithValue("@Codigo", findParameter.Codigo );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Codigo",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.IpIngreso))
				//{
				//	mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IpIngreso",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioIngreso",DBNull.Value);
				//}

				//if(findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@FechaIngreso",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.IpModificacion))
				//{
				//	mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				//}

				//if(findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				//}

				//if(findParameter.IdEstado != short.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
				//}

    
               	
    //            if (conexion.State != ConnectionState.Open) conexion.Open();
    //            reader = mCommand.ExecuteReader();

    //            CiudadEntityCollection ciudadEntityCollection = new CiudadEntityCollection();
    //            CiudadEntity ciudadEntity;
                

    //            while (reader.Read())
    //            {
    //                ciudadEntity = new CiudadEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		ciudadEntity.IdProvinciaAsProvincia = ProvinciaDataAccess.ConvertToProvinciaEntity(reader, "IdProvincia");

    //                }
	   //             #endregion                    
				//	ciudadEntity.Id = Convert.ToInt32(reader["Id"]);
				//	ciudadEntity.IdProvincia = Convert.ToInt32(reader["IdProvincia"]);
				//	ciudadEntity.Ciudad = Convert.ToString(reader["Ciudad"]);
				//	if (reader["Codigo"] != DBNull.Value)
				//	{
				//		ciudadEntity.Codigo = Convert.ToString(reader["Codigo"]).ToUpper();
				//	}
				//	ciudadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	ciudadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	ciudadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	ciudadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                ciudadEntity.SetLoadedState();
    //                ciudadEntityCollection.Add(ciudadEntity);
                    
    //            }

    //            return ciudadEntityCollection;
    //        }
    //        catch (Exception exc)
    //        {
    //            throw exc;
    //        }
    //        finally
    //        {
    //            if (reader != null) reader.Close();
    //            mCommand.Dispose();
    //        }

    //    }
        
    //    public static CiudadEntityCollection FindByAllPaged(CiudadFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static CiudadEntityCollection FindByAllPaged(CiudadFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
    //    {
    //        SqlCommand mCommand = new SqlCommand();
    //        SqlDataReader reader = null;
    //        try
    //        {
    //            mCommand.Connection = conexion;
    //            mCommand.CommandType = CommandType.StoredProcedure;
    //            mCommand.Transaction = transaction;
    //            if (deepLoadLevel >= 1)
		  //   	{
    //            	mCommand.CommandText = "Ciudad_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "Ciudad_FindByAllPaged";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdProvincia != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProvincia", findParameter.IdProvincia);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProvincia",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Ciudad))
				//{
				//	mCommand.Parameters.AddWithValue("@Ciudad", findParameter.Ciudad );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Ciudad",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Codigo))
				//{
				//	mCommand.Parameters.AddWithValue("@Codigo", findParameter.Codigo );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Codigo",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.IpIngreso))
				//{
				//	mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IpIngreso",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioIngreso",DBNull.Value);
				//}

				//if(findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@FechaIngreso",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.IpModificacion))
				//{
				//	mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				//}

				//if(findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				//}

				//if(findParameter.IdEstado != short.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
				//}


				//mCommand.Parameters.AddWithValue("@PageNumber",pageNumber);
				//mCommand.Parameters.AddWithValue("@PageSize",pageSize);
				//if (deepLoadLevel > 1)
		  //   	{
				//	mCommand.Parameters.AddWithValue("@OrderBy",orderBy);
			 //   }
               	
    //            if (conexion.State != ConnectionState.Open) conexion.Open();
    //            reader = mCommand.ExecuteReader();

    //            CiudadEntityCollection ciudadEntityCollection = new CiudadEntityCollection();
    //            CiudadEntity ciudadEntity;
                

    //            while (reader.Read())
    //            {
    //                ciudadEntity = new CiudadEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		ciudadEntity.IdProvinciaAsProvincia = ProvinciaDataAccess.ConvertToProvinciaEntity(reader, "IdProvincia");

    //                }
	   //             #endregion                    
				//	ciudadEntity.Id = Convert.ToInt32(reader["Id"]);
				//	ciudadEntity.IdProvincia = Convert.ToInt32(reader["IdProvincia"]);
				//	ciudadEntity.Ciudad = Convert.ToString(reader["Ciudad"]);
				//	if (reader["Codigo"] != DBNull.Value)
				//	{
				//		ciudadEntity.Codigo = Convert.ToString(reader["Codigo"]).ToUpper();
				//	}
				//	ciudadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	ciudadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	ciudadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		ciudadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	ciudadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                ciudadEntity.SetLoadedState();
    //                ciudadEntityCollection.Add(ciudadEntity);
                    
    //            }

    //            return ciudadEntityCollection;
    //        }
    //        catch (Exception exc)
    //        {
    //            throw exc;
    //        }
    //        finally
    //        {
    //            if (reader != null) reader.Close();
    //            mCommand.Dispose();
    //        }

    //    }
        
          
    }
}

