    
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
     public partial class MarcaEntityCollection : List<MarcaEntity>
    {
    
	
		#region << Get Parents Methods >>
		
		
		#endregion

		#region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
			foreach (MarcaEntity entity in this)
            {
                entity.SetState(state);
            }          
        }
        
        public void RollBackState()
        {
            foreach (MarcaEntity entity in this)
            {
                entity.RollBackState();
            }
        }
        
        #endregion
        
        public MarcaEntity GetByPK(int Id)
        {
            foreach (MarcaEntity entity in this)
            {
                if(entity.Id == Id)
                {
                	return entity;
                }
            }
            
            return null;
        }
        
        
        public MarcaEntityCollection CreateCollection()
        {
        	return new MarcaEntityCollection();
        }
        
          
    }
}
