    
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
    public partial class PresupuestoFacturacionDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static PresupuestoFacturacionEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "PresupuestoFacturacion_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                PresupuestoFacturacionEntityCollection presupuestoFacturacionEntityCollection = new PresupuestoFacturacionEntityCollection();
                PresupuestoFacturacionEntity presupuestoFacturacionEntity;
                
                while (reader.Read())
                {
                    presupuestoFacturacionEntity = new PresupuestoFacturacionEntity();
                    
					presupuestoFacturacionEntity.Id = Convert.ToInt32(reader["Id"]);
					presupuestoFacturacionEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					presupuestoFacturacionEntity.Ruc = Convert.ToString(reader["Ruc"]);
					presupuestoFacturacionEntity.Mes = Convert.ToString(reader["Mes"]);
					presupuestoFacturacionEntity.Fecha = Convert.ToString(reader["Fecha"]);
					presupuestoFacturacionEntity.Presupuesto = (decimal) reader["Presupuesto"];
					presupuestoFacturacionEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					presupuestoFacturacionEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					presupuestoFacturacionEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						presupuestoFacturacionEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						presupuestoFacturacionEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						presupuestoFacturacionEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					presupuestoFacturacionEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    presupuestoFacturacionEntity.SetLoadedState();
                    presupuestoFacturacionEntityCollection.Add(presupuestoFacturacionEntity);
                    
                }

                return presupuestoFacturacionEntityCollection;
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
     
    //    public static PresupuestoFacturacionEntityCollection FindByAll(PresupuestoFacturacionFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static PresupuestoFacturacionEntityCollection FindByAll(PresupuestoFacturacionFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "PresupuestoFacturacion_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "PresupuestoFacturacion_FindByAll";

                
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

				//if(!String.IsNullOrEmpty(findParameter.Ruc))
				//{
				//	mCommand.Parameters.AddWithValue("@Ruc", findParameter.Ruc );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Ruc",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Mes))
				//{
				//	mCommand.Parameters.AddWithValue("@Mes", findParameter.Mes );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Mes",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Fecha))
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				//}

				//if(findParameter.Presupuesto != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Presupuesto", findParameter.Presupuesto);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Presupuesto",DBNull.Value);
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

    //            PresupuestoFacturacionEntityCollection presupuestoFacturacionEntityCollection = new PresupuestoFacturacionEntityCollection();
    //            PresupuestoFacturacionEntity presupuestoFacturacionEntity;
                

    //            while (reader.Read())
    //            {
    //                presupuestoFacturacionEntity = new PresupuestoFacturacionEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		presupuestoFacturacionEntity.IdEmpresaAsEmpresa = EmpresaDataAccess.ConvertToEmpresaEntity(reader, "IdEmpresa");
				//		presupuestoFacturacionEntity.RucAsRuc = RucDataAccess.ConvertToRucEntity(reader, "Ruc");

    //                }
	   //             #endregion                    
				//	presupuestoFacturacionEntity.Id = Convert.ToInt32(reader["Id"]);
				//	presupuestoFacturacionEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	presupuestoFacturacionEntity.Ruc = Convert.ToString(reader["Ruc"]);
				//	presupuestoFacturacionEntity.Mes = Convert.ToString(reader["Mes"]);
				//	presupuestoFacturacionEntity.Fecha = Convert.ToString(reader["Fecha"]);
				//	presupuestoFacturacionEntity.Presupuesto = (decimal) reader["Presupuesto"];
				//	presupuestoFacturacionEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	presupuestoFacturacionEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	presupuestoFacturacionEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	presupuestoFacturacionEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                presupuestoFacturacionEntity.SetLoadedState();
    //                presupuestoFacturacionEntityCollection.Add(presupuestoFacturacionEntity);
                    
    //            }

    //            return presupuestoFacturacionEntityCollection;
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
        
    //    public static PresupuestoFacturacionEntityCollection FindByAllPaged(PresupuestoFacturacionFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static PresupuestoFacturacionEntityCollection FindByAllPaged(PresupuestoFacturacionFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "PresupuestoFacturacion_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "PresupuestoFacturacion_FindByAllPaged";

                
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

				//if(!String.IsNullOrEmpty(findParameter.Ruc))
				//{
				//	mCommand.Parameters.AddWithValue("@Ruc", findParameter.Ruc );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Ruc",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Mes))
				//{
				//	mCommand.Parameters.AddWithValue("@Mes", findParameter.Mes );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Mes",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Fecha))
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				//}

				//if(findParameter.Presupuesto != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Presupuesto", findParameter.Presupuesto);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Presupuesto",DBNull.Value);
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

    //            PresupuestoFacturacionEntityCollection presupuestoFacturacionEntityCollection = new PresupuestoFacturacionEntityCollection();
    //            PresupuestoFacturacionEntity presupuestoFacturacionEntity;
                

    //            while (reader.Read())
    //            {
    //                presupuestoFacturacionEntity = new PresupuestoFacturacionEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		presupuestoFacturacionEntity.IdEmpresaAsEmpresa = EmpresaDataAccess.ConvertToEmpresaEntity(reader, "IdEmpresa");
				//		presupuestoFacturacionEntity.RucAsRuc = RucDataAccess.ConvertToRucEntity(reader, "Ruc");

    //                }
	   //             #endregion                    
				//	presupuestoFacturacionEntity.Id = Convert.ToInt32(reader["Id"]);
				//	presupuestoFacturacionEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	presupuestoFacturacionEntity.Ruc = Convert.ToString(reader["Ruc"]);
				//	presupuestoFacturacionEntity.Mes = Convert.ToString(reader["Mes"]);
				//	presupuestoFacturacionEntity.Fecha = Convert.ToString(reader["Fecha"]);
				//	presupuestoFacturacionEntity.Presupuesto = (decimal) reader["Presupuesto"];
				//	presupuestoFacturacionEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	presupuestoFacturacionEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	presupuestoFacturacionEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		presupuestoFacturacionEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	presupuestoFacturacionEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                presupuestoFacturacionEntity.SetLoadedState();
    //                presupuestoFacturacionEntityCollection.Add(presupuestoFacturacionEntity);
                    
    //            }

    //            return presupuestoFacturacionEntityCollection;
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

