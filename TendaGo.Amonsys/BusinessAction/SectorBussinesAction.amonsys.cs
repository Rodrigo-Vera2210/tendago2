    
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
    public partial class SectorBussinesAction
    {
         
//       #region Implementation
        
//       public static SectorEntity Save(SectorEntity sector )
//       {   
//            return Save(sector,null, null);
//       }
       
//       public static SectorEntity Save(SectorEntity sector , SqlConnection connection, SqlTransaction transaction)
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
//                    switch (sector.CurrentState)
//                    {
//                        case EntityStatesEnum.Deleted:
//                            SectorDataAccess.Delete(sector, connection, transaction);
//                            break;
//                        case EntityStatesEnum.Updated:
//                            SectorDataAccess.Update(sector, connection, transaction);
//                            break;
//                        case EntityStatesEnum.New:
//                            sector = SectorDataAccess.Insert(sector, connection, transaction);
//                            break;
//                        default:
//                            break;
//                    }
                    
                    

////                } 
               
//               //End of Transaction
//               if (isBAParent && transaction != null)
//               {
//					transaction.Commit();
//					sector.SetState(EntityStatesEnum.SavedSuccessfully);
//               }
               
//               return sector;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( sector != null)  sector.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

  
         
         
         
//        public static SectorEntity LoadByPK(int Id)
//        {
//            return LoadByPK(Id , null, null, 1);
//        }
//        public static SectorEntity LoadByPK(int Id ,int deepLoadLevel)
//        {
//            return LoadByPK(Id , null, null, deepLoadLevel);
//        }
        
//        public static SectorEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
//        {
//            return LoadByPK(Id , connection, transaction, 1);
//        }
        
//        public static SectorEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }
            
//            try
//            {

                
//				SectorEntity sector = SectorDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
//				if(sector!=null) 
//                {
//					if (deepLoadLevel > 1)
//	                {
	
//	                }
	                   
//						sector.SetLoadedState();
//				}

//				return sector;
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


