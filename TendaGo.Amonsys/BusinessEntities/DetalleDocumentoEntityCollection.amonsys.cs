    
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
     public partial class DetalleDocumentoEntityCollection : List<DetalleDocumentoEntity>
    {
    
	
		#region << Get Parents Methods >>
		
		public DocumentoEntityCollection GetDocumentoFromIdDocumento()
		{
			DocumentoEntityCollection parentCollection = new DocumentoEntityCollection();

			foreach(DetalleDocumentoEntity entity in this)
			{
				if(entity.IdDocumentoAsDocumento != null)
				{
					if(parentCollection.GetByPK(entity.IdDocumentoAsDocumento.Id) == null)
					{
						parentCollection.Add(entity.IdDocumentoAsDocumento);
					}
				}
			}

			return parentCollection;
		}

		public ProductoEntityCollection GetProductoFromIdProducto()
		{
			ProductoEntityCollection parentCollection = new ProductoEntityCollection();

			foreach(DetalleDocumentoEntity entity in this)
			{
				if(entity.IdProductoAsProducto != null)
				{
					if(parentCollection.GetByPK(entity.IdProductoAsProducto.Id) == null)
					{
						parentCollection.Add(entity.IdProductoAsProducto);
					}
				}
			}

			return parentCollection;
		}

		
		#endregion

		#region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
			foreach (DetalleDocumentoEntity entity in this)
            {
                entity.SetState(state);
            }          
        }
        
        public void RollBackState()
        {
            foreach (DetalleDocumentoEntity entity in this)
            {
                entity.RollBackState();
            }
        }
        
        #endregion
        
        public DetalleDocumentoEntity GetByPK(long Id)
        {
            foreach (DetalleDocumentoEntity entity in this)
            {
                if(entity.Id == Id)
                {
                	return entity;
                }
            }
            
            return null;
        }
        
        
        public DetalleDocumentoEntityCollection CreateCollection()
        {
        	return new DetalleDocumentoEntityCollection();
        }
        
          
    }
}
