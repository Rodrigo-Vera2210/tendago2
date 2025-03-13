    
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
    public partial class PrecioBussinesAction
    {
         
       #region Implementation
        
       public static PrecioEntity Save(PrecioEntity precio )
       {   
            return Save(precio,null, null);
       }
       
       public static PrecioEntity Save(PrecioEntity precio , SqlConnection connection, SqlTransaction transaction)
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
					if( precio.IdProductoAsProducto != null && precio.IdProductoAsProducto.CanSave )
					{
						precio.IdProducto = ProductoBussinesAction.Save(precio.IdProductoAsProducto , connection,transaction).Id;
					}

					if( precio.IdTipoUnidadAsTipoUnidad != null && precio.IdTipoUnidadAsTipoUnidad.CanSave )
					{
						precio.IdTipoUnidad = TipoUnidadBussinesAction.Save(precio.IdTipoUnidadAsTipoUnidad , connection,transaction).Id;
					}


*/
                    switch (precio.CurrentState)
                    {
                        case EntityStatesEnum.Deleted:
                            PrecioDataAccess.Delete(precio, connection, transaction);
                            break;
                        case EntityStatesEnum.Updated:
                            PrecioDataAccess.Update(precio, connection, transaction);
                            break;
                        case EntityStatesEnum.New:
                            precio = PrecioDataAccess.Insert(precio, connection, transaction);
                            break;
                        default:
                            break;
                    }
                    
                    

//                } 
               
               //End of Transaction
               if (isBAParent && transaction != null)
               {
					transaction.Commit();
					precio.SetState(EntityStatesEnum.SavedSuccessfully);
               }
               
               return precio;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( precio != null)  precio.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

  
         
         
         
        public static PrecioEntity LoadByPK(int Id)
        {
            return LoadByPK(Id , null, null, 1);
        }
        public static PrecioEntity LoadByPK(int Id ,int deepLoadLevel)
        {
            return LoadByPK(Id , null, null, deepLoadLevel);
        }
        
        public static PrecioEntity LoadByPK(int Id, SqlConnection connection,SqlTransaction  transaction)
        {
            return LoadByPK(Id , connection, transaction, 1);
        }
        
        public static PrecioEntity LoadByPK(int Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }
            
            try
            {

                
				PrecioEntity precio = PrecioDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
				if(precio!=null) 
                {
					if (deepLoadLevel > 1)
	                {
							precio.IdProductoAsProducto = ProductoBussinesAction.LoadByPK(precio.IdProducto, connection , transaction , deepLoadLevel - 1);
						precio.IdTipoUnidadAsTipoUnidad = TipoUnidadBussinesAction.LoadByPK(precio.IdTipoUnidad, connection , transaction , deepLoadLevel - 1);

	                }
	                   
						precio.SetLoadedState();
				}

				return precio;
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


