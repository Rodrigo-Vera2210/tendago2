
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2020, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ER.BE; 



namespace ER.DA
{
    public partial class TipoDocumentoDataAccess
    {


        ///// <summary>
        ///// Load a entity by your Primary Key
        ///// </summary>
        //public static SecuencialEntity GetDocumentSecuential(string ruc, string idTipoDocumento , SqlConnection connection, SqlTransaction transaction)
        //{
        //    SecuencialEntity result = new SecuencialEntity();

        //    SqlCommand mCommand = new SqlCommand();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Obtener_Secuencial";

        //        #region << Add the params >>

        //        mCommand.Parameters.AddWithValue("@IdTipoDocumento", idTipoDocumento);
        //        mCommand.Parameters.AddWithValue("@Ruc", ruc);


        //        #endregion

        //        if (connection.State != ConnectionState.Open) connection.Open();

        //        reader = mCommand.ExecuteReader();

        //        if (!reader.HasRows) return null;

        //        while (reader.Read())
        //        { 

        //            #region << Load the BusinessEntity Object >>

        //            result.Id = Convert.ToInt32(reader["Id"]);
        //            result.IdTipoDocumento = Convert.ToString(reader["IdTipoDocumento"]);
        //            result.Ruc = Convert.ToString(reader["Ruc"]);
        //            result.Secuencial = Convert.ToInt32(reader["Secuencial"]);
        //            result.FechaVigencia = Convert.ToDateTime(reader["FechaVigencia"]);
        //            if (reader["Autorizacion"] != DBNull.Value)
        //            {
        //                result.Autorizacion = Convert.ToString(reader["Autorizacion"]).ToUpper();
        //            }
        //            if (reader["Establecimiento"] != DBNull.Value)
        //            {
        //                result.Establecimiento = Convert.ToString(reader["Establecimiento"]).ToUpper();
        //            }
        //            if (reader["PuntoVenta"] != DBNull.Value)
        //            {
        //                result.PuntoVenta = Convert.ToString(reader["PuntoVenta"]);
        //            } 

        //            #endregion
        //        }

        //        result.SetLoadedState();
        //        return result;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (reader != null) reader.Close();
        //        mCommand.Dispose();
        //    }
        //}
    }
}


