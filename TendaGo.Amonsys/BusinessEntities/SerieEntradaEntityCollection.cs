using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    [System.Serializable]
    public partial class SerieEntradaEntityCollection : List<SerieEntradaEntity>
    {
        #region << Get Parents Methods >>


        #endregion

        #region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
            foreach (SerieEntradaEntity entity in this)
            {
                entity.SetState(state);
            }
        }

        public void RollBackState()
        {
            foreach (SerieEntradaEntity entity in this)
            {
                entity.RollBackState();
            }
        }

        #endregion

        public SerieEntradaEntity GetByPK(int Id)
        {
            foreach (SerieEntradaEntity entity in this)
            {
                if (entity.Id == Id)
                {
                    return entity;
                }
            }

            return null;
        } 

        public SerieEntradaEntityCollection CreateCollection()
        {
            return new SerieEntradaEntityCollection();
        }
    }
}
