    
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
	public partial class CierreCajaEntityBase : EntityStatesManager

	{

		#region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdEmpresa_EntityColumn = "IdEmpresa";
		public const string IdCajero_EntityColumn = "IdCajero";
		public const string FechaApertura_EntityColumn = "FechaApertura";
		public const string FechaCierre_EntityColumn = "FechaCierre";

		public const string SaldoInicial_EntityColumn = "SaldoInicial";
		public const string TotalIngresos_EntityColumn = "TotalIngresos";
		public const string TotalEgresos_EntityColumn = "TotalEgresos";
		public const string SaldoFinal_EntityColumn = "SaldoFinal";

		public const string IdLocal_EntityColumn = "IdLocal";
		public const string IdLocalAsLocal_EntityColumn = "IdLocalAsLocal";
		public const string IdLocal_DisplayMember_EntityColumn = "IdLocal_DisplayMember";
		public const string Observaciones_EntityColumn = "Observaciones";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";

		#endregion


		#region << Atributtes >>

		private string _id;
		private int _idEmpresa;
		private DateTime _fechaApertura;
		private DateTime _fechaCierre;

		private int _IdLocal;
		private LocalBodegaEntity _IdLocalAsLocal;
		private string _observaciones;

		private string _idCajero;
		private decimal _saldoInicial;
		private decimal _totalIngresos;
		private decimal _totalEgresos;
		private decimal _saldoFinal;

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
		public virtual string Id
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
		/// Get or sets a obligatory value of FechaIngreso. 
		/// </summary>
		public virtual DateTime FechaApertura
		{
			get
			{
				return _fechaApertura;
			}
			set
			{
				if (_fechaApertura != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaApertura = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FechaIngreso. 
		/// </summary>
		public virtual DateTime FechaCierre
		{
			get
			{
				return _fechaCierre;
			}
			set
			{
				if (_fechaCierre != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaCierre = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdLocal. 
		/// </summary>
		public virtual int IdLocal
		{
			get
			{
				return _IdLocal;
			}
			set
			{
				if (_IdLocal != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_IdLocal = value;
			}
		}

		public virtual LocalBodegaEntity IdLocalAsLocal
		{
			get
			{
				return _IdLocalAsLocal;
			}
			set
			{
				_IdLocalAsLocal = value;
			}
		}

		public virtual string IdLocal_DisplayMember
		{
			get
			{
				if (IdLocalAsLocal != null)
				{
					return IdLocalAsLocal.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of CierreCaja. 
		/// </summary>
		public virtual string Observaciones
		{
			get
			{
				return _observaciones;
			}
			set
			{
				if (_observaciones != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_observaciones = value;
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
		/// Get or sets a obligatory value of IdCajero. 
		/// </summary>
		public virtual string IdCajero
		{
			get
			{
				return _idCajero;
			}
			set
			{
				if (_idCajero != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idCajero = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of SaldoInicial. 
		/// </summary>
		public virtual decimal SaldoInicial
		{
			get
			{
				return _saldoInicial;
			}
			set
			{
				if (_saldoInicial != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_saldoInicial = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of TotalIngresos. 
		/// </summary>
		public virtual decimal TotalIngresos
		{
			get
			{
				return _totalIngresos;
			}
			set
			{
				if (_totalIngresos != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_totalIngresos = value;
			}
		}


		/// <summary> 
		/// Get or sets a obligatory value of TotalEgresos. 
		/// </summary>
		public virtual decimal TotalEgresos
		{
			get
			{
				return _totalEgresos;
			}
			set
			{
				if (_totalEgresos != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_totalEgresos = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of SaldoFinal. 
		/// </summary>
		public virtual decimal SaldoFinal
		{
			get
			{
				return _saldoFinal;
			}
			set
			{
				if (_saldoFinal != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_saldoFinal = value;
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


		public CierreCajaEntityBase()
		{
			SetNewState();
		}


		internal CierreCajaEntity CopyBase()
		{
			CierreCajaEntity copy = new CierreCajaEntity();
			copy.Id = this.Id;
			copy.IdEmpresa = this.IdEmpresa;
			copy.IdLocal = this.IdLocal;
			copy.IdCajero = this.IdCajero;
			copy.FechaApertura = this.FechaApertura;
			copy.FechaCierre = this.FechaCierre;
			copy.SaldoInicial = this.SaldoInicial;
			copy.TotalIngresos = this.TotalIngresos;
			copy.TotalEgresos = this.TotalEgresos;
			copy.SaldoFinal = this.SaldoFinal;

			copy.Observaciones = this.Observaciones;
			copy.IpIngreso = this.IpIngreso;
			copy.UsuarioIngreso = this.UsuarioIngreso;
			copy.FechaIngreso = this.FechaIngreso;
			copy.IpModificacion = this.IpModificacion;
			copy.UsuarioModificacion = this.UsuarioModificacion;
			copy.FechaModificacion = this.FechaModificacion;
			copy.IdEstado = this.IdEstado;
			if (this.IdLocalAsLocal != null)
			{
				copy.IdLocalAsLocal = this.IdLocalAsLocal.Copy();
			}

			return copy;

		}

		#region << Entity State Methods >>
		public void RollBackChildrensState()
		{
			if (_IdLocalAsLocal != null)
			{
				if (_IdLocalAsLocal.PreviousState != EntityStatesEnum.None) _IdLocalAsLocal.RollBackState();
			}

		}

		internal void SetStateBase(EntityStatesEnum state)
		{
			this.CurrentState = state;
			if (_IdLocalAsLocal != null)
			{
				if (_IdLocalAsLocal.CurrentState != state) _IdLocalAsLocal.SetState(state);
			}

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