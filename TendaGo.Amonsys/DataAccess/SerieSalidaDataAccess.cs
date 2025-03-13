using ER.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.DA
{
    public partial class SerieSalidaDataAccess
    {
		#region << Default Methods >>

		/// <summary>
		/// Create a new entity type of DetalleCobro
		/// </summary>
		public static SerieSalidaEntity Insert(SerieSalidaEntity serie, SqlConnection connection, SqlTransaction transaction)
		{
			SqlCommand mCommand = new SqlCommand();
			try
			{
				mCommand.Connection = connection;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction;
				mCommand.CommandText = "SerieSalida_Insert";

				#region << Add the params >>

				mCommand.Parameters.AddWithValue("@IdDetalleSalida", serie.IdDetalleSalida);
				mCommand.Parameters.AddWithValue("@IdProducto", serie.IdProducto);
				mCommand.Parameters.AddWithValue("@Serie", serie.Serie);
				mCommand.Parameters.AddWithValue("@IpIngreso", serie.IpIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", serie.UsuarioIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@FechaIngreso", serie.FechaIngreso);

				if (!String.IsNullOrEmpty(serie.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", serie.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
				}

				if (!String.IsNullOrEmpty(serie.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", serie.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
				}

				if (serie.FechaModificacion != null && serie.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", serie.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", serie.IdEstado);
				mCommand.Parameters.AddWithValue("@IdEmpresa", serie.IdEmpresa);

				#endregion

				// Insert DetalleCobro
				if (connection.State != ConnectionState.Open) connection.Open();
				mCommand.ExecuteNonQuery();

				return serie;
			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
				mCommand.Dispose();
			}
		}

		/// <summary>
		/// Update a entity
		/// </summary>
		public static void Update(SerieSalidaEntity serie, SqlConnection connection, SqlTransaction transaction)
		{
			SqlCommand mCommand = new SqlCommand();
			try
			{
				mCommand.Connection = connection;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction; ;
				mCommand.CommandText = "SerieSalida_Update";

				#region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", serie.Id);
				mCommand.Parameters.AddWithValue("@IdDetalleSalida", serie.IdDetalleSalida);
				mCommand.Parameters.AddWithValue("@IdProducto", serie.IdProducto);
				mCommand.Parameters.AddWithValue("@Serie", serie.Serie);

				mCommand.Parameters.AddWithValue("@IpIngreso", serie.IpIngreso);
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", serie.UsuarioIngreso);
				mCommand.Parameters.AddWithValue("@FechaIngreso", serie.FechaIngreso);
				if (!String.IsNullOrEmpty(serie.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", serie.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
				}

				if (!String.IsNullOrEmpty(serie.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", serie.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
				}

				if (serie.FechaModificacion != null && serie.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", serie.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("FechaModificacion", DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", serie.IdEstado);
				mCommand.Parameters.AddWithValue("@IdEmpresa", serie.IdEmpresa);

				#endregion

				// Update detalleCobro
				if (connection.State != ConnectionState.Open) connection.Open();
				mCommand.ExecuteNonQuery();


			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
				mCommand.Dispose();
			}
		}

		/// <summary>
		/// Delete a entity
		/// </summary>
		public static void Delete(SerieSalidaEntity serie, SqlConnection connection, SqlTransaction transaction)
		{
			SqlCommand mCommand = new SqlCommand();
			try
			{
				mCommand.Connection = connection;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction; ;
				mCommand.CommandText = "SerieSalida_Delete";
				mCommand.Parameters.AddWithValue("@Id", serie.Id);
				mCommand.Parameters.AddWithValue("@FechaModificacion", serie.FechaModificacion);
				mCommand.Parameters.AddWithValue("@UsuarioModificacion", serie.UsuarioModificacion.ToUpper());
				mCommand.Parameters.AddWithValue("@IpModificacion", serie.IpModificacion.ToUpper());


				// Update detalleCobro
				if (connection.State != ConnectionState.Open) connection.Open();
				mCommand.ExecuteNonQuery();


			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
				mCommand.Dispose();
			}
		}

		/// <summary>
		/// Load a entity by your Primary Key
		/// </summary>
		public static SerieSalidaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
		{
			return LoadByPK(Id, connection, transaction, 1);
		}

		/// <summary>
		/// Load a entity by your Primary Key
		/// </summary>
		public static SerieSalidaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
		{
			SerieSalidaEntity serie = new SerieSalidaEntity();

			serie.Id = Id;


			SqlCommand mCommand = new SqlCommand();
			SqlDataReader reader = null;
			try
			{
				mCommand.Connection = connection;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction;
				mCommand.CommandText = "SerieSalida_LoadByPK";

				#region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", serie.Id);


				#endregion

				if (connection.State != ConnectionState.Open) connection.Open();

				reader = mCommand.ExecuteReader();

				if (!reader.HasRows) return null;

				while (reader.Read())
				{
					#region << Load the BusinessEntity Object >>

					serie.Id = Convert.ToInt32(reader["Id"]);
					serie.IdDetalleSalida = Convert.ToString(reader["IdDetalleSalida"]);
					serie.IdProducto = Convert.ToInt32(reader["IdProducto"]);
					serie.Serie = Convert.ToString(reader["Serie"]);
					serie.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					serie.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					serie.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);


					if (reader["IpModificacion"] != DBNull.Value)
					{
						serie.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						serie.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						serie.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					serie.IdEstado = Convert.ToInt16(reader["IdEstado"]);
					serie.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);

					#endregion
				}

				serie.SetLoadedState();
				return serie;
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

		#endregion
	}
}
