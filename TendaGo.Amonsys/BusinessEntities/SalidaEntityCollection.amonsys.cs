    
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
     public partial class SalidaEntityCollection : List<SalidaEntity>
    {
    
	
		#region << Get Parents Methods >>
		
		public EntidadEntityCollection GetEntidadFromIdCliente()
		{
			EntidadEntityCollection parentCollection = new EntidadEntityCollection();

			foreach(SalidaEntity entity in this)
			{
				if(entity.IdClienteAsEntidad != null)
				{
					if(parentCollection.GetByPK(entity.IdClienteAsEntidad.Id) == null)
					{
						parentCollection.Add(entity.IdClienteAsEntidad);
					}
				}
			}

			return parentCollection;
		}

		public LocalBodegaEntityCollection GetLocalBodegaFromIdLocal()
		{
			LocalBodegaEntityCollection parentCollection = new LocalBodegaEntityCollection();

			foreach(SalidaEntity entity in this)
			{
				if(entity.IdLocalAsLocalBodega != null)
				{
					if(parentCollection.GetByPK(entity.IdLocalAsLocalBodega.Id) == null)
					{
						parentCollection.Add(entity.IdLocalAsLocalBodega);
					}
				}
			}

			return parentCollection;
		}

		public RucEntityCollection GetRucFromRuc()
		{
			RucEntityCollection parentCollection = new RucEntityCollection();

			foreach(SalidaEntity entity in this)
			{
				if(entity.RucAsRuc != null)
				{
					if(parentCollection.GetByPK(entity.RucAsRuc.Ruc) == null)
					{
						parentCollection.Add(entity.RucAsRuc);
					}
				}
			}

			return parentCollection;
		}

		
		#endregion

		#region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
			foreach (SalidaEntity entity in this)
            {
                entity.SetState(state);
            }          
        }
        
        public void RollBackState()
        {
            foreach (SalidaEntity entity in this)
            {
                entity.RollBackState();
            }
        }
        
        #endregion
        
        public SalidaEntity GetByPK(string Id)
        {
            foreach (SalidaEntity entity in this)
            {
                if(entity.Id == Id)
                {
                	return entity;
                }
            }
            
            return null;
        }
        
        
        public SalidaEntityCollection CreateCollection()
        {
        	return new SalidaEntityCollection();
        }
        
          
    }
}
