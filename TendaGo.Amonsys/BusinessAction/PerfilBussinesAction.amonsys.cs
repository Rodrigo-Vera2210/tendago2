    
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
    public partial class PerfilBussinesAction
    {
         
//       #region Implementation
        
//       public static PerfilEntity Save(PerfilEntity perfil )
//       {   
//            return Save(perfil,null, null);
//       }
       
//       public static PerfilEntity Save(PerfilEntity perfil , SqlConnection connection, SqlTransaction transaction)
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
//                    switch (perfil.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            PerfilDataAccess.Delete(perfil, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            PerfilDataAccess.Update(perfil, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            perfil = PerfilDataAccess.Insert(perfil, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					perfil.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return perfil;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( perfil != null)  perfil.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static PerfilEntity LoadByPK(short Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static PerfilEntity LoadByPK(short Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static PerfilEntity LoadByPK(short Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static PerfilEntity LoadByPK(short Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				PerfilEntity perfil = PerfilDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(perfil!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
	
//	                }
	                   
//						perfil.SetLoadedState();
//				}

//				return perfil;
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


