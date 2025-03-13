using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
	[System.Serializable]
	public partial class SerieEntradaFindParameterEntity
    {
		#region << Atributtes >>

		private int _id = int.MinValue;
		private string _idDetalleEntrada;
		private int _idProducto = int.MinValue;
		private string _serie;
		private string _ipIngreso;
		private string _usuarioIngreso;
		private DateTime _fechaIngreso;
		private string _ipModificacion;
		private string _usuarioModificacion;
		private DateTime? _fechaModificacion;
		private short _idEstado = short.MinValue;
		private int _idEmpresa = int.MinValue;

		#endregion

		#region << Properties >>

		/// <summary> 
		/// Get or sets a obligatory value of Id. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a obligatory value of IdDetalleSalida. 
		/// </summary>
		public string IdDetalleEntrada
		{
			get
			{
				return _idDetalleEntrada;
			}
			set
			{
				_idDetalleEntrada = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IdProducto. 
		/// </summary>
		public int IdProducto
		{
			get
			{
				return _idProducto;
			}
			set
			{
				_idProducto = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of IpIngreso. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a obligatory value of IpIngreso. 
		/// </summary>
		public string Serie
		{
			get
			{
				return _serie;
			}
			set
			{
				_serie = value;
			}
		}

		/// <summary> 
		/// Get or sets a obligatory value of UsuarioIngreso. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a obligatory value of FechaIngreso. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a optional value of IpModificacion. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a optional value of UsuarioModificacion. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a optional value of FechaModificacion. 
		/// </summary>
		public DateTime? FechaModificacion
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

		/// <summary> 
		/// Get or sets a obligatory value of IdEstado. 
		/// </summary>
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

		/// <summary> 
		/// Get or sets a obligatory value of IdProducto. 
		/// </summary>
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
		#endregion

		public bool IsValid(SerieEntradaEntity _entity)
		{
			if (this.Id != int.MinValue)
			{
				if (this.Id != _entity.Id)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.IdDetalleEntrada))
			{
				if (this.IdDetalleEntrada != _entity.IdDetalleEntrada)
				{
					return false;
				}
			}

			if (this.IdProducto != int.MinValue)
			{
				if (this.IdProducto != _entity.IdProducto)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.Serie))
			{
				if (this.Serie != _entity.Serie)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.IpIngreso))
			{
				if (this.IpIngreso != _entity.IpIngreso)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.UsuarioIngreso))
			{
				if (this.UsuarioIngreso != _entity.UsuarioIngreso)
				{
					return false;
				}
			}

			if (this.FechaIngreso != DateTime.MinValue)
			{
				if (this.FechaIngreso != _entity.FechaIngreso)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.IpModificacion))
			{
				if (this.IpModificacion != _entity.IpModificacion)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.UsuarioModificacion))
			{
				if (this.UsuarioModificacion != _entity.UsuarioModificacion)
				{
					return false;
				}
			}

			if (this.FechaModificacion != DateTime.MinValue)
			{
				if (this.FechaModificacion != _entity.FechaModificacion)
				{
					return false;
				}
			}

			if (this.IdEstado != short.MinValue)
			{
				if (this.IdEstado != _entity.IdEstado)
				{
					return false;
				}
			}

			if (this.IdEmpresa != int.MinValue)
			{
				if (this.IdEmpresa != _entity.IdEmpresa)
				{
					return false;
				}
			}

			return true;
		}
	}
}
