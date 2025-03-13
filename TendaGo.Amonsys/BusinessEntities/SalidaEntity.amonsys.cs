    
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
    public partial class SalidaEntityBase : EntityStatesManager
    
    {

        #region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdEmpresa_EntityColumn = "IdEmpresa";
		public const string IdLocal_EntityColumn = "IdLocal";
		public const string IdLocalAsLocalBodega_EntityColumn = "IdLocalAsLocalBodega";
		public const string IdLocal_DisplayMember_EntityColumn = "IdLocal_DisplayMember";
		public const string IdVendedor_EntityColumn = "IdVendedor";
		public const string Periodo_EntityColumn = "Periodo";
		public const string Fecha_EntityColumn = "Fecha";
		public const string TipoTransaccion_EntityColumn = "TipoTransaccion";
		public const string IdCliente_EntityColumn = "IdCliente";
		public const string IdClienteAsEntidad_EntityColumn = "IdClienteAsEntidad";
		public const string IdCliente_DisplayMember_EntityColumn = "IdCliente_DisplayMember";
		public const string Facturar_EntityColumn = "Facturar";
		public const string Ruc_EntityColumn = "Ruc";
		public const string RucAsRuc_EntityColumn = "RucAsRuc";
		public const string Ruc_DisplayMember_EntityColumn = "Ruc_DisplayMember";
		public const string Subtotal0_EntityColumn = "Subtotal0";
		public const string SubtotalIva_EntityColumn = "SubtotalIva";
		public const string Descuento_EntityColumn = "Descuento";
		public const string Total_EntityColumn = "Total";
		public const string Saldo_EntityColumn = "Saldo";
		public const string ValorAdicional_EntityColumn = "ValorAdicional";		
		public const string FechaHoraEntregaPropuesta_EntityColumn = "FechaHoraEntregaPropuesta";
		public const string FechaHoraEntregaReal_EntityColumn = "FechaHoraEntregaReal";
		public const string EstadoActual_EntityColumn = "EstadoActual";
		public const string Observaciones_EntityColumn = "Observaciones";
		public const string IdFormaPago_EntityColumn = "IdFormaPago";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";
		public const string TransaccionPadre_EntityColumn = "TransaccionPadre";
		public const string TipoTransaccionPadre_EntityColumn = "TipoTransaccionPadre";
		public const string Plazo_EntityColumn = "Plazo";
		public const string Cuotas_EntityColumn = "Cuotas";
		public const string Entrega_EntityColumn = "Entrega";
		public const string BorrarCobros_EntityColumn = "BorrarCobros";


		#endregion


		#region << Atributtes >>

		private string _id;
		private int _idEmpresa;
		private int _idLocal;
		private LocalBodegaEntity _IdLocalAsLocalBodega;
		private string _idVendedor;
		private string _periodo;
		private DateTime _fecha;
		private string _tipoTransaccion;
		private int _idCliente;
		private EntidadEntity _IdClienteAsEntidad;
		private bool _facturar;
		private string _ruc;
		private RucEntity _RucAsRuc;
		private decimal _subtotal0;
		private decimal _subtotalIva;
		private decimal _descuento;
		private decimal _total;
		private decimal _saldo;
		private decimal _valorAdicional;
		private DateTime _fechaHoraEntregaPropuesta;
		private DateTime? _fechaHoraEntregaReal;
		private string _estadoActual;
		private string _observaciones;
		private int _idFormaPago;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado;

		private int _plazo;
		private int _cuotas;
		private string _transaccionPadre;
		private string _tipoTransaccionPadre;
		private string _entrega;
		private bool _borrarCobros;
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
		/// Get or sets a obligatory value of IdLocal. 
		/// </summary>
		public virtual int IdLocal
		{
			get
			{
				return _idLocal;
			}
			set
			{
				if (_idLocal != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idLocal = value;
			}
		}

		public virtual LocalBodegaEntity IdLocalAsLocalBodega
		{
			get
			{
				return _IdLocalAsLocalBodega;
			}
			set
			{
				_IdLocalAsLocalBodega = value;
			}
		}

		public virtual string IdLocal_DisplayMember
		{
			get
			{
				if(IdLocalAsLocalBodega!= null ) 
				{
					return IdLocalAsLocalBodega.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdVendedor. 
		/// </summary>
		public virtual string IdVendedor
		{
			get
			{
				return _idVendedor;
			}
			set
			{
				if (_idVendedor != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idVendedor = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Periodo. 
		/// </summary>
		public virtual string Periodo
		{
			get
			{
				return _periodo;
			}
			set
			{
				if (_periodo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_periodo = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Fecha. 
		/// </summary>
		public virtual DateTime Fecha
		{
			get
			{
				return _fecha;
			}
			set
			{
				if (_fecha != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fecha = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of TipoTransaccion. 
		/// </summary>
		public virtual string TipoTransaccion
		{
			get
			{
				return _tipoTransaccion;
			}
			set
			{
				if (_tipoTransaccion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_tipoTransaccion = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of TransaccionPadre. 
		/// </summary>
		public virtual string TransaccionPadre
		{
			get
			{
				return _transaccionPadre;
			}
			set
			{
				if (_transaccionPadre != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_transaccionPadre = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of TransaccionPadre. 
		/// </summary>
		public virtual string TipoTransaccionPadre
		{
			get
			{
				return _tipoTransaccionPadre;
			}
			set
			{
				if (_tipoTransaccionPadre != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_tipoTransaccionPadre = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdCliente. 
		/// </summary>
		public virtual int IdCliente
		{
			get
			{
				return _idCliente;
			}
			set
			{
				if (_idCliente != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idCliente = value;
			}
		}

		public virtual EntidadEntity IdClienteAsEntidad
		{
			get
			{
				return _IdClienteAsEntidad;
			}
			set
			{
				_IdClienteAsEntidad = value;
			}
		}

		public virtual string IdCliente_DisplayMember
		{
			get
			{
				if(IdClienteAsEntidad!= null ) 
				{
					return IdClienteAsEntidad.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Facturar. 
		/// </summary>
		public virtual bool Facturar
		{
			get
			{
				return _facturar;
			}
			set
			{
				if (_facturar != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_facturar = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Ruc. 
		/// </summary>
		public virtual string Ruc
		{
			get
			{
				return _ruc;
			}
			set
			{
				if (_ruc != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_ruc = value;
			}
		}

		public virtual RucEntity RucAsRuc
		{
			get
			{
				return _RucAsRuc;
			}
			set
			{
				_RucAsRuc = value;
			}
		}

		public virtual string Ruc_DisplayMember
		{
			get
			{
				if(RucAsRuc!= null ) 
				{
					return RucAsRuc.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Subtotal0. 
		/// </summary>
		public virtual decimal Subtotal0
		{
			get
			{
				return _subtotal0;
			}
			set
			{
				if (_subtotal0 != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_subtotal0 = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of SubtotalIva. 
		/// </summary>
		public virtual decimal SubtotalIva
		{
			get
			{
				return _subtotalIva;
			}
			set
			{
				if (_subtotalIva != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_subtotalIva = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Descuento. 
		/// </summary>
		public virtual decimal Descuento
		{
			get
			{
				return _descuento;
			}
			set
			{
				if (_descuento != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_descuento = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Total. 
		/// </summary>
		public virtual decimal Total
		{
			get
			{
				return _total;
			}
			set
			{
				if (_total != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_total = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Saldo. 
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

		public virtual decimal ValorAdicional
		{
			get
			{
				return _valorAdicional;
			}
			set
			{
				if (_valorAdicional != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_valorAdicional = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of FechaHoraEntregaPropuesta. 
		/// </summary>
		public virtual DateTime FechaHoraEntregaPropuesta
		{
			get
			{
				return _fechaHoraEntregaPropuesta;
			}
			set
			{
				if (_fechaHoraEntregaPropuesta != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaHoraEntregaPropuesta = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of FechaHoraEntregaReal. 
		/// </summary>
		public virtual DateTime? FechaHoraEntregaReal
		{
			get
			{
				return _fechaHoraEntregaReal;
			}
			set
			{
				if (_fechaHoraEntregaReal != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_fechaHoraEntregaReal = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of EstadoActual. 
		/// </summary>
		public virtual string EstadoActual
		{
			get
			{
				return _estadoActual;
			}
			set
			{
				if (_estadoActual != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_estadoActual = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Observaciones. 
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
		/// Get or sets a obligatory value of IdFormaPago. 
		/// </summary>
		public virtual int IdFormaPago
		{
			get
			{
				return _idFormaPago;
			}
			set
			{
				if (_idFormaPago != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idFormaPago = value;
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
		/// Get or sets a obligatory value of Plazo. 
		/// </summary>
		public virtual int Plazo
		{
			get
			{
				return _plazo;
			}
			set
			{
				if (_plazo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_plazo = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Cuotas. 
		/// </summary>
		public virtual int Cuotas
		{
			get
			{
				return _cuotas;
			}
			set
			{
				if (_cuotas != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_cuotas = value;
			}
		}

		public virtual string Entrega
		{
			get
			{
				return _entrega;
			}
			set
			{
				if (_entrega != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_entrega = value;
			}
		}

		public virtual bool BorrarCobros
		{
			get
			{
				return _borrarCobros;
			}
			set
			{
				if (_borrarCobros != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_borrarCobros = value;
			}
		}

		#endregion


		public SalidaEntityBase ()
        {
            SetNewState();
        }


		internal SalidaEntity CopyBase()
		{
			SalidaEntity copy = new SalidaEntity();
			copy.Id = this.Id;
			copy.IdEmpresa = this.IdEmpresa;
			copy.IdLocal = this.IdLocal;
			copy.IdVendedor = this.IdVendedor;
			copy.Periodo = this.Periodo;
			copy.Fecha = this.Fecha;
			copy.TipoTransaccion = this.TipoTransaccion;
			copy.IdCliente = this.IdCliente;
			copy.Facturar = this.Facturar;
			copy.Ruc = this.Ruc;
			copy.Subtotal0 = this.Subtotal0;
			copy.SubtotalIva = this.SubtotalIva;
			copy.Descuento = this.Descuento;
			copy.Total = this.Total;
			copy.Saldo = this.Saldo;
			copy.ValorAdicional = this.ValorAdicional;
			copy.FechaHoraEntregaPropuesta = this.FechaHoraEntregaPropuesta;
			copy.FechaHoraEntregaReal = this.FechaHoraEntregaReal;
			copy.EstadoActual = this.EstadoActual;
			copy.Observaciones = this.Observaciones;
			copy.IdFormaPago = this.IdFormaPago;
			copy.IpIngreso = this.IpIngreso;
			copy.UsuarioIngreso = this.UsuarioIngreso;
			copy.FechaIngreso = this.FechaIngreso;
			copy.IpModificacion = this.IpModificacion;
			copy.UsuarioModificacion = this.UsuarioModificacion;
			copy.FechaModificacion = this.FechaModificacion;
			copy.IdEstado = this.IdEstado;
			copy.TipoTransaccionPadre = this.TipoTransaccionPadre;
			copy.TransaccionPadre = this.TransaccionPadre;
			copy.Plazo = this.Plazo;
			copy.Cuotas = this.Cuotas;
			copy.Entrega = this.Entrega;
			copy.BorrarCobros = this.BorrarCobros;

			if (this.IdLocalAsLocalBodega != null)
			{
				copy.IdLocalAsLocalBodega = this.IdLocalAsLocalBodega.Copy();
			}
			if (this.IdClienteAsEntidad != null)
			{
				copy.IdClienteAsEntidad = this.IdClienteAsEntidad.Copy();
			}
			if (this.RucAsRuc != null)
			{
				copy.RucAsRuc = this.RucAsRuc.Copy();
			}

			return copy;

		}
        
        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {
			if( _IdLocalAsLocalBodega != null )
			{
				if (_IdLocalAsLocalBodega.PreviousState != EntityStatesEnum.None)_IdLocalAsLocalBodega.RollBackState();
			}
			if( _IdClienteAsEntidad != null )
			{
				if (_IdClienteAsEntidad.PreviousState != EntityStatesEnum.None)_IdClienteAsEntidad.RollBackState();
			}
			if( _RucAsRuc != null )
			{
				if (_RucAsRuc.PreviousState != EntityStatesEnum.None)_RucAsRuc.RollBackState();
			}
             
        }
        
        internal void SetStateBase(EntityStatesEnum state)
        {
			this.CurrentState=state;
			if( _IdLocalAsLocalBodega != null )
			{
				if (_IdLocalAsLocalBodega.CurrentState != state)_IdLocalAsLocalBodega.SetState(state);
			}
			if( _IdClienteAsEntidad != null )
			{
				if (_IdClienteAsEntidad.CurrentState != state)_IdClienteAsEntidad.SetState(state);
			}
			if( _RucAsRuc != null )
			{
				if (_RucAsRuc.CurrentState != state)_RucAsRuc.SetState(state);
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

		private CuentaPorCobrarEntityCollection _CuentaPorCobrarFromIdSalida;

		public virtual CuentaPorCobrarEntityCollection CuentaPorCobrarFromIdSalida
		{
			get
			{
				return _CuentaPorCobrarFromIdSalida;
			}
			set
			{
				_CuentaPorCobrarFromIdSalida = value;
			}
		}

		private EmpaquetadoEntityCollection _EmpaquetadoFromIdSalida;

		public virtual EmpaquetadoEntityCollection EmpaquetadoFromIdSalida
		{
			get
			{
				return _EmpaquetadoFromIdSalida;
			}
			set
			{
				_EmpaquetadoFromIdSalida = value;
			}
		}


        private CobroDebitoEntity reciboCobro;
        public CobroDebitoEntity ReciboCobro {
            get
            {
                return reciboCobro;
            }
            set
            {
                reciboCobro = value;
            }
        }

		//private EstadoSalidaEntityCollection _EstadoSalidaFromSalida;

		//public virtual EstadoSalidaEntityCollection EstadoSalidaFromSalida
		//{
		//	get
		//	{
		//		return _EstadoSalidaFromSalida;
		//	}
		//	set
		//	{
		//		_EstadoSalidaFromSalida = value;
		//	}
		//}

		private DetalleSalidaEntityCollection _DetalleSalidaFromIdSalida;

		public virtual DetalleSalidaEntityCollection DetalleSalidaFromIdSalida
		{
			get
			{
				return _DetalleSalidaFromIdSalida;
			}
			set
			{
				_DetalleSalidaFromIdSalida = value;
			}
		}


		private DocumentoEntityCollection _DocumentoFromIdSalida = new DocumentoEntityCollection();

		public virtual DocumentoEntityCollection DocumentoFromIdSalida
		{
			get
			{
				return _DocumentoFromIdSalida;
			}
			set
			{
				_DocumentoFromIdSalida = value;
			}
		}

		private InfoAdicionalEntityCollection _InfoAdicionalFromIdSalida;

		public virtual InfoAdicionalEntityCollection InfoAdicionalFromIdSalida
		{
			get
			{
				return _InfoAdicionalFromIdSalida;
			}
			set
			{
				_InfoAdicionalFromIdSalida = value;
			}
		}
		#endregion



	}
}

