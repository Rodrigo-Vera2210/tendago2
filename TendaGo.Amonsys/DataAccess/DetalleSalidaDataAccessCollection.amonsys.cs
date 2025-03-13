    
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
using System.Linq;

namespace ER.DA
{
    public partial class DetalleSalidaDataAccessCollection
    {
 

        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        /*public static DetalleSalidaEntityCollection LoadAll(SqlConnection conexion, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = conexion;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                
                mCommand.CommandText = "DetalleSalida_LoadAll";


                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                DetalleSalidaEntityCollection detalleSalidaEntityCollection = new DetalleSalidaEntityCollection();
                DetalleSalidaEntity detalleSalidaEntity;
                
                while (reader.Read())
                {
                    detalleSalidaEntity = new DetalleSalidaEntity();
                    
					detalleSalidaEntity.Id = Convert.ToString(reader["Id"]);
					detalleSalidaEntity.Periodo = Convert.ToString(reader["Periodo"]);
					detalleSalidaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
					detalleSalidaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
					detalleSalidaEntity.IdSalida = Convert.ToString(reader["IdSalida"]);
					detalleSalidaEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					detalleSalidaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
					detalleSalidaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
					if (reader["CostoUnitarioImportacion"] != DBNull.Value)
					{
						detalleSalidaEntity.CostoUnitarioImportacion = (decimal) reader["CostoUnitarioImportacion"];
					}
					detalleSalidaEntity.Cantidad = (decimal) reader["Cantidad"];
					detalleSalidaEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
					detalleSalidaEntity.CostoVenta = (decimal) reader["CostoVenta"];
					detalleSalidaEntity.Precio = (decimal) reader["Precio"];
					if (reader["Descuento"] != DBNull.Value)
					{
						detalleSalidaEntity.Descuento = (decimal) reader["Descuento"];
					}
					if (reader["Subtotal"] != DBNull.Value)
					{
						detalleSalidaEntity.Subtotal = (decimal) reader["Subtotal"];
					}
					if (reader["FechaFabricacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaFabricacion = Convert.ToDateTime(reader["FechaFabricacion"]);
					}
					if (reader["FechaExpiracion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"]);
					}
					if (reader["Lote"] != DBNull.Value)
					{
						detalleSalidaEntity.Lote = Convert.ToString(reader["Lote"]).ToUpper();
					}
					detalleSalidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					detalleSalidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					detalleSalidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					detalleSalidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
					if (reader["IdEmpresa"] != DBNull.Value)
					{
						detalleSalidaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					}

                    
                    detalleSalidaEntity.SetLoadedState();
                    detalleSalidaEntityCollection.Add(detalleSalidaEntity);
                    
                }

                return detalleSalidaEntityCollection;
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
     
        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction)
        {
        	return FindByAll(findParameter,conexion,transaction,1);
        }
        
        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter , SqlConnection conexion, SqlTransaction  transaction, int deepLoadLevel)
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
                	mCommand.CommandText = "DetalleSalida_DeepFindByAll";
                }
                else mCommand.CommandText = "DetalleSalida_FindByAll";

                
				if(!String.IsNullOrEmpty(findParameter.Id))
				{
					mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.Periodo))
				{
					mCommand.Parameters.AddWithValue("@Periodo", findParameter.Periodo );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Periodo",DBNull.Value);
				}

				if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.TipoTransaccion))
				{
					mCommand.Parameters.AddWithValue("@TipoTransaccion", findParameter.TipoTransaccion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@TipoTransaccion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IdSalida))
				{
					mCommand.Parameters.AddWithValue("@IdSalida", findParameter.IdSalida );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdSalida",DBNull.Value);
				}

				if(findParameter.IdProducto != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
				}

				if(findParameter.IdProveedor != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdProveedor", findParameter.IdProveedor);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProveedor",DBNull.Value);
				}

				if(findParameter.IdLocal != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
				}

				if(findParameter.CostoUnitarioImportacion != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@CostoUnitarioImportacion", findParameter.CostoUnitarioImportacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@CostoUnitarioImportacion",DBNull.Value);
				}

				if(findParameter.Cantidad != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
				}

				if(findParameter.IdTipoUnidad != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
				}

				if(findParameter.CostoVenta != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@CostoVenta", findParameter.CostoVenta);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@CostoVenta",DBNull.Value);
				}

				if(findParameter.Precio != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				}

				if(findParameter.Descuento != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Descuento", findParameter.Descuento);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Descuento",DBNull.Value);
				}

				if(findParameter.Subtotal != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Subtotal", findParameter.Subtotal);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Subtotal",DBNull.Value);
				}

				if(findParameter.FechaFabricacion != null && findParameter.FechaFabricacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaFabricacion", findParameter.FechaFabricacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaFabricacion",DBNull.Value);
				}

				if(findParameter.FechaExpiracion != null && findParameter.FechaExpiracion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaExpiracion", findParameter.FechaExpiracion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaExpiracion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.Lote))
				{
					mCommand.Parameters.AddWithValue("@Lote", findParameter.Lote );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Lote",DBNull.Value);
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

				if(findParameter.IdEmpresa != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				}

                if (conexion.State != ConnectionState.Open) conexion.Open();
//query que se manda a la base
				//var z= mCommand.CommandText + " " + string.Join(", ", mCommand.Parameters.Cast<SqlParameter>().Select(p =>
    //            {
    //                string valor = (p.Value == DBNull.Value) ? "NULL" : $"'{p.Value}'";
    //                return $"{p.ParameterName} = {valor}";
    //            }));

                reader = mCommand.ExecuteReader();

                DetalleSalidaEntityCollection detalleSalidaEntityCollection = new DetalleSalidaEntityCollection();
                DetalleSalidaEntity detalleSalidaEntity;
                

                while (reader.Read())
                {
                    detalleSalidaEntity = new DetalleSalidaEntity();
					#region << Deep Load >>
                    if (deepLoadLevel == 1)
		     		{
						detalleSalidaEntity.IdSalidaAsSalida = SalidaDataAccess.ConvertToSalidaEntity(reader, "IdSalida");
						detalleSalidaEntity.IdProveedorAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdProveedor");
						detalleSalidaEntity.IdLocalAsLocalBodega = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");
						detalleSalidaEntity.IdTipoUnidadAsTipoUnidad = TipoUnidadDataAccess.ConvertToTipoUnidadEntity(reader, "IdTipoUnidad");
						detalleSalidaEntity.IdProductoAsProducto = ProductoDataAccess.ConvertToProductoEntity(reader, "IdProducto");
						detalleSalidaEntity.IdProductoAsProducto.IdMarcaAsMarca = MarcaDataAccess.ConvertToMarcaEntity(reader, "IdMarca");
					}

					#endregion
					detalleSalidaEntity.Id = Convert.ToString(reader["Id"]);
					detalleSalidaEntity.Periodo = Convert.ToString(reader["Periodo"]);
					detalleSalidaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
					detalleSalidaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
					detalleSalidaEntity.IdSalida = Convert.ToString(reader["IdSalida"]);
					detalleSalidaEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					detalleSalidaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
					detalleSalidaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
					if (reader["CostoUnitarioImportacion"] != DBNull.Value)
					{
						detalleSalidaEntity.CostoUnitarioImportacion = (decimal) reader["CostoUnitarioImportacion"];
					}
					detalleSalidaEntity.Cantidad = (decimal) reader["Cantidad"];
					detalleSalidaEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
					detalleSalidaEntity.CostoVenta = (decimal) reader["CostoVenta"];
					detalleSalidaEntity.Precio = (decimal) reader["Precio"];
					if (reader["Descuento"] != DBNull.Value)
					{
						detalleSalidaEntity.Descuento = (decimal) reader["Descuento"];
					}
					if (reader["Subtotal"] != DBNull.Value)
					{
						detalleSalidaEntity.Subtotal = (decimal) reader["Subtotal"];
					}
					if (reader["FechaFabricacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaFabricacion = Convert.ToDateTime(reader["FechaFabricacion"]);
					}
					if (reader["FechaExpiracion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"]);
					}
					if (reader["Lote"] != DBNull.Value)
					{
						detalleSalidaEntity.Lote = Convert.ToString(reader["Lote"]).ToUpper();
					}
					try
					{
						if (reader["IncluyeIva"] != DBNull.Value)
						{
							detalleSalidaEntity.IncluyeIva = (bool)reader["IncluyeIva"];
						}
					}
					catch { }
					detalleSalidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					detalleSalidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					detalleSalidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					detalleSalidaEntity.Empaquetado = Convert.ToBoolean(reader["Empaquetado"]); 
					detalleSalidaEntity.Revisado = Convert.ToBoolean(reader["Revisado"]);
					detalleSalidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
					if (reader["IdEmpresa"] != DBNull.Value)
					{
						detalleSalidaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					}

                    
                    detalleSalidaEntity.SetLoadedState();
                    detalleSalidaEntityCollection.Add(detalleSalidaEntity);
                    
                }

                return detalleSalidaEntityCollection;
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
        
        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion, SqlTransaction  transaction )
        {
        	return FindByAllPaged(findParameter,pageNumber, pageSize,orderBy, conexion,transaction,1);
        }
        
        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter , int pageNumber, int pageSize ,string orderBy, SqlConnection conexion ,SqlTransaction  transaction, int deepLoadLevel)
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
                	mCommand.CommandText = "DetalleSalida_DeepFindByAllPaged";
                	
                }
                else mCommand.CommandText = "DetalleSalida_FindByAllPaged";

                
				if(!String.IsNullOrEmpty(findParameter.Id))
				{
					mCommand.Parameters.AddWithValue("@Id", findParameter.Id );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Id",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.Periodo))
				{
					mCommand.Parameters.AddWithValue("@Periodo", findParameter.Periodo );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Periodo",DBNull.Value);
				}

				if(findParameter.Fecha != null && findParameter.Fecha != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Fecha", findParameter.Fecha);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Fecha",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.TipoTransaccion))
				{
					mCommand.Parameters.AddWithValue("@TipoTransaccion", findParameter.TipoTransaccion );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@TipoTransaccion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.IdSalida))
				{
					mCommand.Parameters.AddWithValue("@IdSalida", findParameter.IdSalida );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdSalida",DBNull.Value);
				}

				if(findParameter.IdProducto != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProducto",DBNull.Value);
				}

				if(findParameter.IdProveedor != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdProveedor", findParameter.IdProveedor);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdProveedor",DBNull.Value);
				}

				if(findParameter.IdLocal != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdLocal",DBNull.Value);
				}

				if(findParameter.CostoUnitarioImportacion != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@CostoUnitarioImportacion", findParameter.CostoUnitarioImportacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@CostoUnitarioImportacion",DBNull.Value);
				}

				if(findParameter.Cantidad != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Cantidad", findParameter.Cantidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Cantidad",DBNull.Value);
				}

				if(findParameter.IdTipoUnidad != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdTipoUnidad", findParameter.IdTipoUnidad);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdTipoUnidad",DBNull.Value);
				}

				if(findParameter.CostoVenta != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@CostoVenta", findParameter.CostoVenta);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@CostoVenta",DBNull.Value);
				}

				if(findParameter.Precio != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Precio", findParameter.Precio);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Precio",DBNull.Value);
				}

				if(findParameter.Descuento != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Descuento", findParameter.Descuento);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Descuento",DBNull.Value);
				}

				if(findParameter.Subtotal != decimal.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Subtotal", findParameter.Subtotal);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Subtotal",DBNull.Value);
				}

				if(findParameter.FechaFabricacion != null && findParameter.FechaFabricacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaFabricacion", findParameter.FechaFabricacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaFabricacion",DBNull.Value);
				}

				if(findParameter.FechaExpiracion != null && findParameter.FechaExpiracion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaExpiracion", findParameter.FechaExpiracion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaExpiracion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(findParameter.Lote))
				{
					mCommand.Parameters.AddWithValue("@Lote", findParameter.Lote );
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Lote",DBNull.Value);
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

				if(findParameter.IdEmpresa != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdEmpresa",DBNull.Value);
				}


				mCommand.Parameters.AddWithValue("@PageNumber",pageNumber);
				mCommand.Parameters.AddWithValue("@PageSize",pageSize);
				if (deepLoadLevel > 1)
		     	{
					mCommand.Parameters.AddWithValue("@OrderBy",orderBy);
			    }
               	
                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                DetalleSalidaEntityCollection detalleSalidaEntityCollection = new DetalleSalidaEntityCollection();
                DetalleSalidaEntity detalleSalidaEntity;
                

                while (reader.Read())
                {
                    detalleSalidaEntity = new DetalleSalidaEntity();
					#region << Deep Load >>
                    if (deepLoadLevel > 1)
		     		{
						detalleSalidaEntity.IdSalidaAsSalida = SalidaDataAccess.ConvertToSalidaEntity(reader, "IdSalida");
						detalleSalidaEntity.IdProveedorAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdProveedor");
						detalleSalidaEntity.IdLocalAsLocalBodega = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");
						detalleSalidaEntity.IdTipoUnidadAsTipoUnidad = TipoUnidadDataAccess.ConvertToTipoUnidadEntity(reader, "IdTipoUnidad");

                    }
	                #endregion                    
					detalleSalidaEntity.Id = Convert.ToString(reader["Id"]);
					detalleSalidaEntity.Periodo = Convert.ToString(reader["Periodo"]);
					detalleSalidaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
					detalleSalidaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
					detalleSalidaEntity.IdSalida = Convert.ToString(reader["IdSalida"]);
					detalleSalidaEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					detalleSalidaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);
					detalleSalidaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
					if (reader["CostoUnitarioImportacion"] != DBNull.Value)
					{
						detalleSalidaEntity.CostoUnitarioImportacion = (decimal) reader["CostoUnitarioImportacion"];
					}
					detalleSalidaEntity.Cantidad = (decimal) reader["Cantidad"];
					detalleSalidaEntity.IdTipoUnidad = Convert.ToInt32(reader["IdTipoUnidad"]);
					detalleSalidaEntity.CostoVenta = (decimal) reader["CostoVenta"];
					detalleSalidaEntity.Precio = (decimal) reader["Precio"];
					if (reader["Descuento"] != DBNull.Value)
					{
						detalleSalidaEntity.Descuento = (decimal) reader["Descuento"];
					}
					if (reader["Subtotal"] != DBNull.Value)
					{
						detalleSalidaEntity.Subtotal = (decimal) reader["Subtotal"];
					}
					if (reader["FechaFabricacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaFabricacion = Convert.ToDateTime(reader["FechaFabricacion"]);
					}
					if (reader["FechaExpiracion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"]);
					}
					if (reader["Lote"] != DBNull.Value)
					{
						detalleSalidaEntity.Lote = Convert.ToString(reader["Lote"]).ToUpper();
					}
					detalleSalidaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					detalleSalidaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					detalleSalidaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						detalleSalidaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					detalleSalidaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
					if (reader["IdEmpresa"] != DBNull.Value)
					{
						detalleSalidaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
					}
					try
					{
						if (reader["IncluyeIva"] != DBNull.Value)
						{
							detalleSalidaEntity.IncluyeIva = (bool)reader["IncluyeIva"];
						}
					}
					catch { }


					detalleSalidaEntity.SetLoadedState();
                    detalleSalidaEntityCollection.Add(detalleSalidaEntity);
                    
                }

                return detalleSalidaEntityCollection;
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

