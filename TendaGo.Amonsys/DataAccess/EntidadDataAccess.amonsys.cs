    
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
    public partial class EntidadDataAccess
    {
    
   
   //     #region << Default Methods >>

   //     /// <summary>
   //     /// Create a new entity type of Entidad
   //     /// </summary>
   //     public static EntidadEntity Insert(EntidadEntity entidad, SqlConnection connection, SqlTransaction transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText =  "Entidad_Insert";

   //             #region << Add the params >>
                 
			//	mCommand.Parameters.AddWithValue("@IdEmpresa", entidad.IdEmpresa);
			//	mCommand.Parameters.AddWithValue("@TipoEntidad", entidad.TipoEntidad.ToUpper());
   //             mCommand.Parameters.AddWithValue("@TipoIdentificacion", entidad.TipoIdentificacion.ToUpper());
   //             mCommand.Parameters.AddWithValue("@RazonSocial", entidad.RazonSocial.ToUpper());
			//	mCommand.Parameters.AddWithValue("@Identificacion", entidad.Identificacion.ToUpper());
			//	if(entidad.IdPais != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdPais", entidad.IdPais);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdPais",DBNull.Value);
			//	}

			//	if(entidad.IdProvincia != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdProvincia", entidad.IdProvincia);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdProvincia",DBNull.Value);
			//	}

			//	if(entidad.IdCiudad != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdCiudad", entidad.IdCiudad);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdCiudad",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Direccion))
			//	{
			//		mCommand.Parameters.AddWithValue("@Direccion", entidad.Direccion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Direccion",DBNull.Value);
			//	}

			//	if(entidad.IdSector != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSector", entidad.IdSector);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSector",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Telefono))
			//	{
			//		mCommand.Parameters.AddWithValue("@Telefono", entidad.Telefono);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Telefono",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Celular))
			//	{
			//		mCommand.Parameters.AddWithValue("@Celular", entidad.Celular);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Celular",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Correo))
			//	{
			//		mCommand.Parameters.AddWithValue("@Correo", entidad.Correo.ToLower());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Correo",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Observaciones))
			//	{
			//		mCommand.Parameters.AddWithValue("@Observaciones", entidad.Observaciones);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Observaciones",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@Foto", entidad.Foto);
			//	mCommand.Parameters.AddWithValue("@IpIngreso", entidad.IpIngreso);
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", entidad.UsuarioIngreso?.ToUpper());
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", entidad.FechaIngreso);

			//	mCommand.Parameters.AddWithValue("@EsCliente", entidad.EsCliente);
			//	mCommand.Parameters.AddWithValue("@EsProveedor", entidad.EsProveedor);
			//	mCommand.Parameters.AddWithValue("@Latitud", entidad.Latitud);
			//	mCommand.Parameters.AddWithValue("@Longitud", entidad.Longitud);

			//	if (entidad.FechaNacimiento != null && entidad.FechaNacimiento != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaNacimiento", entidad.FechaNacimiento);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("FechaNacimiento", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Genero))
			//	{
			//		mCommand.Parameters.AddWithValue("@Genero", entidad.Genero.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Genero", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.EstadoCivil))
			//	{
			//		mCommand.Parameters.AddWithValue("@EstadoCivil", entidad.EstadoCivil.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@EstadoCivil", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Nacionalidad))
			//	{
			//		mCommand.Parameters.AddWithValue("@Nacionalidad", entidad.Nacionalidad.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Nacionalidad", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.NivelEstudio))
			//	{
			//		mCommand.Parameters.AddWithValue("@NivelEstudio", entidad.NivelEstudio.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@NivelEstudio", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Profesion))
			//	{
			//		mCommand.Parameters.AddWithValue("@Profesion", entidad.Profesion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Profesion", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", entidad.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", entidad.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(entidad.FechaModificacion != null && entidad.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", entidad.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", entidad.IdEstado);

			//	// Add the primary keys columns
			//	mCommand.Parameters.Add("@Id", SqlDbType.Int);
			//	mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


   //             #endregion
                
   //             // Insert Entidad
   //             if (connection.State != ConnectionState.Open) connection.Open();
   //             mCommand.ExecuteNonQuery();

			//	entidad.Id = Convert.ToInt32(mCommand.Parameters["@Id"].Value);


   //             return entidad;
   //         }
   //         catch (Exception exc)
   //         {
   //             throw exc;
   //         }
   //         finally
   //         {
   //             mCommand.Dispose();
   //         }
   //     }

   //     /// <summary>
   //     /// Update a entity
   //     /// </summary>
   //     public static void Update(EntidadEntity entidad, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "Entidad_Update";

   //              #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", entidad.Id);
			//	mCommand.Parameters.AddWithValue("@IdEmpresa", entidad.IdEmpresa);
			//	mCommand.Parameters.AddWithValue("@TipoEntidad", entidad.TipoEntidad);
   //             mCommand.Parameters.AddWithValue("@TipoIdentificacion", entidad.TipoIdentificacion.ToUpper());
   //             mCommand.Parameters.AddWithValue("@RazonSocial", entidad.RazonSocial);
			//	mCommand.Parameters.AddWithValue("@Identificacion", entidad.Identificacion);
			//	mCommand.Parameters.AddWithValue("@EsCliente", entidad.EsCliente);
			//	mCommand.Parameters.AddWithValue("@EsProveedor", entidad.EsProveedor);
			//	mCommand.Parameters.AddWithValue("@Latitud", entidad.Latitud);
			//	mCommand.Parameters.AddWithValue("@Longitud", entidad.Longitud);

			//	if (entidad.FechaNacimiento != null && entidad.FechaNacimiento != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaNacimiento", entidad.FechaNacimiento);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("FechaNacimiento", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Genero))
			//	{
			//		mCommand.Parameters.AddWithValue("@Genero", entidad.Genero.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Genero", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.EstadoCivil))
			//	{
			//		mCommand.Parameters.AddWithValue("@EstadoCivil", entidad.EstadoCivil.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@EstadoCivil", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Nacionalidad))
			//	{
			//		mCommand.Parameters.AddWithValue("@Nacionalidad", entidad.Nacionalidad.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Nacionalidad", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.NivelEstudio))
			//	{
			//		mCommand.Parameters.AddWithValue("@NivelEstudio", entidad.NivelEstudio.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@NivelEstudio", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(entidad.Profesion))
			//	{
			//		mCommand.Parameters.AddWithValue("@Profesion", entidad.Profesion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Profesion", DBNull.Value);
			//	}

			//	if (entidad.IdPais != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdPais", entidad.IdPais);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdPais",DBNull.Value);
			//	}

			//	if(entidad.IdProvincia != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdProvincia", entidad.IdProvincia);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdProvincia",DBNull.Value);
			//	}

			//	if(entidad.IdCiudad != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdCiudad", entidad.IdCiudad);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdCiudad",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Direccion))
			//	{
			//		mCommand.Parameters.AddWithValue("@Direccion", entidad.Direccion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Direccion",DBNull.Value);
			//	}

			//	if(entidad.IdSector != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSector", entidad.IdSector);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSector",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Telefono))
			//	{
			//		mCommand.Parameters.AddWithValue("@Telefono", entidad.Telefono.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Telefono",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Celular))
			//	{
			//		mCommand.Parameters.AddWithValue("@Celular", entidad.Celular.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Celular",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Correo))
			//	{
			//		mCommand.Parameters.AddWithValue("@Correo", entidad.Correo.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Correo",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.Observaciones))
			//	{
			//		mCommand.Parameters.AddWithValue("@Observaciones", entidad.Observaciones.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Observaciones",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@Foto", entidad.Foto);
			//	mCommand.Parameters.AddWithValue("@IpIngreso", entidad.IpIngreso);
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", entidad.UsuarioIngreso);
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", entidad.FechaIngreso);
			//	if(!String.IsNullOrEmpty(entidad.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", entidad.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(entidad.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", entidad.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(entidad.FechaModificacion != null && entidad.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", entidad.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", entidad.IdEstado);
                
   
   //             #endregion
                
   //             // Update entidad
   //             if (connection.State != ConnectionState.Open) connection.Open();
   //             mCommand.ExecuteNonQuery();


   //         }
   //         catch (Exception exc)
   //         {
   //             throw exc;
   //         }
   //         finally
   //         {
   //             mCommand.Dispose();
   //         }
   //     }

   //      /// <summary>
   //     /// Delete a entity
   //     /// </summary>
   //     public static void Delete(EntidadEntity entidad, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "Entidad_Delete";
			//	mCommand.Parameters.AddWithValue("@Id", entidad.Id);
			//	mCommand.Parameters.AddWithValue("@FechaModificacion", entidad.FechaModificacion);
			//	mCommand.Parameters.AddWithValue("@UsuarioModificacion", entidad.UsuarioModificacion.ToUpper());
			//	mCommand.Parameters.AddWithValue("@IpModificacion", entidad.IpModificacion.ToUpper());

                
   //             // Update entidad
   //             if (connection.State != ConnectionState.Open) connection.Open();
   //             mCommand.ExecuteNonQuery();


   //         }
   //         catch (Exception exc)
   //         {
   //             throw exc;
   //         }
   //         finally
   //         {
   //             mCommand.Dispose();
   //         }
   //     }
        
        
         
         
   //      /// <summary>
   //     /// Load a entity by your Primary Key
   //     /// </summary>
   //     public static EntidadEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //     	return LoadByPK(Id,connection,transaction,1);
   //     }
        
   //     /// <summary>
   //     /// Load a entity by your Primary Key
   //     /// </summary>
   //     public static EntidadEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction, int deepLoadLevel)
   //     {
   //         EntidadEntity entidad = new EntidadEntity();
            
			//entidad.Id = Id;
            
            
   //         SqlCommand mCommand = new SqlCommand();
   //         SqlDataReader reader = null;
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText = "Entidad_LoadByPK";

   //             #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", entidad.Id);
                
 
   //             #endregion 
                
   //             if (connection.State != ConnectionState.Open) connection.Open();

   //             reader = mCommand.ExecuteReader();

   //             if(!reader.HasRows) return null;
                
	  //          while (reader.Read())
	  //          {
			//		#region << Deep Load >>
   //                 if (deepLoadLevel == 1)
		 //    		{
			//			entidad.IdPaisAsPais = PaisDataAccess.ConvertToPaisEntity(reader, "IdPais");
			//			entidad.IdProvinciaAsProvincia = ProvinciaDataAccess.ConvertToProvinciaEntity(reader, "IdProvincia");
			//			entidad.IdCiudadAsCiudad = CiudadDataAccess.ConvertToCiudadEntity(reader, "IdCiudad");
			//			entidad.IdSectorAsSector = SectorDataAccess.ConvertToSectorEntity(reader, "IdSector");

   //                 }
	  //              #endregion
	                
	  //              #region << Load the BusinessEntity Object >>
					
			//		entidad.Id = Convert.ToInt32(reader["Id"]);
			//		entidad.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
			//		entidad.TipoEntidad = Convert.ToString(reader["TipoEntidad"]);
   //                 entidad.TipoIdentificacion = Convert.ToString(reader["TipoIdentificacion"]);
   //                 entidad.RazonSocial = Convert.ToString(reader["RazonSocial"]);
			//		entidad.Identificacion = Convert.ToString(reader["Identificacion"]);
			//		entidad.EsCliente = Convert.ToBoolean(reader["EsCliente"]);
			//		entidad.EsProveedor = Convert.ToBoolean(reader["EsProveedor"]);
			//		entidad.EsConsumidorFinal = Convert.ToBoolean(reader["EsConsumidorFinal"]);

			//		if (reader["IdPais"] != DBNull.Value)
			//		{
			//			entidad.IdPais = Convert.ToInt16(reader["IdPais"]);
			//		}
			//		if (reader["IdProvincia"] != DBNull.Value)
			//		{
			//			entidad.IdProvincia = Convert.ToInt32(reader["IdProvincia"]);
			//		}
			//		if (reader["IdCiudad"] != DBNull.Value)
			//		{
			//			entidad.IdCiudad = Convert.ToInt32(reader["IdCiudad"]);
			//		}
			//		if (reader["Direccion"] != DBNull.Value)
			//		{
			//			entidad.Direccion = Convert.ToString(reader["Direccion"]).ToUpper();
			//		}
			//		if (reader["IdSector"] != DBNull.Value)
			//		{
			//			entidad.IdSector = Convert.ToInt32(reader["IdSector"]);
			//		}
			//		if (reader["Telefono"] != DBNull.Value)
			//		{
			//			entidad.Telefono = Convert.ToString(reader["Telefono"]).ToUpper();
			//		}
			//		if (reader["Celular"] != DBNull.Value)
			//		{
			//			entidad.Celular = Convert.ToString(reader["Celular"]).ToUpper();
			//		}
			//		if (reader["Correo"] != DBNull.Value)
			//		{
			//			entidad.Correo = Convert.ToString(reader["Correo"]).ToUpper();
			//		}
			//		if (reader["Observaciones"] != DBNull.Value)
			//		{
			//			entidad.Observaciones = Convert.ToString(reader["Observaciones"]).ToUpper();
			//		}
			//		if (reader["Foto"] != DBNull.Value)
			//		{
			//			//entidad.Foto = (byte[]) reader["Foto"];
			//			entidad.Foto = Convert.ToString(reader["Foto"]);
			//		}
			//		entidad.IpIngreso = Convert.ToString(reader["IpIngreso"]);
			//		entidad.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
			//		entidad.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
			//		if (reader["IpModificacion"] != DBNull.Value)
			//		{
			//			entidad.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
			//		}
			//		if (reader["UsuarioModificacion"] != DBNull.Value)
			//		{
			//			entidad.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
			//		}
			//		if (reader["FechaModificacion"] != DBNull.Value)
			//		{
			//			entidad.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
			//		}
   //                 if (reader["Latitud"] != DBNull.Value)
   //                 {
   //                     entidad.Latitud = Convert.ToString(reader["Latitud"]).ToUpper();
   //                 }
   //                 if (reader["Longitud"] != DBNull.Value)
   //                 {
   //                     entidad.Longitud = Convert.ToString(reader["Longitud"]).ToUpper();
   //                 }
   //                 if (reader["FechaNacimiento"] != DBNull.Value)
   //                 {
   //                     entidad.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
   //                 }
			//		if (reader["Genero"] != DBNull.Value)
			//		{
			//			entidad.Genero = Convert.ToString(reader["Genero"]).ToUpper();
			//		}
			//		if (reader["EstadoCivil"] != DBNull.Value)
			//		{
			//			entidad.EstadoCivil = Convert.ToString(reader["EstadoCivil"]).ToUpper();
			//		}
			//		if (reader["Nacionalidad"] != DBNull.Value)
			//		{
			//			entidad.Nacionalidad = Convert.ToString(reader["Nacionalidad"]).ToUpper();
			//		}
			//		if (reader["NivelEstudio"] != DBNull.Value)
			//		{
			//			entidad.NivelEstudio = Convert.ToString(reader["NivelEstudio"]).ToUpper();
			//		}
			//		if (reader["Profesion"] != DBNull.Value)
			//		{
			//			entidad.Profesion = Convert.ToString(reader["Profesion"]).ToUpper();
			//		}
			//		entidad.IdEstado = Convert.ToInt16(reader["IdEstado"]);

	  //              #endregion
	  //          }

   //             entidad.SetLoadedState();
   //             return entidad;
   //         }
   //         catch (Exception exc)
   //         {
   //             throw exc;
   //         }
   //         finally
   //         {
   //             if (reader != null) reader.Close();
   //             mCommand.Dispose();
   //         }
   //     }
        
   //     #endregion
        
        
        
        
   //     #region << Mappers >>
        
   //     public static EntidadEntity ConvertToEntidadEntity (SqlDataReader reader,string fkColumnName)
   //     {
   //         EntidadEntity entidad = new EntidadEntity();
            
   //         try
   //         {
   //             bool hasData=false;
   //             string columName;
                
   //             #region << Load the BusinessEntity Object >>
                
			//	try
			//	{
			//		columName = String.Format("Id_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Id = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdEmpresa_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdEmpresa = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TipoEntidad_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.TipoEntidad = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
   //             try
   //             {
   //                 columName = String.Format("TipoIdentificacion_EntidadFrom{0}", fkColumnName);
   //                 if (reader[columName] != DBNull.Value)
   //                 {
   //                     entidad.TipoIdentificacion = Convert.ToString(reader[columName]).ToUpper();
   //                     hasData = true;
   //                 }
   //             }
   //             catch { }
   //             try
			//	{
			//		columName = String.Format("RazonSocial_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.RazonSocial = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Identificacion_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Identificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdPais_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdPais = Convert.ToInt16(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdProvincia_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdProvincia = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdCiudad_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdCiudad = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Direccion_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Direccion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdSector_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdSector = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Telefono_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Telefono = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Celular_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Celular = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Correo_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Correo = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Observaciones_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Observaciones = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Foto_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			//entidad.Foto = (byte[]) reader[columName];
			//			entidad.Foto = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpIngreso_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IpIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioIngreso_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.UsuarioIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaIngreso_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.FechaIngreso = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpModificacion_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IpModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioModificacion_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.UsuarioModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaModificacion_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.FechaModificacion = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}

			//	try
			//	{
			//		columName = String.Format("IdEstado_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.IdEstado = Convert.ToInt16(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("EsCliente_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.EsCliente = Convert.ToBoolean(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }
			//	try
			//	{
			//		columName = String.Format("EsProveedor_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.EsProveedor = Convert.ToBoolean(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	try
			//	{
			//		columName = String.Format("Latitud_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Latitud = Convert.ToString(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	try
			//	{
			//		columName = String.Format("Longitud_EntidadFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			entidad.Longitud = Convert.ToString(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }
			//	#endregion

			//	entidad.SetLoadedState();
   //             if(hasData)
   //             {
   //             	return entidad;
   //             }
   //             else return null;
   //         }
   //         catch (Exception exc)
   //         {
   //             return null;
   //         }
   //         finally
   //         {
                
   //         }
   //     }
        
   //     #endregion
        
   
    }
}


