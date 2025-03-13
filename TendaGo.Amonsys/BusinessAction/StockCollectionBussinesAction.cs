
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2018, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



#region using

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Net;
using ER.BE; 
using ER.DA; 
using System.Transactions;
using System.Configuration;


#endregion using

namespace ER.BA
{
    public partial class StockCollectionBussinesAction
    {

        public static DataSet SaldoInventario(string Id, int IdEmpresa, int IdProducto, int IdLocal)
        {
            return SaldoInventario(Id, IdEmpresa, IdProducto, IdLocal, null, null);
        }

        public static DataSet SaldoInventario(string Id, int IdEmpresa, int IdProducto, int IdLocal, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }
            try
            {
                return StockDataAccessCollection.SaldoInventarioDS(IdEmpresa, IdProducto, IdLocal, connection, transaction);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        public static DataSet StockInventario(int IdEmpresa, object IdProducto, int IdLocal = 0, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }
            try
            {
                return StockDataAccessCollection.SaldoInventarioDS(IdEmpresa, IdProducto, IdLocal, connection, transaction);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }
        
        public static ProductoEntityCollection SaldoInventario(int IdEmpresa, object IdProducto, int IdLocal)
        {
            return SaldoInventario(IdEmpresa, IdProducto, IdLocal, null, null);
        }

        public static ProductoEntityCollection SaldoInventario(int IdEmpresa, object IdProducto, int IdLocal, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }

            try
            {
                ProductoEntityCollection producto = StockDataAccessCollection.SaldoInventario(IdEmpresa, IdProducto, IdLocal, connection, transaction);
                
                return producto;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        public static DataSet CrearCarrito(int IdEmpresa, object IdProducto, string idSalida, int IdLocal, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }

            try
            {
                return StockDataAccessCollection.CrearCarrito(IdEmpresa, IdProducto, idSalida, IdLocal, connection, transaction);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        public static StockEntityCollection SaldoInventario(int IdEmpresa, object IdProducto)
        {
            return SaldoInventario(IdEmpresa, IdProducto, null, null);
        }
        public static StockEntityCollection SaldoInventario(int IdEmpresa, object IdProducto, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }
            try
            {
                StockEntityCollection stocksProducto = StockDataAccessCollection.SaldoInventario(IdEmpresa, IdProducto, connection, transaction);
                if (stocksProducto != null)
                {
                    stocksProducto.SetState(EntityStatesEnum.Loaded);
                }
                return stocksProducto;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

    }
}

