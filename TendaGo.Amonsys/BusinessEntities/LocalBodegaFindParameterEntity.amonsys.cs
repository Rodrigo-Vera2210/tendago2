    
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
    public partial class LocalBodegaFindParameterEntity
    {
     
        #region << Atributtes >>

		private int _id = int.MinValue;
		private int _idEmpresa = int.MinValue;
		private string _idEmpresa_DisplayMember;
		private string _tipo;
		private string _local;
		private string _direccion;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime _fechaModificacion;
		private short _idEstado = short.MinValue;
		private string _defaultRUC;

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

		public int IdEmpresa
		{
			get
			{
				return _idEmpresa;
			}
			set
			{
				_idEmpresa = value;
			}
		}

		public string IdEmpresa_DisplayMember
		{
			get
			{
				return _idEmpresa_DisplayMember;
			}
			set
			{
				_idEmpresa_DisplayMember = value;
			}
		}

		public string Tipo
		{
			get
			{
				return _tipo;
			}
			set
			{
				_tipo = value;
			}
		}

		public string Local
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

		public string Direccion
		{
			get
			{
				return _direccion;
			}
			set
			{
				_direccion = value;
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

		public string DefaultRUC
		{
			get
			{
				return _defaultRUC;
			}
			set
			{
				_defaultRUC = value;
			}
		}

		#endregion

		public bool IsValid(LocalBodegaEntity _entity)
        { 
			if(this.IdEmpresa != int.MinValue)
			{
				if(this.IdEmpresa!=_entity.IdEmpresa)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.Tipo))
			{
				if(this.Tipo!=_entity.Tipo)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.Local))
			{
				if(this.Local!=_entity.Local)
				{
					return false;
				}
			}

			if(!String.IsNullOrEmpty(this.Direccion))
			{
				if(this.Direccion!=_entity.Direccion)
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

			if (!String.IsNullOrEmpty(this.DefaultRUC))
			{
				if (this.DefaultRUC != _entity.DefaultRUC)
				{
					return false;
				}
			}


			return true;       
        }

     

    }
}

