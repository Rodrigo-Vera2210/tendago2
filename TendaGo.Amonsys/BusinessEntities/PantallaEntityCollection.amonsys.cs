    
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
     public partial class PantallaEntityCollection : List<PantallaEntity>
    {
    
	
		#region << Get Parents Methods >>
		
		public ModuloEntityCollection GetModuloFromIdModulo()
		{
			ModuloEntityCollection parentCollection = new ModuloEntityCollection();

			foreach(PantallaEntity entity in this)
			{
				if(entity.IdModuloAsModulo != null)
				{
					if(parentCollection.GetByPK(entity.IdModuloAsModulo.Id) == null)
					{
						parentCollection.Add(entity.IdModuloAsModulo);
					}
				}
			}

			return parentCollection;
		}

		public PantallaEntityCollection GetPantallaFromIdGrupo()
		{
			PantallaEntityCollection parentCollection = new PantallaEntityCollection();

			foreach(PantallaEntity entity in this)
			{
				if(entity.IdGrupoAsPantalla != null)
				{
					if(parentCollection.GetByPK(entity.IdGrupoAsPantalla.Id) == null)
					{
						parentCollection.Add(entity.IdGrupoAsPantalla);
					}
				}
			}

			return parentCollection;
		}

		
		#endregion

		#region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
			foreach (PantallaEntity entity in this)
            {
                entity.SetState(state);
            }          
        }
        
        public void RollBackState()
        {
            foreach (PantallaEntity entity in this)
            {
                entity.RollBackState();
            }
        }
        
        #endregion
        
        public PantallaEntity GetByPK(short Id)
        {
            foreach (PantallaEntity entity in this)
            {
                if(entity.Id == Id)
                {
                	return entity;
                }
            }
            
            return null;
        }
        
        
        public PantallaEntityCollection CreateCollection()
        {
        	return new PantallaEntityCollection();
        }
        
          
    }
}
