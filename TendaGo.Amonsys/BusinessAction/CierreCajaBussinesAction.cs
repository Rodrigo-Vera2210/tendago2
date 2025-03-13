
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
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
    public partial class CierreCajaBussinesAction
    { 
        public static DataSet Consulta(int empresaId, int? idLocal, DateTime? desde, DateTime? hasta, string vendedor, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }

            try
            {
                return CierreCajaDataAccess.Consulta(empresaId, idLocal, desde, hasta, vendedor, connection, transaction);
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




        #region Implementation

        public static CierreCajaEntity Save(CierreCajaEntity CierreCaja)
        {
            return Save(CierreCaja, null, null);
        }

        public static CierreCajaEntity Save(CierreCajaEntity CierreCaja, SqlConnection connection, SqlTransaction transaction)
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
                                    if( CierreCaja.IdLineaAsLinea != null && CierreCaja.IdLineaAsLinea.CanSave )
                                    {
                                        CierreCaja.IdLinea = LineaBussinesAction.Save(CierreCaja.IdLineaAsLinea , connection,transaction).Id;
                                    }


                */
                switch (CierreCaja.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        CierreCajaDataAccess.Delete(CierreCaja, connection, transaction);
                        break;
                    case EntityStatesEnum.Updated:
                        CierreCajaDataAccess.Update(CierreCaja, connection, transaction);
                        break;
                    case EntityStatesEnum.New:
                        CierreCaja = CierreCajaDataAccess.Insert(CierreCaja, connection, transaction);
                        break;
                    default:
                        break;
                }



                //                } 

                //End of Transaction
                if (isBAParent && transaction != null)
                {
                    transaction.Commit();
                    CierreCaja.SetState(EntityStatesEnum.SavedSuccessfully);
                }

                return CierreCaja;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if (CierreCaja != null) CierreCaja.RollBackState();

                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }





        public static CierreCajaEntity LoadByPK(string Id)
        {
            return LoadByPK(Id, null, null, 1);
        }
        public static CierreCajaEntity LoadByPK(string Id, int deepLoadLevel)
        {
            return LoadByPK(Id, null, null, deepLoadLevel);
        }

        public static CierreCajaEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadByPK(Id, connection, transaction, 1);
        }

        public static CierreCajaEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            try
            {


                CierreCajaEntity CierreCaja = CierreCajaDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
                if (CierreCaja != null)
                {
                    if (deepLoadLevel > 1)
                    {
                        CierreCaja.IdLocalAsLocal = LocalBodegaBussinesAction.LoadByPK(CierreCaja.IdLocal, connection, transaction, deepLoadLevel - 1);

                    }

                    CierreCaja.SetLoadedState();
                }

                return CierreCaja;
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


