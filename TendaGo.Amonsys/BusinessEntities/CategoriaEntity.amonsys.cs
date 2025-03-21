    
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
    public partial class CategoriaEntityBase : EntityStatesManager
    
    {

        #region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdEmpresa_EntityColumn = "IdEmpresa";
		public const string IdLinea_EntityColumn = "IdLinea";
		public const string IdLineaAsLinea_EntityColumn = "IdLineaAsLinea";
		public const string IdLinea_DisplayMember_EntityColumn = "IdLinea_DisplayMember";
		public const string Categoria_EntityColumn = "Categoria";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";

        #endregion

        
        #region << Atributtes >>

		private int _id;
		private int _idEmpresa;
		private int _idLinea;
		private LineaEntity _IdLineaAsLinea;
		private string _categoria;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado;

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
		/// Get or sets a obligatory value of IdEmpresa. 
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

		/// <summary> 
		/// Get or sets a obligatory value of IdLinea. 
		/// </summary>
		public virtual int IdLinea
		{
			get
			{
				return _idLinea;
			}
			set
			{
				if (_idLinea != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idLinea = value;
			}
		}

		public virtual LineaEntity IdLineaAsLinea
		{
			get
			{
				return _IdLineaAsLinea;
			}
			set
			{
				_IdLineaAsLinea = value;
			}
		}

		public virtual string IdLinea_DisplayMember
		{
			get
			{
				if(IdLineaAsLinea!= null ) 
				{
					return IdLineaAsLinea.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Categoria. 
		/// </summary>
		public virtual string Categoria
		{
			get
			{
				return _categoria;
			}
			set
			{
				if (_categoria != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_categoria = value;
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

 
        #endregion
        
        
        public CategoriaEntityBase ()
        {
            SetNewState();
        }
        
        
        internal CategoriaEntity CopyBase()
        {
          CategoriaEntity copy = new CategoriaEntity();
          copy.Id = this.Id;
          copy.IdEmpresa = this.IdEmpresa;
          copy.IdLinea = this.IdLinea;
          copy.Categoria = this.Categoria;
          copy.IpIngreso = this.IpIngreso;
          copy.UsuarioIngreso = this.UsuarioIngreso;
          copy.FechaIngreso = this.FechaIngreso;
          copy.IpModificacion = this.IpModificacion;
          copy.UsuarioModificacion = this.UsuarioModificacion;
          copy.FechaModificacion = this.FechaModificacion;
          copy.IdEstado = this.IdEstado;
			if( this.IdLineaAsLinea!=null)
			{
				copy.IdLineaAsLinea = this.IdLineaAsLinea.Copy(); 
			}

          return copy;
          
        }
        
        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {
			if( _IdLineaAsLinea != null )
			{
				if (_IdLineaAsLinea.PreviousState != EntityStatesEnum.None)_IdLineaAsLinea.RollBackState();
			}
             
        }
        
        internal void SetStateBase(EntityStatesEnum state)
        {
			this.CurrentState=state;
			if( _IdLineaAsLinea != null )
			{
				if (_IdLineaAsLinea.CurrentState != state)_IdLineaAsLinea.SetState(state);
			}
             
        }
        #endregion
        
        private bool __selected;

        public bool __Selected
        {
            get { return __selected; }
            set { __selected = value; }
        }
        
        
#region << Children >>

		private ProductoEntityCollection _ProductoFromIdCategoria;

		public virtual ProductoEntityCollection ProductoFromIdCategoria
		{
			get
			{
				return _ProductoFromIdCategoria;
			}
			set
			{
				_ProductoFromIdCategoria = value;
			}
		}

        
#endregion
        
        
        
    }
}

