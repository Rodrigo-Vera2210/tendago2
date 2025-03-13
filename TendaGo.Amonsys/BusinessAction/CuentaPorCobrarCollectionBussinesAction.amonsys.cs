    
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
    public partial class CuentaPorCobrarCollectionBussinesAction
    {



        #region << Custom Stored Procedures >>


        public static CuentaPorCobrarEntityCollection FindByIdSalida(string idSalida)
        {
            return FindByAll(new CuentaPorCobrarFindParameterEntity { IdSalida = idSalida });
        }


        public static CuentaPorCobrarEntityCollection FindByIdCliente(int idEmpresa, int idCliente)
        {
            return FindByIdCliente(idEmpresa, idCliente, null, null, 1);
        }

        public static CuentaPorCobrarEntityCollection FindByIdCliente(int idEmpresa, int idCliente, int deepLoadLevel)
        {
            return FindByIdCliente(idEmpresa, idCliente, null, null, deepLoadLevel);
        }

        public static CuentaPorCobrarEntityCollection FindByIdCliente(int idEmpresa, int idCliente, SqlConnection connection, SqlTransaction transaction)
        {
            return FindByIdCliente(idEmpresa, idCliente, connection, transaction, 1);
        }

        public static CuentaPorCobrarEntityCollection FindByIdCliente(int idEmpresa, int idCliente, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            CuentaPorCobrarEntityCollection cuentaPorCobrarCollection = null;

            try
            {
                 
                cuentaPorCobrarCollection = CuentaPorCobrarDataAccessCollection.FindByIdCliente(idEmpresa, idCliente, connection, transaction, deepLoadLevel);

                if (cuentaPorCobrarCollection != null && deepLoadLevel > 1)
                {
                    foreach (CuentaPorCobrarEntity cuentaPorCobrar in cuentaPorCobrarCollection)
                    {
                        cuentaPorCobrar.IdSalidaAsSalida = SalidaBussinesAction.LoadByPK(cuentaPorCobrar.IdSalida, connection, transaction, deepLoadLevel - 1);
                    }

                }
                 
                cuentaPorCobrarCollection.SetState(EntityStatesEnum.Loaded);
                 

                return cuentaPorCobrarCollection;
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

        #endregion

        #region Implementation

        public static CuentaPorCobrarEntityCollection Save(CuentaPorCobrarEntityCollection cuentaPorCobrarCollection )
        {
            return Save(cuentaPorCobrarCollection, null, null);
        }
        
        public static CuentaPorCobrarEntityCollection Save(CuentaPorCobrarEntityCollection cuentaPorCobrarCollection , SqlConnection connection, SqlTransaction transaction)
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

                    foreach (CuentaPorCobrarEntity cuentaPorCobrar in cuentaPorCobrarCollection)
                    {
                        CuentaPorCobrarBussinesAction.Save(cuentaPorCobrar , connection, transaction);
                    }
                    
                    if (isBAParent && transaction != null) 
                    {
                    	transaction.Commit();
                    	cuentaPorCobrarCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                    }

                    return cuentaPorCobrarCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( cuentaPorCobrarCollection != null)  cuentaPorCobrarCollection.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>
        
        public static CuentaPorCobrarEntityCollection FindByAll(CuentaPorCobrarFindParameterEntity findParameter )
        {
        	return FindByAll(findParameter,null,null,1);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAll(CuentaPorCobrarFindParameterEntity findParameter ,int deepLoadLevel)
        {
        	return FindByAll(findParameter,null,null,deepLoadLevel);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAll(CuentaPorCobrarFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAll(findParameter,connection,transaction,1);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAll(CuentaPorCobrarFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			CuentaPorCobrarEntityCollection cuentaPorCobrarCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   cuentaPorCobrarCollection  = CuentaPorCobrarDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
				if (cuentaPorCobrarCollection!=null && deepLoadLevel > 1)
                {
                	foreach (CuentaPorCobrarEntity cuentaPorCobrar in cuentaPorCobrarCollection)
                    {
						cuentaPorCobrar.IdSalidaAsSalida = SalidaBussinesAction.LoadByPK(cuentaPorCobrar.IdSalida, connection, transaction, deepLoadLevel - 1);

                    }

                }

                   
                   cuentaPorCobrarCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return cuentaPorCobrarCollection;
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
        #endregion

        #region << Find by All Paged >>

        public static CuentaPorCobrarEntityCollection FindByAllPaged(CuentaPorCobrarFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAllPaged(CuentaPorCobrarFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAllPaged(CuentaPorCobrarFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
        }
        
        public static CuentaPorCobrarEntityCollection FindByAllPaged(CuentaPorCobrarFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			CuentaPorCobrarEntityCollection cuentaPorCobrarCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   cuentaPorCobrarCollection  = CuentaPorCobrarDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
                   cuentaPorCobrarCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return cuentaPorCobrarCollection;
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
        
        #endregion

      
        #endregion Implementation
        
          
     }
}

