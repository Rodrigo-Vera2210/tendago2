    
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
    public partial class StockDataAccessCollection
    {
 

  //      #region << Custom Stored Procedures >>
        
		//public static void CostoPromedioPonderado(string Id,string Tipo,int IdEmpresa,int IdProducto,int IdLocal,string IdSalidaEntrada,string IdDetalle,decimal Cantidad,decimal ValorUnitario,int IdTipoUnidad,short IdEstado, SqlConnection conexion, SqlTransaction transaction)
		//{
		//	SqlCommand mCommand = new SqlCommand();
		//	try
		//	{
		//		mCommand.Connection = conexion;
		//		mCommand.CommandType = CommandType.StoredProcedure;
		//		mCommand.Transaction = transaction;
		//		mCommand.CommandText = "Custom_Stock_CostoPromedioPonderado";

		//		mCommand.Parameters.AddWithValue("@Id", Id);
		//		mCommand.Parameters.AddWithValue("@Tipo", Tipo);
		//		mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
		//		mCommand.Parameters.AddWithValue("@IdProducto", IdProducto);
		//		mCommand.Parameters.AddWithValue("@IdLocal", IdLocal);
		//		mCommand.Parameters.AddWithValue("@IdSalidaEntrada", IdSalidaEntrada);
		//		mCommand.Parameters.AddWithValue("@IdDetalle", IdDetalle);
		//		mCommand.Parameters.AddWithValue("@Cantidad", Cantidad);
		//		mCommand.Parameters.AddWithValue("@ValorUnitario", ValorUnitario);
		//		mCommand.Parameters.AddWithValue("@IdTipoUnidad", IdTipoUnidad);
		//		mCommand.Parameters.AddWithValue("@IdEstado", IdEstado);
		//		if (conexion.State != ConnectionState.Open) conexion.Open();
		//		mCommand.ExecuteNonQuery();
		//	}
		//	catch (Exception exc)
		//	{
		//		throw exc;
		//	}
		//	finally
		//	{
		//		mCommand.Dispose();
		//	}
		//}

	

        
  //      #endregion
        
  //      /*public static StockEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
  //      {
  //          SqlCommand mCommand = new SqlCommand();
  //          SqlDataReader reader = null;
  //          try
  //          {
  //              mCommand.Connection = conexion;
  //              mCommand.CommandType = CommandType.StoredProcedure;
  //              mCommand.Transaction = transaction;
                
  //              mCommand.CommandText = "Stock_LoadAll";


  //              if (conexion.State != ConnectionState.Open) conexion.Open();
  //              reader = mCommand.ExecuteReader();

  //              StockEntityCollection stockEntityCollection = new StockEntityCollection();
  //              StockEntity stockEntity;
                
  //              while (reader.Read())
  //              {
  //                  stockEntity = new StockEntity();
                    
		//			stockEntity.Id = Convert.ToString(reader["Id"]);
		//			stockEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
		//			if (reader["Tipo"] != DBNull.Value)
		//			{
		//				stockEntity.Tipo = Convert.ToString(reader["Tipo"]).ToUpper();
		//			}
		//			if (reader["IdSalidaEntrada"] != DBNull.Value)
		//			{
		//				stockEntity.IdSalidaEntrada = Convert.ToString(reader["IdSalidaEntrada"]).ToUpper();
		//			}
		//			if (reader["IdDetalle"] != DBNull.Value)
		//			{
		//				stockEntity.IdDetalle = Convert.ToString(reader["IdDetalle"]).ToUpper();
		//			}
		//			stockEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
		//			stockEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
		//			stockEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
		//			stockEntity.Cantidad = (decimal) reader["Cantidad"];
		//			stockEntity.ValorUnitario = (decimal) reader["ValorUnitario"];
		//			if (reader["ValorTotal"] != DBNull.Value)
		//			{
		//				stockEntity.ValorTotal = (decimal) reader["ValorTotal"];
		//			}
		//			stockEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
		//			if (reader["CantidadTipoUnidad"] != DBNull.Value)
		//			{
		//				stockEntity.CantidadTipoUnidad = (decimal) reader["CantidadTipoUnidad"];
		//			}
		//			if (reader["StockInventario"] != DBNull.Value)
		//			{
		//				stockEntity.StockInventario = (decimal) reader["StockInventario"];
		//			}
		//			if (reader["CostoPromedioPonderado"] != DBNull.Value)
		//			{
		//				stockEntity.CostoPromedioPonderado = (decimal) reader["CostoPromedioPonderado"];
		//			}
		//			if (reader["SaldoInventario"] != DBNull.Value)
		//			{
		//				stockEntity.SaldoInventario = (decimal) reader["SaldoInventario"];
		//			}
		//			if (reader["IdEstado"] != DBNull.Value)
		//			{
		//				stockEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
		//			}

                    
  //                  stockEntity.SetLoadedState();
  //                  stockEntityCollection.Add(stockEntity);
                    
  //              }

  //              return stockEntityCollection;
  //          }
  //          catch (Exception exc)
  //          {
  //              throw exc;
  //          }
  //          finally
  //          {
  //              if (reader != null) reader.Close();
  //              mCommand.Dispose();
  //          }

  //      }
  //      */
     
  //      public static StockEntityCollection FindByAll(StockFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
  //      {
  //      	return FindByAll(findParameter,conexion,transaction,1);
  //      }
        
  //      public static StockEntityCollection FindByAll(StockFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
  //      {
  //          SqlCommand mCommand = new SqlCommand();
  //          SqlDataReader reader = null;
  //          try
  //          {
  //              mCommand.Connection = conexion;
  //              mCommand.CommandType = CommandType.StoredProcedure;
  //              mCommand.Transaction = transaction;
  //              if (deepLoadLevel >= 1)
		//     	{
  //              	mCommand.CommandText = "Stock_DeepFindByAll";
  //              }
  //              else mCommand.CommandText = "Stock_FindByAll";

                
		//		if(!String.IsNullOrEmpty(findParameter.Id))
		//		{
		//			mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
		//		}

		//		if(findParameter.IdEmpresa != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.Tipo))
		//		{
		//			mCommand.Parameters.AddWithValue("@Tipo", findParameter.Tipo );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Tipo",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.IdSalidaEntrada))
		//		{
		//			mCommand.Parameters.AddWithValue("@IdSalidaEntrada", findParameter.IdSalidaEntrada );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdSalidaEntrada",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.IdDetalle))
		//		{
		//			mCommand.Parameters.AddWithValue("@IdDetalle", findParameter.IdDetalle );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdDetalle",DBNull.Value);
		//		}

		//		if(findParameter.IdProducto != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
		//		}

		//		if(findParameter.IdLocal != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
		//		}

		//		if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
		//		}

		//		if(findParameter.Cantidad != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
		//		}

		//		if(findParameter.ValorUnitario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorUnitario", findParameter.ValorUnitario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorUnitario",DBNull.Value);
		//		}

		//		if(findParameter.ValorTotal != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorTotal", findParameter.ValorTotal);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorTotal",DBNull.Value);
		//		}

		//		if(findParameter.IdTipoUnidad != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
		//		}

		//		if(findParameter.CantidadTipoUnidad != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@CantidadTipoUnidad", findParameter.CantidadTipoUnidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@CantidadTipoUnidad",DBNull.Value);
		//		}

		//		if(findParameter.StockInventario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@StockInventario", findParameter.StockInventario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@StockInventario",DBNull.Value);
		//		}

		//		if(findParameter.CostoPromedioPonderado != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@CostoPromedioPonderado", findParameter.CostoPromedioPonderado);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@CostoPromedioPonderado",DBNull.Value);
		//		}

		//		if(findParameter.SaldoInventario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@SaldoInventario", findParameter.SaldoInventario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@SaldoInventario",DBNull.Value);
		//		}

		//		if(findParameter.IdEstado != short.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
		//		}

    
               	
  //              if (conexion.State != ConnectionState.Open) conexion.Open();
  //              reader = mCommand.ExecuteReader();

  //              StockEntityCollection stockEntityCollection = new StockEntityCollection();
  //              StockEntity stockEntity;
                

  //              while (reader.Read())
  //              {
  //                  stockEntity = new StockEntity();
		//			#region << Deep Load >>
  //                  if (deepLoadLevel == 1)
		//     		{

  //                  }
	 //               #endregion                    
		//			stockEntity.Id = Convert.ToString(reader["Id"]);
		//			stockEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
		//			if (reader["Tipo"] != DBNull.Value)
		//			{
		//				stockEntity.Tipo = Convert.ToString(reader["Tipo"]).ToUpper();
		//			}
		//			if (reader["IdSalidaEntrada"] != DBNull.Value)
		//			{
		//				stockEntity.IdSalidaEntrada = Convert.ToString(reader["IdSalidaEntrada"]).ToUpper();
		//			}
		//			if (reader["IdDetalle"] != DBNull.Value)
		//			{
		//				stockEntity.IdDetalle = Convert.ToString(reader["IdDetalle"]).ToUpper();
		//			}
		//			stockEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
		//			stockEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
		//			stockEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
		//			stockEntity.Cantidad = (decimal) reader["Cantidad"];
		//			stockEntity.ValorUnitario = (decimal) reader["ValorUnitario"];
		//			if (reader["ValorTotal"] != DBNull.Value)
		//			{
		//				stockEntity.ValorTotal = (decimal) reader["ValorTotal"];
		//			}
		//			stockEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
		//			if (reader["CantidadTipoUnidad"] != DBNull.Value)
		//			{
		//				stockEntity.CantidadTipoUnidad = (decimal) reader["CantidadTipoUnidad"];
		//			}
		//			if (reader["StockInventario"] != DBNull.Value)
		//			{
		//				stockEntity.StockInventario = (decimal) reader["StockInventario"];
		//			}
		//			if (reader["CostoPromedioPonderado"] != DBNull.Value)
		//			{
		//				stockEntity.CostoPromedioPonderado = (decimal) reader["CostoPromedioPonderado"];
		//			}
		//			if (reader["SaldoInventario"] != DBNull.Value)
		//			{
		//				stockEntity.SaldoInventario = (decimal) reader["SaldoInventario"];
		//			}
		//			if (reader["IdEstado"] != DBNull.Value)
		//			{
		//				stockEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
		//			}

                    
  //                  stockEntity.SetLoadedState();
  //                  stockEntityCollection.Add(stockEntity);
                    
  //              }

  //              return stockEntityCollection;
  //          }
  //          catch (Exception exc)
  //          {
  //              throw exc;
  //          }
  //          finally
  //          {
  //              if (reader != null) reader.Close();
  //              mCommand.Dispose();
  //          }

  //      }
        
  //      public static StockEntityCollection FindByAllPaged(StockFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
  //      {
  //      	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
  //      }
        
  //      public static StockEntityCollection FindByAllPaged(StockFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
  //      {
  //          SqlCommand mCommand = new SqlCommand();
  //          SqlDataReader reader = null;
  //          try
  //          {
  //              mCommand.Connection = conexion;
  //              mCommand.CommandType = CommandType.StoredProcedure;
  //              mCommand.Transaction = transaction;
  //              if (deepLoadLevel >= 1)
		//     	{
  //              	mCommand.CommandText = "Stock_DeepFindByAllPaged";
                	
  //              }
  //              else mCommand.CommandText = "Stock_FindByAllPaged";

                
		//		if(!String.IsNullOrEmpty(findParameter.Id))
		//		{
		//			mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
		//		}

		//		if(findParameter.IdEmpresa != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.Tipo))
		//		{
		//			mCommand.Parameters.AddWithValue("@Tipo", findParameter.Tipo );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Tipo",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.IdSalidaEntrada))
		//		{
		//			mCommand.Parameters.AddWithValue("@IdSalidaEntrada", findParameter.IdSalidaEntrada );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdSalidaEntrada",DBNull.Value);
		//		}

		//		if(!String.IsNullOrEmpty(findParameter.IdDetalle))
		//		{
		//			mCommand.Parameters.AddWithValue("@IdDetalle", findParameter.IdDetalle );
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdDetalle",DBNull.Value);
		//		}

		//		if(findParameter.IdProducto != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
		//		}

		//		if(findParameter.IdLocal != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
		//		}

		//		if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
		//		}

		//		if(findParameter.Cantidad != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
		//		}

		//		if(findParameter.ValorUnitario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorUnitario", findParameter.ValorUnitario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorUnitario",DBNull.Value);
		//		}

		//		if(findParameter.ValorTotal != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorTotal", findParameter.ValorTotal);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@ValorTotal",DBNull.Value);
		//		}

		//		if(findParameter.IdTipoUnidad != int.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
		//		}

		//		if(findParameter.CantidadTipoUnidad != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@CantidadTipoUnidad", findParameter.CantidadTipoUnidad);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@CantidadTipoUnidad",DBNull.Value);
		//		}

		//		if(findParameter.StockInventario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@StockInventario", findParameter.StockInventario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@StockInventario",DBNull.Value);
		//		}

		//		if(findParameter.CostoPromedioPonderado != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@CostoPromedioPonderado", findParameter.CostoPromedioPonderado);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@CostoPromedioPonderado",DBNull.Value);
		//		}

		//		if(findParameter.SaldoInventario != decimal.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@SaldoInventario", findParameter.SaldoInventario);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@SaldoInventario",DBNull.Value);
		//		}

		//		if(findParameter.IdEstado != short.MinValue)
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
		//		}
		//		else
		//		{
		//			mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
		//		}


		//		mCommand.Parameters.AddWithValue("@PageNumber",pageNumber);
		//		mCommand.Parameters.AddWithValue("@PageSize",pageSize);
		//		if (deepLoadLevel > 1)
		//     	{
		//			mCommand.Parameters.AddWithValue("@OrderBy",orderBy);
		//	    }
               	
  //              if (conexion.State != ConnectionState.Open) conexion.Open();
  //              reader = mCommand.ExecuteReader();

  //              StockEntityCollection stockEntityCollection = new StockEntityCollection();
  //              StockEntity stockEntity;
                

  //              while (reader.Read())
  //              {
  //                  stockEntity = new StockEntity();
		//			#region << Deep Load >>
  //                  if (deepLoadLevel > 1)
		//     		{

  //                  }
	 //               #endregion                    
		//			stockEntity.Id = Convert.ToString(reader["Id"]);
		//			stockEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
		//			if (reader["Tipo"] != DBNull.Value)
		//			{
		//				stockEntity.Tipo = Convert.ToString(reader["Tipo"]).ToUpper();
		//			}
		//			if (reader["IdSalidaEntrada"] != DBNull.Value)
		//			{
		//				stockEntity.IdSalidaEntrada = Convert.ToString(reader["IdSalidaEntrada"]).ToUpper();
		//			}
		//			if (reader["IdDetalle"] != DBNull.Value)
		//			{
		//				stockEntity.IdDetalle = Convert.ToString(reader["IdDetalle"]).ToUpper();
		//			}
		//			stockEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
		//			stockEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
		//			stockEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
		//			stockEntity.Cantidad = (decimal) reader["Cantidad"];
		//			stockEntity.ValorUnitario = (decimal) reader["ValorUnitario"];
		//			if (reader["ValorTotal"] != DBNull.Value)
		//			{
		//				stockEntity.ValorTotal = (decimal) reader["ValorTotal"];
		//			}
		//			stockEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
		//			if (reader["CantidadTipoUnidad"] != DBNull.Value)
		//			{
		//				stockEntity.CantidadTipoUnidad = (decimal) reader["CantidadTipoUnidad"];
		//			}
		//			if (reader["StockInventario"] != DBNull.Value)
		//			{
		//				stockEntity.StockInventario = (decimal) reader["StockInventario"];
		//			}
		//			if (reader["CostoPromedioPonderado"] != DBNull.Value)
		//			{
		//				stockEntity.CostoPromedioPonderado = (decimal) reader["CostoPromedioPonderado"];
		//			}
		//			if (reader["SaldoInventario"] != DBNull.Value)
		//			{
		//				stockEntity.SaldoInventario = (decimal) reader["SaldoInventario"];
		//			}
		//			if (reader["IdEstado"] != DBNull.Value)
		//			{
		//				stockEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
		//			}

                    
  //                  stockEntity.SetLoadedState();
  //                  stockEntityCollection.Add(stockEntity);
                    
  //              }

  //              return stockEntityCollection;
  //          }
  //          catch (Exception exc)
  //          {
  //              throw exc;
  //          }
  //          finally
  //          {
  //              if (reader != null) reader.Close();
  //              mCommand.Dispose();
  //          }

  //      }
        
          
    }
}

