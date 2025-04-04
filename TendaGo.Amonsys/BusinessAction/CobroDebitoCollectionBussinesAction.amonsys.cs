    
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
    public partial class CobroDebitoCollectionBussinesAction
    {
    
  
    
//        #region << Custom Stored Procedures >>
        
        
//        #endregion
        
//        #region Implementation
        
//        public static CobroDebitoEntityCollection Save(CobroDebitoEntityCollection cobroDebitoCollection )
//        {
//            return Save(cobroDebitoCollection, null, null);
//        }
        
//        public static CobroDebitoEntityCollection Save(CobroDebitoEntityCollection cobroDebitoCollection , SqlConnection connection, SqlTransaction transaction)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
//                connection.Open();
//                transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

//            }

//            try
//            {

//                    foreach (CobroDebitoEntity cobroDebito in cobroDebitoCollection)
//                    {
//                        CobroDebitoBussinesAction.Save(cobroDebito , connection, transaction);
//                    }
                    
//                    if (isBAParent && transaction != null) 
//                    {
//                    	transaction.Commit();
//                    	cobroDebitoCollection.SetState(EntityStatesEnum.SavedSuccessfully);
//                    }

//                    return cobroDebitoCollection;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( cobroDebitoCollection != null)  cobroDebitoCollection.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #region << Find by All >>
        
//        public static CobroDebitoEntityCollection FindByAll(CobroDebitoFindParameterEntity findParameter )
//        {
//        	return FindByAll(findParameter,null,null,1);
//        }
        
//        public static CobroDebitoEntityCollection FindByAll(CobroDebitoFindParameterEntity findParameter ,int deepLoadLevel)
//        {
//        	return FindByAll(findParameter,null,null,deepLoadLevel);
//        }
        
//        public static CobroDebitoEntityCollection FindByAll(CobroDebitoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAll(findParameter,connection,transaction,1);
//        }
        
//        public static CobroDebitoEntityCollection FindByAll(CobroDebitoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			CobroDebitoEntityCollection cobroDebitoCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   cobroDebitoCollection  = CobroDebitoDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
//				if (cobroDebitoCollection!=null && deepLoadLevel > 1)
//                {
//                	foreach (CobroDebitoEntity cobroDebito in cobroDebitoCollection)
//                    {

//                    }

//                }

                   
//                   cobroDebitoCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return cobroDebitoCollection;
//            }
//            catch (Exception exc)
//            {
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        public static CobroDebitoEntityCollection search(CobroDebitoFindParameterEntity findParameter, DateTime? startDate = null, DateTime? endDate = null, int idLocal = 0, SqlConnection connection = null, SqlTransaction transaction = null)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//            CobroDebitoEntityCollection salidaCollection = null;

//            try
//            {

//                salidaCollection = CobroDebitoDataAccessCollection.Search(findParameter.Detalle, findParameter.IdEmpresa, 0, findParameter.IdCliente, startDate, endDate, connection, transaction);
//                salidaCollection.SetState(EntityStatesEnum.Loaded);

//                return salidaCollection;
//            }
//            catch (Exception exc)
//            {
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #endregion

//        #region << Find by All Paged >>

//        public static CobroDebitoEntityCollection FindByAllPaged(CobroDebitoFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
//        }
        
//        public static CobroDebitoEntityCollection FindByAllPaged(CobroDebitoFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
//        }
        
//        public static CobroDebitoEntityCollection FindByAllPaged(CobroDebitoFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
//        }
        
//        public static CobroDebitoEntityCollection FindByAllPaged(CobroDebitoFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			CobroDebitoEntityCollection cobroDebitoCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   cobroDebitoCollection  = CobroDebitoDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
//                   cobroDebitoCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return cobroDebitoCollection;
//            }
//            catch (Exception exc)
//            {
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }
        
//        #endregion

      
//        #endregion Implementation
        
          
     }
}

