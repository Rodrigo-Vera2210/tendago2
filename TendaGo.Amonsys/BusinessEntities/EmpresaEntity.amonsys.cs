    
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
    public partial class EmpresaEntityBase : EntityStatesManager
    
    {

        #region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string NombreEmpresa_EntityColumn = "NombreEmpresa";
		public const string Direccion_EntityColumn = "Direccion";
		public const string Telefono_EntityColumn = "Telefono";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";
		public const string Iva_EntityColumn = "Iva";
        public const string FacturaGo_EntityColumn = "FacturaGo";


        #endregion


        #region << Atributtes >>

        private int _id;
		private string _nombreEmpresa;
		private string _direccion;
		private string _telefono;
        private string _logo;
        private string _RaizArchivo;
        private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado;
		private bool _facturaPOS;
		private bool _flujoInventario;
		private bool _importacion;
		private bool _incluyeIVA;
		private decimal _iva;
        private bool _facturaGo;

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
		/// Get or sets a obligatory value of NombreEmpresa. 
		/// </summary>
		public virtual string NombreEmpresa
		{
			get
			{
				return _nombreEmpresa;
			}
			set
			{
				if (_nombreEmpresa != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_nombreEmpresa = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Direccion. 
		/// </summary>
		public virtual string Direccion
		{
			get
			{
				return _direccion;
			}
			set
			{
				if (_direccion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_direccion = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Telefono. 
		/// </summary>
		public virtual string Telefono
		{
			get
			{
				return _telefono;
			}
			set
			{
				if (_telefono != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_telefono = value;
			}
		}
        /// <summary> 
		/// Get or sets a obligatory value of Telefono. 
		/// </summary>
		public virtual string Logo
        {
            get
            {
                return _logo;
            }
            set
            {
                if (_logo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _logo = value;
            }
        }
        /// <summary> 
		/// Get or sets a obligatory value of Telefono. 
		/// </summary>
		public virtual string RaizArchivo
        {
            get
            {
                return _RaizArchivo;
            }
            set
            {
                if (_RaizArchivo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _RaizArchivo = value;
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

		/// <summary> 
		/// Get or sets a obligatory value of FacturaPOS. 
		/// </summary>
		public virtual bool IncluyeIVA
		{
			get
			{
				return _incluyeIVA;
			}
			set
			{
				if (_incluyeIVA != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_incluyeIVA = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FacturaPOS. 
		/// </summary>
		public virtual bool FacturaPOS
		{
			get
			{
				return _facturaPOS;
			}
			set
			{
				if (_facturaPOS != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_facturaPOS = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FlujoInventario. 
		/// </summary>
		public virtual bool FlujoInventario
		{
			get
			{
				return _flujoInventario;
			}
			set
			{
				if (_flujoInventario != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_flujoInventario = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Importacion. 
		/// </summary>
		public virtual bool Importacion
		{
			get
			{
				return _importacion;
			}
			set
			{
				if (_importacion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_importacion = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of iva. 
		/// </summary>
		public virtual decimal Iva
		{
			get
			{
				return _iva;
			}
			set
			{
				if (_iva != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_iva = value;
			}
		}

        public virtual bool FacturaGo
        {
            get
            {
                return _facturaGo;
            }
            set
            {
                if (_facturaGo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _facturaGo = value;
            }
        }

        #endregion


        public EmpresaEntityBase ()
        {
            SetNewState();
        }
        
        
        internal EmpresaEntity CopyBase()
        {
          EmpresaEntity copy = new EmpresaEntity();
          copy.Id = this.Id;
          copy.NombreEmpresa = this.NombreEmpresa;
          copy.Direccion = this.Direccion;
          copy.Telefono = this.Telefono;
          copy.IpIngreso = this.IpIngreso;
          copy.UsuarioIngreso = this.UsuarioIngreso;
          copy.FechaIngreso = this.FechaIngreso;
          copy.IpModificacion = this.IpModificacion;
          copy.UsuarioModificacion = this.UsuarioModificacion;
          copy.FechaModificacion = this.FechaModificacion;
          copy.IdEstado = this.IdEstado;
			copy.FlujoInventario = this.FlujoInventario;
			copy.FacturaPOS = this.FacturaPOS;
			copy.Importacion = this.Importacion;
			copy.IncluyeIVA = this.IncluyeIVA;
			copy.Iva = this.Iva;
			copy.FacturaGo= this.FacturaGo;

			return copy;
          
        }
        
        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {
             
        }
        
        internal void SetStateBase(EntityStatesEnum state)
        {
			this.CurrentState=state;
             
        }
        #endregion
        
        private bool __selected;

        public bool __Selected
        {
            get { return __selected; }
            set { __selected = value; }
        }
        
        
#region << Children >>

		private LocalBodegaEntityCollection _LocalBodegaFromIdEmpresa;

		public virtual LocalBodegaEntityCollection LocalBodegaFromIdEmpresa
		{
			get
			{
				return _LocalBodegaFromIdEmpresa;
			}
			set
			{
				_LocalBodegaFromIdEmpresa = value;
			}
		}

		private PresupuestoFacturacionEntityCollection _PresupuestoFacturacionFromIdEmpresa;

		public virtual PresupuestoFacturacionEntityCollection PresupuestoFacturacionFromIdEmpresa
		{
			get
			{
				return _PresupuestoFacturacionFromIdEmpresa;
			}
			set
			{
				_PresupuestoFacturacionFromIdEmpresa = value;
			}
		}

		private RucEntityCollection _RucFromIdEmpresa;

		public virtual RucEntityCollection RucFromIdEmpresa
		{
			get
			{
				return _RucFromIdEmpresa;
			}
			set
			{
				_RucFromIdEmpresa = value;
			}
		}

		private UsuarioEntityCollection _UsuarioFromIdEmpresa;

		public virtual UsuarioEntityCollection UsuarioFromIdEmpresa
		{
			get
			{
				return _UsuarioFromIdEmpresa;
			}
			set
			{
				_UsuarioFromIdEmpresa = value;
			}
		}

        
#endregion
        
        
        
    }
}

