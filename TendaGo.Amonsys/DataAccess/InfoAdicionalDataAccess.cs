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
    public partial class InfoAdicionalDataAccess
    {
        #region << Default Methods >>

        public static InfoAdicionalEntity Insert(InfoAdicionalEntity infoAdic, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "InformacionAdicional_Insert";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@IdSalida", infoAdic.IdSalida);
                mCommand.Parameters.AddWithValue("@TituloInfoAdicional", infoAdic.TituloInfoAdicional);
                mCommand.Parameters.AddWithValue("@InfoAdicional", infoAdic.InfoAdicional);
               
                // Add the primary keys columns
                mCommand.Parameters.Add("@Id", SqlDbType.Int);
                mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;

                #endregion

                // Insert CobroDebito
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();

                infoAdic.Id = Convert.ToInt32(mCommand.Parameters["@Id"].Value);


                return infoAdic;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                mCommand.Dispose();
            }
        }

        #endregion
    }
}
