    
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
    public partial class LineaCollectionBussinesAction
    {
    
  
    
//        #region << Custom Stored Procedures >>
        
        
//        #endregion
        
//        #region Implementation
        
//        public static LineaEntityCollection Save(LineaEntityCollection lineaCollection )
//        {
//            return Save(lineaCollection, null, null);
//        }
        
//        public static LineaEntityCollection Save(LineaEntityCollection lineaCollection , SqlConnection connection, SqlTransaction transaction)
//        {
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

//                    foreach (LineaEntity linea in lineaCollection)
//                    {
//                        LineaBussinesAction.Save(linea , connection, transaction);
//                    }
                    
//                    if (isBAParent && transaction != null) 
//                    {
//                    	transaction.Commit();
//                    	lineaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
//                    }

//                    return lineaCollection;
//            }
//            catch (Exception exc)
//            {
//                if (isBAParent && transaction != null)
//                {
//                    transaction.Rollback();
//                    if ( lineaCollection != null)  lineaCollection.RollBackState();
                    
//                }
//                throw exc;
//            }
//            finally
//            {
//                if (isBAParent) connection.Close();
//            }
//        }

//        #region << Find by All >>
        
//        public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter )
//        {
//        	return FindByAll(findParameter,null,null,1);
//        }
        
//        public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter ,int deepLoadLevel)
//        {
//        	return FindByAll(findParameter,null,null,deepLoadLevel);
//        }
        
//        public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAll(findParameter,connection,transaction,1);
//        }
        
//        public static LineaEntityCollection FindByAll(LineaFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			LineaEntityCollection lineaCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   lineaCollection  = LineaDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
//				if (lineaCollection!=null && deepLoadLevel > 1)
//                {
//                	foreach (LineaEntity linea in lineaCollection)
//                    {
//						linea.IdDivisionAsDivision = DivisionBussinesAction.LoadByPK(linea.IdDivision, connection, transaction, deepLoadLevel - 1);

//                    }

//                }

                   
//                   lineaCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return lineaCollection;
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
        
//        #endregion
        
//        #region << Find by All Paged >>
        
//        public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
//        }
        
//        public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
//        }
        
//        public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
//        {
//        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
//        }
        
//        public static LineaEntityCollection FindByAllPaged(LineaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
//        {
//            bool isBAParent = false;
//            if (connection == null)
//            {
//                isBAParent = true;
//                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

//            }

//			LineaEntityCollection lineaCollection = null; 

//            try
//            {

////                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
////                {
//                   lineaCollection  = LineaDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
//                   lineaCollection.SetState(EntityStatesEnum.Loaded);
////                    transactionScope.Complete();
////
////                }  //End of Transaction

//                return lineaCollection;
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
        
//        #endregion

      
//        #endregion Implementation
        
          
     }
}

