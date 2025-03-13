    
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
    public partial class UnidadMedidaCollectionBussinesAction
    {
    
  
    
        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        #region Implementation
        
        public static UnidadMedidaEntityCollection Save(UnidadMedidaEntityCollection unidadMedidaCollection )
        {
            return Save(unidadMedidaCollection, null, null);
        }
        
        public static UnidadMedidaEntityCollection Save(UnidadMedidaEntityCollection unidadMedidaCollection , SqlConnection connection, SqlTransaction transaction)
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

                    foreach (UnidadMedidaEntity unidadMedida in unidadMedidaCollection)
                    {
                        UnidadMedidaBussinesAction.Save(unidadMedida , connection, transaction);
                    }
                    
                    if (isBAParent && transaction != null) 
                    {
                    	transaction.Commit();
                    	unidadMedidaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                    }

                    return unidadMedidaCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( unidadMedidaCollection != null)  unidadMedidaCollection.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>
        
        public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter )
        {
        	return FindByAll(findParameter,null,null,1);
        }
        
        public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter ,int deepLoadLevel)
        {
        	return FindByAll(findParameter,null,null,deepLoadLevel);
        }
        
        public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAll(findParameter,connection,transaction,1);
        }
        
        public static UnidadMedidaEntityCollection FindByAll(UnidadMedidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			UnidadMedidaEntityCollection unidadMedidaCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   unidadMedidaCollection  = UnidadMedidaDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
				if (unidadMedidaCollection!=null && deepLoadLevel > 1)
                {
                	foreach (UnidadMedidaEntity unidadMedida in unidadMedidaCollection)
                    {

                    }

                }

                   
                   unidadMedidaCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return unidadMedidaCollection;
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
        
        public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
        }
        
        public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
        }
        
        public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
        }
        
        public static UnidadMedidaEntityCollection FindByAllPaged(UnidadMedidaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			UnidadMedidaEntityCollection unidadMedidaCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   unidadMedidaCollection  = UnidadMedidaDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
                   unidadMedidaCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return unidadMedidaCollection;
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

