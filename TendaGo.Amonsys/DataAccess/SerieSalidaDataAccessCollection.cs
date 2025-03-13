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
    public partial class SerieSalidaDataAccessCollection
    {
        //#region << Custom Stored Procedures >>


        //#endregion

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction)
        //{
        //    return FindByAll(findParameter, conexion, transaction, 1);
        //}

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter, SqlConnection conexion, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    SqlCommand mCommand = new SqlCommand();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        mCommand.Connection = conexion;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        if (deepLoadLevel >= 1)
        //        {
        //            mCommand.CommandText = "SerieSalida_DeepFindByAll";
        //        }
        //        else mCommand.CommandText = "SerieSalida_FindByAll";

        //        #region Parameters
        //        if (findParameter.Id != int.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@Id", findParameter.Id);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@Id", DBNull.Value);
        //        }

        //        if (!string.IsNullOrEmpty(findParameter.IdDetalleSalida))
        //        {
        //            mCommand.Parameters.AddWithValue("@IdDetalleSalida", findParameter.IdDetalleSalida);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdDetalleSalida", DBNull.Value);
        //        }

        //        if (findParameter.IdProducto != int.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdProducto", findParameter.IdProducto);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdProducto", DBNull.Value);
        //        }

        //        if (!string.IsNullOrEmpty(findParameter.Serie))
        //        {
        //            mCommand.Parameters.AddWithValue("@Serie", findParameter.Serie);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@Serie", DBNull.Value);
        //        }

        //        if (!String.IsNullOrEmpty(findParameter.IpIngreso))
        //        {
        //            mCommand.Parameters.AddWithValue("@IpIngreso", findParameter.IpIngreso);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IpIngreso", DBNull.Value);
        //        }

        //        if (!String.IsNullOrEmpty(findParameter.UsuarioIngreso))
        //        {
        //            mCommand.Parameters.AddWithValue("@UsuarioIngreso", findParameter.UsuarioIngreso);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@UsuarioIngreso", DBNull.Value);
        //        }

        //        if (findParameter.FechaIngreso != null && findParameter.FechaIngreso != DateTime.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@FechaIngreso", findParameter.FechaIngreso);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@FechaIngreso", DBNull.Value);
        //        }

        //        if (!String.IsNullOrEmpty(findParameter.IpModificacion))
        //        {
        //            mCommand.Parameters.AddWithValue("@IpModificacion", findParameter.IpModificacion);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IpModificacion", DBNull.Value);
        //        }

        //        if (!String.IsNullOrEmpty(findParameter.UsuarioModificacion))
        //        {
        //            mCommand.Parameters.AddWithValue("@UsuarioModificacion", findParameter.UsuarioModificacion);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);
        //        }

        //        if (findParameter.FechaModificacion != null && findParameter.FechaModificacion != DateTime.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@FechaModificacion", findParameter.FechaModificacion);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@FechaModificacion", DBNull.Value);
        //        }

        //        if (findParameter.IdEstado != short.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdEstado", findParameter.IdEstado);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdEstado", DBNull.Value);
        //        }

        //        if (findParameter.IdEmpresa != int.MinValue)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdEmpresa", findParameter.IdEmpresa);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdEmpresa", DBNull.Value);
        //        }
        //        #endregion


        //        if (conexion.State != ConnectionState.Open) conexion.Open();
        //        reader = mCommand.ExecuteReader();

        //        SerieSalidaEntityCollection SerieSalidaEntityCollection = new SerieSalidaEntityCollection();
        //        SerieSalidaEntity detalleCobroEntity;


        //        while (reader.Read())
        //        {
        //            detalleCobroEntity = new SerieSalidaEntity();
                    
        //            #region << Deep Load >>                    
        //            #endregion

        //            detalleCobroEntity.Id = Convert.ToInt32(reader["Id"]);
        //            detalleCobroEntity.IdDetalleSalida = Convert.ToString(reader["IdDetalleSalida"]);
        //            detalleCobroEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);
        //            detalleCobroEntity.Serie = Convert.ToString(reader["Serie"]);
                    
        //            detalleCobroEntity.IpIngreso = Convert.ToString(reader["IpIngreso"]);
        //            detalleCobroEntity.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
        //            detalleCobroEntity.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
        //            if (reader["IpModificacion"] != DBNull.Value)
        //            {
        //                detalleCobroEntity.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
        //            }
        //            if (reader["UsuarioModificacion"] != DBNull.Value)
        //            {
        //                detalleCobroEntity.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
        //            }
        //            if (reader["FechaModificacion"] != DBNull.Value)
        //            {
        //                detalleCobroEntity.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
        //            }                    
        //            detalleCobroEntity.IdEstado = Convert.ToInt16(reader["IdEstado"]);
        //            detalleCobroEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);


        //            detalleCobroEntity.SetLoadedState();
        //            SerieSalidaEntityCollection.Add(detalleCobroEntity);

        //        }

        //        return SerieSalidaEntityCollection;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (reader != null) reader.Close();
        //        mCommand.Dispose();
        //    }

        //}
    }
}
