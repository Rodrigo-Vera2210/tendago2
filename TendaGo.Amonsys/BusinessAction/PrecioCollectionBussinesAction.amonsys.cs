    
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
    public partial class PrecioCollectionBussinesAction
    {
    
  
    
//        #region << Custom Stored Procedures >>
        
        
//        #endregion
        
//        #region Implementation
        
//        public static PrecioEntityCollection Save(PrecioEntityCollection precioCollection )
//        {
//            return Save(precioCollection, null, null);
//        }
        
//        public static PrecioEntityCollection Save(PrecioEntityCollection precioCollection , SqlConnection connection, SqlTransaction transaction)
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

//                    foreach (PrecioEntity precio in precioCollection)
//                    {
//                        PrecioBussinesAction.Save(precio , connection, transaction);
//                    }
                    
//                    if (isBAParent && transaction != null) 
//                    {
//                    	transaction.Commit();
//                    	precioCollection.SetState(EntityStatesEnum.SavedSuccessfully);
//                    }

//                    return precioCollection;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( precioCollection != null)  precioCollection.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #region << Find by All >>
        
//        public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter )
//        {
//        	return FindByAll(findParameter,null,null,1);
//        }
        
//        public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter ,int deepLoadLevel)
//        {
//        	return FindByAll(findParameter,null,null,deepLoadLevel);
//        }
        
//        public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAll(findParameter,connection,transaction,1);
//        }
        
//        public static PrecioEntityCollection FindByAll(PrecioFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			PrecioEntityCollection precioCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   precioCollection  = PrecioDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
//				if (precioCollection!=null && deepLoadLevel > 1)
//                {
//                	foreach (PrecioEntity precio in precioCollection)
//                    {
//						precio.IdProductoAsProducto = ProductoBussinesAction.LoadByPK(precio.IdProducto, connection, transaction, deepLoadLevel - 1);
//						precio.IdTipoUnidadAsTipoUnidad = TipoUnidadBussinesAction.LoadByPK(precio.IdTipoUnidad, connection, transaction, deepLoadLevel - 1);

//                    }

//                }

                   
//                   precioCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return precioCollection;
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
        
//        public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
//        }
        
//        public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
//        }
        
//        public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
//        }
        
//        public static PrecioEntityCollection FindByAllPaged(PrecioFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			PrecioEntityCollection precioCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   precioCollection  = PrecioDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
//                   precioCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return precioCollection;
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

