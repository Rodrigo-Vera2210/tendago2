    
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
    public partial class DetalleCobroDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static DetalleCobroEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "DetalleCobro_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                DetalleCobroEntityCollection detalleCobroEntityCollection = new DetalleCobroEntityCollection();
                DetalleCobroEntity detalleCobroEntity;
                
                while (reader.Read())
                {
                    detalleCobroEntity = new DetalleCobroEntity();
                    
					detalleCobroEntity.Id = Convert.ToInt32(reader["Id"]);
					detalleCobroEntity.IdCobroDebito = Convert.ToInt32(reader["IdCobroDebito"]);
					detalleCobroEntity.IdCuentaPorCobrar = Convert.ToInt32(reader["IdCuentaPorCobrar"]);
					if (reader["IdMedioPago"] != DBNull.Value)
					{
						detalleCobroEntity.IdMedioPago = Convert.ToInt32(reader["IdMedioPago"]);
					}
					detalleCobroEntity.Valor = (decimal) reader["Valor"];
					detalleCobroEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					detalleCobroEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					detalleCobroEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						detalleCobroEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						detalleCobroEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						detalleCobroEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					detalleCobroEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    detalleCobroEntity.SetLoadedState();
                    detalleCobroEntityCollection.Add(detalleCobroEntity);
                    
                }

                return detalleCobroEntityCollection;
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
     
    //    public static DetalleCobroEntityCollection FindByAll(DetalleCobroFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static DetalleCobroEntityCollection FindByAll(DetalleCobroFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "DetalleCobro_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "DetalleCobro_FindByAll";

    //            #region Parameters
    //            if (findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(!string.IsNullOrEmpty(findParameter.IdCobroDebito))
				//{
				//	mCommand.Parameters.AddWithValue("@IdCobroDebito", findParameter.IdCobroDebito);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdCobroDebito",DBNull.Value);
				//}

				//if(findParameter.IdCuentaPorCobrar != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar", findParameter.IdCuentaPorCobrar);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar",DBNull.Value);
				//}

				//if(findParameter.IdMedioPago != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdMedioPago", findParameter.IdMedioPago);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdMedioPago",DBNull.Value);
				//}

				//if(findParameter.Valor != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Valor", findParameter.Valor);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Valor",DBNull.Value);
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
    //            #endregion


    //            if (conexion.State != ConnectionState.Open) conexion.Open();
    //            reader = mCommand.ExecuteReader();

    //            DetalleCobroEntityCollection detalleCobroEntityCollection = new DetalleCobroEntityCollection();
    //            DetalleCobroEntity detalleCobroEntity;
                

    //            while (reader.Read())
    //            {
    //                detalleCobroEntity = new DetalleCobroEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		detalleCobroEntity.IdCobroDebitoAsCobroDebito = CobroDebitoDataAccess.ConvertToCobroDebitoEntity(reader, "IdCobroDebito");
				//		detalleCobroEntity.IdCuentaPorCobrarAsCuentaPorCobrar = CuentaPorCobrarDataAccess.ConvertToCuentaPorCobrarEntity(reader, "IdCuentaPorCobrar");
				//		detalleCobroEntity.IdMedioPagoAsMedioPago = MedioPagoDataAccess.ConvertToMedioPagoEntity(reader, "IdMedioPago");

    //                }
	   //             #endregion                    
				//	detalleCobroEntity.Id = Convert.ToInt32(reader["Id"]);
				//	detalleCobroEntity.IdCobroDebito = Convert.ToString(reader["IdCobroDebito"]);
				//	detalleCobroEntity.IdCuentaPorCobrar = Convert.ToInt32(reader["IdCuentaPorCobrar"]);
				//	if (reader["IdMedioPago"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.IdMedioPago = Convert.ToInt32(reader["IdMedioPago"]);
				//	}
				//	detalleCobroEntity.Valor = (decimal) reader["Valor"];
				//	detalleCobroEntity.Descripcion = Convert.ToString(reader["Descripcion"]);
				//	detalleCobroEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	detalleCobroEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	detalleCobroEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	if (reader["IdCierreCaja"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.IdCierreCaja = Convert.ToString(reader["IdCierreCaja"]).ToUpper();
				//	}
				//	detalleCobroEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                detalleCobroEntity.SetLoadedState();
    //                detalleCobroEntityCollection.Add(detalleCobroEntity);
                    
    //            }

    //            return detalleCobroEntityCollection;
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
        
    //    public static DetalleCobroEntityCollection FindByAllPaged(DetalleCobroFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static DetalleCobroEntityCollection FindByAllPaged(DetalleCobroFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "DetalleCobro_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "DetalleCobro_FindByAllPaged";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(!string.IsNullOrEmpty(findParameter.IdCobroDebito))
				//{
				//	mCommand.Parameters.AddWithValue("@IdCobroDebito", findParameter.IdCobroDebito);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdCobroDebito",DBNull.Value);
				//}

				//if(findParameter.IdCuentaPorCobrar != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar", findParameter.IdCuentaPorCobrar);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar",DBNull.Value);
				//}

				//if(findParameter.IdMedioPago != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdMedioPago", findParameter.IdMedioPago);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdMedioPago",DBNull.Value);
				//}

				//if(findParameter.Valor != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Valor", findParameter.Valor);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Valor",DBNull.Value);
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

    //            DetalleCobroEntityCollection detalleCobroEntityCollection = new DetalleCobroEntityCollection();
    //            DetalleCobroEntity detalleCobroEntity;
                

    //            while (reader.Read())
    //            {
    //                detalleCobroEntity = new DetalleCobroEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		detalleCobroEntity.IdCobroDebitoAsCobroDebito = CobroDebitoDataAccess.ConvertToCobroDebitoEntity(reader, "IdCobroDebito");
				//		detalleCobroEntity.IdCuentaPorCobrarAsCuentaPorCobrar = CuentaPorCobrarDataAccess.ConvertToCuentaPorCobrarEntity(reader, "IdCuentaPorCobrar");
				//		detalleCobroEntity.IdMedioPagoAsMedioPago = MedioPagoDataAccess.ConvertToMedioPagoEntity(reader, "IdMedioPago");

    //                }
	   //             #endregion                    
				//	detalleCobroEntity.Id = Convert.ToInt32(reader["Id"]);
				//	detalleCobroEntity.IdCobroDebito = Convert.ToString(reader["IdCobroDebito"]);
				//	detalleCobroEntity.IdCuentaPorCobrar = Convert.ToInt32(reader["IdCuentaPorCobrar"]);
				//	if (reader["IdMedioPago"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.IdMedioPago = Convert.ToInt32(reader["IdMedioPago"]);
				//	}
				//	detalleCobroEntity.Valor = (decimal) reader["Valor"];
				//	detalleCobroEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	detalleCobroEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	detalleCobroEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		detalleCobroEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	detalleCobroEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                detalleCobroEntity.SetLoadedState();
    //                detalleCobroEntityCollection.Add(detalleCobroEntity);
                    
    //            }

    //            return detalleCobroEntityCollection;
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

