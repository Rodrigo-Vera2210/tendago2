
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
    public partial class EntradaCollectionBussinesAction
    {

        public static EntradaEntityCollection Search(int idEmpresa, int idLocal, string tipoTransaccion = null, string searchTerm = null, string idVendedor = null, int? idProveedor = null, DateTime? startDate = null, DateTime? endDate = null, string estado = null, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            EntradaEntityCollection salidaCollection = null;

            try
            {

                salidaCollection = EntradaDataAccessCollection.Search(idEmpresa, idLocal, tipoTransaccion, searchTerm, idVendedor, idProveedor, startDate, endDate, estado, connection, transaction);
                salidaCollection.SetState(EntityStatesEnum.Loaded);

                return salidaCollection;
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



        public static DataSet ValidarCompra(string CodigoProducto, string CodigoUnidad, string Local, int IdEmpresa)
        {
            return ValidarCompra( CodigoProducto,  CodigoUnidad,  Local,  IdEmpresa, null, null);
        }

        public static DataSet ValidarCompra(string CodigoProducto, string CodigoUnidad, string Local, int IdEmpresa, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }
            try
            {
                return EntradaDataAccessCollection.ValidarCompra( CodigoProducto,  CodigoUnidad,  Local,  IdEmpresa, connection, transaction);
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

