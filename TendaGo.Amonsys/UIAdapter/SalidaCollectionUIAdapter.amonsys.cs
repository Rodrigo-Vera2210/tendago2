    
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
    public partial class SalidaCollectionUIAdapter
    {
    
  
    
        #region << Custom Stored Procedures >>
        
        
        #endregion
        
        #region Implementation
        
        public static SalidaFindParameterEntity CreateNewFindParameter()
        {
        	return new SalidaFindParameterEntity();
        }
        
        public static SalidaEntityCollection Save(SalidaEntityCollection salidaCollection )
        {
          
            try
            {

               
               
              return SalidaCollectionBussinesAction.Save(salidaCollection);
              
            }
            catch (Exception exc)
            {
               
                throw exc;
            }
            finally
            {
              
            }
        }

        
        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter )
        {
        	  
               
              return FindByAll(findParameter,1);
              
        	  
        }
        
        public static SalidaEntityCollection FindByAll(SalidaFindParameterEntity findParameter ,int deepLoadLevel)
        {
            try
            {
               
               
              return SalidaCollectionBussinesAction.FindByAll( findParameter, deepLoadLevel);
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }
        
        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter ,int pageNumber, int pageSize, string orderBy )
        {
        	  
        	  
        	   
              return FindByAllPaged(findParameter,pageNumber,pageSize,orderBy,1);
              
              
        }
        
        public static SalidaEntityCollection FindByAllPaged(SalidaFindParameterEntity findParameter ,int pageNumber, int pageSize , string orderBy, int deepLoadLevel)
        {
            try
            {
               
               
               
              return SalidaCollectionBussinesAction.FindByAllPaged( findParameter,pageNumber,pageSize,orderBy,deepLoadLevel);
              
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

