    
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
    public partial class TipoUnidadDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static TipoUnidadEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "TipoUnidad_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                TipoUnidadEntityCollection tipoUnidadEntityCollection = new TipoUnidadEntityCollection();
                TipoUnidadEntity tipoUnidadEntity;
                
                while (reader.Read())
                {
                    tipoUnidadEntity = new TipoUnidadEntity();
                    
					tipoUnidadEntity.Id = Convert.ToInt32(reader["Id"]);
					tipoUnidadEntity.IdEmpresa = Convert.ToInt16(reader["IdEmpresa"]);
					tipoUnidadEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					tipoUnidadEntity.TipoUnidad = Convert.ToString(reader["TipoUnidad"]);
					tipoUnidadEntity.Cantidad = (decimal) reader["Cantidad"];
					tipoUnidadEntity.UnidadMedidad = Convert.ToInt32(reader["UnidadMedidad"]);
					if (reader["Precio"] != DBNull.Value)
					{
						tipoUnidadEntity.Precio = (decimal) reader["Precio"];
					}
					if (reader["IncluyeIva"] != DBNull.Value)
					{
						tipoUnidadEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
					}
					tipoUnidadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					tipoUnidadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					tipoUnidadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					tipoUnidadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    tipoUnidadEntity.SetLoadedState();
                    tipoUnidadEntityCollection.Add(tipoUnidadEntity);
                    
                }

                return tipoUnidadEntityCollection;
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
     
        public static TipoUnidadEntityCollection FindByAll(TipoUnidadFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
        {
        	return FindByAll(findParameter,conexion,transaction,1);
        }
        
        public static TipoUnidadEntityCollection FindByAll(TipoUnidadFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                if (deepLoadLevel >= 1)
		     	{
                	mCommand.CommandText = "TipoUnidad_DeepFindByAll";
                }
                else mCommand.CommandText = "TipoUnidad_FindByAll";

                #region Parametros
                if (findParameter.Id != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				}

				if(findParameter.IdEmpresa != short.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				}

				if(findParameter.IdProducto != null)
				{
					mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProducto", DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.TipoUnidad))
				{
					mCommand.Parameters.AddWithValue("@TipoUnidad", findParameter.TipoUnidad );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@TipoUnidad",DBNull.Value);
				}

				if(findParameter.Cantidad != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
				}

                if (findParameter.CantidadMinima != decimal.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@CantidadMinima", findParameter.CantidadMinima);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@CantidadMinima", DBNull.Value);
                }

                if (findParameter.UnidadMedidad != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@UnidadMedidad", findParameter.UnidadMedidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UnidadMedidad",DBNull.Value);
				}

				if(findParameter.Precio != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				}

				if(findParameter.IncluyeIva != -1)
				{
					if (findParameter.IncluyeIva == 1)
					{
						mCommand.Parameters.AddWithValue("@IncluyeIva", true);
					}
					else
					{
						mCommand.Parameters.AddWithValue("@IncluyeIva", false);
					}
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IncluyeIva",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IpIngreso))
				{
					mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpIngreso",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
				{
					mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioIngreso",DBNull.Value);
				}

				if(findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaIngreso",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				}

				if(findParameter.IdEstado != short.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
				}
                #endregion


                if (conexion.State != ConnectionState.Open) conexion.Open();
				//// ver parametros ver sql query sql
				string sql = mCommand.CommandText;

				foreach (SqlParameter parameter in mCommand.Parameters)
				{
					string valueString = (parameter.Value != null && parameter.Value != DBNull.Value) ? $"'{parameter.Value}'" : "NULL";
					sql += $" {valueString},";
				}

				// Eliminamos la coma final
				sql = sql.TrimEnd(',');
				reader = mCommand.ExecuteReader();

                TipoUnidadEntityCollection tipoUnidadEntityCollection = new TipoUnidadEntityCollection();
                TipoUnidadEntity tipoUnidadEntity;
				 				
				while (reader.Read())
                {
                    tipoUnidadEntity = new TipoUnidadEntity();


					#region << Deep Load >>
                    if (deepLoadLevel == 1)
		     		{
						tipoUnidadEntity.IdProductoAsProducto = ProductoDataAccess.ConvertToProductoEntity(reader, "IdProducto");
						tipoUnidadEntity.UnidadMedidadAsUnidadMedida = UnidadMedidaDataAccess.ConvertToUnidadMedidaEntity(reader, "UnidadMedidad");

                    }
	                #endregion                    
					tipoUnidadEntity.Id = Convert.ToInt32(reader["Id"]);
					tipoUnidadEntity.IdEmpresa = Convert.ToInt16(reader["IdEmpresa"]);
					tipoUnidadEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					tipoUnidadEntity.TipoUnidad = Convert.ToString(reader["TipoUnidad"]);
					tipoUnidadEntity.Cantidad = (decimal) reader["Cantidad"];
                    if (reader["CantidadMinima"] != DBNull.Value)
                    {
                        tipoUnidadEntity.CantidadMinima = (decimal)reader["CantidadMinima"];
                    }
                    tipoUnidadEntity.UnidadMedidad = Convert.ToInt32(reader["UnidadMedidad"]);
					if (reader["Precio"] != DBNull.Value)
					{
						tipoUnidadEntity.Precio = (decimal) reader["Precio"];
					}
					if (reader["IncluyeIva"] != DBNull.Value)
					{
						tipoUnidadEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
					}
					tipoUnidadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					tipoUnidadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					tipoUnidadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					tipoUnidadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
					
					//if (reader["Descuento"] != DBNull.Value)
					//{
					//	tipoUnidadEntity.Descuento = (decimal)reader["Descuento"];
					//}
					//if (reader["PrecioSinIva"] != DBNull.Value)
					//{
					//	tipoUnidadEntity.PrecioSinIva = (decimal)reader["PrecioSinIva"];
					//}
					
					tipoUnidadEntity.SetLoadedState();
                    tipoUnidadEntityCollection.Add(tipoUnidadEntity);
                    
                }

                return tipoUnidadEntityCollection;
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
        
        public static TipoUnidadEntityCollection FindByAllPaged(TipoUnidadFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
        {
        	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
        }
        
        public static TipoUnidadEntityCollection FindByAllPaged(TipoUnidadFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                if (deepLoadLevel >= 1)
		     	{
                	mCommand.CommandText = "TipoUnidad_DeepFindByAllPaged";
                	
                }
                else mCommand.CommandText = "TipoUnidad_FindByAllPaged";

                
				if(findParameter.Id != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				}

				if(findParameter.IdEmpresa != short.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				}

				if(findParameter.IdProducto != null)
				{
					mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.TipoUnidad))
				{
					mCommand.Parameters.AddWithValue("@TipoUnidad", findParameter.TipoUnidad );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@TipoUnidad",DBNull.Value);
				}

				if(findParameter.Cantidad != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
				}

                if (findParameter.CantidadMinima != decimal.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@CantidadMinima", findParameter.CantidadMinima);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@CantidadMinima", DBNull.Value);
                }


                if (findParameter.UnidadMedidad != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@UnidadMedidad", findParameter.UnidadMedidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UnidadMedidad",DBNull.Value);
				}

				if(findParameter.Precio != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				}

				if(findParameter.IncluyeIva != -1)
				{
					if (findParameter.IncluyeIva == 1)
					{
						mCommand.Parameters.AddWithValue("@IncluyeIva", true);
					}
					else
					{
						mCommand.Parameters.AddWithValue("@IncluyeIva", false);
					}
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IncluyeIva",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IpIngreso))
				{
					mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpIngreso",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
				{
					mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioIngreso",DBNull.Value);
				}

				if(findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaIngreso",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				}

				if(findParameter.IdEstado != short.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEstado",DBNull.Value);
				}


				mCommand.Parameters.AddWithValue("@PageNumber",pageNumber);
				mCommand.Parameters.AddWithValue("@PageSize",pageSize);
				if (deepLoadLevel > 1)
		     	{
					mCommand.Parameters.AddWithValue("@OrderBy",orderBy);
			    }
               	
                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                TipoUnidadEntityCollection tipoUnidadEntityCollection = new TipoUnidadEntityCollection();
                TipoUnidadEntity tipoUnidadEntity;
                

                while (reader.Read())
                {
                    tipoUnidadEntity = new TipoUnidadEntity();
					#region << Deep Load >>
                    if (deepLoadLevel > 1)
		     		{
						tipoUnidadEntity.IdProductoAsProducto = ProductoDataAccess.ConvertToProductoEntity(reader, "IdProducto");
						tipoUnidadEntity.UnidadMedidadAsUnidadMedida = UnidadMedidaDataAccess.ConvertToUnidadMedidaEntity(reader, "UnidadMedidad");

                    }
	                #endregion                    
					tipoUnidadEntity.Id = Convert.ToInt32(reader["Id"]);
					tipoUnidadEntity.IdEmpresa = Convert.ToInt16(reader["IdEmpresa"]);
					tipoUnidadEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					tipoUnidadEntity.TipoUnidad = Convert.ToString(reader["TipoUnidad"]);
					tipoUnidadEntity.Cantidad = (decimal) reader["Cantidad"];
					tipoUnidadEntity.UnidadMedidad = Convert.ToInt32(reader["UnidadMedidad"]);
                    if (reader["CantidadMinima"] != DBNull.Value)
                    {
                        tipoUnidadEntity.CantidadMinima = (decimal)reader["CantidadMinima"];
                    }
                    if (reader["Precio"] != DBNull.Value)
					{
						tipoUnidadEntity.Precio = (decimal) reader["Precio"];
					}
					if (reader["IncluyeIva"] != DBNull.Value)
					{
						tipoUnidadEntity.IncluyeIva = Convert.ToBoolean(reader["IncluyeIva"]);
					}
					tipoUnidadEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					tipoUnidadEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					tipoUnidadEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						tipoUnidadEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					tipoUnidadEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    
                    tipoUnidadEntity.SetLoadedState();
                    tipoUnidadEntityCollection.Add(tipoUnidadEntity);
                    
                }

                return tipoUnidadEntityCollection;
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
        
          
    }
}

