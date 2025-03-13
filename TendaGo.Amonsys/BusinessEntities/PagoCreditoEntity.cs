
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
    public partial class PagoCreditoEntity : PagoCreditoEntityBase
    
    {

        
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
        

        public PagoCreditoEntity Copy()
        {
          PagoCreditoEntity copy = CopyBase();
          
          //Add Custom Copy Properties here 
          
          copy.CurrentState = EntityStatesEnum.None;
          return copy;
          
        }
        
        public override string ToString()
        {
            return this.Detalle;
        }
        
     
        
        
    }
}

