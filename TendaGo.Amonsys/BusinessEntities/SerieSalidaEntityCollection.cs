using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    [System.Serializable]
    public partial class SerieSalidaEntityCollection:List<SerieSalidaEntity>
    {
        #region << Get Parents Methods >>


        #endregion

        #region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
            foreach (SerieSalidaEntity entity in this)
            {
                entity.SetState(state);
            }
        }

        public void RollBackState()
        {
            foreach (SerieSalidaEntity entity in this)
            {
                entity.RollBackState();
            }
        }

        #endregion

        public SerieSalidaEntity GetByPK(int Id)
        {
            foreach (SerieSalidaEntity entity in this)
            {
                if (entity.Id == Id)
                {
                    return entity;
                }
            }

            return null;
        }


        public SerieSalidaEntityCollection CreateCollection()
        {
            return new SerieSalidaEntityCollection();
        }
    }
}
