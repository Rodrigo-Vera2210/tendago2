    
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
    public partial class PantallaXPerfilBussinesAction
    {
         
       #region Implementation
        
       public static PantallaXPerfilEntity Save(PantallaXPerfilEntity pantallaXPerfil )
       {   
            return Save(pantallaXPerfil,null, null);
       }
       
       public static PantallaXPerfilEntity Save(PantallaXPerfilEntity pantallaXPerfil , SqlConnection connection, SqlTransaction transaction)
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
					if( pantallaXPerfil.IdPerfilAsPerfil != null && pantallaXPerfil.IdPerfilAsPerfil.CanSave )
					{
						pantallaXPerfil.IdPerfil = PerfilBussinesAction.Save(pantallaXPerfil.IdPerfilAsPerfil , connection,transaction).Id;
					}

					if( pantallaXPerfil.IdPantallaAsPantalla != null && pantallaXPerfil.IdPantallaAsPantalla.CanSave )
					{
						pantallaXPerfil.IdPantalla = PantallaBussinesAction.Save(pantallaXPerfil.IdPantallaAsPantalla , connection,transaction).Id;
					}


*/
                    switch (pantallaXPerfil.CurrentState)
                    {
                        case EntityStatesEnum.Deleted:
                            PantallaXPerfilDataAccess.Delete(pantallaXPerfil, connection, transaction);
                            break;
                        case EntityStatesEnum.Updated:
                            PantallaXPerfilDataAccess.Update(pantallaXPerfil, connection, transaction);
                            break;
                        case EntityStatesEnum.New:
                            pantallaXPerfil = PantallaXPerfilDataAccess.Insert(pantallaXPerfil, connection, transaction);
                            break;
                        default:
                            break;
                    }
                    
                    

//                } 
               
               //End of Transaction
               if (isBAParent && transaction != null)
               {
					transaction.Commit();
					pantallaXPerfil.SetState(EntityStatesEnum.SavedSuccessfully);
               }
               
               return pantallaXPerfil;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( pantallaXPerfil != null)  pantallaXPerfil.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

  
         
         
         
        public static PantallaXPerfilEntity LoadByPK(short Id)
        {
            return LoadByPK(Id , null, null, 1);
        }
        public static PantallaXPerfilEntity LoadByPK(short Id ,int deepLoadLevel)
        {
            return LoadByPK(Id , null, null, deepLoadLevel);
        }
        
        public static PantallaXPerfilEntity LoadByPK(short Id, SqlConnection connection,SqlTransaction  transaction)
        {
            return LoadByPK(Id , connection, transaction, 1);
        }
        
        public static PantallaXPerfilEntity LoadByPK(short Id , SqlConnection connection,SqlTransaction  transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }
            
            try
            {

                
				PantallaXPerfilEntity pantallaXPerfil = PantallaXPerfilDataAccess.LoadByPK(Id , connection, transaction, deepLoadLevel);
				if(pantallaXPerfil!=null) 
                {
					if (deepLoadLevel > 1)
	                {
							pantallaXPerfil.IdPerfilAsPerfil = PerfilBussinesAction.LoadByPK(pantallaXPerfil.IdPerfil, connection , transaction , deepLoadLevel - 1);
						pantallaXPerfil.IdPantallaAsPantalla = PantallaBussinesAction.LoadByPK(pantallaXPerfil.IdPantalla, connection , transaction , deepLoadLevel - 1);

	                }
	                   
						pantallaXPerfil.SetLoadedState();
				}

				return pantallaXPerfil;
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


