    
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
    public partial class MarcaBussinesAction
    {
         
       #region Implementation
        
       public static MarcaEntity Save(MarcaEntity marca )
       {   
            return Save(marca,null, null);
       }
       
       public static MarcaEntity Save(MarcaEntity marca , SqlConnection connection, SqlTransaction transaction)
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

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
/*

*/
                    switch (marca.CurrentState)
                    {
                        case EntityStatesEnum.Deleted:
                            MarcaDataAccess.Delete(marca, connection, transaction);
                            break;
                        case EntityStatesEnum.Updated:
                            MarcaDataAccess.Update(marca, connection, transaction);
                            break;
                        case EntityStatesEnum.New:
                            marca = MarcaDataAccess.Insert(marca, connection, transaction);
                            break;
                        default:
                            break;
                    }
                    
                    

//                } 
               
               //End of Transaction
               if (isBAParent && transaction != null)
               {
					transaction.Commit();
					marca.SetState(EntityStatesEnum.SavedSuccessfully);
               }
               
               return marca;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( marca != null)  marca.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

  
         
         
         
        public static MarcaEntity LoadByPK(int Id)
        {
            return LoadByPK(Id , null, null, 1);
        }
        public static MarcaEntity LoadByPK(int Id ,int deepLoadLevel)
        {
            return LoadByPK(Id , null, null, deepLoadLevel);
        }
        
        public static MarcaEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
        {
            return LoadByPK(Id , connection, transaction, 1);
        }
        
        public static MarcaEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }
            
            try
            {

                
				MarcaEntity marca = MarcaDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
				if(marca!=null) 
                {
					if (deepLoadLevel > 1)
	                {
	
	                }
	                   
						marca.SetLoadedState();
				}

				return marca;
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
        
         
        #endregion Implementation
          
     }
}


