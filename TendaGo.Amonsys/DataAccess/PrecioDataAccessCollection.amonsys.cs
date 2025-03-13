    
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
    public partial class PrecioDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static PrecioEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "Precio_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                PrecioEntityCollection precioEntityCollection = new PrecioEntityCollection();
                PrecioEntity precioEntity;
                
                while (reader.Read())
                {
                    precioEntity = new PrecioEntity();
                    
					precioEntity.Id = Convert.ToInt32(reader["Id"]);
					precioEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					precioEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
					precioEntity.Precio = (decimal) reader["Precio"];
					precioEntity.Moneda = Convert.ToInt32(reader["Moneda"]);
					precioEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
					precioEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					precioEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					precioEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						precioEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						precioEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						precioEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					precioEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    precioEntity.SetLoadedState();
                    precioEntityCollection.Add(precioEntity);
                    
                }

                return precioEntityCollection;
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
     
    //    public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
    //    {
    //    	return FindByAll(findParameter,conexion,transaction,1);
    //    }
        
    //    public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Precio_DeepFindByAll";
    //            }
    //            else mCommand.CommandText = "Precio_FindByAll";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdProducto != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
				//}

				//if(findParameter.IdTipoUnidad != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
				//}

				//if(findParameter.Precio != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				//}

				//if(findParameter.Moneda != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Moneda", findParameter.Moneda);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Moneda",DBNull.Value);
				//}

				//if(findParameter.IncluyeIva != -1)
				//{
				//	if (findParameter.IncluyeIva == 1)
				//	{
				//		mCommand.Parameters.AddWithValue("@IncluyeIva", true);
				//	}
				//	else
				//	{
				//		mCommand.Parameters.AddWithValue("@IncluyeIva", false);
				//	}
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IncluyeIva",DBNull.Value);
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

    //            PrecioEntityCollection precioEntityCollection = new PrecioEntityCollection();
    //            PrecioEntity precioEntity;
                

    //            while (reader.Read())
    //            {
    //                precioEntity = new PrecioEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel == 1)
		  //   		{
				//		precioEntity.IdProductoAsProducto = ProductoDataAccess.ConvertToProductoEntity(reader, "IdProducto");
				//		precioEntity.IdTipoUnidadAsTipoUnidad = TipoUnidadDataAccess.ConvertToTipoUnidadEntity(reader, "IdTipoUnidad");

    //                }
	   //             #endregion                    
				//	precioEntity.Id = Convert.ToInt32(reader["Id"]);
				//	precioEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
				//	precioEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
				//	precioEntity.Precio = reader["Precio"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Precio"]);
				//	precioEntity.Moneda = Convert.ToInt32(reader["Moneda"]);
				//	precioEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
				//	precioEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	precioEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	precioEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	precioEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                precioEntity.SetLoadedState();
    //                precioEntityCollection.Add(precioEntity);
                    
    //            }

    //            return precioEntityCollection;
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
        
    //    public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
    //    {
    //    	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
    //    }
        
    //    public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
    //            	mCommand.CommandText = "Precio_DeepFindByAllPaged";
                	
    //            }
    //            else mCommand.CommandText = "Precio_FindByAllPaged";

                
				//if(findParameter.Id != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				//}

				//if(findParameter.IdProducto != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
				//}

				//if(findParameter.IdTipoUnidad != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
				//}

				//if(findParameter.Precio != decimal.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				//}

				//if(findParameter.Moneda != int.MinValue)
				//{
				//	mCommand.Parameters.AddWithValue("@Moneda", findParameter.Moneda);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@Moneda",DBNull.Value);
				//}

				//if(findParameter.IncluyeIva != -1)
				//{
				//	if (findParameter.IncluyeIva == 1)
				//	{
				//		mCommand.Parameters.AddWithValue("@IncluyeIva", true);
				//	}
				//	else
				//	{
				//		mCommand.Parameters.AddWithValue("@IncluyeIva", false);
				//	}
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@IncluyeIva",DBNull.Value);
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

    //            PrecioEntityCollection precioEntityCollection = new PrecioEntityCollection();
    //            PrecioEntity precioEntity;
                

    //            while (reader.Read())
    //            {
    //                precioEntity = new PrecioEntity();
				//	#region << Deep Load >>
    //                if (deepLoadLevel > 1)
		  //   		{
				//		precioEntity.IdProductoAsProducto = ProductoDataAccess.ConvertToProductoEntity(reader, "IdProducto");
				//		precioEntity.IdTipoUnidadAsTipoUnidad = TipoUnidadDataAccess.ConvertToTipoUnidadEntity(reader, "IdTipoUnidad");

    //                }
	   //             #endregion                    
				//	precioEntity.Id = Convert.ToInt32(reader["Id"]);
				//	precioEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
				//	precioEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
				//	precioEntity.Precio = (decimal) reader["Precio"];
				//	precioEntity.Moneda = Convert.ToInt32(reader["Moneda"]);
				//	precioEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
				//	precioEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
				//	precioEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
				//	precioEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
				//	if (reader["IpModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
				//	}
				//	if (reader["UsuarioModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
				//	}
				//	if (reader["FechaModificacion"] != DBNull.Value)
				//	{
				//		precioEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
				//	}
				//	precioEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
    //                precioEntity.SetLoadedState();
    //                precioEntityCollection.Add(precioEntity);
                    
    //            }

    //            return precioEntityCollection;
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

