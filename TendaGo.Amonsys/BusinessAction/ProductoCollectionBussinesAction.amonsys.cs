    
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
    public partial class ProductoCollectionBussinesAction
    {



        #region << Custom Stored Procedures >>
        public static ProductoEntityCollection SearchProducts(int empresaId, string searchTerm, bool parentsOnly = false, SqlConnection connection = null, SqlTransaction transaction = null, int? page = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            ProductoEntityCollection productoCollection = null;

            try
            {

                //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                //                {
                productoCollection = ProductoDataAccessCollection.SearchProducts(empresaId, searchTerm, parentsOnly, connection, transaction, page);

                productoCollection.SetState(EntityStatesEnum.Loaded);
                //                    transactionScope.Complete();
                //
                //                }  //End of Transaction

                return productoCollection;
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

        public static DataSet SalesByProduct(int empresaId, int idLocal, DateTime desde, DateTime hasta, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
            }

            try
            {
                return ProductoDataAccessCollection.SalesByProduct(empresaId, idLocal, desde, hasta, connection, transaction);
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


        public static ProductoEntityCollection SearchProductsByLocal(int empresaId, int idLocal, string searchTerm, SqlConnection connection = null, SqlTransaction transaction = null, int? page = null, string productType = null)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            ProductoEntityCollection productoCollection = null;

            try
            {

                //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                //                {
                productoCollection = ProductoDataAccessCollection.SearchProductsByLocal(empresaId, idLocal, searchTerm, connection, transaction, page, productType);

                productoCollection.SetState(EntityStatesEnum.Loaded);
                //                    transactionScope.Complete();
                //
                //                }  //End of Transaction

                return productoCollection;
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

        #endregion

        #region Implementation

        public static ProductoEntityCollection Save(ProductoEntityCollection productoCollection )
        {
            return Save(productoCollection, null, null);
        }
        
        public static ProductoEntityCollection Save(ProductoEntityCollection productoCollection , SqlConnection connection, SqlTransaction transaction)
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

                    foreach (ProductoEntity producto in productoCollection)
                    {
                        ProductoBussinesAction.Save(producto , connection, transaction);
                    }
                    
                    if (isBAParent && transaction != null) 
                    {
                    	transaction.Commit();
                    	productoCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                    }

                    return productoCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if ( productoCollection != null)  productoCollection.RollBackState();
                    
                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>
        
        public static ProductoEntityCollection FindByAll(ProductoFindParameterEntity findParameter )
        {
        	return FindByAll(findParameter,null,null,1);
        }
        
        public static ProductoEntityCollection FindByAll(ProductoFindParameterEntity findParameter ,int deepLoadLevel)
        {
        	return FindByAll(findParameter,null,null,deepLoadLevel);
        }
        
        public static ProductoEntityCollection FindByAll(ProductoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAll(findParameter,connection,transaction,1);
        }
        
        public static ProductoEntityCollection FindByAll(ProductoFindParameterEntity findParameter , SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			ProductoEntityCollection productoCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   productoCollection  = ProductoDataAccessCollection.FindByAll(findParameter , connection, transaction, deepLoadLevel);
                   
				if (productoCollection!=null && deepLoadLevel > 1)
                {
                	foreach (ProductoEntity producto in productoCollection)
                    {

                    }

                }

                   
                   productoCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return productoCollection;
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
        
        #endregion
        
        #region << Find by All Paged >>
        
        public static ProductoEntityCollection FindByAllPaged(ProductoFindParameterEntity findParameter, int pageNumber, int pageSize ,string orderBy)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize,orderBy, null,null,1);
        }
        
        public static ProductoEntityCollection FindByAllPaged(ProductoFindParameterEntity findParameter , int pageNumber, int pageSize,string orderBy, int deepLoadLevel)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, null,null,deepLoadLevel);
        }
        
        public static ProductoEntityCollection FindByAllPaged(ProductoFindParameterEntity findParameter , int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction)
        {
        	return FindByAllPaged(findParameter, pageNumber, pageSize, orderBy, connection,transaction,1);
        }
        
        public static ProductoEntityCollection FindByAllPaged(ProductoFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy, SqlConnection connection,  SqlTransaction transaction,int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

			ProductoEntityCollection productoCollection = null; 

            try
            {

//                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
//                {
                   productoCollection  = ProductoDataAccessCollection.FindByAllPaged(findParameter , pageNumber, pageSize, orderBy, connection, transaction, deepLoadLevel);
                   productoCollection.SetState(EntityStatesEnum.Loaded);
//                    transactionScope.Complete();
//
//                }  //End of Transaction

                return productoCollection;
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
        
        #endregion

      
        #endregion Implementation
        
          
     }
}

