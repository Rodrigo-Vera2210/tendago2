    
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
    public partial class CuentaPorCobrarEntityBase : EntityStatesManager
    
    {

        #region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdSalida_EntityColumn = "IdSalida";
		public const string IdSalidaAsSalida_EntityColumn = "IdSalidaAsSalida";
		public const string IdSalida_DisplayMember_EntityColumn = "IdSalida_DisplayMember";
		public const string Numero_EntityColumn = "Numero";
		public const string FechaVencimiento_EntityColumn = "FechaVencimiento";
		public const string Valor_EntityColumn = "Valor";
		public const string Saldo_EntityColumn = "Saldo";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";
		public const string NumeroFactura_EntityColumn = "NumeroFactura";

        #endregion


        #region << Atributtes >>

        private int _id;
		private string _idSalida;
		private SalidaEntity _IdSalidaAsSalida;
		private int _numero;
		private DateTime _fechaVencimiento;
		private decimal _valor;
		private decimal _saldo;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado;
        private string _numeroFactura;

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
		/// Get or sets a obligatory value of IdSalida. 
		/// </summary>
		public virtual string IdSalida
		{
			get
			{
				return _idSalida;
			}
			set
			{
				if (_idSalida != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idSalida = value;
			}
		}

		public virtual SalidaEntity IdSalidaAsSalida
		{
			get
			{
				return _IdSalidaAsSalida;
			}
			set
			{
				_IdSalidaAsSalida = value;
			}
		}

		public virtual string IdSalida_DisplayMember
		{
			get
			{
				if(IdSalidaAsSalida!= null ) 
				{
					return IdSalidaAsSalida.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Numero. 
		/// </summary>
		public virtual int Numero
		{
			get
			{
				return _numero;
			}
			set
			{
				if (_numero != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_numero = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FechaVencimiento. 
		/// </summary>
		public virtual DateTime FechaVencimiento
		{
			get
			{
				return _fechaVencimiento;
			}
			set
			{
				if (_fechaVencimiento != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaVencimiento = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Valor. 
		/// </summary>
		public virtual decimal Valor
		{
			get
			{
				return _valor;
			}
			set
			{
				if (_valor != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_valor = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Saldo. 
		/// </summary>
		public virtual decimal Saldo
		{
			get
			{
				return _saldo;
			}
			set
			{
				if (_saldo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_saldo = value;
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

        public virtual string NumeroFactura
        {
            get
            {
                return _numeroFactura;
            }
            set
            {
                if (_numeroFactura != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _numeroFactura = value;
            }
        }


        #endregion


        public CuentaPorCobrarEntityBase ()
        {
            SetNewState();
        }
        
        
        internal CuentaPorCobrarEntity CopyBase()
        {
          CuentaPorCobrarEntity copy = new CuentaPorCobrarEntity();
          copy.Id = this.Id;
          copy.IdSalida = this.IdSalida;
          copy.Numero = this.Numero;
          copy.FechaVencimiento = this.FechaVencimiento;
          copy.Valor = this.Valor;
          copy.Saldo = this.Saldo;
          copy.IpIngreso = this.IpIngreso;
          copy.UsuarioIngreso = this.UsuarioIngreso;
          copy.FechaIngreso = this.FechaIngreso;
          copy.IpModificacion = this.IpModificacion;
          copy.UsuarioModificacion = this.UsuarioModificacion;
          copy.FechaModificacion = this.FechaModificacion;
          copy.IdEstado = this.IdEstado;
		  copy.NumeroFactura = this.NumeroFactura;
			if( this.IdSalidaAsSalida!=null)
			{
				copy.IdSalidaAsSalida = this.IdSalidaAsSalida.Copy(); 
			}

          return copy;
          
        }
        
        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {
			if( _IdSalidaAsSalida != null )
			{
				if (_IdSalidaAsSalida.PreviousState != EntityStatesEnum.None)_IdSalidaAsSalida.RollBackState();
			}
             
        }
        
        internal void SetStateBase(EntityStatesEnum state)
        {
			this.CurrentState=state;
			if( _IdSalidaAsSalida != null )
			{
				if (_IdSalidaAsSalida.CurrentState != state)_IdSalidaAsSalida.SetState(state);
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

		private DetalleCobroEntityCollection _DetalleCobroFromIdCuentaPorCobrar;

		public virtual DetalleCobroEntityCollection DetalleCobroFromIdCuentaPorCobrar
		{
			get
			{
				return _DetalleCobroFromIdCuentaPorCobrar;
			}
			set
			{
				_DetalleCobroFromIdCuentaPorCobrar = value;
			}
		}

        
#endregion
        
        
        
    }
}

