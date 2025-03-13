
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ER.BE; 



namespace ER.DA
{
    public partial class UsuarioDataAccess
    {
        /// <summary>
        /// Load a entity by your token
        /// </summary>
        public static UsuarioEntity LoadByToken(string Token, SqlConnection connection, SqlTransaction transaction)
        {
            UsuarioEntity usuario = new UsuarioEntity();

            usuario.Token = Token;


            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "Custom_Usuario_LoadByToken";

                #region << Add the params >>

                mCommand.Parameters.AddWithValue("@Token", Token);


                #endregion

                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                if (!reader.HasRows) return null;

                while (reader.Read())
                {
                   

                    #region << Load the BusinessEntity Object >>

                    usuario.InicioSesion = Convert.ToString(reader["InicioSesion"]);
                    usuario.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
                    usuario.IdPerifl = Convert.ToInt16(reader["IdPerifl"]);
                    usuario.Nombres = Convert.ToString(reader["Nombres"]);
                    usuario.Identificacion = Convert.ToString(reader["Identificacion"]);
                    if (reader["Sexo"] != DBNull.Value)
                    {
                        usuario.Sexo = Convert.ToBoolean(reader["Sexo"]);
                    }
                    if (reader["Direccion"] != DBNull.Value)
                    {
                        usuario.Direccion = Convert.ToString(reader["Direccion"]).ToUpper();
                    }
                    usuario.Correo = Convert.ToString(reader["Correo"]);
                   
                    if (reader["Telefono"] != DBNull.Value)
                    {
                        usuario.Telefono = Convert.ToString(reader["Telefono"]).ToUpper();
                    }

                    usuario.IdEstado = Convert.ToInt16(reader["IdEstado"]);

                    #endregion
                }

                usuario.SetLoadedState();
                return usuario;
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


