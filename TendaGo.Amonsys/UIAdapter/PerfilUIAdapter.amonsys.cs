    
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
    public partial class PerfilUIAdapter
    {
         
       #region Implementation
        
       public static PerfilEntity Save(PerfilEntity perfil)
       {
            try
            {
             
              return PerfilBussinesAction.Save(perfil);
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }

  
         
         
        public static PerfilEntity LoadByPK(short Id)
        {
            try
            {
              
              return PerfilBussinesAction.LoadByPK(Id);
              
              
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                
            }
        }
        
        public static PerfilEntity LoadByPK(short Id , int deepLoadLevel)
        {
            try
            {
               
              
              return PerfilBussinesAction.LoadByPK(Id, deepLoadLevel );
              
               
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


