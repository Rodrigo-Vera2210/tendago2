    
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
    public partial class CuentaPorCobrarBussinesAction
    {

        #region Implementation

        public static CuentaPorCobrarEntity Save(CuentaPorCobrarEntity cuentaPorCobrar)
        {
            return Save(cuentaPorCobrar, null, null);
        }

        public static CuentaPorCobrarEntity Save(CuentaPorCobrarEntity cuentaPorCobrar, SqlConnection connection, SqlTransaction transaction)
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

                //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                //                {
                /*
                                    if( cuentaPorCobrar.IdSalidaAsSalida != null && cuentaPorCobrar.IdSalidaAsSalida.CanSave )
                                    {
                                        cuentaPorCobrar.IdSalida = SalidaBussinesAction.Save(cuentaPorCobrar.IdSalidaAsSalida , connection,transaction).Id;
                                    }


                */
                switch (cuentaPorCobrar.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        CuentaPorCobrarDataAccess.Delete(cuentaPorCobrar, connection, transaction);
                        break;
                    case EntityStatesEnum.Updated:
                        CuentaPorCobrarDataAccess.Update(cuentaPorCobrar, connection, transaction);
                        break;
                    case EntityStatesEnum.New:
                        cuentaPorCobrar = CuentaPorCobrarDataAccess.Insert(cuentaPorCobrar, connection, transaction);
                        break;
                    default:
                        break;
                }
                
                if (cuentaPorCobrar.DetalleCobroFromIdCuentaPorCobrar != null)
                {
                    foreach (var detalle in cuentaPorCobrar.DetalleCobroFromIdCuentaPorCobrar)
                    {
                        detalle.IdCuentaPorCobrar = cuentaPorCobrar.Id;
                    }
                }

                //                } 

                //End of Transaction
                if (isBAParent && transaction != null)
                {
                    transaction.Commit();
                    cuentaPorCobrar.SetState(EntityStatesEnum.SavedSuccessfully);
                }

                return cuentaPorCobrar;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if (cuentaPorCobrar != null) cuentaPorCobrar.RollBackState();

                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }





        public static CuentaPorCobrarEntity LoadByPK(int Id)
        {
            return LoadByPK(Id, null, null, 1);
        }
        public static CuentaPorCobrarEntity LoadByPK(int Id, int deepLoadLevel)
        {
            return LoadByPK(Id, null, null, deepLoadLevel);
        }

        public static CuentaPorCobrarEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadByPK(Id, connection, transaction, 1);
        }

        public static CuentaPorCobrarEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            try
            {


                CuentaPorCobrarEntity cuentaPorCobrar = CuentaPorCobrarDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
                if (cuentaPorCobrar != null)
                {
                    if (deepLoadLevel > 1)
                    {
                        cuentaPorCobrar.IdSalidaAsSalida = SalidaBussinesAction.LoadByPK(cuentaPorCobrar.IdSalida, connection, transaction, deepLoadLevel - 1);

                    }

                    cuentaPorCobrar.SetLoadedState();
                }

                return cuentaPorCobrar;
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

        public static decimal ValorNotaCreditoByCliente(int idCliente)
        {
            return ValorNotaCreditoByCliente(idCliente, null, null);
        }

        public static decimal ValorNotaCreditoByCliente(int idCliente, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }

            try
            {
                return CuentaPorCobrarDataAccess.ValorNotaCreditoByCliente(idCliente, connection, transaction);
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


        #endregion Implementation

    }
}