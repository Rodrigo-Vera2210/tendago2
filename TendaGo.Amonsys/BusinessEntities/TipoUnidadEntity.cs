
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Text;

namespace ER.BE
{
    
    [System.Serializable] 
    public partial class TipoUnidadEntity : TipoUnidadEntityBase
    
    {
        public decimal Margen {
            get {
                if (IdProductoAsProducto!=null)
                {
                    if (Cantidad!=0 && this.Precio!=0)
                    {
                        return ((this.Precio / Cantidad) - IdProductoAsProducto.Costo) / (this.Precio / Cantidad)*100;
                    }
                }
                return 0;
            }
        }
        
        #region << Entity State Methods >>
        public override void RollBackState()
        {
             RollBackChildrensState();
            
             //Add Custom Rollback State here
          
             base.RollBackState();
             
        }
        
        public void SetState(EntityStatesEnum state)
        {
			this.SetStateBase(state);
            //Add Custom SetState here
        }
        #endregion
        

        public TipoUnidadEntity Copy()
        {
          TipoUnidadEntity copy = CopyBase();
          
          //Add Custom Copy Properties here 
          
          copy.CurrentState = EntityStatesEnum.None;
          return copy;
          
        }
        
        public override string ToString()
        {
            return this.TipoUnidad;
        }
        
     
        
        
    }
}

