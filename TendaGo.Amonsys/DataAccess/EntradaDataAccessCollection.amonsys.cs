    
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
    public partial class EntradaDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static EntradaEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "Entrada_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                EntradaEntityCollection entradaEntityCollection = new EntradaEntityCollection();
                EntradaEntity entradaEntity;
                
                while (reader.Read())
                {
                    entradaEntity = new EntradaEntity();
                    
					entradaEntity.Id = Convert.ToString(reader["Id"]);
					entradaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					entradaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
					entradaEntity.Periodo = Convert.ToString(reader["Periodo"]);
					entradaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
					entradaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
					entradaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
					if (reader["Subtotal0"] != DBNull.Value)
					{
						entradaEntity.Subtotal0 = (decimal) reader["Subtotal0"];
					}
					if (reader["Subtotal12"] != DBNull.Value)
					{
						entradaEntity.Subtotal12 = (decimal) reader["Subtotal12"];
					}
					entradaEntity.Total = (decimal) reader["Total"];
					entradaEntity.Saldo = (decimal) reader["Saldo"];
					if (reader["NumeroFacturaPedido"] != DBNull.Value)
					{
						entradaEntity.NumeroFacturaPedido = Convert.ToString(reader["NumeroFacturaPedido"]).ToUpper();
					}
					entradaEntity.IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]);
					entradaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					entradaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					entradaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						entradaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						entradaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						entradaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					entradaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    entradaEntity.SetLoadedState();
                    entradaEntityCollection.Add(entradaEntity);
                    
                }

                return entradaEntityCollection;
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
     
    //    public static EntradaEntityCollection FindByAll(EntradaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static EntradaEntityCollection FindByAll(EntradaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Entrada_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "Entrada_FindByAll";

                
				//if(!String.IsNullOrEmpty(findParameter.Id))
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
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

				//if(findParameter.IdLocal != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Periodo))
				//{
				//	mCommand.Parameters.AddWithValue("@Periodo", findParameter.Periodo );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Periodo",DBNull.Value);
				//}

				//if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.TipoTransaccion))
				//{
				//	mCommand.Parameters.AddWithValue("@TipoTransaccion", findParameter.TipoTransaccion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@TipoTransaccion",DBNull.Value);
				//}

				//if(findParameter.IdProveedor != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProveedor", findParameter.IdProveedor);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProveedor",DBNull.Value);
				//}

				//if(findParameter.Subtotal0 != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal0", findParameter.Subtotal0);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal0",DBNull.Value);
				//}

				//if(findParameter.Subtotal12 != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal12", findParameter.Subtotal12);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal12",DBNull.Value);
				//}

				//if(findParameter.Total != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Total", findParameter.Total);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Total",DBNull.Value);
				//}

				//if(findParameter.Saldo != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Saldo", findParameter.Saldo);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Saldo",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.NumeroFacturaPedido))
				//{
				//	mCommand.Parameters.AddWithValue("@NumeroFacturaPedido", findParameter.NumeroFacturaPedido );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@NumeroFacturaPedido",DBNull.Value);
				//}
    //            if (!String.IsNullOrEmpty(findParameter.TransaccionPadre))
    //            {
    //                mCommand.Parameters.AddWithValue("@TransaccionPadre", findParameter.TransaccionPadre.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@TransaccionPadre", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.TipoTransaccionPadre))
    //            {
    //                mCommand.Parameters.AddWithValue("@TipoTransaccionPadre", findParameter.TipoTransaccionPadre.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@TipoTransaccionPadre", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.EstadoActual))
    //            {
    //                mCommand.Parameters.AddWithValue("@EstadoActual", findParameter.EstadoActual.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@EstadoActual", DBNull.Value);
    //            }
    //            if (findParameter.FechaHoraEntrega != null && findParameter.FechaHoraEntrega != DateTime.MinValue)
    //            {
    //                mCommand.Parameters.AddWithValue("@FechaHoraEntrega", findParameter.FechaHoraEntrega);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@FechaHoraEntrega", DBNull.Value);
    //            }
    //            if (findParameter.ValorAdicional != decimal.MinValue)
    //            {
    //                mCommand.Parameters.AddWithValue("@ValorAdicional", findParameter.ValorAdicional);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@ValorAdicional", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.Ruc))
    //            {
    //                mCommand.Parameters.AddWithValue("@Ruc", findParameter.Ruc);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@Ruc", DBNull.Value);
    //            }

    //            if (findParameter.IdFormaPago != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdFormaPago", findParameter.IdFormaPago);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdFormaPago",DBNull.Value);
				//}

    //            if (findParameter.Tasa != decimal.MinValue)
    //            {
    //                mCommand.Parameters.AddWithValue("@Tasa", findParameter.Tasa);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@Tasa", DBNull.Value);
    //            }

    //            if (findParameter.IdMonedaOrigen != short.MinValue)
    //            {
    //                mCommand.Parameters.AddWithValue("@IdMonedaOrigen", findParameter.IdMonedaOrigen);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@IdMonedaOrigen", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.IpIngreso))
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

    //            EntradaEntityCollection entradaEntityCollection = new EntradaEntityCollection();
    //            EntradaEntity entradaEntity;
                

    //            while (reader.Read())
    //            {
    //                entradaEntity = new EntradaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		entradaEntity.IdLocalAsLocalBodega = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");
				//		entradaEntity.IdProveedorAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdProveedor");

    //                }
	   //             #endregion                    
				//	entradaEntity.Id = Convert.ToString(reader["Id"]);
				//	entradaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	entradaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
				//	entradaEntity.Periodo = Convert.ToString(reader["Periodo"]);
				//	entradaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
				//	entradaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
				//	entradaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
				//	if (reader["Subtotal0"] != DBNull.Value)
				//	{
				//		entradaEntity.Subtotal0 = (decimal) reader["Subtotal0"];
				//	}
				//	if (reader["Subtotal12"] != DBNull.Value)
				//	{
				//		entradaEntity.Subtotal12 = (decimal) reader["Subtotal12"];
				//	}
				//	entradaEntity.Total = (decimal) reader["Total"];
				//	entradaEntity.Saldo = (decimal) reader["Saldo"];
				//	if (reader["NumeroFacturaPedido"] != DBNull.Value)
				//	{
				//		entradaEntity.NumeroFacturaPedido = Convert.ToString(reader["NumeroFacturaPedido"]).ToUpper();
				//	}
    //                if (reader["TransaccionPadre"] != DBNull.Value)
    //                {
    //                    entradaEntity.TransaccionPadre = Convert.ToString(reader["TransaccionPadre"]).ToUpper();
    //                }
    //                if (reader["TipoTransaccionPadre"] != DBNull.Value)
    //                {
    //                    entradaEntity.TipoTransaccionPadre = Convert.ToString(reader["TipoTransaccionPadre"]).ToUpper();
    //                }
    //                if (reader["EstadoActual"] != DBNull.Value)
    //                {
    //                    entradaEntity.EstadoActual = Convert.ToString(reader["EstadoActual"]).ToUpper();
    //                }
    //                if (reader["FechaHoraEntrega"] != DBNull.Value)
    //                {
    //                    entradaEntity.FechaHoraEntrega = Convert.ToDateTime(reader["FechaHoraEntrega"]);
    //                }
    //                if (reader["ValorAdicional"] != DBNull.Value)
    //                {
    //                    entradaEntity.ValorAdicional = (decimal)(reader["ValorAdicional"]);
    //                }
    //                if (reader["Ruc"] != DBNull.Value)
    //                {
    //                    entradaEntity.Ruc = Convert.ToString(reader["Ruc"]).ToUpper();
    //                }
    //                entradaEntity.IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]);
    //                if (reader["Tasa"] != DBNull.Value)
    //                {
    //                    entradaEntity.Tasa = (decimal)reader["Tasa"];
    //                }
    //                if (reader["IdMonedaOrigen"] != DBNull.Value)
    //                {
    //                    entradaEntity.IdMonedaOrigen = Convert.ToInt16(reader["IdMonedaOrigen"]);
    //                }
    //                entradaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	entradaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	entradaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	entradaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                entradaEntity.SetLoadedState();
    //                entradaEntityCollection.Add(entradaEntity);
                    
    //            }

    //            return entradaEntityCollection;
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
        
    //    public static EntradaEntityCollection FindByAllPaged(EntradaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static EntradaEntityCollection FindByAllPaged(EntradaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Entrada_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "Entrada_FindByAllPaged";

                
				//if(!String.IsNullOrEmpty(findParameter.Id))
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
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

				//if(findParameter.IdLocal != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.Periodo))
				//{
				//	mCommand.Parameters.AddWithValue("@Periodo", findParameter.Periodo );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Periodo",DBNull.Value);
				//}

				//if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.TipoTransaccion))
				//{
				//	mCommand.Parameters.AddWithValue("@TipoTransaccion", findParameter.TipoTransaccion );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@TipoTransaccion",DBNull.Value);
				//}

				//if(findParameter.IdProveedor != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProveedor", findParameter.IdProveedor);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProveedor",DBNull.Value);
				//}

				//if(findParameter.Subtotal0 != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal0", findParameter.Subtotal0);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal0",DBNull.Value);
				//}

				//if(findParameter.Subtotal12 != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal12", findParameter.Subtotal12);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Subtotal12",DBNull.Value);
				//}

				//if(findParameter.Total != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Total", findParameter.Total);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Total",DBNull.Value);
				//}

				//if(findParameter.Saldo != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Saldo", findParameter.Saldo);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Saldo",DBNull.Value);
				//}

				//if(!String.IsNullOrEmpty(findParameter.NumeroFacturaPedido))
				//{
				//	mCommand.Parameters.AddWithValue("@NumeroFacturaPedido", findParameter.NumeroFacturaPedido );
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@NumeroFacturaPedido",DBNull.Value);
				//}
    //            if (!String.IsNullOrEmpty(findParameter.TransaccionPadre))
    //            {
    //                mCommand.Parameters.AddWithValue("@TransaccionPadre", findParameter.TransaccionPadre.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@TransaccionPadre", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.TipoTransaccionPadre))
    //            {
    //                mCommand.Parameters.AddWithValue("@TipoTransaccionPadre", findParameter.TipoTransaccionPadre.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@TipoTransaccionPadre", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.EstadoActual))
    //            {
    //                mCommand.Parameters.AddWithValue("@EstadoActual", findParameter.EstadoActual.ToUpper());
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@EstadoActual", DBNull.Value);
    //            }
    //            if (findParameter.FechaHoraEntrega != null && findParameter.FechaHoraEntrega != DateTime.MinValue)
    //            {
    //                mCommand.Parameters.AddWithValue("@FechaHoraEntrega", findParameter.FechaHoraEntrega);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@FechaHoraEntrega", DBNull.Value);
    //            }
    //            if (findParameter.ValorAdicional != 0)
    //            {
    //                mCommand.Parameters.AddWithValue("@ValorAdicional", findParameter.ValorAdicional);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@ValorAdicional", DBNull.Value);
    //            }
    //            if (!String.IsNullOrEmpty(findParameter.Ruc))
    //            {
    //                mCommand.Parameters.AddWithValue("@Ruc", findParameter.Ruc);
    //            }
    //            else
    //            {
    //                mCommand.Parameters.AddWithValue("@Ruc", DBNull.Value);
    //            }

    //            if (findParameter.IdFormaPago != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdFormaPago", findParameter.IdFormaPago);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdFormaPago",DBNull.Value);
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

    //            EntradaEntityCollection entradaEntityCollection = new EntradaEntityCollection();
    //            EntradaEntity entradaEntity;
                

    //            while (reader.Read())
    //            {
    //                entradaEntity = new EntradaEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		entradaEntity.IdLocalAsLocalBodega = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");
				//		entradaEntity.IdProveedorAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdProveedor");

    //                }
	   //             #endregion                    
				//	entradaEntity.Id = Convert.ToString(reader["Id"]);
				//	entradaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
				//	entradaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
				//	entradaEntity.Periodo = Convert.ToString(reader["Periodo"]);
				//	entradaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
				//	entradaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
				//	entradaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
				//	if (reader["Subtotal0"] != DBNull.Value)
				//	{
				//		entradaEntity.Subtotal0 = (decimal) reader["Subtotal0"];
				//	}
				//	if (reader["Subtotal12"] != DBNull.Value)
				//	{
				//		entradaEntity.Subtotal12 = (decimal) reader["Subtotal12"];
				//	}
    //                if (reader["TransaccionPadre"] != DBNull.Value)
    //                {
    //                    entradaEntity.TransaccionPadre = Convert.ToString(reader["TransaccionPadre"]).ToUpper();
    //                }
    //                if (reader["TipoTransaccionPadre"] != DBNull.Value)
    //                {
    //                    entradaEntity.TipoTransaccionPadre = Convert.ToString(reader["TipoTransaccionPadre"]).ToUpper();
    //                }
    //                if (reader["EstadoActual"] != DBNull.Value)
    //                {
    //                    entradaEntity.EstadoActual = Convert.ToString(reader["EstadoActual"]).ToUpper();
    //                }
    //                if (reader["FechaHoraEntrega"] != DBNull.Value)
    //                {
    //                    entradaEntity.FechaHoraEntrega = Convert.ToDateTime(reader["FechaHoraEntrega"]);
    //                }
    //                if (reader["ValorAdicional"] != DBNull.Value)
    //                {
    //                    entradaEntity.ValorAdicional = (decimal)(reader["ValorAdicional"]);
    //                }
    //                entradaEntity.Total = (decimal) reader["Total"];
				//	entradaEntity.Saldo = (decimal) reader["Saldo"];
				//	if (reader["NumeroFacturaPedido"] != DBNull.Value)
				//	{
				//		entradaEntity.NumeroFacturaPedido = Convert.ToString(reader["NumeroFacturaPedido"]).ToUpper();
				//	}
    //                if (reader["Ruc"] != DBNull.Value)
    //                {
    //                    entradaEntity.Ruc = Convert.ToString(reader["Ruc"]).ToUpper();
    //                }
    //                entradaEntity.IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]);
    //                if (reader["Tasa"] != DBNull.Value)
    //                {
    //                    entradaEntity.Tasa = (decimal)reader["Tasa"];
    //                }
    //                if (reader["IdMonedaOrigen"] != DBNull.Value)
    //                {
    //                    entradaEntity.IdMonedaOrigen = Convert.ToInt16(reader["IdMonedaOrigen"]);
    //                }
    //                entradaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	entradaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	entradaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		entradaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	entradaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                entradaEntity.SetLoadedState();
    //                entradaEntityCollection.Add(entradaEntity);
                    
    //            }

    //            return entradaEntityCollection;
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

