    
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
    public partial class ProductoBussinesAction
    {
         
//       #region Implementation
        
//       public static ProductoEntity Save(ProductoEntity producto )
//       {   
//            return Save(producto,null, null);
//       }
       
//       public static ProductoEntity Save(ProductoEntity producto , SqlConnection connection, SqlTransaction transaction)
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

//*/
//                    switch (producto.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            ProductoDataAccess.Delete(producto, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            ProductoDataAccess.Update(producto, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            producto = ProductoDataAccess.Insert(producto, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					producto.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return producto;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( producto != null)  producto.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static ProductoEntity LoadByPK(int Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static ProductoEntity LoadByPK(int Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static ProductoEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static ProductoEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				ProductoEntity producto = ProductoDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(producto!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
	
//	                }
	                   
//						producto.SetLoadedState();
//				}

//				return producto;
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


