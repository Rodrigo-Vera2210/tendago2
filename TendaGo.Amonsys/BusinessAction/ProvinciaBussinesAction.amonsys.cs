    
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
    public partial class ProvinciaBussinesAction
    {
         
       #region Implementation
        
       public static ProvinciaEntity Save(ProvinciaEntity provincia )
       {   
            return Save(provincia,null, null);
       }
       
       public static ProvinciaEntity Save(ProvinciaEntity provincia , SqlConnection connection, SqlTransaction transaction)
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
					if( provincia.IdPaisAsPais != null && provincia.IdPaisAsPais.CanSave )
					{
						provincia.IdPais = PaisBussinesAction.Save(provincia.IdPaisAsPais , connection,transaction).Id;
					}


*/
                    switch (provincia.CurrentState)
                    {
                        case EntityStatesEnum.Deleted:
                            ProvinciaDataAccess.Delete(provincia, connection, transaction);
                            break;
                        case EntityStatesEnum.Updated:
                            ProvinciaDataAccess.Update(provincia, connection, transaction);
                            break;
                        case EntityStatesEnum.New:
                            provincia = ProvinciaDataAccess.Insert(provincia, connection, transaction);
                            break;
                        default:
                            break;
                    }
                    
                    

//                } 
               
               //End of Transaction
               if (isBAParent && transaction != null)
               {
					transaction.Commit();
					provincia.SetState(EntityStatesEnum.SavedSuccessfully);
               }
               
               return provincia;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( provincia != null)  provincia.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

  
         
         
         
        public static ProvinciaEntity LoadByPK(int Id)
        {
            return LoadByPK(Id , null, null, 1);
        }
        public static ProvinciaEntity LoadByPK(int Id ,int deepLoadLevel)
        {
            return LoadByPK(Id , null, null, deepLoadLevel);
        }
        
        public static ProvinciaEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
        {
            return LoadByPK(Id , connection, transaction, 1);
        }
        
        public static ProvinciaEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }
            
            try
            {

                
				ProvinciaEntity provincia = ProvinciaDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
				if(provincia!=null) 
                {
					if (deepLoadLevel > 1)
	                {
							provincia.IdPaisAsPais = PaisBussinesAction.LoadByPK(provincia.IdPais, connection , transaction , deepLoadLevel - 1);

	                }
	                   
						provincia.SetLoadedState();
				}

				return provincia;
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


