    
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
    public partial class LocalBodegaBussinesAction
    {
         
//       #region Implementation
        
//       public static LocalBodegaEntity Save(LocalBodegaEntity localBodega )
//       {   
//            return Save(localBodega,null, null);
//       }
       
//       public static LocalBodegaEntity Save(LocalBodegaEntity localBodega , SqlConnection connection, SqlTransaction transaction)
//       {
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

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
///*
//					if( localBodega.IdEmpresaAsEmpresa != null && localBodega.IdEmpresaAsEmpresa.CanSave )
//					{
//						localBodega.IdEmpresa = EmpresaBussinesAction.Save(localBodega.IdEmpresaAsEmpresa , connection,transaction).Id;
//					}


//*/
//                    switch (localBodega.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            LocalBodegaDataAccess.Delete(localBodega, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            LocalBodegaDataAccess.Update(localBodega, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            localBodega = LocalBodegaDataAccess.Insert(localBodega, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					localBodega.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return localBodega;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( localBodega != null)  localBodega.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static LocalBodegaEntity LoadByPK(int Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static LocalBodegaEntity LoadByPK(int Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static LocalBodegaEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static LocalBodegaEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				LocalBodegaEntity localBodega = LocalBodegaDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(localBodega!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
//							localBodega.IdEmpresaAsEmpresa = EmpresaBussinesAction.LoadByPK(localBodega.IdEmpresa, connection , transaction , deepLoadLevel - 1);

//	                }
	                   
//						localBodega.SetLoadedState();
//				}

//				return localBodega;
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
        
         
//        #endregion Implementation
          
     }
}


