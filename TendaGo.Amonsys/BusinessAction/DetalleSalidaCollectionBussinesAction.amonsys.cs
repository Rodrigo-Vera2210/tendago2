    
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
    public partial class DetalleSalidaCollectionBussinesAction
    {
    
  
    
//        #region << Custom Stored Procedures >>
        
        
//        #endregion
        
//        #region Implementation
        
//        public static DetalleSalidaEntityCollection Save(DetalleSalidaEntityCollection detalleSalidaCollection )
//        {
//            return Save(detalleSalidaCollection, null, null);
//        }
        
//        public static DetalleSalidaEntityCollection Save(DetalleSalidaEntityCollection detalleSalidaCollection , SqlConnection connection, SqlTransaction transaction)
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

//                    foreach (DetalleSalidaEntity detalleSalida in detalleSalidaCollection)
//                    {
//                        DetalleSalidaBussinesAction.Save(detalleSalida , connection, transaction);
//                    }
                    
//                    if (isBAParent && transaction != null) 
//                    {
//                    	transaction.Commit();
//                    	detalleSalidaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
//                    }

//                    return detalleSalidaCollection;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( detalleSalidaCollection != null)  detalleSalidaCollection.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #region << Find by All >>
        
//        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter )
//        {
//        	return FindByAll(findParameter,null,null,1);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter ,int deepLoadLevel)
//        {
//        	return FindByAll(findParameter,null,null,deepLoadLevel);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAll(findParameter,connection,transaction,1);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAll(DetalleSalidaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			DetalleSalidaEntityCollection detalleSalidaCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   detalleSalidaCollection  = DetalleSalidaDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
//				if (detalleSalidaCollection!=null && deepLoadLevel > 1)
//                {
//                	foreach (DetalleSalidaEntity detalleSalida in detalleSalidaCollection)
//                    {
//						detalleSalida.IdSalidaAsSalida = SalidaBussinesAction.LoadByPK(detalleSalida.IdSalida, connection, transaction, deepLoadLevel - 1);
//						detalleSalida.IdProveedorAsEntidad = EntidadBussinesAction.LoadByPK(detalleSalida.IdProveedor, connection, transaction, deepLoadLevel - 1);
//						detalleSalida.IdLocalAsLocalBodega = LocalBodegaBussinesAction.LoadByPK(detalleSalida.IdLocal, connection, transaction, deepLoadLevel - 1);
//						detalleSalida.IdTipoUnidadAsTipoUnidad = TipoUnidadBussinesAction.LoadByPK(detalleSalida.IdTipoUnidad, connection, transaction, deepLoadLevel - 1);

//                    }

//                }

                   
//                   detalleSalidaCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return detalleSalidaCollection;
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
        
//        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
//        }
        
//        public static DetalleSalidaEntityCollection FindByAllPaged(DetalleSalidaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			DetalleSalidaEntityCollection detalleSalidaCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   detalleSalidaCollection  = DetalleSalidaDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
//                   detalleSalidaCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return detalleSalidaCollection;
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

