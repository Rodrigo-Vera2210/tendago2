    
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
    public partial class DocumentoDataAccess
    {
    
   
   //     #region << Default Methods >>

   //     /// <summary>
   //     /// Create a new entity type of Documento
   //     /// </summary>
   //     public static DocumentoEntity Insert(DocumentoEntity documento, SqlConnection connection, SqlTransaction transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText =  "Documento_Insert";

   //             #region << Add the params >>
                 
			//	mCommand.Parameters.AddWithValue("@IdTipoDocumento", documento.IdTipoDocumento.ToUpper());
			//	mCommand.Parameters.AddWithValue("@IdEmpresa", documento.IdEmpresa);
				
			//	if(!String.IsNullOrEmpty(documento.IdSalida))
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSalida", documento.IdSalida.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSalida",DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(documento.Establecimiento))
			//	{
			//		mCommand.Parameters.AddWithValue("@Establecimiento", documento.Establecimiento);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Establecimiento", DBNull.Value);
			//	}
				
			//	if (!String.IsNullOrEmpty(documento.PuntoEmision))
			//	{
			//		mCommand.Parameters.AddWithValue("@PuntoEmision", documento.PuntoEmision);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@PuntoEmision", DBNull.Value);
			//	}

			//	if (!String.IsNullOrEmpty(documento.RUC))
			//	{
			//		mCommand.Parameters.AddWithValue("@RUC", documento.RUC);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@RUC", DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@ConsumidorFinal", documento.ConsumidorFinal);
			//	mCommand.Parameters.AddWithValue("@IdEntidad", documento.IdEntidad);
			//	mCommand.Parameters.AddWithValue("@IdMoneda", documento.IdMoneda);
			//	if(!String.IsNullOrEmpty(documento.Notas))
			//	{
			//		mCommand.Parameters.AddWithValue("@Notas", documento.Notas.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Notas",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.GuiaRemision))
			//	{
			//		mCommand.Parameters.AddWithValue("@GuiaRemision", documento.GuiaRemision.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@GuiaRemision",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.NumeroDocumento))
			//	{
			//		mCommand.Parameters.AddWithValue("@NumeroDocumento", documento.NumeroDocumento.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@NumeroDocumento",DBNull.Value);
			//	}

   //             if (documento.IdFacturaGo>0)
   //             {
   //                 mCommand.Parameters.AddWithValue("@IdFacturaGo", documento.IdFacturaGo);
   //             }
   //             else
   //             {
   //                 mCommand.Parameters.AddWithValue("@IdFacturaGo", DBNull.Value);
   //             }

   //             mCommand.Parameters.AddWithValue("@Fecha", documento.Fecha);
			//	mCommand.Parameters.AddWithValue("@Base0", documento.Base0);
			//	mCommand.Parameters.AddWithValue("@BaseIva", documento.BaseIva);
			//	mCommand.Parameters.AddWithValue("@TotalDescuento", documento.TotalDescuento);
			//	mCommand.Parameters.AddWithValue("@TotalIce", documento.TotalIce);
			//	mCommand.Parameters.AddWithValue("@TotalIva", documento.TotalIva);
			//	mCommand.Parameters.AddWithValue("@FormaPago", documento.FormaPago.ToUpper());
			//	mCommand.Parameters.AddWithValue("@Plazo", documento.Plazo);
			//	mCommand.Parameters.AddWithValue("@UnidadTiempo", documento.UnidadTiempo.ToUpper());
			//	mCommand.Parameters.AddWithValue("@IpIngreso", documento.IpIngreso.ToUpper());
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", documento.UsuarioIngreso.ToUpper());
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", documento.FechaIngreso);
			//	if(!String.IsNullOrEmpty(documento.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", documento.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", documento.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(documento.FechaModificacion != null && documento.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", documento.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", documento.IdEstado);

			//	// Add the primary keys columns
			//	mCommand.Parameters.Add("@Id", SqlDbType.VarChar, 20);
			//	mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


   //             #endregion
                
   //             // Insert Documento
   //             if (connection.State != ConnectionState.Open) connection.Open();
   //             mCommand.ExecuteNonQuery();

			//	documento.Id = Convert.ToString(mCommand.Parameters["@Id"].Value);


   //             return documento;
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
   //     public static void Update(DocumentoEntity documento, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "Documento_Update";

   //              #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", documento.Id);
			//	mCommand.Parameters.AddWithValue("@IdTipoDocumento", documento.IdTipoDocumento);
			//	mCommand.Parameters.AddWithValue("@IdEmpresa", documento.IdEmpresa);
			//	if(!String.IsNullOrEmpty(documento.IdSalida))
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSalida", documento.IdSalida.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IdSalida",DBNull.Value);
			//	}
			//	mCommand.Parameters.AddWithValue("@ConsumidorFinal", documento.ConsumidorFinal);
			//	mCommand.Parameters.AddWithValue("@IdEntidad", documento.IdEntidad);
			//	mCommand.Parameters.AddWithValue("@IdMoneda", documento.IdMoneda);
			//	if(!String.IsNullOrEmpty(documento.Notas))
			//	{
			//		mCommand.Parameters.AddWithValue("@Notas", documento.Notas.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Notas",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.GuiaRemision))
			//	{
			//		mCommand.Parameters.AddWithValue("@GuiaRemision", documento.GuiaRemision.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@GuiaRemision",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.NumeroDocumento))
			//	{
			//		mCommand.Parameters.AddWithValue("@NumeroDocumento", documento.NumeroDocumento.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@NumeroDocumento",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@Base0", documento.Base0);
			//	mCommand.Parameters.AddWithValue("@BaseIva", documento.BaseIva);
			//	mCommand.Parameters.AddWithValue("@TotalDescuento", documento.TotalDescuento);
			//	if(documento.TotalSinImpuesto != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@TotalSinImpuesto", documento.TotalSinImpuesto);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@TotalSinImpuesto",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@TotalIce", documento.TotalIce);
			//	mCommand.Parameters.AddWithValue("@TotalIva", documento.TotalIva);
			//	if(documento.Total != 0)
			//	{
			//		mCommand.Parameters.AddWithValue("@Total", documento.Total);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@Total",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@FormaPago", documento.FormaPago);
			//	mCommand.Parameters.AddWithValue("@Plazo", documento.Plazo);
			//	mCommand.Parameters.AddWithValue("@UnidadTiempo", documento.UnidadTiempo);
			//	mCommand.Parameters.AddWithValue("@IpIngreso", documento.IpIngreso);
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", documento.UsuarioIngreso);
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", documento.FechaIngreso);
			//	if(!String.IsNullOrEmpty(documento.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", documento.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(documento.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", documento.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(documento.FechaModificacion != null && documento.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", documento.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", documento.IdEstado);
                
   
   //             #endregion
                
   //             // Update documento
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
   //     public static void Delete(DocumentoEntity documento, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "Documento_Delete";
			//	mCommand.Parameters.AddWithValue("@Id", documento.Id.ToUpper());
			//	mCommand.Parameters.AddWithValue("@Fecha_Modificacion", documento.FechaModificacion);
			//	mCommand.Parameters.AddWithValue("@Usuario_Modificacion", documento.UsuarioModificacion.ToUpper());
			//	mCommand.Parameters.AddWithValue("@Ip_Modificacion", documento.IpModificacion.ToUpper());

                
   //             // Update documento
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
   //     public static DocumentoEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //     	return LoadByPK(Id,connection,transaction,1);
   //     }
        
   //     /// <summary>
   //     /// Load a entity by your Primary Key
   //     /// </summary>
   //     public static DocumentoEntity LoadByPK(string Id, SqlConnection connection, SqlTransaction  transaction, int deepLoadLevel)
   //     {
   //         DocumentoEntity documento = new DocumentoEntity();
            
			//documento.Id = Id.ToUpper();
            
            
   //         SqlCommand mCommand = new SqlCommand();
   //         SqlDataReader reader = null;
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText = "Documento_LoadByPK";

   //             #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", documento.Id.ToUpper());
                
 
   //             #endregion 
                
   //             if (connection.State != ConnectionState.Open) connection.Open();

   //             reader = mCommand.ExecuteReader();

   //             if(!reader.HasRows) return null;
                
	  //          while (reader.Read())
	  //          {
			//		#region << Deep Load >>
   //                 if (deepLoadLevel == 1)
		 //    		{
			//			documento.IdTipoDocumentoAsTipoDocumento = TipoDocumentoDataAccess.ConvertToTipoDocumentoEntity(reader, "IdTipoDocumento");
			//			documento.IdEntidadAsEntidad = EntidadDataAccess.ConvertToEntidadEntity(reader, "IdEntidad");
			//			documento.IdMonedaAsMoneda = MonedaDataAccess.ConvertToMonedaEntity(reader, "IdMoneda");

   //                 }
	  //              #endregion
	                
	  //              #region << Load the BusinessEntity Object >>
					
			//		documento.Id = Convert.ToString(reader["Id"]);
			//		documento.IdTipoDocumento = Convert.ToString(reader["IdTipoDocumento"]);
			//		documento.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
			//		if (reader["IdSalida"] != DBNull.Value)
			//		{
			//			documento.IdSalida = Convert.ToString(reader["IdSalida"]).ToUpper();
			//		}
			//		documento.IdEntidad = Convert.ToInt32(reader["IdEntidad"]);
			//		documento.IdMoneda = Convert.ToInt16(reader["IdMoneda"]);
			//		if (reader["Notas"] != DBNull.Value)
			//		{
			//			documento.Notas = Convert.ToString(reader["Notas"]).ToUpper();
			//		}
			//		if (reader["GuiaRemision"] != DBNull.Value)
			//		{
			//			documento.GuiaRemision = Convert.ToString(reader["GuiaRemision"]).ToUpper();
			//		}
			//		if (reader["NumeroDocumento"] != DBNull.Value)
			//		{
			//			documento.NumeroDocumento = Convert.ToString(reader["NumeroDocumento"]).ToUpper();
			//		}
			//		documento.Base0 = (decimal) reader["Base0"];
			//		documento.BaseIva = (decimal) reader["BaseIva"];
			//		documento.TotalDescuento = (decimal) reader["TotalDescuento"];
			//		if (reader["TotalSinImpuesto"] != DBNull.Value)
			//		{
			//			documento.TotalSinImpuesto = (decimal) reader["TotalSinImpuesto"];
			//		}
			//		documento.TotalIce = (decimal) reader["TotalIce"];
			//		documento.TotalIva = (decimal) reader["TotalIva"];
			//		if (reader["Total"] != DBNull.Value)
			//		{
			//			documento.Total = (decimal) reader["Total"];
			//		}
			//		documento.ConsumidorFinal = Convert.ToBoolean(reader["ConsumidorFinal"]);
			//		documento.FormaPago = Convert.ToString(reader["FormaPago"]);
			//		documento.Plazo = Convert.ToInt32(reader["Plazo"]);
			//		documento.UnidadTiempo = Convert.ToString(reader["UnidadTiempo"]);
			//		documento.IpIngreso = Convert.ToString(reader["IpIngreso"]);
			//		documento.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
			//		documento.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
			//		if (reader["IpModificacion"] != DBNull.Value)
			//		{
			//			documento.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
			//		}
			//		if (reader["UsuarioModificacion"] != DBNull.Value)
			//		{
			//			documento.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
			//		}
			//		if (reader["FechaModificacion"] != DBNull.Value)
			//		{
			//			documento.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
			//		}
			//		documento.IdEstado = Convert.ToInt16(reader["IdEstado"]);
			//		documento.Fecha = Convert.ToDateTime(reader["Fecha"]);

			//		if (reader["RUC"] != DBNull.Value)
			//		{
			//			documento.RUC = Convert.ToString(reader["RUC"]);
			//		}
			//		if (reader["Establecimiento"] != DBNull.Value)
			//		{
			//			documento.Establecimiento = Convert.ToString(reader["Establecimiento"]);
			//		}
			//		if (reader["PuntoEmision"] != DBNull.Value)
			//		{
			//			documento.PuntoEmision = Convert.ToString(reader["PuntoEmision"]);
			//		}

   //                 if (reader["IdFacturaGo"] != DBNull.Value)
   //                 {
   //                     documento.IdFacturaGo = Convert.ToInt64(reader["IdFacturaGo"]);
   //                 }

   //                 #endregion
   //             }

   //             documento.SetLoadedState();
   //             return documento;
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
        
   //     public static DocumentoEntity ConvertToDocumentoEntity (SqlDataReader reader,string fkColumnName)
   //     {
   //         DocumentoEntity documento = new DocumentoEntity();
            
   //         try
   //         {
   //             bool hasData=false;
   //             string columName;
                
   //             #region << Load the BusinessEntity Object >>
                
			//	try
			//	{
			//		columName = String.Format("Id_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Id = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdTipoDocumento_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdTipoDocumento = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdEmpresa_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdEmpresa = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdSalida_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdSalida = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdEntidad_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdEntidad = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdMoneda_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdMoneda = Convert.ToInt16(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Notas_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Notas = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("GuiaRemision_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.GuiaRemision = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch { }
			//	try
			//	{
			//		columName = String.Format("ConsumidorFinal_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.ConsumidorFinal = Convert.ToBoolean(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch {}
			//	try
			//	{
			//		columName = String.Format("NumeroDocumento_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.NumeroDocumento = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Base0_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Base0 = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("BaseIva_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.BaseIva = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TotalDescuento_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.TotalDescuento = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TotalSinImpuesto_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.TotalSinImpuesto = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TotalIce_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.TotalIce = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TotalIva_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.TotalIva = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Total_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Total = (decimal) reader[columName];
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FormaPago_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.FormaPago = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("Plazo_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Plazo = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UnidadTiempo_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.UnidadTiempo = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpIngreso_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IpIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioIngreso_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.UsuarioIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaIngreso_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.FechaIngreso = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpModificacion_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IpModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioModificacion_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.UsuarioModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaModificacion_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.FechaModificacion = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdEstado_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.IdEstado = Convert.ToInt16(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}


			//	try
			//	{
			//		columName = String.Format("RUC_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.RUC = Convert.ToString(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	try
			//	{
			//		columName = String.Format("Establecimiento_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Establecimiento = Convert.ToString(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	try
			//	{
			//		columName = String.Format("PuntoEmision_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.PuntoEmision = Convert.ToString(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	try
			//	{
			//		columName = String.Format("Fecha_DocumentoFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			documento.Fecha = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch { }

			//	#endregion

			//	documento.SetLoadedState();
   //             if(hasData)
   //             {
   //             	return documento;
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


