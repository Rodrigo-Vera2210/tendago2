using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    public partial class EstadisticaEntity: EstadisticaEntityBase
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

        public EstadisticaEntity Copy()
        {
            EstadisticaEntity copy = CopyBase();

            //Add Custom Copy Properties here 

            copy.CurrentState = EntityStatesEnum.None;
            return copy;

        }
    }

    [System.Serializable]
    public partial class EstadisticaEntityBase : EntityStatesManager
    {
        #region << Columns >>
        public const string Id_EntityColumn = "Id";
        public const string ValorVentasDia_EntityColumn = "ValorVentasDia";
        public const string ValorVentasMes_EntityColumn = "ValorVentasMes";
        public const string CantidadNPDia_EntityColumn = "CantidadNPDia";
        public const string CantidadCTDia_EntityColumn = "CantidadCTDia";
        public const string ValorCotizacionesMes_EntityColumn = "ValorCotizacionesMes";
        
        #endregion

        #region << Atributtes >>
        private decimal _id;
        private decimal _valorVentasDia;
        private decimal _valorVentasMes;
        private decimal _cantidadNPDia;
        private decimal _cantidadCTDia;
        private decimal _valorCotizacionesMes;
        #endregion

        #region << Properties >>
        public virtual decimal Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public virtual decimal ValorVentasDia
        {
            get
            {
                return _valorVentasDia;
            }
            set
            {               
                _valorVentasDia = value;
            }
        }

        public virtual decimal ValorVentasMes
        {
            get
            {
                return _valorVentasMes;
            }
            set
            {
                _valorVentasMes = value;
            }
        }

        public virtual decimal CantidadNPDia
        {
            get
            {
                return _cantidadNPDia;
            }
            set
            {
                _cantidadNPDia = value;
            }
        }

        public virtual decimal CantidadCTDia
        {
            get
            {
                return _cantidadCTDia;
            }
            set
            {
                _cantidadCTDia = value;
            }
        }

        public virtual decimal ValorCotizacionesMes
        {
            get
            {
                return _valorCotizacionesMes;
            }
            set
            {
                _valorCotizacionesMes = value;
            }
        }
        #endregion

        public EstadisticaEntityBase()
        {
            SetNewState();
        }


        internal EstadisticaEntity CopyBase()
        {
            EstadisticaEntity copy = new EstadisticaEntity();
            copy.Id = this.Id;
            copy.ValorVentasDia = this.ValorVentasDia;
            copy.ValorVentasMes = this.ValorVentasMes;
            copy.CantidadNPDia = this.CantidadNPDia;
            copy.CantidadCTDia = this.CantidadCTDia;
            copy.ValorCotizacionesMes = this.ValorCotizacionesMes;
            return copy;
        }

        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {


        }

        internal void SetStateBase(EntityStatesEnum state)
        {


        }
        #endregion

    }

    public partial class VentasMensualEntity : VentasMensualEntityBase
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

        public VentasMensualEntity Copy()
        {
            VentasMensualEntity copy = CopyBase();

            //Add Custom Copy Properties here 

            copy.CurrentState = EntityStatesEnum.None;
            return copy;

        }
    }
    public partial class VentasMensualEntityBase : EntityStatesManager
    {
        #region << Columns >>
        public const string Id_EntityColumn = "Id";
        public const string Fecha_EntityColumn = "fecha";
        public const string Total_EntityColumn = "total";
        public const string IdLocal_EntityColumn = "idLocal";
        public const string Local_EntityColumn = "local";

        #endregion

        #region << Atributtes >>
        private decimal _id;
        private DateTime _fecha;
        private decimal _total;
        private int _idLocal;
        private string _local;

        #endregion

        #region << Properties >>
        public virtual decimal Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public virtual DateTime Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
            }
        }

        public virtual decimal Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
            }
        }

        public virtual int IdLocal
        {
            get
            {
                return _idLocal;
            }
            set
            {
                _idLocal = value;
            }
        }

        public virtual string Local
        {
            get
            {
                return _local;
            }
            set
            {
                _local = value;
            }
        }
        #endregion

        public VentasMensualEntityBase()
        {
            SetNewState();
        }


        internal VentasMensualEntity CopyBase()
        {
            VentasMensualEntity copy = new VentasMensualEntity();
            copy.Id = this.Id;
            copy.Fecha = this.Fecha;
            copy.Total = this.Total;
            copy.IdLocal = this.IdLocal;
            copy.Local = this.Local;

            return copy;
        }

        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {


        }

        internal void SetStateBase(EntityStatesEnum state)
        {


        }
        #endregion

    }
    public partial class VentasMensualEntityCollection : List<VentasMensualEntity>
    {
        #region << Get Parents Methods >>


        #endregion

        #region << Entity State Methods >>

        public void SetState(EntityStatesEnum state)
        {
            foreach (VentasMensualEntity entity in this)
            {
                entity.SetState(state);
            }
        }

        public void RollBackState()
        {
            foreach (VentasMensualEntity entity in this)
            {
                entity.RollBackState();
            }
        }

        #endregion

        public VentasMensualEntity GetByPK(int Id)
        {
            foreach (VentasMensualEntity entity in this)
            {
                if (entity.Id == Id)
                {
                    return entity;
                }
            }

            return null;
        }


        public VentasMensualEntityCollection CreateCollection()
        {
            return new VentasMensualEntityCollection();
        }
    }
}
