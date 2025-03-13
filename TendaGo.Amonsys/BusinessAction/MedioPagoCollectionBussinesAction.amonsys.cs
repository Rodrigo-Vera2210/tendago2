    
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
    public partial class MedioPagoCollectionBussinesAction
    {
    
  
    
        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        #region Implementation
        
        public static MedioPagoEntityCollection Save(MedioPagoEntityCollection medioPagoCollection )
        {
            return Save(medioPagoCollection, null, null);
        }
        
        public static MedioPagoEntityCollection Save(MedioPagoEntityCollection medioPagoCollection , SqlConnection connection, SqlTransaction transaction)
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

                    foreach (MedioPagoEntity medioPago in medioPagoCollection)
                    {
                        MedioPagoBussinesAction.Save(medioPago , connection, transaction);
                    }
                    
                    if (isBAParent && transaction != null) 
                    {
                    	transaction.Commit();
                    	medioPagoCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                    }

                    return medioPagoCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( medioPagoCollection != null)  medioPagoCollection.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>
        
        public static MedioPagoEntityCollection FindByAll(MedioPagoFindParameterEntity findParameter )
        {
        	return FindByAll(findParameter,null,null,1);
        }
        
        public static MedioPagoEntityCollection FindByAll(MedioPagoFindParameterEntity findParameter ,int deepLoadLevel)
        {
        	return FindByAll(findParameter,null,null,deepLoadLevel);
        }
        
        public static MedioPagoEntityCollection FindByAll(MedioPagoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAll(findParameter,connection,transaction,1);
        }
        
        public static MedioPagoEntityCollection FindByAll(MedioPagoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			MedioPagoEntityCollection medioPagoCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   medioPagoCollection  = MedioPagoDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
				if (medioPagoCollection!=null && deepLoadLevel > 1)
                {
                	foreach (MedioPagoEntity medioPago in medioPagoCollection)
                    {

                    }

                }

                   
                   medioPagoCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return medioPagoCollection;
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
        
        public static MedioPagoEntityCollection FindByAllPaged(MedioPagoFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
        }
        
        public static MedioPagoEntityCollection FindByAllPaged(MedioPagoFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
        }
        
        public static MedioPagoEntityCollection FindByAllPaged(MedioPagoFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
        }
        
        public static MedioPagoEntityCollection FindByAllPaged(MedioPagoFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			MedioPagoEntityCollection medioPagoCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   medioPagoCollection  = MedioPagoDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
                   medioPagoCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return medioPagoCollection;
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

