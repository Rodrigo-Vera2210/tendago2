    
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
    public partial class SalidaCollectionBussinesAction
    {
    
  
    
//        #region << Custom Stored Procedures >>
        
        
//        #endregion
        
//        #region Implementation
        
//        public static SalidaEntityCollection Save(SalidaEntityCollection salidaCollection )
//        {
//            return Save(salidaCollection, null, null);
//        }
        
//        public static SalidaEntityCollection Save(SalidaEntityCollection salidaCollection , SqlConnection connection, SqlTransaction transaction)
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

//                    foreach (SalidaEntity salida in salidaCollection)
//                    {
//                        SalidaBussinesAction.Save(salida , connection, transaction);
//                    }
                    
//                    if (isBAParent && transaction != null) 
//                    {
//                    	transaction.Commit();
//                    	salidaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
//                    }

//                    return salidaCollection;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( salidaCollection != null)  salidaCollection.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #region << Find by All >>
        
//        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter )
//        {
//        	return FindByAll(findParameter,null,null,1);
//        }
        
//        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter ,int deepLoadLevel)
//        {
//        	return FindByAll(findParameter,null,null,deepLoadLevel);
//        }
        
//        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAll(findParameter,connection,transaction,1);
//        }
        
//        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			SalidaEntityCollection salidaCollection = null;

//            try
//            {

//                //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                //                {
//                salidaCollection = SalidaDataAccessCollection.FindByAll(findParameter, connection, transaction, deepLoadLevel);

//                //if (salidaCollection != null && deepLoadLevel > 1)
//                //{
//                //    foreach (SalidaEntity salida in salidaCollection)
//                //    {
//                //        salida.IdLocalAsLocalBodega = LocalBodegaBussinesAction.LoadByPK(salida.IdLocal, connection, transaction, deepLoadLevel - 1);
//                //        salida.IdClienteAsEntidad = EntidadBussinesAction.LoadByPK(salida.IdCliente, connection, transaction, deepLoadLevel - 1);

//                //    }

//                //}


//                salidaCollection.SetState(EntityStatesEnum.Loaded);
//                //                    transactionScope.Complete();
//                //
//                //                }  //End of Transaction

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
        
//        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
//        }
        
//        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
//        }
        
//        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
//        }
        
//        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			SalidaEntityCollection salidaCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   salidaCollection  = SalidaDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
//                   salidaCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

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

      
//        #endregion Implementation
        
          
     }
}

