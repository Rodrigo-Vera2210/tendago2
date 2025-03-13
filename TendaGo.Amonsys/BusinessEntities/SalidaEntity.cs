
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace ER.BE
{
    
    [System.Serializable] 
    public partial class SalidaEntity : SalidaEntityBase
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
         
        public SalidaEntity Copy()
        {
          SalidaEntity copy = CopyBase();
          
          //Add Custom Copy Properties here 
          
          copy.CurrentState = EntityStatesEnum.None;
          return copy;
          
        }
        
        public override string ToString()
        {
            return this.Id;
        }

        //public Func<DocumentoEntityCollection, SqlConnection, SqlTransaction, DocumentoEntityCollection> ProcesarFacturaHandler;
    }
}

