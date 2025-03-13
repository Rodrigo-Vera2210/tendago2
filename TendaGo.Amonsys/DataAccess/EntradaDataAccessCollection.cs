
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



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
		//	public static EntradaEntityCollection Search(int idEmpresa, int idLocal, string tipoTransaccion, string searchTerm, string idVendedor, int? idProveedor, DateTime? startDate, DateTime? endDate, string estado, SqlConnection conexion, SqlTransaction transaction)
		//	{
		//		SqlCommand mCommand = new SqlCommand();
		//		SqlDataReader reader = null;
		//		try
		//		{
		//			mCommand.Connection = conexion;
		//			mCommand.CommandType = CommandType.StoredProcedure;
		//			mCommand.Transaction = transaction;
		//			mCommand.CommandText = "Custom_Entrada_SearchEntrada";

		//			if (idEmpresa > 0)
		//			{
		//				mCommand.Parameters.AddWithValue("@IdEmpresa", idEmpresa);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@IdEmpresa", DBNull.Value);
		//			}

		//			if (idLocal > 0)
		//			{
		//				mCommand.Parameters.AddWithValue("@IdLocal", idLocal);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
		//			}

		//			if (!String.IsNullOrEmpty(searchTerm))
		//			{
		//				mCommand.Parameters.AddWithValue("@SearchTerm", searchTerm);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@SearchTerm", DBNull.Value);
		//			}

		//			if (!string.IsNullOrEmpty(idVendedor))
		//			{
		//				mCommand.Parameters.AddWithValue("@IdVendedor", idVendedor);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@IdVendedor", DBNull.Value);
		//			}

		//			if (idProveedor > 0)
		//			{
		//				mCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@IdProveedor", DBNull.Value);
		//			}

		//			if (startDate != null)
		//			{
		//				mCommand.Parameters.AddWithValue("@FechaInicio", startDate);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@FechaInicio", DBNull.Value);
		//			}


		//			if (endDate != null)
		//			{
		//				mCommand.Parameters.AddWithValue("@FechaFin", endDate);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@FechaFin", DBNull.Value);
		//			}

		//			if (!string.IsNullOrEmpty(tipoTransaccion))
		//			{
		//				mCommand.Parameters.AddWithValue("@TipoTransaccion", tipoTransaccion);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@TipoTransaccion", DBNull.Value);
		//			}


		//			if (!string.IsNullOrEmpty(estado))
		//			{
		//				mCommand.Parameters.AddWithValue("@Estado", estado);
		//			}
		//			else
		//			{
		//				mCommand.Parameters.AddWithValue("@Estado", DBNull.Value);
		//			}


		//			if (conexion.State != ConnectionState.Open) conexion.Open();
		//			reader = mCommand.ExecuteReader();

		//			EntradaEntityCollection entradaEntityCollection = new EntradaEntityCollection();
		//			EntradaEntity entradaEntity;


		//			while (reader.Read())
		//			{
		//				entradaEntity = new EntradaEntity();
		//				// entradaEntity.IdProveedorAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdProveedor");
		//				entradaEntity.IdProveedorAsEntidad = new EntidadEntity
		//				{
		//					Identificacion = Convert.ToString(reader["Identificacion_EntidadFromIdProveedor"]),
		//					RazonSocial = Convert.ToString(reader["RazonSocial_EntidadFromIdProveedor"])
		//				};
		//				entradaEntity.Id = Convert.ToString(reader["Id"]);
		//				entradaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
		//				entradaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
		//				entradaEntity.Periodo = Convert.ToString(reader["Periodo"]);
		//				entradaEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
		//				entradaEntity.TipoTransaccion = Convert.ToString(reader["TipoTransaccion"]);
		//				entradaEntity.IdProveedor = Convert.ToInt32(reader["IdProveedor"]);

		//				if (reader["Ruc"] != DBNull.Value)
		//				{
		//					entradaEntity.Ruc = Convert.ToString(reader["Ruc"]).ToUpper();
		//				}
		//				if (reader["Subtotal0"] != DBNull.Value)
		//				{
		//					entradaEntity.Subtotal0 = (decimal)reader["Subtotal0"];
		//				} 

		//				entradaEntity.Total = (decimal)reader["Total"];
		//				entradaEntity.Saldo = (decimal)reader["Saldo"];

		//				entradaEntity.EstadoActual = Convert.ToString(reader["EstadoActual"]);


		//				entradaEntity.TransaccionPadre = (reader["TransaccionPadre"] != DBNull.Value) ? Convert.ToString(reader["TransaccionPadre"]) : null;
		//				entradaEntity.TipoTransaccionPadre = (reader["TipoTransaccionPadre"] != DBNull.Value) ? Convert.ToString(reader["TipoTransaccionPadre"]) : null;

		//				entradaEntity.IdFormaPago = Convert.ToInt32(reader["IdFormaPago"]);
		//				entradaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
		//				entradaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
		//				entradaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);

		//				if (reader["IpModificacion"] != DBNull.Value)
		//				{
		//					entradaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
		//				}
		//				if (reader["UsuarioModificacion"] != DBNull.Value)
		//				{
		//					entradaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
		//				}
		//				if (reader["FechaModificacion"] != DBNull.Value)
		//				{
		//					entradaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
		//				}

		//				entradaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);


		//				entradaEntity.SetLoadedState();
		//				entradaEntityCollection.Add(entradaEntity);

		//			}

		//			return entradaEntityCollection;
		//		}
		//		catch (Exception exc)
		//		{
		//			throw exc;
		//		}
		//		finally
		//		{
		//			if (reader != null) reader.Close();
		//			mCommand.Dispose();
		//		}

		//	}

		//	public static DataSet ValidarCompra(string CodigoProducto, string CodigoUnidad, string Local,int IdEmpresa, SqlConnection conexion, SqlTransaction transaction)
		//       {
		//           SqlCommand mCommand = new SqlCommand();
		//           try
		//           {
		//               SqlDataAdapter adapter = new SqlDataAdapter();

		//               mCommand.Connection = conexion;
		//               mCommand.CommandType = CommandType.StoredProcedure;
		//               mCommand.Transaction = transaction;
		//               mCommand.CommandText = "Custom_Entrada_ValidarCompra";

		//               mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
		//               mCommand.Parameters.AddWithValue("@CodigoProducto", CodigoProducto);
		//               mCommand.Parameters.AddWithValue("@CodigoUnidad", CodigoUnidad);
		//               mCommand.Parameters.AddWithValue("@Local", Local);

		//               adapter.SelectCommand = mCommand;

		//               DataSet result = new DataSet();
		//               adapter.Fill(result);
		//               return result;
		//           }
		//           catch (Exception exc)
		//           {
		//               throw exc;
		//           }
		//           finally
		//           {
		//               mCommand.Dispose();
		//           }
		//       }
	}
}

