    
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
    public partial class ProductoEntityBase : EntityStatesManager
    
    {

        #region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdEmpresa_EntityColumn = "IdEmpresa";
		public const string CodigoProveedor_EntityColumn = "CodigoProveedor";
		public const string CodigoInterno_EntityColumn = "CodigoInterno";
        public const string CodigoBarra_EntityColumn = "CodigoBarra";
        public const string TipoProducto_EntityColumn = "TipoProducto";
		public const string Producto_EntityColumn = "Producto";
		public const string Descripcion_EntityColumn = "Descripcion";
		public const string DescipcionBusqueda_EntityColumn = "DescipcionBusqueda";
		public const string Foto_EntityColumn = "Foto";
		public const string Stock_EntityColumn = "Stock";
		public const string StockMinimo_EntityColumn = "StockMinimo";
		public const string Costo_EntityColumn = "Costo";
		public const string UnidadMedida_EntityColumn = "UnidadMedida";
		public const string UnidadMedidaAsUnidadMedida_EntityColumn = "UnidadMedidaAsUnidadMedida";
		public const string UnidadMedida_DisplayMember_EntityColumn = "UnidadMedida_DisplayMember";
		public const string Descuento_EntityColumn = "Descuento";
		public const string CobraIva_EntityColumn = "CobraIva";
		public const string IdDivision_EntityColumn = "IdDivision";
		public const string IdDivisionAsDivision_EntityColumn = "IdDivisionAsDivision";
		public const string IdDivision_DisplayMember_EntityColumn = "IdDivision_DisplayMember";
		public const string IdLinea_EntityColumn = "IdLinea";
		public const string IdLineaAsLinea_EntityColumn = "IdLineaAsLinea";
		public const string IdLinea_DisplayMember_EntityColumn = "IdLinea_DisplayMember";
		public const string IdCategoria_EntityColumn = "IdCategoria";
		public const string IdCategoriaAsCategoria_EntityColumn = "IdCategoriaAsCategoria";
		public const string IdCategoria_DisplayMember_EntityColumn = "IdCategoria_DisplayMember";
		public const string IdMarca_EntityColumn = "IdMarca";
		public const string IdMarcaAsMarca_EntityColumn = "IdMarcaAsMarca";
		public const string IdMarca_DisplayMember_EntityColumn = "IdMarca_DisplayMember";
		public const string Peso_EntityColumn = "Peso";
		public const string Volumen_EntityColumn = "Volumen";
		public const string RegistroSanitario_EntityColumn = "RegistroSanitario";
		public const string IpIngreso_EntityColumn = "IpIngreso";
		public const string UsuarioIngreso_EntityColumn = "UsuarioIngreso";
		public const string FechaIngreso_EntityColumn = "FechaIngreso";
		public const string IpModificacion_EntityColumn = "IpModificacion";
		public const string UsuarioModificacion_EntityColumn = "UsuarioModificacion";
		public const string FechaModificacion_EntityColumn = "FechaModificacion";
		public const string IdEstado_EntityColumn = "IdEstado";
		public const string IdTarifaImpuesto_EntityColumn = "IdTarifaImpuesto";
		public const string PorcentajeTarifaImpuesto_EntityColumn = "PorcentajeTarifaImpuesto";


		#endregion


		#region << Atributtes >>

		private int _id;
		private int _idEmpresa;
		private string _codigoProveedor;
		private string _codigoInterno;
        private string _codigoBarra;
        private string _tipoProducto;
		private string _producto;
		private string _descripcion;
		private string _descipcionBusqueda;
		private decimal _stock;
		private decimal _stockMinimo;
		private decimal _costo;
		private int _unidadMedida;
		private UnidadMedidaEntity _UnidadMedidaAsUnidadMedida;
		private decimal _descuento;
		private bool _cobraIva;
		private int _idDivision;
		private DivisionEntity _IdDivisionAsDivision;
		private int _idLinea;
		private LineaEntity _IdLineaAsLinea;
		private int _idCategoria;
		private CategoriaEntity _IdCategoriaAsCategoria;
		private int _idMarca;
		private MarcaEntity _IdMarcaAsMarca;
		private decimal _peso;
		private decimal _volumen;
		private string _registroSanitario;
        private string _pathArchivo;
        private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado;
        private int? productoPadre;
        private int _IdTarifaImpuesto;
        private int _PorcentajeTarifaImpuesto;
        private ProductoEntity productoPadreAsProducto;

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
		/// Get or sets a obligatory value of CodigoProveedor. 
		/// </summary>
		public virtual string CodigoProveedor
		{
			get
			{
				return _codigoProveedor;
			}
			set
			{
				if (_codigoProveedor != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_codigoProveedor = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of CodigoInterno. 
		/// </summary>
		public virtual string CodigoInterno
		{
			get
			{
				return _codigoInterno;
			}
			set
			{
				if (_codigoInterno != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_codigoInterno = value;
			}
		}

        /// <summary> 
		/// Get or sets a obligatory value of CodigoInterno. 
		/// </summary>
		public virtual string CodigoBarra
        {
            get
            {
                return _codigoBarra;
            }
            set
            {
                if (_codigoBarra != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _codigoBarra = value;
            }
        }

        /// <summary> 
        /// Get or sets a obligatory value of TipoProducto. 
        /// </summary>
        public virtual string TipoProducto
		{
			get
			{
				return _tipoProducto;
			}
			set
			{
				if (_tipoProducto != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_tipoProducto = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Producto. 
		/// </summary>
		public virtual string Producto
		{
			get
			{
				return _producto;
			}
			set
			{
				if (_producto != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_producto = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Descripcion. 
		/// </summary>
		public virtual string Descripcion
		{
			get
			{
				return _descripcion;
			}
			set
			{
				if (_descripcion != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_descripcion = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of DescipcionBusqueda. 
		/// </summary>
		public virtual string DescipcionBusqueda
		{
			get
			{
				return _descipcionBusqueda;
			}
			set
			{
				if (_descipcionBusqueda != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_descipcionBusqueda = value;
			}
		}

		

		/// <summary> 
		/// Get or sets a obligatory value of Stock. 
		/// </summary>
		public virtual decimal Stock
		{
			get
			{
				return _stock;
			}
			set
			{
				if (_stock != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_stock = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of StockMinimo. 
		/// </summary>
		public virtual decimal StockMinimo
		{
			get
			{
				return _stockMinimo;
			}
			set
			{
				if (_stockMinimo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_stockMinimo = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of Costo. 
		/// </summary>
		public virtual decimal Costo
		{
			get
			{
				return _costo;
			}
			set
			{
				if (_costo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_costo = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of UnidadMedida. 
		/// </summary>
		public virtual int UnidadMedida
		{
			get
			{
				return _unidadMedida;
			}
			set
			{
				if (_unidadMedida != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_unidadMedida = value;
			}
		}

		public virtual UnidadMedidaEntity UnidadMedidaAsUnidadMedida
		{
			get
			{
				return _UnidadMedidaAsUnidadMedida;
			}
			set
			{
				_UnidadMedidaAsUnidadMedida = value;
			}
		}

		public virtual string UnidadMedida_DisplayMember
		{
			get
			{
				if(UnidadMedidaAsUnidadMedida!= null ) 
				{
					return UnidadMedidaAsUnidadMedida.ToString();
				}
				return "";
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
		/// Get or sets a optional value of CobraIva. 
		/// </summary>
		public virtual bool CobraIva
		{
			get
			{
				return _cobraIva;
			}
			set
			{
				if (_cobraIva != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_cobraIva = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of IdDivision. 
		/// </summary>
		public virtual int IdDivision
		{
			get
			{
				return _idDivision;
			}
			set
			{
				if (_idDivision != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idDivision = value;
			}
		}

		public virtual DivisionEntity IdDivisionAsDivision
		{
			get
			{
				return _IdDivisionAsDivision;
			}
			set
			{
				_IdDivisionAsDivision = value;
			}
		}

		public virtual string IdDivision_DisplayMember
		{
			get
			{
				if(IdDivisionAsDivision!= null ) 
				{
					return IdDivisionAsDivision.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a optional value of IdLinea. 
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
		/// Get or sets a optional value of IdCategoria. 
		/// </summary>
		public virtual int IdCategoria
		{
			get
			{
				return _idCategoria;
			}
			set
			{
				if (_idCategoria != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idCategoria = value;
			}
		}

		public virtual CategoriaEntity IdCategoriaAsCategoria
		{
			get
			{
				return _IdCategoriaAsCategoria;
			}
			set
			{
				_IdCategoriaAsCategoria = value;
			}
		}

		public virtual string IdCategoria_DisplayMember
		{
			get
			{
				if(IdCategoriaAsCategoria!= null ) 
				{
					return IdCategoriaAsCategoria.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a optional value of IdMarca. 
		/// </summary>
		public virtual int IdMarca
		{
			get
			{
				return _idMarca;
			}
			set
			{
				if (_idMarca != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_idMarca = value;
			}
		}

		public virtual MarcaEntity IdMarcaAsMarca
		{
			get
			{
				return _IdMarcaAsMarca;
			}
			set
			{
				_IdMarcaAsMarca = value;
			}
		}

		public virtual string IdMarca_DisplayMember
		{
			get
			{
				if(IdMarcaAsMarca!= null ) 
				{
					return IdMarcaAsMarca.ToString();
				}
				return "";
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Peso. 
		/// </summary>
		public virtual decimal Peso
		{
			get
			{
				return _peso;
			}
			set
			{
				if (_peso != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_peso = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of Volumen. 
		/// </summary>
		public virtual decimal Volumen
		{
			get
			{
				return _volumen;
			}
			set
			{
				if (_volumen != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_volumen = value;
			}
		}

		/// <summary> 
		/// Get or sets a optional value of RegistroSanitario. 
		/// </summary>
		public virtual string RegistroSanitario
		{
			get
			{
				return _registroSanitario;
			}
			set
			{
				if (_registroSanitario != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_registroSanitario = value;
			}
		}
        /// <summary> 
		/// Get or sets a optional value of Patharchivo. 
		/// </summary>
		public virtual string PathArchivo
        {
            get
            {
                return _pathArchivo;
            }
            set
            {
                if (_pathArchivo != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _pathArchivo = value;
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
		/// Get or sets a obligatory value of Prodcuto Padre. 
		/// </summary>
		public virtual int? ProductoPadre
        {
            get
            {
                return productoPadre;
            }
            set
            {
                if (productoPadre != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                productoPadre = value;
            }
        }

        public virtual ProductoEntity ProductoPadreAsProducto
        {
            get
            {
                return productoPadreAsProducto;
            }
            set
            {
                productoPadreAsProducto = value;
            }
        }

        public virtual int IdTarifaImpuesto
        {
            get
            {
                return _IdTarifaImpuesto;
            }
            set
            {
                if (_IdTarifaImpuesto != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _IdTarifaImpuesto = value;
            }
        }

        public virtual int PorcentajeTarifaImpuesto
        {
            get
            {
                return _PorcentajeTarifaImpuesto;
            }
            set
            {
                if (_PorcentajeTarifaImpuesto != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
                {
                    CurrentState = EntityStatesEnum.Updated;
                }
                _PorcentajeTarifaImpuesto = value;
            }
        }


        #endregion

        public string CodigoProductoPadre { get; set; }
		public string Marca { get; set; }

		public ProductoEntityBase ()
        {
            SetNewState();
        }
        
        
        internal ProductoEntity CopyBase()
        {
          ProductoEntity copy = new ProductoEntity();
          copy.Id = this.Id;
          copy.IdEmpresa = this.IdEmpresa;
          copy.CodigoProveedor = this.CodigoProveedor;
          copy.CodigoInterno = this.CodigoInterno;
          copy.TipoProducto = this.TipoProducto;
          copy.Producto = this.Producto;
          copy.Descripcion = this.Descripcion;
          copy.DescipcionBusqueda = this.DescipcionBusqueda;
            copy.CodigoBarra = this.CodigoBarra;
          copy.Stock = this.Stock;
          copy.StockMinimo = this.StockMinimo;
          copy.Costo = this.Costo;
          copy.UnidadMedida = this.UnidadMedida;
          copy.Descuento = this.Descuento;
          copy.CobraIva = this.CobraIva;
          copy.IdDivision = this.IdDivision;
          copy.IdLinea = this.IdLinea;
          copy.IdCategoria = this.IdCategoria;
          copy.IdMarca = this.IdMarca;
          copy.Peso = this.Peso;
          copy.Volumen = this.Volumen;
          copy.RegistroSanitario = this.RegistroSanitario;
          copy.PathArchivo = this.PathArchivo;
          copy.IpIngreso = this.IpIngreso;
          copy.UsuarioIngreso = this.UsuarioIngreso;
          copy.FechaIngreso = this.FechaIngreso;
          copy.IpModificacion = this.IpModificacion;
          copy.UsuarioModificacion = this.UsuarioModificacion;
          copy.FechaModificacion = this.FechaModificacion;
          copy.IdEstado = this.IdEstado;
          copy.IdTarifaImpuesto = this.IdTarifaImpuesto;
          copy.PorcentajeTarifaImpuesto = this.PorcentajeTarifaImpuesto;


            copy.ProductoPadre = this.ProductoPadre;
			if( this.UnidadMedidaAsUnidadMedida!=null)
			{
				copy.UnidadMedidaAsUnidadMedida = this.UnidadMedidaAsUnidadMedida.Copy(); 
			}
			if( this.IdDivisionAsDivision!=null)
			{
				copy.IdDivisionAsDivision = this.IdDivisionAsDivision.Copy(); 
			}
			if( this.IdLineaAsLinea!=null)
			{
				copy.IdLineaAsLinea = this.IdLineaAsLinea.Copy(); 
			}
			if( this.IdCategoriaAsCategoria!=null)
			{
				copy.IdCategoriaAsCategoria = this.IdCategoriaAsCategoria.Copy(); 
			}
			if( this.IdMarcaAsMarca!=null)
			{
				copy.IdMarcaAsMarca = this.IdMarcaAsMarca.Copy(); 
			}

          return copy;
          
        }
        
        #region << Entity State Methods >>
        public void RollBackChildrensState()
        {
			if( _UnidadMedidaAsUnidadMedida != null )
			{
				if (_UnidadMedidaAsUnidadMedida.PreviousState != EntityStatesEnum.None)_UnidadMedidaAsUnidadMedida.RollBackState();
			}
			if( _IdDivisionAsDivision != null )
			{
				if (_IdDivisionAsDivision.PreviousState != EntityStatesEnum.None)_IdDivisionAsDivision.RollBackState();
			}
			if( _IdLineaAsLinea != null )
			{
				if (_IdLineaAsLinea.PreviousState != EntityStatesEnum.None)_IdLineaAsLinea.RollBackState();
			}
			if( _IdCategoriaAsCategoria != null )
			{
				if (_IdCategoriaAsCategoria.PreviousState != EntityStatesEnum.None)_IdCategoriaAsCategoria.RollBackState();
			}
			if( _IdMarcaAsMarca != null )
			{
				if (_IdMarcaAsMarca.PreviousState != EntityStatesEnum.None)_IdMarcaAsMarca.RollBackState();
			}
             
        }
        
        internal void SetStateBase(EntityStatesEnum state)
        {
			this.CurrentState=state;
			if( _UnidadMedidaAsUnidadMedida != null )
			{
				if (_UnidadMedidaAsUnidadMedida.CurrentState != state)_UnidadMedidaAsUnidadMedida.SetState(state);
			}
			if( _IdDivisionAsDivision != null )
			{
				if (_IdDivisionAsDivision.CurrentState != state)_IdDivisionAsDivision.SetState(state);
			}
			if( _IdLineaAsLinea != null )
			{
				if (_IdLineaAsLinea.CurrentState != state)_IdLineaAsLinea.SetState(state);
			}
			if( _IdCategoriaAsCategoria != null )
			{
				if (_IdCategoriaAsCategoria.CurrentState != state)_IdCategoriaAsCategoria.SetState(state);
			}
			if( _IdMarcaAsMarca != null )
			{
				if (_IdMarcaAsMarca.CurrentState != state)_IdMarcaAsMarca.SetState(state);
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

		private DetalleEntradaEntityCollection _DetalleEntradaFromIdProducto;

		public virtual DetalleEntradaEntityCollection DetalleEntradaFromIdProducto
		{
			get
			{
				return _DetalleEntradaFromIdProducto;
			}
			set
			{
				_DetalleEntradaFromIdProducto = value;
			}
		}

		private PrecioEntityCollection _PrecioFromIdProducto;

		public virtual PrecioEntityCollection PrecioFromIdProducto
		{
			get
			{
				return _PrecioFromIdProducto;
			}
			set
			{
				_PrecioFromIdProducto = value;
			}
		}

		private TipoUnidadEntityCollection _TipoUnidadFromIdProducto;

		public virtual TipoUnidadEntityCollection TipoUnidadFromIdProducto
		{
			get
			{
				return _TipoUnidadFromIdProducto;
			}
			set
			{
				_TipoUnidadFromIdProducto = value;
			}
		}

        
#endregion
        
        
        
    }
}

