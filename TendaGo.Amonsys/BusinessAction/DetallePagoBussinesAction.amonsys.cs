    
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
    public partial class DetallePagoBussinesAction
    {
         
//       #region Implementation
        
//       public static DetallePagoEntity Save(DetallePagoEntity detallePago )
//       {   
//            return Save(detallePago,null, null);
//       }
       
//       public static DetallePagoEntity Save(DetallePagoEntity detallePago , SqlConnection connection, SqlTransaction transaction)
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
//					if( detallePago.IdPagoCreditoAsPagoCredito != null && detallePago.IdPagoCreditoAsPagoCredito.CanSave )
//					{
//						detallePago.IdPagoCredito = PagoCreditoBussinesAction.Save(detallePago.IdPagoCreditoAsPagoCredito , connection,transaction).Id;
//					}

//					if( detallePago.IdCuentaPorPagarAsCuentaPorPagar != null && detallePago.IdCuentaPorPagarAsCuentaPorPagar.CanSave )
//					{
//						detallePago.IdCuentaPorPagar = CuentaPorPagarBussinesAction.Save(detallePago.IdCuentaPorPagarAsCuentaPorPagar , connection,transaction).Id;
//					}


//*/
//                    switch (detallePago.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            DetallePagoDataAccess.Delete(detallePago, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            DetallePagoDataAccess.Update(detallePago, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            detallePago = DetallePagoDataAccess.Insert(detallePago, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					detallePago.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return detallePago;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( detallePago != null)  detallePago.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static DetallePagoEntity LoadByPK(int Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static DetallePagoEntity LoadByPK(int Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static DetallePagoEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static DetallePagoEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				DetallePagoEntity detallePago = DetallePagoDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(detallePago!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
//							detallePago.IdPagoCreditoAsPagoCredito = PagoCreditoBussinesAction.LoadByPK(detallePago.IdPagoCredito, connection , transaction , deepLoadLevel - 1);
//						detallePago.IdCuentaPorPagarAsCuentaPorPagar = CuentaPorPagarBussinesAction.LoadByPK(detallePago.IdCuentaPorPagar, connection , transaction , deepLoadLevel - 1);

//	                }
	                   
//						detallePago.SetLoadedState();
//				}

//				return detallePago;
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


