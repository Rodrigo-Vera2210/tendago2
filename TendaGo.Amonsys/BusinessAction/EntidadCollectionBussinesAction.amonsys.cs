    
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
    public partial class EntidadCollectionBussinesAction
    {
    
  
    
        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        #region Implementation
        
        public static EntidadEntityCollection Save(EntidadEntityCollection entidadCollection )
        {
            return Save(entidadCollection, null, null);
        }
        
        public static EntidadEntityCollection Save(EntidadEntityCollection entidadCollection , SqlConnection connection, SqlTransaction transaction)
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

                    foreach (EntidadEntity entidad in entidadCollection)
                    {
                        EntidadBussinesAction.Save(entidad , connection, transaction);
                    }
                    
                    if (isBAParent && transaction != null) 
                    {
                    	transaction.Commit();
                    	entidadCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                    }

                    return entidadCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( entidadCollection != null)  entidadCollection.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>
        
        public static EntidadEntityCollection FindByAll(EntidadFindParameterEntity findParameter )
        {
        	return FindByAll(findParameter,null,null,1);
        }
        
        public static EntidadEntityCollection FindByAll(EntidadFindParameterEntity findParameter ,int deepLoadLevel)
        {
        	return FindByAll(findParameter,null,null,deepLoadLevel);
        }
        
        public static EntidadEntityCollection FindByAll(EntidadFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAll(findParameter,connection,transaction,1);
        }
        
        public static EntidadEntityCollection FindByAll(EntidadFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			EntidadEntityCollection entidadCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   entidadCollection  = EntidadDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   entidadCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return entidadCollection;
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
        
        public static EntidadEntityCollection FindByAllPaged(EntidadFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
        }
        
        public static EntidadEntityCollection FindByAllPaged(EntidadFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
        }
        
        public static EntidadEntityCollection FindByAllPaged(EntidadFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
        }
        
        public static EntidadEntityCollection FindByAllPaged(EntidadFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			EntidadEntityCollection entidadCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   entidadCollection  = EntidadDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
                   entidadCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return entidadCollection;
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

