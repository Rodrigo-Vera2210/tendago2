    
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



using System;
using System.Collections.Generic;
using System.Text;

namespace ER.BE
{
   
     [System.Serializable]         
     public partial class RucEntityCollection : List<RucEntity>
    {
    
	
		#region << Get Parents Methods >>
		
		public EmpresaEntityCollection GetEmpresaFromIdEmpresa()
		{
			EmpresaEntityCollection parentCollection = new EmpresaEntityCollection();

			foreach(RucEntity entity in this)
			{
				if(entity.IdEmpresaAsEmpresa != null)
				{
					if(parentCollection.GetByPK(entity.IdEmpresaAsEmpresa.Id) == null)
					{
						parentCollection.Add(entity.IdEmpresaAsEmpresa);
					}
				}
			}

			return parentCollection;
		}

		
		#endregion

		#region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
			foreach (RucEntity entity in this)
            {
                entity.SetState(state);
            }          
        }
        
        public void RollBackState()
        {
            foreach (RucEntity entity in this)
            {
                entity.RollBackState();
            }
        }
        
        #endregion
        
        public RucEntity GetByPK(string Ruc)
        {
            foreach (RucEntity entity in this)
            {
                if(entity.Ruc == Ruc)
                {
                	return entity;
                }
            }
            
            return null;
        }
        
        
        public RucEntityCollection CreateCollection()
        {
        	return new RucEntityCollection();
        }
        
          
    }
}
