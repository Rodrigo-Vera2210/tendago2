    
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
    public partial class EmpaquetadoBussinesAction
    {
         
//       #region Implementation
        
//       public static EmpaquetadoEntity Save(EmpaquetadoEntity empaquetado )
//       {   
//            return Save(empaquetado,null, null);
//       }
       
//       public static EmpaquetadoEntity Save(EmpaquetadoEntity empaquetado , SqlConnection connection, SqlTransaction transaction)
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
//					if( empaquetado.IdSalidaAsSalida != null && empaquetado.IdSalidaAsSalida.CanSave )
//					{
//						empaquetado.IdSalida = SalidaBussinesAction.Save(empaquetado.IdSalidaAsSalida , connection,transaction).Id;
//					}


//*/
//                    switch (empaquetado.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            EmpaquetadoDataAccess.Delete(empaquetado, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            EmpaquetadoDataAccess.Update(empaquetado, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            empaquetado = EmpaquetadoDataAccess.Insert(empaquetado, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					empaquetado.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return empaquetado;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( empaquetado != null)  empaquetado.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static EmpaquetadoEntity LoadByPK(string Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static EmpaquetadoEntity LoadByPK(string Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static EmpaquetadoEntity LoadByPK(string Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static EmpaquetadoEntity LoadByPK(string Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				EmpaquetadoEntity empaquetado = EmpaquetadoDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(empaquetado!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
//							empaquetado.IdSalidaAsSalida = SalidaBussinesAction.LoadByPK(empaquetado.IdSalida, connection , transaction , deepLoadLevel - 1);

//	                }
	                   
//						empaquetado.SetLoadedState();
//				}

//				return empaquetado;
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


