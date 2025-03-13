
using System;
using System.Collections.Generic;
using System.Text;

namespace ER.BE
{
   
    [System.Serializable]
    public enum EntityStatesEnum
    {
        None,
        Loaded,
        Deleted,
        Updated,
        New,
        Processing,
        SavedSuccessfully
    }
   

   
     [System.Serializable]    
     public partial class EntityStatesManager
    {
   
        #region <<Atributos Privados>>
        private EntityStatesEnum currentState;
        private EntityStatesEnum previousState;
        private bool loadChildren = true;

        #endregion

        #region <<Propiedades Publicas>>

        /// <summary>
        /// 
        /// </summary>
        public bool LoadChildren
        {
            get
            {
                return loadChildren;
            }
            set
            {
                loadChildren = value;
            }
        }


       
        public EntityStatesEnum CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                currentState = value;
            }

        }


     
        public EntityStatesEnum PreviousState
        {
            get
            {
                return previousState;
            }
            set
            {
                previousState = value;
            }
        }

        public bool CanSave()
        {
//			get
//			{
                if (currentState == EntityStatesEnum.New || currentState == EntityStatesEnum.Updated || currentState == EntityStatesEnum.Deleted)
                {
                    return true;
                }

                return false;
//			}
        }
        
        #endregion

        #region <<Constructor>>
        public EntityStatesManager()
        {
            this.previousState = EntityStatesEnum.None;
            this.currentState = EntityStatesEnum.None;
        }
        #endregion

        #region <<General Methods>>

        public void SetDeletedState()
        {
            this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.Deleted;
        }

        public void SetSavedSuccessfullyState()
        {
            this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.SavedSuccessfully;
        }

        public void SetNewState()
        {
            this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.New;
        }

        public void SetProcessingState()
        {
            this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.Processing;
        }

        public void SetUpdatedState()
        {
           this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.Updated;
        }

        public void SetLoadedState()
        {
            this.previousState = this.currentState;
            this.currentState = EntityStatesEnum.Loaded;
        }

        public virtual void RollBackState()
        {
//            if (this.currentState == EntityStatesEnum.Processing || this.currentState == EntityStatesEnum.Updated)
//            {
              if (this.currentState != EntityStatesEnum.Loaded)
              {
                this.currentState = this.previousState;
                this.previousState = EntityStatesEnum.None;
               }
            //}
        }
        
       
        #endregion

     
    }
}


