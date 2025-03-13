using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
    [System.Serializable]
    public partial class InfoAdicionalFindParameterEntity
    {
        #region << Atributtes >>

        private int _id= int.MinValue;
        private string _idSalida;
        private string _tituloInfoAdicional;
        private string _infoAdicional;


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

		public string IdSalida
		{
			get
			{
				return _idSalida;
			}
			set
			{
				_idSalida = value;
			}
		}

		public string TituloInfoAdicional
		{
			get
			{
				return _tituloInfoAdicional;
			}
			set
			{
				_tituloInfoAdicional = value;
			}
		}

		public string InfoAdicional
		{
			get
			{
				return _infoAdicional;
			}
			set
			{
				_infoAdicional = value;
			}
		}

		#endregion

		public bool IsValid(InfoAdicionalEntity _entity)
		{
			if (this.Id != int.MinValue)
			{
				if (this.Id != _entity.Id)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.IdSalida))
			{
				if (this.IdSalida != _entity.IdSalida)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.TituloInfoAdicional))
			{
				if (this.TituloInfoAdicional != _entity.TituloInfoAdicional)
				{
					return false;
				}
			}

			if (!String.IsNullOrEmpty(this.InfoAdicional))
			{
				if (this.InfoAdicional != _entity.InfoAdicional)
				{
					return false;
				}
			}

			return true;
		}


	}
}
