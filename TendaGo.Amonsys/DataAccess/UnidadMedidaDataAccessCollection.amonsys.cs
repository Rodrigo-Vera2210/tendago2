    
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
    public partial class UnidadMedidaDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static UnidadMedidaEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "UnidadMedida_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                UnidadMedidaEntityCollection unidadMedidaEntityCollection = new UnidadMedidaEntityCollection();
                UnidadMedidaEntity unidadMedidaEntity;
                
                while (reader.Read())
                {
                    unidadMedidaEntity = new UnidadMedidaEntity();
                    
					unidadMedidaEntity.Id = Convert.ToInt32(reader["Id"]);
					unidadMedidaEntity.UnidadMedida = Convert.ToString(reader["UnidadMedida"]);
					unidadMedidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					unidadMedidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					unidadMedidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						unidadMedidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						unidadMedidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						unidadMedidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					unidadMedidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    unidadMedidaEntity.SetLoadedState();
                    unidadMedidaEntityCollection.Add(unidadMedidaEntity);
                    
                }

                return unidadMedidaEntityCollection;
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
     
    //    public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "UnidadMedida_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "UnidadMedida_FindByAll";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UnidadMedida))
				//{
				//	mCommand.Parameters.AddWithValue("@UnidadMedida", findParameter.UnidadMedida );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UnidadMedida",DBNull.Value);
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

    //            UnidadMedidaEntityCollection unidadMedidaEntityCollection = new UnidadMedidaEntityCollection();
    //            UnidadMedidaEntity unidadMedidaEntity;
                

    //            while (reader.Read())
    //            {
    //                unidadMedidaEntity = new UnidadMedidaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{

    //                }
	   //             #endregion                    
				//	unidadMedidaEntity.Id = Convert.ToInt32(reader["Id"]);
				//	unidadMedidaEntity.UnidadMedida = Convert.ToString(reader["UnidadMedida"]);
				//	unidadMedidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	unidadMedidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	unidadMedidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	unidadMedidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                unidadMedidaEntity.SetLoadedState();
    //                unidadMedidaEntityCollection.Add(unidadMedidaEntity);
                    
    //            }

    //            return unidadMedidaEntityCollection;
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
        
    //    public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "UnidadMedida_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "UnidadMedida_FindByAllPaged";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.UnidadMedida))
				//{
				//	mCommand.Parameters.AddWithValue("@UnidadMedida", findParameter.UnidadMedida );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@UnidadMedida",DBNull.Value);
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

    //            UnidadMedidaEntityCollection unidadMedidaEntityCollection = new UnidadMedidaEntityCollection();
    //            UnidadMedidaEntity unidadMedidaEntity;
                

    //            while (reader.Read())
    //            {
    //                unidadMedidaEntity = new UnidadMedidaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{

    //                }
	   //             #endregion                    
				//	unidadMedidaEntity.Id = Convert.ToInt32(reader["Id"]);
				//	unidadMedidaEntity.UnidadMedida = Convert.ToString(reader["UnidadMedida"]);
				//	unidadMedidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	unidadMedidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	unidadMedidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		unidadMedidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	unidadMedidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                unidadMedidaEntity.SetLoadedState();
    //                unidadMedidaEntityCollection.Add(unidadMedidaEntity);
                    
    //            }

    //            return unidadMedidaEntityCollection;
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

