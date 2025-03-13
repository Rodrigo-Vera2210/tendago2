
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
    public partial class PagoCreditoBussinesAction
    {
        /// <summary>
        /// Guarda un pago de cuentas por pagar con su detalle
        /// </summary>
        /// <param name="pagoCredito">El detalle del pago debe estar contenido en la propiedad DetallePagoFromIdPagoCredito</param>
        /// <returns></returns>
        public static PagoCreditoEntity Guardar(PagoCreditoEntity pagoCredito)
        {
            return Guardar(pagoCredito, null, null);
        }

        /// <summary>
        /// Guarda un pago de cuentas por pagar con su detalle
        /// </summary>
        /// <param name="pagoCredito">El detalle del pago debe estar contenido en la propiedad DetallePagoFromIdPagoCredito</param>
        /// <param name="connection">conexion</param>
        /// <param name="transaction">transaccion</param>
        /// <returns></returns>
        public static PagoCreditoEntity Guardar(PagoCreditoEntity pagoCredito, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
                connection.Open();
                transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            }

            try
            {

                pagoCredito = Save(pagoCredito, connection, transaction);


                if (isBAParent && transaction != null)
                {
                    transaction.Commit();
                    pagoCredito.SetState(EntityStatesEnum.SavedSuccessfully);
                }

                return pagoCredito;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if (pagoCredito != null) pagoCredito.RollBackState();

                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }
    }
}


