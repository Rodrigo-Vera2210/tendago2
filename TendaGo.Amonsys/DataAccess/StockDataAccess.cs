
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
using ER.BE; 



namespace ER.DA
{
    public partial class StockDataAccess
    {
        /// <summary>
        /// Calcula los saldos de inventario por el metodo de costo promedio ponderado
        /// </summary>
        //public static StockEntity Kardex(StockEntity stock, SqlConnection connection, SqlTransaction transaction)
        //{
        //    SqlCommand mCommand = new SqlCommand();
        //    try
        //    {
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Stock_CostoPromedioPonderado";

        //        #region << Add the params >>

        //        mCommand.Parameters.AddWithValue("@IdEmpresa", stock.IdEmpresa);
        //        mCommand.Parameters.AddWithValue("@Tipo", stock.Tipo.ToUpper());
        //        mCommand.Parameters.AddWithValue("@IdSalidaEntrada", stock.IdSalidaEntrada.ToUpper());
        //        mCommand.Parameters.AddWithValue("@IdDetalle", stock.IdDetalle.ToUpper());
        //        mCommand.Parameters.AddWithValue("@IdProducto", stock.IdProducto);
        //        mCommand.Parameters.AddWithValue("@IdLocal", stock.IdLocal);
        //        mCommand.Parameters.AddWithValue("@Cantidad", stock.Cantidad);
        //        mCommand.Parameters.AddWithValue("@ValorUnitario", stock.ValorUnitario);
        //        mCommand.Parameters.AddWithValue("@IdTipoUnidad", stock.IdTipoUnidad);
        //        mCommand.Parameters.AddWithValue("@IdEstado", stock.IdEstado);

        //        // Add the primary keys columns
        //        mCommand.Parameters.Add("@Id", SqlDbType.VarChar,50);
        //        mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


        //        #endregion

        //        // Insert Stock
        //        if (connection.State != ConnectionState.Open) connection.Open();
        //        mCommand.ExecuteNonQuery();

        //        stock.Id = Convert.ToString(mCommand.Parameters["@Id"].Value);


        //        return stock;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        mCommand.Dispose();
        //    }
        //}

    }
}


