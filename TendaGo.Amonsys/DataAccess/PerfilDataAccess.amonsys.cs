    
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ER.BE; 



namespace ER.DA
{
    public partial class PerfilDataAccess
    {
    
   
        #region << Default Methods >>

        /// <summary>
        /// Create a new entity type of Perfil
        /// </summary>
        public static PerfilEntity Insert(PerfilEntity perfil, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText =  "Perfil_Insert";

                #region << Add the params >>
                 
				mCommand.Parameters.AddWithValue("@Perfil", perfil.Perfil.ToUpper());
				mCommand.Parameters.AddWithValue("@IpIngreso", perfil.IpIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", perfil.UsuarioIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@FechaIngreso", perfil.FechaIngreso);
				if(!String.IsNullOrEmpty(perfil.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", perfil.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(perfil.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", perfil.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(perfil.FechaModificacion != null && perfil.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", perfil.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", perfil.IdEstado);

				// Add the primary keys columns
				mCommand.Parameters.Add("@Id", SqlDbType.SmallInt);
				mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


                #endregion
                
                // Insert Perfil
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();

				perfil.Id = Convert.ToInt16(mCommand.Parameters["@Id"].Value);


                return perfil;
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

        /// <summary>
        /// Update a entity
        /// </summary>
        public static void Update(PerfilEntity perfil, SqlConnection connection, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;;
                mCommand.CommandText = "Perfil_Update";

                 #region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", perfil.Id);
				mCommand.Parameters.AddWithValue("@Perfil", perfil.Perfil);
				mCommand.Parameters.AddWithValue("@IpIngreso", perfil.IpIngreso);
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", perfil.UsuarioIngreso);
				mCommand.Parameters.AddWithValue("@FechaIngreso", perfil.FechaIngreso);
				if(!String.IsNullOrEmpty(perfil.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", perfil.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(perfil.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", perfil.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(perfil.FechaModificacion != null && perfil.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", perfil.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("FechaModificacion",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", perfil.IdEstado);
                
   
                #endregion
                
                // Update perfil
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();


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

         /// <summary>
        /// Delete a entity
        /// </summary>
        public static void Delete(PerfilEntity perfil, SqlConnection connection, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;;
                mCommand.CommandText = "Perfil_Delete";
				mCommand.Parameters.AddWithValue("@Id", perfil.Id);
				mCommand.Parameters.AddWithValue("@FechaModificacion", perfil.FechaModificacion);
				mCommand.Parameters.AddWithValue("@UsuarioModificacion", perfil.UsuarioModificacion.ToUpper());
				mCommand.Parameters.AddWithValue("@IpModificacion", perfil.IpModificacion.ToUpper());

                
                // Update perfil
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();


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
        
        
         
         
         /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static PerfilEntity LoadByPK(short Id, SqlConnection connection, SqlTransaction  transaction)
        {
        	return LoadByPK(Id,connection,transaction,1);
        }
        
        /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static PerfilEntity LoadByPK(short Id, SqlConnection connection, SqlTransaction  transaction, int deepLoadLevel)
        {
            PerfilEntity perfil = new PerfilEntity();
            
			perfil.Id = Id;
            
            
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "Perfil_LoadByPK";

                #region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", perfil.Id);
                
 
                #endregion 
                
                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                if(!reader.HasRows) return null;
                
	            while (reader.Read())
	            {
					#region << Deep Load >>
                    if (deepLoadLevel == 1)
		     		{

                    }
	                #endregion
	                
	                #region << Load the BusinessEntity Object >>
					
					perfil.Id = Convert.ToInt16(reader["Id"]);
					perfil.Perfil = Convert.ToString(reader["Perfil"]);
					perfil.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					perfil.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					perfil.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						perfil.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						perfil.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						perfil.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					perfil.IdEstado = Convert.ToInt16(reader["IdEstado"]);

	                #endregion
	            }

                perfil.SetLoadedState();
                return perfil;
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
        
        #endregion
        
        
        
        
        #region << Mappers >>
        
        public static PerfilEntity ConvertToPerfilEntity (SqlDataReader reader,string fkColumnName)
        {
            PerfilEntity perfil = new PerfilEntity();
            
            try
            {
                bool hasData=false;
                string columName;
                
                #region << Load the BusinessEntity Object >>
                
				try
				{
					columName = String.Format("Id_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.Id = Convert.ToInt16(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("Perfil_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.Perfil = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IpIngreso_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.IpIngreso = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("UsuarioIngreso_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.UsuarioIngreso = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("FechaIngreso_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.FechaIngreso = Convert.ToDateTime(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IpModificacion_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.IpModificacion = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("UsuarioModificacion_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.UsuarioModificacion = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("FechaModificacion_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.FechaModificacion = Convert.ToDateTime(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IdEstado_PerfilFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						perfil.IdEstado = Convert.ToInt16(reader[columName]);
						hasData = true;
					}
				}
				catch{}

                
                #endregion
                
                perfil.SetLoadedState();
                if(hasData)
                {
                	return perfil;
                }
                else return null;
            }
            catch (Exception exc)
            {
                return null;
            }
            finally
            {
                
            }
        }
        
        #endregion
        
   
    }
}


