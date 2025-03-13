using ER.BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.DA
{
    public partial class InfoAdicionalDataAccessCollection
    {
		public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction)
		{
			return FindByAll(findParameter, conexion, transaction, 1);
		}

		public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction, int deepLoadLevel)
		{
			SqlCommand mCommand = new SqlCommand();
			SqlDataReader reader = null;
			try
			{
				mCommand.Connection = conexion;
				mCommand.CommandType = CommandType.StoredProcedure;
				mCommand.Transaction = transaction;
				if (deepLoadLevel >= 1)
				{
					mCommand.CommandText = "InformacionAdicional_DeepFindByAll";
				}
				else mCommand.CommandText = "InformacionAdicional_FindByAll";

				if (findParameter.Id != int.MinValue)
				{
					mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@Id", DBNull.Value);
				}

				if (!String.IsNullOrEmpty(findParameter.IdSalida))
				{
					mCommand.Parameters.AddWithValue("@IdSalida", findParameter.IdSalida);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdSalida", DBNull.Value);
				}

				//if (!String.IsNullOrEmpty(findParameter.TituloInfoAdicional))
				//{
				//	mCommand.Parameters.AddWithValue("@TituloInfoAdicional", findParameter.TituloInfoAdicional);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@TituloInfoAdicional", DBNull.Value);
				//}

				//if (!String.IsNullOrEmpty(findParameter.InfoAdicional))
				//{
				//	mCommand.Parameters.AddWithValue("@InfoAdicional", findParameter.InfoAdicional);
				//}
				//else
				//{
				//	mCommand.Parameters.AddWithValue("@InfoAdicional", DBNull.Value);
				//}

				if (conexion.State != ConnectionState.Open) conexion.Open();
				reader = mCommand.ExecuteReader();

				InfoAdicionalEntityCollection infoEntityCollection = new InfoAdicionalEntityCollection();
				InfoAdicionalEntity infoEntity;


				while (reader.Read())
				{
					infoEntity = new InfoAdicionalEntity();
					infoEntity.Id = Convert.ToInt32(reader["Id"]);
					infoEntity.IdSalida = Convert.ToString(reader["IdSalida"]);
					infoEntity.TituloInfoAdicional = Convert.ToString(reader["TituloInfoAdicional"]);
					infoEntity.InfoAdicional = Convert.ToString(reader["InfoAdicional"]);

					infoEntity.SetLoadedState();
					infoEntityCollection.Add(infoEntity);

				}

				return infoEntityCollection;
			}
			catch (Exception exc)
			{
				throw exc;
			}
			finally
			{
				if (reader != null) reader.Close();
				mCommand.Dispose();
			}

		}
	}
}
