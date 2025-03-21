    
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
    public partial class DetallePagoFindParameterEntity
    {
     
        #region << Atributtes >>

		private int _id = int.MinValue;
		private string _idPagoCredito ;
		private string _idPagoCredito_DisplayMember;
		private int _idCuentaPorPagar = int.MinValue;
		private string _idCuentaPorPagar_DisplayMember;
		private decimal _valor = decimal.MinValue;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime _fechaModificacion;
		private short _idEstado = short.MinValue;

        #endregion

        #region << Properties >>

		public int Id
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

		public string IdPagoCredito
		{
			get
			{
				return _idPagoCredito;
			}
			set
			{
				_idPagoCredito = value;
			}
		}

		public string IdPagoCredito_DisplayMember
		{
			get
			{
				return _idPagoCredito_DisplayMember;
			}
			set
			{
				_idPagoCredito_DisplayMember = value;
			}
		}

		public int IdCuentaPorPagar
		{
			get
			{
				return _idCuentaPorPagar;
			}
			set
			{
				_idCuentaPorPagar = value;
			}
		}

		public string IdCuentaPorPagar_DisplayMember
		{
			get
			{
				return _idCuentaPorPagar_DisplayMember;
			}
			set
			{
				_idCuentaPorPagar_DisplayMember = value;
			}
		}

		public decimal Valor
		{
			get
			{
				return _valor;
			}
			set
			{
				_valor = value;
			}
		}

		public string IpIngreso
		{
			get
			{
				return _ipIngreso;
			}
			set
			{
				_ipIngreso = value;
			}
		}

		public string UsuarioIngreso
		{
			get
			{
				return _usuarioIngreso;
			}
			set
			{
				_usuarioIngreso = value;
			}
		}

		public DateTime FechaIngreso
		{
			get
			{
				return _fechaIngreso;
			}
			set
			{
				_fechaIngreso = value;
			}
		}

		public string IpModificacion
		{
			get
			{
				return _ipModificacion;
			}
			set
			{
				_ipModificacion = value;
			}
		}

		public string UsuarioModificacion
		{
			get
			{
				return _usuarioModificacion;
			}
			set
			{
				_usuarioModificacion = value;
			}
		}

		public DateTime FechaModificacion
		{
			get
			{
				return _fechaModificacion;
			}
			set
			{
				_fechaModificacion = value;
			}
		}

		public short IdEstado
		{
			get
			{
				return _idEstado;
			}
			set
			{
				_idEstado = value;
			}
		}

 
        #endregion

        public bool IsValid(DetallePagoEntity _entity)
        { 
			if(!String.IsNullOrEmpty(this.IdPagoCredito))
			{
				if(this.IdPagoCredito!=_entity.IdPagoCredito)
				{
					return false;
				}
			}

			if(this.IdCuentaPorPagar != int.MinValue)
			{
				if(this.IdCuentaPorPagar!=_entity.IdCuentaPorPagar)
				{
					return false;
				}
			}

			if(this.Valor != decimal.MinValue)
			{
				if(this.Valor!=_entity.Valor)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.IpIngreso))
			{
				if(this.IpIngreso!=_entity.IpIngreso)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.UsuarioIngreso))
			{
				if(this.UsuarioIngreso!=_entity.UsuarioIngreso)
				{
					return false;
				}
			}

			if(this.FechaIngreso != DateTime.MinValue)
			{
				if(this.FechaIngreso!=_entity.FechaIngreso)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.IpModificacion))
			{
				if(this.IpModificacion!=_entity.IpModificacion)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.UsuarioModificacion))
			{
				if(this.UsuarioModificacion!=_entity.UsuarioModificacion)
				{
					return false;
				}
			}

			if(this.FechaModificacion != DateTime.MinValue)
			{
				if(this.FechaModificacion!=_entity.FechaModificacion)
				{
					return false;
				}
			}

			if(this.IdEstado != short.MinValue)
			{
				if(this.IdEstado!=_entity.IdEstado)
				{
					return false;
				}
			}

     

			return true;       
        }

     

    }
}

