    
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
    public partial class LineaDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static LineaEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "Linea_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                LineaEntityCollection lineaEntityCollection = new LineaEntityCollection();
                LineaEntity lineaEntity;
                
                while (reader.Read())
                {
                    lineaEntity = new LineaEntity();
                    
					lineaEntity.Id = Convert.ToInt32(reader["Id"]);
					lineaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					lineaEntity.IdDivision = Convert.ToInt32(reader["IdDivision"]);
					lineaEntity.Linea = Convert.ToString(reader["Linea"]);
					lineaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					lineaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					lineaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						lineaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						lineaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						lineaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					lineaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    lineaEntity.SetLoadedState();
                    lineaEntityCollection.Add(lineaEntity);
                    
                }

                return lineaEntityCollection;
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
     
    //    public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Linea_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "Linea_FindByAll";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdEmpresa != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				//}

				//if(findParameter.IdDivision != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdDivision", findParameter.IdDivision);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdDivision",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Linea))
				//{
				//	mCommand.Parameters.AddWithValue("@Linea", findParameter.Linea );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Linea",DBNull.Value);
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

    //            LineaEntityCollection lineaEntityCollection = new LineaEntityCollection();
    //            LineaEntity lineaEntity;
                

    //            while (reader.Read())
    //            {
    //                lineaEntity = new LineaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		lineaEntity.IdDivisionAsDivision = DivisionDataAccess.ConvertToDivisionEntity(reader, "IdDivision");

    //                }
	   //             #endregion                    
				//	lineaEntity.Id = Convert.ToInt32(reader["Id"]);
				//	lineaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	lineaEntity.IdDivision = Convert.ToInt32(reader["IdDivision"]);
				//	lineaEntity.Linea = Convert.ToString(reader["Linea"]);
				//	lineaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	lineaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	lineaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	lineaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                lineaEntity.SetLoadedState();
    //                lineaEntityCollection.Add(lineaEntity);
                    
    //            }

    //            return lineaEntityCollection;
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
        
    //    public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Linea_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "Linea_FindByAllPaged";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdEmpresa != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				//}

				//if(findParameter.IdDivision != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdDivision", findParameter.IdDivision);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdDivision",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Linea))
				//{
				//	mCommand.Parameters.AddWithValue("@Linea", findParameter.Linea );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Linea",DBNull.Value);
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

    //            LineaEntityCollection lineaEntityCollection = new LineaEntityCollection();
    //            LineaEntity lineaEntity;
                

    //            while (reader.Read())
    //            {
    //                lineaEntity = new LineaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		lineaEntity.IdDivisionAsDivision = DivisionDataAccess.ConvertToDivisionEntity(reader, "IdDivision");

    //                }
	   //             #endregion                    
				//	lineaEntity.Id = Convert.ToInt32(reader["Id"]);
				//	lineaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	lineaEntity.IdDivision = Convert.ToInt32(reader["IdDivision"]);
				//	lineaEntity.Linea = Convert.ToString(reader["Linea"]);
				//	lineaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	lineaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	lineaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		lineaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	lineaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                lineaEntity.SetLoadedState();
    //                lineaEntityCollection.Add(lineaEntity);
                    
    //            }

    //            return lineaEntityCollection;
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

