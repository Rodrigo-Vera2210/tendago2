using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    [System.Serializable]
    public partial class SerieSalidaEntity: SerieSalidaEntityBase
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


		public SerieSalidaEntity Copy()
		{
			SerieSalidaEntity copy = CopyBase();

			//Add Custom Copy Properties here 

			copy.CurrentState = EntityStatesEnum.None;
			return copy;

		}

		public override string ToString()
		{
			return this.IdDetalleSalida;
		}
	}

    [System.Serializable]
    public partial class SerieSalidaEntityBase : EntityStatesManager
    {
        #region << Columns >>
        public const string Id_EntityColumn = "Id";
        public const string IdDetalleSalida_EntityColumn = "IdDetalleSalida";
        public const string IdProducto_EntityColumn = "IdProducto";
        public const string Serie_EntityColumn = "Serie";
        public const string IpIngreso_EntityColumn = "IpIngreso";
        public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
        public const string FechaIngreso_EntityColumn = "FechaIngreso";
        public const string IpModificacion_EntityColumn = "IpModificacion";
        public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
        public const string FechaModificacion_EntityColumn = "FechaModificacion";
        public const string IdEstado_EntityColumn = "IdEstado";
		public const string IdEmpresa_EntityColumn = "IdEmpresa";
		#endregion

		#region << Atributtes >>
		private int _id;
        private string _idDetalleSalida;
        private int _idProducto;
        private string _serie;
        private string _ipIngreso;
        private string _usuarioIngreso;
        private DateTime _fechaIngreso;
        private string _ipModificacion;
        private string _usuarioModificacion;
        private DateTime? _fechaModificacion;
        private short _idEstado;
		private int _idEmpresa;
		#endregion

		#region << Properties >>

		/// <summary> 
		/// Get or sets a obligatory value of Id. 
		/// </summary>
		public virtual int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _id = value;
            }
        }

		/// <summary> 
		/// Get or sets a obligatory value of IdDetalleSalida. 
		/// </summary>
		public virtual string IdDetalleSalida
		{
			get
			{
				return _idDetalleSalida;
			}
			set
			{
				if (_idDetalleSalida != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idDetalleSalida = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdProducto. 
		/// </summary>
		public virtual int IdProducto
		{
			get
			{
				return _idProducto;
			}
			set
			{
				if (_idProducto != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idProducto = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IpIngreso. 
		/// </summary>
		public virtual string IpIngreso
		{
			get
			{
				return _ipIngreso;
			}
			set
			{
				if (_ipIngreso != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_ipIngreso = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IpIngreso. 
		/// </summary>
		public virtual string Serie
		{
			get
			{
				return _serie;
			}
			set
			{
				if (_serie != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_serie = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of UsuarioIngreso. 
		/// </summary>
		public virtual string UsuarioIngreso
		{
			get
			{
				return _usuarioIngreso;
			}
			set
			{
				if (_usuarioIngreso != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_usuarioIngreso = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FechaIngreso. 
		/// </summary>
		public virtual DateTime FechaIngreso
		{
			get
			{
				return _fechaIngreso;
			}
			set
			{
				if (_fechaIngreso != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaIngreso = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of IpModificacion. 
		/// </summary>
		public virtual string IpModificacion
		{
			get
			{
				return _ipModificacion;
			}
			set
			{
				if (_ipModificacion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_ipModificacion = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of UsuarioModificacion. 
		/// </summary>
		public virtual string UsuarioModificacion
		{
			get
			{
				return _usuarioModificacion;
			}
			set
			{
				if (_usuarioModificacion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_usuarioModificacion = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of FechaModificacion. 
		/// </summary>
		public virtual DateTime? FechaModificacion
		{
			get
			{
				return _fechaModificacion;
			}
			set
			{
				if (_fechaModificacion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaModificacion = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdEstado. 
		/// </summary>
		public virtual short IdEstado
		{
			get
			{
				return _idEstado;
			}
			set
			{
				if (_idEstado != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idEstado = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdProducto. 
		/// </summary>
		public virtual int IdEmpresa
		{
			get
			{
				return _idEmpresa;
			}
			set
			{
				if (_idEmpresa != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idEmpresa = value;
			}
		}
		#endregion

		public SerieSalidaEntityBase()
		{
			SetNewState();
		}


		internal SerieSalidaEntity CopyBase()
		{
			SerieSalidaEntity copy = new SerieSalidaEntity();
			copy.Id = this.Id;
			copy.IdDetalleSalida = this.IdDetalleSalida;
			copy.IdProducto = this.IdProducto;
			copy.Serie = this.Serie;			
			copy.IpIngreso = this.IpIngreso;
			copy.UsuarioIngreso = this.UsuarioIngreso;
			copy.FechaIngreso = this.FechaIngreso;
			copy.IpModificacion = this.IpModificacion;
			copy.UsuarioModificacion = this.UsuarioModificacion;
			copy.FechaModificacion = this.FechaModificacion;
			copy.IdEstado = this.IdEstado;
			copy.IdEmpresa = this.IdEmpresa;

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

		private bool __selected;

		public bool __Selected
		{
			get { return __selected; }
			set { __selected = value; }
		}

	}
}
