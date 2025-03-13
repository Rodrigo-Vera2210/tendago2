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
    public partial class EstadisticaDataAccess
    {
        #region << Default Methods >>
        /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static EstadisticaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadByPK(Id, connection, transaction, 1);
        }

        /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static EstadisticaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            EstadisticaEntity estadistic = new EstadisticaEntity();

            estadistic.Id = Id;


            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "Custom_Estadisticas_Resumidas";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@IdEmpresa", estadistic.Id);


                #endregion

                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    #region << Deep Load >>
                    if (deepLoadLevel == 1)
                    {

                    }
                    #endregion

                    #region << Load the BusinessEntity Object >>

                    estadistic.Id = Convert.ToInt32(reader["IdEmpresa"]);
                    estadistic.ValorVentasDia = Convert.ToDecimal(reader["ValorVentasDia"]);
                    estadistic.ValorVentasMes = Convert.ToDecimal(reader["ValorVentasMes"]);
                    estadistic.CantidadNPDia = Convert.ToInt32(reader["CantidadNPDia"]);
                    estadistic.CantidadCTDia = Convert.ToInt32(reader["CantidadCTDia"]);
                    estadistic.ValorCotizacionesMes = Convert.ToInt32(reader["ValorCotizacionesMes"]);

                    #endregion
                }

                estadistic.SetLoadedState();
                return estadistic;
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

        public static VentasMensualEntityCollection LoadMesByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadMesByPK(Id, connection, transaction, 1);
        }
        public static VentasMensualEntityCollection LoadMesByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {           

            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "Custom_Estadisticas_Ventas_Mensual";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@IdEmpresa",Id);


                #endregion

                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                VentasMensualEntityCollection estadisticColl = new VentasMensualEntityCollection();
                VentasMensualEntity estadistic;

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    estadistic = new VentasMensualEntity();
                    #region << Deep Load >>
                    if (deepLoadLevel == 1)
                    {

                    }
                    #endregion

                    #region << Load the BusinessEntity Object >>

                    estadistic.Id = Convert.ToInt32(reader["IdEmpresa"]);
                    estadistic.Fecha = Convert.ToDateTime(reader["fecha"]);
                    estadistic.Total = Convert.ToDecimal(reader["total"]);

                    #endregion

                    estadistic.SetLoadedState();
                    estadisticColl.Add(estadistic);

                }

                return estadisticColl;
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


        public static VentasMensualEntityCollection LoadCobroByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadCobroByPK(Id, connection, transaction, 1);
        }

        public static VentasMensualEntityCollection LoadCobroByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {

            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "Custom_Estadisticas_Cobros";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@IdEmpresa", Id);


                #endregion

                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                VentasMensualEntityCollection estadisticColl = new VentasMensualEntityCollection();
                VentasMensualEntity estadistic;

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                    estadistic = new VentasMensualEntity();
                    #region << Deep Load >>
                    if (deepLoadLevel == 1)
                    {

                    }
                    #endregion

                    #region << Load the BusinessEntity Object >>

                    estadistic.Id = Id;
                    estadistic.Fecha = Convert.ToDateTime(reader["fecha"]);
                    estadistic.Total = Convert.ToDecimal(reader["total"]);
                    estadistic.IdLocal = Convert.ToInt32(reader["IdLocal"]);
                    estadistic.Local = Convert.ToString(reader["Local"]);

                    #endregion

                    estadistic.SetLoadedState();
                    estadisticColl.Add(estadistic);

                }

                return estadisticColl;
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
