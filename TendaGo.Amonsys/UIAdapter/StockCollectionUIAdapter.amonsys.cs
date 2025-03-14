    
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
using System.Net;
using ER.BE; 
using ER.BA; 
using System.Transactions;
using System.Configuration;


#endregion using

namespace ER.UIAdapter
{
    public partial class StockCollectionUIAdapter
    {
    
  
    
        #region << Custom Stored Procedures >>
        
		public static void CostoPromedioPonderado(string Id,string Tipo,int IdEmpresa,int IdProducto,int IdLocal,string IdSalidaEntrada,string IdDetalle,decimal Cantidad,decimal ValorUnitario,int IdTipoUnidad,short IdEstado )
		{
			try
			{
				StockCollectionBussinesAction.CostoPromedioPonderado(Id,Tipo,IdEmpresa,IdProducto,IdLocal,IdSalidaEntrada,IdDetalle,Cantidad,ValorUnitario,IdTipoUnidad,IdEstado);
			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
			}
		}

		public static DataSet SaldoInventario(string Id,int IdEmpresa,int IdProducto,int IdLocal )
		{
			try
			{
				return StockCollectionBussinesAction.SaldoInventario(Id,IdEmpresa,IdProducto,IdLocal);
			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
			}
		}

        
        #endregion
        
        #region Implementation
        
        public static StockFindParameterEntity CreateNewFindParameter()
        {
        	return new StockFindParameterEntity();
        }
        
        public static StockEntityCollection Save(StockEntityCollection stockCollection )
        {
          
            try
            {

               
               
              return StockCollectionBussinesAction.Save(stockCollection);
              
            }
            catch (Exception exc)
            {
               
                throw exc;
            }
            finally
            {
              
            }
        }

        
        public static StockEntityCollection FindByAll(StockFindParameterEntity findParameter )
        {
        	  
               
              return FindByAll(findParameter,1);
              
        	  
        }
        
        public static StockEntityCollection FindByAll(StockFindParameterEntity findParameter ,int deepLoadLevel)
        {
            try
            {
               
               
              return StockCollectionBussinesAction.FindByAll( findParameter, deepLoadLevel);
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }
        
        public static StockEntityCollection FindByAllPaged(StockFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy )
        {
        	  
        	  
        	   
              return FindByAllPaged(findParameter,pageNumber,pageSize,orderBy,1);
              
              
        }
        
        public static StockEntityCollection FindByAllPaged(StockFindParameterEntity findParameter ,int pageNumber, int pageSize , string orderBy, int deepLoadLevel)
        {
            try
            {
               
               
               
              return StockCollectionBussinesAction.FindByAllPaged( findParameter,pageNumber,pageSize,orderBy,deepLoadLevel);
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }
        

      
        #endregion Implementation
        
          
     }
}

