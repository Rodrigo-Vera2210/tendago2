    
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
    public partial class EntradaCollectionUIAdapter
    {
    
  
    
        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        #region Implementation
        
        public static EntradaFindParameterEntity CreateNewFindParameter()
        {
        	return new EntradaFindParameterEntity();
        }
        
        public static EntradaEntityCollection Save(EntradaEntityCollection entradaCollection )
        {
          
            try
            {

               
               
              return EntradaCollectionBussinesAction.Save(entradaCollection);
              
            }
            catch (Exception exc)
            {
               
                throw exc;
            }
            finally
            {
              
            }
        }

        
        public static EntradaEntityCollection FindByAll(EntradaFindParameterEntity findParameter )
        {
        	  
               
              return FindByAll(findParameter,1);
              
        	  
        }
        
        public static EntradaEntityCollection FindByAll(EntradaFindParameterEntity findParameter ,int deepLoadLevel)
        {
            try
            {
               
               
              return EntradaCollectionBussinesAction.FindByAll( findParameter, deepLoadLevel);
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }
        
        public static EntradaEntityCollection FindByAllPaged(EntradaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy )
        {
        	  
        	  
        	   
              return FindByAllPaged(findParameter,pageNumber,pageSize,orderBy,1);
              
              
        }
        
        public static EntradaEntityCollection FindByAllPaged(EntradaFindParameterEntity findParameter ,int pageNumber, int pageSize , string orderBy, int deepLoadLevel)
        {
            try
            {
               
               
               
              return EntradaCollectionBussinesAction.FindByAllPaged( findParameter,pageNumber,pageSize,orderBy,deepLoadLevel);
              
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

