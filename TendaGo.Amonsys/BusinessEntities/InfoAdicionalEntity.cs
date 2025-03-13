using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BE
{
	[System.Serializable]
	public partial class InfoAdicionalEntity: InformacionAdicionalEntityBase
	{
		#region << Entity State Methods >>
		public override void RollBackState()
		{
			RollBackChildrensState();

			//Add Custom Rollback State here

			base.RollBackState();

		}

		public void SetState(EntityStatesEnum state)
		{
			this.SetStateBase(state);
			//Add Custom SetState here
		}
		#endregion


		public InfoAdicionalEntity Copy()
		{
			InfoAdicionalEntity copy = CopyBase();

			//Add Custom Copy Properties here 

			copy.CurrentState = EntityStatesEnum.None;
			return copy;

		}

		public override string ToString()
		{
			return this.IdSalida;
		}
	}

	[System.Serializable]
	public partial class InformacionAdicionalEntityBase : EntityStatesManager

	{

		#region << Columns >>

		public const string Id_EntityColumn = "Id";
		public const string IdSalida_EntityColumn = "IdSalida";
		public const string TituloInfoAdicional_EntityColumn = "TituloInfoAdicional";
		public const string InfoAdicional_EntityColumn = "InfoAdicional";

		#endregion

		#region << Atributtes >>

		private int _id;
		private string _idSalida;
		private string _tituloInfoAdicional;
		private string _infoAdicional;


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

		public virtual string TituloInfoAdicional
		{
			get
			{
				return _tituloInfoAdicional;
			}
			set
			{
				if (_tituloInfoAdicional != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_tituloInfoAdicional = value;
			}
		}

		public virtual string InfoAdicional
		{
			get
			{
				return _infoAdicional;
			}
			set
			{
				if (_infoAdicional != value && CurrentState != EntityStatesEnum.New && CurrentState != EntityStatesEnum.Deleted)
				{
					CurrentState = EntityStatesEnum.Updated;
				}
				_infoAdicional = value;
			}
		}

		#endregion


		public InformacionAdicionalEntityBase()
		{
			SetNewState();
		}


		internal InfoAdicionalEntity CopyBase()
		{
			InfoAdicionalEntity copy = new InfoAdicionalEntity();
			copy.Id = this.Id;
			copy.IdSalida = this.IdSalida;
			copy.TituloInfoAdicional = this.TituloInfoAdicional;
			copy.InfoAdicional = this.InfoAdicional;
			
			return copy;

		}

		#region << Entity State Methods >>
		public void RollBackChildrensState()
		{


		}

		internal void SetStateBase(EntityStatesEnum state)
		{


		}
		#endregion

		private bool __selected;

		public bool __Selected
		{
			get { return __selected; }
			set { __selected = value; }
		}


		#region << Children >>

		#endregion



	}
}
