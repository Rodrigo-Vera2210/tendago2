    
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
    public partial class TipoPaqueteDataAccess
    {
    
   
   //     #region << Default Methods >>

   //     /// <summary>
   //     /// Create a new entity type of TipoPaquete
   //     /// </summary>
   //     public static TipoPaqueteEntity Insert(TipoPaqueteEntity tipoPaquete, SqlConnection connection, SqlTransaction transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText =  "TipoPaquete_Insert";

   //             #region << Add the params >>
                 
			//	mCommand.Parameters.AddWithValue("@TipoPaquete", tipoPaquete.TipoPaquete.ToUpper());
			//	mCommand.Parameters.AddWithValue("@IpIngreso", tipoPaquete.IpIngreso.ToUpper());
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", tipoPaquete.UsuarioIngreso.ToUpper());
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", tipoPaquete.FechaIngreso);
			//	if(!String.IsNullOrEmpty(tipoPaquete.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", tipoPaquete.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(tipoPaquete.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", tipoPaquete.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(tipoPaquete.FechaModificacion != null && tipoPaquete.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", tipoPaquete.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", tipoPaquete.IdEstado);

			//	// Add the primary keys columns
			//	mCommand.Parameters.Add("@Id", SqlDbType.Int);
			//	mCommand.Parameters["@Id"].Direction = ParameterDirection.Output;


   //             #endregion
                
   //             // Insert TipoPaquete
   //             if (connection.State != ConnectionState.Open) connection.Open();
   //             mCommand.ExecuteNonQuery();

			//	tipoPaquete.Id = Convert.ToInt32(mCommand.Parameters["@Id"].Value);


   //             return tipoPaquete;
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
   //     public static void Update(TipoPaqueteEntity tipoPaquete, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "TipoPaquete_Update";

   //              #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", tipoPaquete.Id);
			//	mCommand.Parameters.AddWithValue("@TipoPaquete", tipoPaquete.TipoPaquete);
			//	mCommand.Parameters.AddWithValue("@IpIngreso", tipoPaquete.IpIngreso);
			//	mCommand.Parameters.AddWithValue("@UsuarioIngreso", tipoPaquete.UsuarioIngreso);
			//	mCommand.Parameters.AddWithValue("@FechaIngreso", tipoPaquete.FechaIngreso);
			//	if(!String.IsNullOrEmpty(tipoPaquete.IpModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion", tipoPaquete.IpModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@IpModificacion",DBNull.Value);
			//	}

			//	if(!String.IsNullOrEmpty(tipoPaquete.UsuarioModificacion))
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion", tipoPaquete.UsuarioModificacion.ToUpper());
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("@UsuarioModificacion",DBNull.Value);
			//	}

			//	if(tipoPaquete.FechaModificacion != null && tipoPaquete.FechaModificacion != DateTime.MinValue)
			//	{
			//		mCommand.Parameters.AddWithValue("@FechaModificacion", tipoPaquete.FechaModificacion);
			//	}
			//	else
			//	{
			//		mCommand.Parameters.AddWithValue("FechaModificacion",DBNull.Value);
			//	}

			//	mCommand.Parameters.AddWithValue("@IdEstado", tipoPaquete.IdEstado);
                
   
   //             #endregion
                
   //             // Update tipoPaquete
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
   //     public static void Delete(TipoPaqueteEntity tipoPaquete, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //         SqlCommand mCommand = new SqlCommand();
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;;
   //             mCommand.CommandText = "TipoPaquete_Delete";
			//	mCommand.Parameters.AddWithValue("@Id", tipoPaquete.Id);
			//	mCommand.Parameters.AddWithValue("@FechaModificacion", tipoPaquete.FechaModificacion);
			//	mCommand.Parameters.AddWithValue("@UsuarioModificacion", tipoPaquete.UsuarioModificacion.ToUpper());
			//	mCommand.Parameters.AddWithValue("@IpModificacion", tipoPaquete.IpModificacion.ToUpper());

                
   //             // Update tipoPaquete
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
   //     public static TipoPaqueteEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction)
   //     {
   //     	return LoadByPK(Id,connection,transaction,1);
   //     }
        
   //     /// <summary>
   //     /// Load a entity by your Primary Key
   //     /// </summary>
   //     public static TipoPaqueteEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction  transaction, int deepLoadLevel)
   //     {
   //         TipoPaqueteEntity tipoPaquete = new TipoPaqueteEntity();
            
			//tipoPaquete.Id = Id;
            
            
   //         SqlCommand mCommand = new SqlCommand();
   //         SqlDataReader reader = null;
   //         try
   //         {
   //             mCommand.Connection = connection;
   //             mCommand.CommandType = CommandType.StoredProcedure;
   //             mCommand.Transaction = transaction;
   //             mCommand.CommandText = "TipoPaquete_LoadByPK";

   //             #region << Add the params >>

			//	mCommand.Parameters.AddWithValue("@Id", tipoPaquete.Id);
                
 
   //             #endregion 
                
   //             if (connection.State != ConnectionState.Open) connection.Open();

   //             reader = mCommand.ExecuteReader();

   //             if(!reader.HasRows) return null;
                
	  //          while (reader.Read())
	  //          {
			//		#region << Deep Load >>
   //                 if (deepLoadLevel == 1)
		 //    		{

   //                 }
	  //              #endregion
	                
	  //              #region << Load the BusinessEntity Object >>
					
			//		tipoPaquete.Id = Convert.ToInt32(reader["Id"]);
			//		tipoPaquete.TipoPaquete = Convert.ToString(reader["TipoPaquete"]);
			//		tipoPaquete.IpIngreso = Convert.ToString(reader["IpIngreso"]);
			//		tipoPaquete.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
			//		tipoPaquete.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
			//		if (reader["IpModificacion"] != DBNull.Value)
			//		{
			//			tipoPaquete.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
			//		}
			//		if (reader["UsuarioModificacion"] != DBNull.Value)
			//		{
			//			tipoPaquete.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
			//		}
			//		if (reader["FechaModificacion"] != DBNull.Value)
			//		{
			//			tipoPaquete.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
			//		}
			//		tipoPaquete.IdEstado = Convert.ToInt16(reader["IdEstado"]);

	  //              #endregion
	  //          }

   //             tipoPaquete.SetLoadedState();
   //             return tipoPaquete;
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
        
   //     public static TipoPaqueteEntity ConvertToTipoPaqueteEntity (SqlDataReader reader,string fkColumnName)
   //     {
   //         TipoPaqueteEntity tipoPaquete = new TipoPaqueteEntity();
            
   //         try
   //         {
   //             bool hasData=false;
   //             string columName;
                
   //             #region << Load the BusinessEntity Object >>
                
			//	try
			//	{
			//		columName = String.Format("Id_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.Id = Convert.ToInt32(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("TipoPaquete_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.TipoPaquete = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpIngreso_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.IpIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioIngreso_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.UsuarioIngreso = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaIngreso_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.FechaIngreso = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IpModificacion_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.IpModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("UsuarioModificacion_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.UsuarioModificacion = Convert.ToString(reader[columName]).ToUpper();
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("FechaModificacion_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.FechaModificacion = Convert.ToDateTime(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}
			//	try
			//	{
			//		columName = String.Format("IdEstado_TipoPaqueteFrom{0}", fkColumnName);
			//		if (reader[columName] != DBNull.Value)
			//		{
			//			tipoPaquete.IdEstado = Convert.ToInt16(reader[columName]);
			//			hasData = true;
			//		}
			//	}
			//	catch{}

                
   //             #endregion
                
   //             tipoPaquete.SetLoadedState();
   //             if(hasData)
   //             {
   //             	return tipoPaquete;
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


