    
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
    public partial class DetalleCobroDataAccess
    {
    
   
        #region << Default Methods >>

        /// <summary>
        /// Create a new entity type of DetalleCobro
        /// </summary>
        public static DetalleCobroEntity Insert(DetalleCobroEntity detalleCobro, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText =  "DetalleCobro_Insert";

                #region << Add the params >>
                 
				mCommand.Parameters.AddWithValue("@IdCobroDebito", detalleCobro.IdCobroDebito);
				mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar", detalleCobro.IdCuentaPorCobrar);
				if(detalleCobro.IdMedioPago != 0)
				{
					mCommand.Parameters.AddWithValue("@IdMedioPago", detalleCobro.IdMedioPago);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdMedioPago",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@Valor", detalleCobro.Valor);

				if (!String.IsNullOrEmpty(detalleCobro.Descripcion))
				{
					mCommand.Parameters.AddWithValue("@descripcion", detalleCobro.Descripcion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@descripcion", DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IpIngreso", detalleCobro.IpIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", detalleCobro.UsuarioIngreso.ToUpper());
				mCommand.Parameters.AddWithValue("@FechaIngreso", detalleCobro.FechaIngreso);
				if(!String.IsNullOrEmpty(detalleCobro.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", detalleCobro.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(detalleCobro.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", detalleCobro.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(detalleCobro.FechaModificacion != null && detalleCobro.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", detalleCobro.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", detalleCobro.IdEstado);

                if (detalleCobro.IdLocal != 0)
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", detalleCobro.IdLocal);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
                }

                // Add the primary keys columns
                mCommand.Parameters.Add("@Id", SqlDbType.Int);
				mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


                #endregion
                
                // Insert DetalleCobro
                if (connection.State != ConnectionState.Open) connection.Open();
                mCommand.ExecuteNonQuery();

				detalleCobro.Id = Convert.ToInt32(mCommand.Parameters["@Id"].Value);


                return detalleCobro;
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
        public static void Update(DetalleCobroEntity detalleCobro, SqlConnection connection, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;;
                mCommand.CommandText = "DetalleCobro_Update";

                 #region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", detalleCobro.Id);
				mCommand.Parameters.AddWithValue("@IdCobroDebito", detalleCobro.IdCobroDebito);
				mCommand.Parameters.AddWithValue("@IdCuentaPorCobrar", detalleCobro.IdCuentaPorCobrar);
				if(detalleCobro.IdMedioPago != 0)
				{
					mCommand.Parameters.AddWithValue("@IdMedioPago", detalleCobro.IdMedioPago);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IdMedioPago",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@Valor", detalleCobro.Valor);

				if (!String.IsNullOrEmpty(detalleCobro.Descripcion))
				{
					mCommand.Parameters.AddWithValue("@descripcion", detalleCobro.Descripcion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("@descripcion", DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IpIngreso", detalleCobro.IpIngreso);
				mCommand.Parameters.AddWithValue("@UsuarioIngreso", detalleCobro.UsuarioIngreso);
				mCommand.Parameters.AddWithValue("@FechaIngreso", detalleCobro.FechaIngreso);
				if(!String.IsNullOrEmpty(detalleCobro.IpModificacion))
				{
					mCommand.Parameters.AddWithValue("@IpModificacion", detalleCobro.IpModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
				}

				if(!String.IsNullOrEmpty(detalleCobro.UsuarioModificacion))
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion", detalleCobro.UsuarioModificacion.ToUpper());
				}
				else
				{
					mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
				}

				if(detalleCobro.FechaModificacion != null && detalleCobro.FechaModificacion != DateTime.MinValue)
				{
					mCommand.Parameters.AddWithValue("@FechaModificacion", detalleCobro.FechaModificacion);
				}
				else
				{
					mCommand.Parameters.AddWithValue("FechaModificacion",DBNull.Value);
				}

				mCommand.Parameters.AddWithValue("@IdEstado", detalleCobro.IdEstado);

                if (detalleCobro.IdLocal != 0)
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", detalleCobro.IdLocal);
                }
                else
                {
                    mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
                }


                #endregion

                // Update detalleCobro
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
        public static void Delete(DetalleCobroEntity detalleCobro, SqlConnection connection, SqlTransaction  transaction)
        {
            SqlCommand mCommand = new SqlCommand();
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;;
                mCommand.CommandText = "DetalleCobro_Delete";
				mCommand.Parameters.AddWithValue("@Id", detalleCobro.Id);
				mCommand.Parameters.AddWithValue("@FechaModificacion", detalleCobro.FechaModificacion == null ? DateTime.Now : detalleCobro.FechaModificacion);
				mCommand.Parameters.AddWithValue("@UsuarioModificacion", detalleCobro.UsuarioModificacion == null ? detalleCobro.UsuarioIngreso.ToUpper() : detalleCobro.UsuarioModificacion.ToUpper());
				mCommand.Parameters.AddWithValue("@IpModificacion", detalleCobro.IpModificacion == null ? detalleCobro.IpIngreso : detalleCobro.IpModificacion.ToUpper());

                
                // Update detalleCobro
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
        public static DetalleCobroEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction)
        {
        	return LoadByPK(Id,connection,transaction,1);
        }
        
        /// <summary>
        /// Load a entity by your Primary Key
        /// </summary>
        public static DetalleCobroEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction, int deepLoadLevel)
        {
            DetalleCobroEntity detalleCobro = new DetalleCobroEntity();
            
			detalleCobro.Id = Id;
            
            
            SqlCommand mCommand = new SqlCommand();
            SqlDataReader reader = null;
            try
            {
                mCommand.Connection = connection;
                mCommand.CommandType = CommandType.StoredProcedure;
                mCommand.Transaction = transaction;
                mCommand.CommandText = "DetalleCobro_LoadByPK";

                #region << Add the params >>

				mCommand.Parameters.AddWithValue("@Id", detalleCobro.Id);
                
 
                #endregion 
                
                if (connection.State != ConnectionState.Open) connection.Open();

                reader = mCommand.ExecuteReader();

                if(!reader.HasRows) return null;
                
	            while (reader.Read())
	            {
					#region << Deep Load >>
                    if (deepLoadLevel == 1)
		     		{
						detalleCobro.IdCobroDebitoAsCobroDebito = CobroDebitoDataAccess.ConvertToCobroDebitoEntity(reader, "IdCobroDebito");
						detalleCobro.IdCuentaPorCobrarAsCuentaPorCobrar = CuentaPorCobrarDataAccess.ConvertToCuentaPorCobrarEntity(reader, "IdCuentaPorCobrar");
						detalleCobro.IdMedioPagoAsMedioPago = MedioPagoDataAccess.ConvertToMedioPagoEntity(reader, "IdMedioPago");

                    }
	                #endregion
	                
	                #region << Load the BusinessEntity Object >>
					
					detalleCobro.Id = Convert.ToInt32(reader["Id"]);
					detalleCobro.IdCobroDebito = Convert.ToString(reader["IdCobroDebito"]);
					detalleCobro.IdCuentaPorCobrar = Convert.ToInt32(reader["IdCuentaPorCobrar"]);
					if (reader["IdMedioPago"] != DBNull.Value)
					{
						detalleCobro.IdMedioPago = Convert.ToInt32(reader["IdMedioPago"]);
					}
					detalleCobro.Valor = (decimal) reader["Valor"];
					detalleCobro.IpIngreso = Convert.ToString(reader["IpIngreso"]);
					detalleCobro.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
					detalleCobro.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
					if (reader["IpModificacion"] != DBNull.Value)
					{
						detalleCobro.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
					}
					if (reader["UsuarioModificacion"] != DBNull.Value)
					{
						detalleCobro.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
					}
					if (reader["FechaModificacion"] != DBNull.Value)
					{
						detalleCobro.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
					}
					detalleCobro.IdEstado = Convert.ToInt16(reader["IdEstado"]);

	                #endregion
	            }

                detalleCobro.SetLoadedState();
                return detalleCobro;
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
        
        public static DetalleCobroEntity ConvertToDetalleCobroEntity (SqlDataReader reader,string fkColumnName)
        {
            DetalleCobroEntity detalleCobro = new DetalleCobroEntity();
            
            try
            {
                bool hasData=false;
                string columName;
                
                #region << Load the BusinessEntity Object >>
                
				try
				{
					columName = String.Format("Id_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.Id = Convert.ToInt32(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IdCobroDebito_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IdCobroDebito = Convert.ToString(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IdCuentaPorCobrar_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IdCuentaPorCobrar = Convert.ToInt32(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IdMedioPago_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IdMedioPago = Convert.ToInt32(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("Valor_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.Valor = (decimal) reader[columName];
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IpIngreso_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IpIngreso = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("UsuarioIngreso_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.UsuarioIngreso = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("FechaIngreso_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.FechaIngreso = Convert.ToDateTime(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IpModificacion_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IpModificacion = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("UsuarioModificacion_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.UsuarioModificacion = Convert.ToString(reader[columName]).ToUpper();
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("FechaModificacion_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.FechaModificacion = Convert.ToDateTime(reader[columName]);
						hasData = true;
					}
				}
				catch{}
				try
				{
					columName = String.Format("IdEstado_DetalleCobroFrom{0}", fkColumnName);
					if (reader[columName] != DBNull.Value)
					{
						detalleCobro.IdEstado = Convert.ToInt16(reader[columName]);
						hasData = true;
					}
				}
				catch{}

                
                #endregion
                
                detalleCobro.SetLoadedState();
                if(hasData)
                {
                	return detalleCobro;
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


