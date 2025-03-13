using ER.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ER.DA
{
    public class CierreCajaDataAccess
    {

		public static DataSet Consulta(int idEmpresa, int? idLocal, DateTime? desde, DateTime? hasta, string vendedor, SqlConnection conexion, SqlTransaction transaction)
		{
			SqlCommand mCommand = new SqlCommand();
			try
			{
				SqlDataAdapter adapter = new SqlDataAdapter();

				mCommand.Connection = conexion;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction;
				mCommand.CommandText = "Custom_CierreCaja_Consulta5";

				mCommand.Parameters.AddWithValue("@IdEmpresa", idEmpresa);
				mCommand.Parameters.AddWithValue("@IdLocal", (idLocal as object) ?? DBNull.Value);
				mCommand.Parameters.AddWithValue("@fechaDesde", (desde as object) ?? DBNull.Value);
				mCommand.Parameters.AddWithValue("@fechaHasta", (hasta as object) ?? DBNull.Value);
				mCommand.Parameters.AddWithValue("@idVendedor", (vendedor as object) ?? DBNull.Value);

				adapter.SelectCommand = mCommand;

				DataSet result = new DataSet();
				adapter.Fill(result);
				return result;
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



        #region << Default Methods >>

        /// <summary>
        /// Create a new entity type of CierreCaja
        /// </summary>
        public static CierreCajaEntity Insert(CierreCajaEntity CierreCaja, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "CierreCaja_Insert";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@IdEmpresa", CierreCaja.IdEmpresa);
                mCommand.Parameters.AddWithValue("@IdLocal", CierreCaja.IdLocal);
                mCommand.Parameters.AddWithValue("@IdCajero", CierreCaja.IdCajero);
                mCommand.Parameters.AddWithValue("@FechaApertura", CierreCaja.FechaApertura);
                mCommand.Parameters.AddWithValue("@FechaCierre", CierreCaja.FechaCierre);

                mCommand.Parameters.AddWithValue("@SaldoInicial", CierreCaja.SaldoInicial);
                mCommand.Parameters.AddWithValue("@TotalIngresos", CierreCaja.TotalIngresos);
                mCommand.Parameters.AddWithValue("@TotalEgresos", CierreCaja.TotalEgresos);
                mCommand.Parameters.AddWithValue("@SaldoFinal", CierreCaja.SaldoFinal);

                mCommand.Parameters.AddWithValue("@IpIngreso", CierreCaja.IpIngreso);
                mCommand.Parameters.AddWithValue("@UsuarioIngreso", CierreCaja.UsuarioIngreso.ToUpper());
                mCommand.Parameters.AddWithValue("@FechaIngreso", CierreCaja.FechaIngreso);
                 
                if (!String.IsNullOrEmpty(CierreCaja.Observaciones))
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", CierreCaja.Observaciones);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(CierreCaja.IpModificacion))
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", CierreCaja.IpModificacion.ToUpper());
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(CierreCaja.UsuarioModificacion))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", CierreCaja.UsuarioModificacion.ToUpper());
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
                }

                if (CierreCaja.FechaModificacion != null && CierreCaja.FechaModificacion != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", CierreCaja.FechaModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", DBNull.Value);
                }

                mCommand.Parameters.AddWithValue("@IdEstado", CierreCaja.IdEstado);

                // Add the primary keys columns
                mCommand.Parameters.Add("@Id", SqlDbType.VarChar, 50);
                mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


                #endregion

                // Insert CierreCaja
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();

                CierreCaja.Id = Convert.ToString(mCommand.Parameters["@Id"].Value);


                return CierreCaja;
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
        public static void Update(CierreCajaEntity CierreCaja, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction; ;
                mCommand.CommandText = "CierreCaja_Update";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@Id", CierreCaja.Id);
                mCommand.Parameters.AddWithValue("@IdEmpresa", CierreCaja.IdEmpresa);
                mCommand.Parameters.AddWithValue("@IdLocal", CierreCaja.IdLocal);
                mCommand.Parameters.AddWithValue("@IdCajero", CierreCaja.IdCajero);
                mCommand.Parameters.AddWithValue("@FechaApertura", CierreCaja.FechaApertura);
                mCommand.Parameters.AddWithValue("@FechaCierre", CierreCaja.FechaCierre);

                mCommand.Parameters.AddWithValue("@Observaciones", CierreCaja.Observaciones);
                mCommand.Parameters.AddWithValue("@SaldoInicial", CierreCaja.SaldoInicial);
                mCommand.Parameters.AddWithValue("@TotalIngresos", CierreCaja.TotalIngresos);
                mCommand.Parameters.AddWithValue("@TotalEgresos", CierreCaja.TotalEgresos);
                mCommand.Parameters.AddWithValue("@SaldoFinal", CierreCaja.SaldoFinal);

                mCommand.Parameters.AddWithValue("@IpIngreso", CierreCaja.IpIngreso);
                mCommand.Parameters.AddWithValue("@UsuarioIngreso", CierreCaja.UsuarioIngreso);
                mCommand.Parameters.AddWithValue("@FechaIngreso", CierreCaja.FechaIngreso);

                if (!String.IsNullOrEmpty(CierreCaja.IpModificacion))
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", CierreCaja.IpModificacion.ToUpper());
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(CierreCaja.UsuarioModificacion))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", CierreCaja.UsuarioModificacion.ToUpper());
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
                }

                if (CierreCaja.FechaModificacion != null && CierreCaja.FechaModificacion != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", CierreCaja.FechaModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("FechaModificacion", DBNull.Value);
                }

                mCommand.Parameters.AddWithValue("@IdEstado", CierreCaja.IdEstado);


                #endregion

                // Update CierreCaja
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
        public static void Delete(CierreCajaEntity CierreCaja, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction; ;
                mCommand.CommandText = "CierreCaja_Delete";
                mCommand.Parameters.AddWithValue("@Id", CierreCaja.Id);
                mCommand.Parameters.AddWithValue("@FechaModificacion", CierreCaja.FechaModificacion);
                mCommand.Parameters.AddWithValue("@UsuarioModificacion", CierreCaja.UsuarioModificacion.ToUpper());
                mCommand.Parameters.AddWithValue("@IpModificacion", CierreCaja.IpModificacion.ToUpper());


                // Update CierreCaja
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
        public static CierreCajaEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadByPK(Id, connection, transaction, 1);
        }

        /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static CierreCajaEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            CierreCajaEntity CierreCaja = new CierreCajaEntity();

            CierreCaja.Id = Id;


            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "CierreCaja_LoadByPK";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@Id", CierreCaja.Id);


                #endregion

                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    #region << Deep Load >>
                    if (deepLoadLevel == 1)
                    {
                        CierreCaja.IdLocalAsLocal = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");

                    }
                    #endregion

                    #region << Load the BusinessEntity Object >>

                    CierreCaja.Id = Convert.ToString(reader["Id"]);
                    CierreCaja.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
                    CierreCaja.IdLocal = Convert.ToInt32(reader["IdLocal"]);
                    CierreCaja.IdCajero = Convert.ToString(reader["IdCajero"]);
                    CierreCaja.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                    CierreCaja.FechaCierre = Convert.ToDateTime(reader["FechaCierre"]);

                    CierreCaja.Observaciones = Convert.ToString(reader["Observaciones"]);

                    CierreCaja.SaldoInicial = Convert.ToDecimal(reader["SaldoInicial"]);
                    CierreCaja.TotalIngresos = Convert.ToDecimal(reader["TotalIngresos"]);
                    CierreCaja.TotalEgresos = Convert.ToDecimal(reader["TotalEgresos"]);
                    CierreCaja.SaldoFinal = Convert.ToDecimal(reader["SaldoFinal"]);

                    CierreCaja.IpIngreso = Convert.ToString(reader["IpIngreso"]);
                    CierreCaja.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
                    CierreCaja.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                    if (reader["IpModificacion"] != DBNull.Value)
                    {
                        CierreCaja.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
                    }
                    if (reader["UsuarioModificacion"] != DBNull.Value)
                    {
                        CierreCaja.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
                    }
                    if (reader["FechaModificacion"] != DBNull.Value)
                    {
                        CierreCaja.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
                    }
                    CierreCaja.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    #endregion
                }

                CierreCaja.SetLoadedState();
                return CierreCaja;
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





        public static CierreCajaEntityCollection FindByAll(CierreCajaFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction)
        {
            return FindByAll(findParameter, conexion, transaction, 1);
        }

        public static CierreCajaEntityCollection FindByAll(CierreCajaFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction, int deepLoadLevel)
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
                    mCommand.CommandText = "CierreCaja_DeepFindByAll";
                }
                else mCommand.CommandText = "CierreCaja_FindByAll";


                if (findParameter.Id != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@Id", DBNull.Value);
                }

                if (findParameter.IdEmpresa != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdEmpresa", DBNull.Value);
                }

                if (findParameter.IdLocal != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IdCajero))
                {
                    mCommand.Parameters.AddWithValue("@IdCajero", findParameter.IdCajero);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdCajero", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.Observaciones))
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", findParameter.Observaciones);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IpIngreso))
                {
                    mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpIngreso", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioIngreso", DBNull.Value);
                }

                if (findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@FechaIngreso", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IpModificacion))
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
                }

                if (findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", DBNull.Value);
                }

                if (findParameter.IdEstado != short.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdEstado", DBNull.Value);
                }



                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                CierreCajaEntityCollection CierreCajaEntityCollection = new CierreCajaEntityCollection();
                CierreCajaEntity CierreCajaEntity;


                while (reader.Read())
                {
                    CierreCajaEntity = new CierreCajaEntity();
                    #region << Deep Load >>
                    if (deepLoadLevel == 1)
                    {
                        CierreCajaEntity.IdLocalAsLocal = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");

                    }
                    #endregion
                    CierreCajaEntity.Id = Convert.ToString(reader["Id"]);
                    CierreCajaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
                    CierreCajaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
                    CierreCajaEntity.IdCajero = Convert.ToString(reader["IdCajero"]);
                    CierreCajaEntity.Observaciones = Convert.ToString(reader["Observaciones"]);
                    CierreCajaEntity.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                    CierreCajaEntity.FechaCierre = Convert.ToDateTime(reader["FechaCierre"]);

                    CierreCajaEntity.SaldoInicial = Convert.ToDecimal(reader["SaldoInicial"]);
                    CierreCajaEntity.TotalIngresos = Convert.ToDecimal(reader["TotalIngresos"]);
                    CierreCajaEntity.TotalEgresos = Convert.ToDecimal(reader["TotalEgresos"]);
                    CierreCajaEntity.SaldoFinal = Convert.ToDecimal(reader["SaldoFinal"]);
                     
                    CierreCajaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
                    CierreCajaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
                    CierreCajaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                    if (reader["IpModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
                    }
                    if (reader["UsuarioModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
                    }
                    if (reader["FechaModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
                    }
                    CierreCajaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);


                    CierreCajaEntity.SetLoadedState();
                    CierreCajaEntityCollection.Add(CierreCajaEntity);

                }

                return CierreCajaEntityCollection;
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

        public static CierreCajaEntityCollection FindByAllPaged(CierreCajaFindParameterEntity findParameter, int pageNumber, int pageSize, string orderBy, SqlConnection conexion, SqlTransaction transaction)
        {
            return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, conexion, transaction, 1);
        }

        public static CierreCajaEntityCollection FindByAllPaged(CierreCajaFindParameterEntity findParameter, int pageNumber, int pageSize, string orderBy, SqlConnection conexion, SqlTransaction transaction, int deepLoadLevel)
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
                    mCommand.CommandText = "CierreCaja_DeepFindByAllPaged";

                }
                else mCommand.CommandText = "CierreCaja_FindByAllPaged";


                if (findParameter.Id != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@Id", DBNull.Value);
                }

                if (findParameter.IdEmpresa != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdEmpresa", DBNull.Value);
                }

                if (findParameter.IdLocal != int.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", findParameter.IdLocal);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IdCajero))
                {
                    mCommand.Parameters.AddWithValue("@IdCajero", findParameter.IdCajero);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdCajero", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.Observaciones))
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", findParameter.Observaciones);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@Observaciones", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IpIngreso))
                {
                    mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpIngreso", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioIngreso", DBNull.Value);
                }

                if (findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@FechaIngreso", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.IpModificacion))
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
                }

                if (!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
                }

                if (findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@FechaModificacion", DBNull.Value);
                }

                if (findParameter.IdEstado != short.MinValue)
                {
                    mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdEstado", DBNull.Value);
                }


                mCommand.Parameters.AddWithValue("@PageNumber", pageNumber);
                mCommand.Parameters.AddWithValue("@PageSize", pageSize);
                if (deepLoadLevel > 1)
                {
                    mCommand.Parameters.AddWithValue("@OrderBy", orderBy);
                }

                if (conexion.State != ConnectionState.Open) conexion.Open();
                reader = mCommand.ExecuteReader();

                CierreCajaEntityCollection CierreCajaEntityCollection = new CierreCajaEntityCollection();
                CierreCajaEntity CierreCajaEntity;


                while (reader.Read())
                {
                    CierreCajaEntity = new CierreCajaEntity();
                    #region << Deep Load >>
                    if (deepLoadLevel > 1)
                    {
                        CierreCajaEntity.IdLocalAsLocal = LocalBodegaDataAccess.ConvertToLocalBodegaEntity(reader, "IdLocal");

                    }
                    #endregion
                    CierreCajaEntity.Id = Convert.ToString(reader["Id"]);
                    CierreCajaEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
                    CierreCajaEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
                    CierreCajaEntity.IdCajero = Convert.ToString(reader["IdCajero"]);
                    CierreCajaEntity.Observaciones = Convert.ToString(reader["Observaciones"]);
                    CierreCajaEntity.FechaApertura = Convert.ToDateTime(reader["FechaApertura"]);
                    CierreCajaEntity.FechaCierre = Convert.ToDateTime(reader["FechaCierre"]);

                    CierreCajaEntity.SaldoInicial = Convert.ToDecimal(reader["SaldoInicial"]);
                    CierreCajaEntity.TotalIngresos = Convert.ToDecimal(reader["TotalIngresos"]);
                    CierreCajaEntity.TotalEgresos = Convert.ToDecimal(reader["TotalEgresos"]);
                    CierreCajaEntity.SaldoFinal = Convert.ToDecimal(reader["SaldoFinal"]);
                    
                    CierreCajaEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
                    CierreCajaEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
                    CierreCajaEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
                    if (reader["IpModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
                    }
                    if (reader["UsuarioModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
                    }
                    if (reader["FechaModificacion"] != DBNull.Value)
                    {
                        CierreCajaEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
                    }
                    CierreCajaEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);


                    CierreCajaEntity.SetLoadedState();
                    CierreCajaEntityCollection.Add(CierreCajaEntity);

                }

                return CierreCajaEntityCollection;
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
