using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    [System.Serializable]
    public partial class InfoAdicionalEntityCollection: List<InfoAdicionalEntity>
    {
        #region << Get Parents Methods >>


        #endregion

        #region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
            foreach (InfoAdicionalEntity entity in this)
            {
                entity.SetState(state);
            }
        }

        public void RollBackState()
        {
            foreach (InfoAdicionalEntity entity in this)
            {
                entity.RollBackState();
            }
        }

        #endregion

        public InfoAdicionalEntity GetByPK(int Id)
        {
            foreach (InfoAdicionalEntity entity in this)
            {
                if (entity.Id == Id)
                {
                    return entity;
                }
            }

            return null;
        }


        public InfoAdicionalEntityCollection CreateCollection()
        {
            return new InfoAdicionalEntityCollection();
        }
    }
}
