
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
using System.Collections.Generic;
using ER.BE; 


namespace ER.DA
{
    public partial class StockDataAccessCollection
    {
        //public static DataSet SaldoInventarioDS(int IdEmpresa, object IdProducto, int IdLocal, SqlConnection conexion, SqlTransaction transaction)
        //{
        //    SqlCommand mCommand = new SqlCommand();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter();

        //        mCommand.Connection = conexion;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Stock_SaldoInventario";

        //        mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
        //        mCommand.Parameters.AddWithValue("@IdProducto", Convert.ToString(IdProducto));
                
        //        if (IdLocal != 0)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", IdLocal);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
        //        }

        //        adapter.SelectCommand = mCommand;

        //        DataSet result = new DataSet();
        //        adapter.Fill(result);
        //        return result;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        mCommand.Dispose();
        //    }
        //}

        //public static ProductoEntityCollection SaldoInventario(int IdEmpresa, object IdProducto, int IdLocal, SqlConnection connection, SqlTransaction transaction)
        //{
        //    ProductoEntityCollection productoCollection = new ProductoEntityCollection();

        //    SqlCommand mCommand = new SqlCommand();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Stock_SaldoInventario";

        //        #region << Add the params >>

        //        mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
        //        mCommand.Parameters.AddWithValue("@IdProducto", Convert.ToString(IdProducto));

        //        if (IdLocal > 0)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", IdLocal);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
        //        }

        //        #endregion

        //        if (connection.State != ConnectionState.Open) connection.Open();

        //        reader = mCommand.ExecuteReader();

        //        if (!reader.HasRows) return null;

        //        while (reader.Read())
        //        {
        //            var producto = new ProductoEntity();

        //            #region << Load the BusinessEntity Object >>

        //            producto.Id = Convert.ToInt32(reader["Id"]);
        //            producto.IdEmpresa = Convert.ToInt16(reader["IdEmpresa"]);
        //            producto.IdTarifaImpuesto = Convert.ToInt16(reader["IdTarifaImpuesto"]);
        //            producto.PorcentajeTarifaImpuesto = Convert.ToInt16(reader["PorcentajeTarifaImpuesto"]);
        //            //producto.CodigoProveedor = Convert.ToString(reader["CodigoProveedor"]);
        //            //producto.CodigoInterno = Convert.ToString(reader["CodigoInterno"]);
        //            //producto.TipoProducto = Convert.ToString(reader["TipoProducto"]);
        //            producto.Producto = Convert.ToString(reader["Producto"]);
        //            producto.CodigoInterno = Convert.ToString(reader["CodigoInterno"]);
        //            producto.CodigoProveedor = Convert.ToString(reader["CodigoProveedor"]);
        //            producto.CodigoBarra = Convert.ToString(reader["CodigoBarra"]);
        //            producto.TipoProducto = Convert.ToString(reader["TipoProducto"]);
        //            if (reader["Descripcion"] != DBNull.Value)
        //            {
        //                producto.Descripcion = Convert.ToString(reader["Descripcion"]);
        //            }
        //            //producto.DescipcionBusqueda = Convert.ToString(reader["DescipcionBusqueda"]);
        //            if (reader["PathArchivo"] != DBNull.Value)
        //            {
        //                producto.PathArchivo = Convert.ToString(reader["PathArchivo"]);
        //            }
        //            producto.Stock = Convert.ToDecimal(reader["Stock"]);
        //            //if (reader["StockMinimo"] != DBNull.Value)
        //            //{
        //            //    producto.StockMinimo = Convert.ToInt32(reader["StockMinimo"]);
        //            //}

        //            producto.Costo = (decimal)reader["Costo"];

        //            if (reader["ProductoPadre"] != DBNull.Value)
        //            {
        //                producto.ProductoPadre = Convert.ToInt32(reader["ProductoPadre"]);
        //            }

        //            //if (reader["UnidadMedida"] != DBNull.Value)
        //            //{
        //            //    producto.UnidadMedida = Convert.ToInt32(reader["UnidadMedida"]);
        //            //}
        //            if (reader["UnidadMedida"] != DBNull.Value)
        //            {
        //                producto.UnidadMedida = Convert.ToInt32(reader["UnidadMedida"]);
        //                producto.UnidadMedidaAsUnidadMedida = UnidadMedidaDataAccess.ConvertToUnidadMedidaEntity(reader, "UnidadMedida");
        //            }

        //            if (reader["Descuento"] != DBNull.Value)
        //            {
        //                producto.Descuento = (decimal)reader["Descuento"];
        //            }

        //            if (reader["CobraIva"] != DBNull.Value)
        //            {
        //                producto.CobraIva = Convert.ToBoolean(reader["CobraIva"]);
        //            }
        //            //if (reader["IdCategoria"] != DBNull.Value)
        //            //{
        //            //    producto.IdCategoria = Convert.ToInt32(reader["IdCategoria"]);
        //            //}
        //            //if (reader["IdClasificacion"] != DBNull.Value)
        //            //{
        //            //    producto.IdClasificacion = Convert.ToInt32(reader["IdClasificacion"]);
        //            //}
        //            //producto.IpIngreso = Convert.ToString(reader["IpIngreso"]);
        //            //producto.UsuarioIngreso = Convert.ToString(reader["UsuarioIngreso"]);
        //            //producto.FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]);
        //            //if (reader["IpModificacion"] != DBNull.Value)
        //            //{
        //            //    producto.IpModificacion = Convert.ToString(reader["IpModificacion"]).ToUpper();
        //            //}
        //            //if (reader["UsuarioModificacion"] != DBNull.Value)
        //            //{
        //            //    producto.UsuarioModificacion = Convert.ToString(reader["UsuarioModificacion"]).ToUpper();
        //            //}
        //            //if (reader["FechaModificacion"] != DBNull.Value)
        //            //{
        //            //    producto.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
        //            //}
        //            //producto.IdEstado = Convert.ToInt16(reader["IdEstado"]);

        //            #endregion

        //            producto.SetLoadedState();
        //            productoCollection.Add(producto);

        //        }

                
        //        return productoCollection;
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

        //public static DataSet CrearCarrito(int IdEmpresa, object IdProducto, string idSalida, int IdLocal, SqlConnection connection, SqlTransaction transaction)
        //{
        //    SqlCommand mCommand = new SqlCommand();
        //    try
        //    {
        //        SqlDataAdapter adapter = new SqlDataAdapter();
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Stock_RecrearCarrito";

        //        #region << Add the params >>

        //        mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
        //        mCommand.Parameters.AddWithValue("@IdProducto", Convert.ToString(IdProducto));
        //        mCommand.Parameters.AddWithValue("@IdSalida", idSalida);

        //        if (IdLocal > 0)
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", IdLocal);
        //        }
        //        else
        //        {
        //            mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);
        //        }

        //        #endregion

        //        if (connection.State != ConnectionState.Open) connection.Open();


        //        adapter.SelectCommand = mCommand;

        //        DataSet result = new DataSet();
        //        adapter.Fill(result);
        //        return result;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        mCommand.Dispose();
        //    }
        //}

        //public static StockEntityCollection SaldoInventario(int IdEmpresa, object IdProducto, SqlConnection connection, SqlTransaction transaction)
        //{
        //    ProductoEntity producto = new ProductoEntity();

        //    SqlCommand mCommand = new SqlCommand();
        //    SqlDataReader reader = null;
        //    try
        //    {
        //        mCommand.Connection = connection;
        //        mCommand.CommandType = CommandType.StoredProcedure;
        //        mCommand.Transaction = transaction;
        //        mCommand.CommandText = "Custom_Stock_SaldoInventario";

        //        #region << Add the params >>

        //        mCommand.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
        //        mCommand.Parameters.AddWithValue("@IdProducto", Convert.ToString(IdProducto));
        //        mCommand.Parameters.AddWithValue("@IdLocal", DBNull.Value);


        //        #endregion
             
        //        if (connection.State != ConnectionState.Open) connection.Open();

        //        reader = mCommand.ExecuteReader();

        //        if (!reader.HasRows) return null;

        //        StockEntityCollection stockEntityCollection = new StockEntityCollection();
        //        StockEntity stockEntity;

        //        while (reader.Read())
        //        { 
        //            #region << Load the BusinessEntity Object >>
        //            stockEntity = new StockEntity();
        //            stockEntity.Id = Convert.ToString(reader["Id"]);
        //            stockEntity.IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]);
        //            stockEntity.IdProducto = Convert.ToInt32(reader["IdProducto"]);

        //            stockEntity.IdProductoAsProducto = new ProductoEntity();
        //            stockEntity.IdProductoAsProducto.CodigoInterno= Convert.ToString(reader["Id"]);
        //            stockEntity.IdProductoAsProducto.Id= Convert.ToInt32(reader["IdProducto"]);
        //            stockEntity.IdProductoAsProducto.Producto = Convert.ToString(reader["Producto"]);
        //            stockEntity.IdProductoAsProducto.CodigoInterno = Convert.ToString(reader["CodigoInterno"]);
        //            stockEntity.IdProductoAsProducto.CodigoProveedor = Convert.ToString(reader["CodigoProveedor"]);
        //            stockEntity.IdProductoAsProducto.CodigoBarra = Convert.ToString(reader["CodigoBarra"]);
        //            stockEntity.IdProductoAsProducto.Descripcion= Convert.ToString(reader["Descripcion"]);
        //            stockEntity.IdProductoAsProducto.PathArchivo= Convert.ToString(reader["PathArchivo"]);

        //            stockEntity.IdLocal = Convert.ToInt32(reader["IdLocal"]);
        //            stockEntity.IdLocalAsLocal = new LocalBodegaEntity();
        //            stockEntity.IdLocalAsLocal.Id = stockEntity.IdLocal;
        //            stockEntity.IdLocalAsLocal.Local= Convert.ToString(reader["Local"]);
        //            stockEntity.IdLocalAsLocal.Direccion= Convert.ToString(reader["Direccion"]);

        //            stockEntity.Fecha = Convert.ToDateTime(reader["Fecha"]);
        //            stockEntity.StockInventario = (decimal)reader["Stock"];
        //            stockEntity.CostoPromedioPonderado = (decimal)reader["Costo"];

        //            if (reader["ProductoPadre"] != DBNull.Value)
        //            {
        //                stockEntity.ProductoPadre = Convert.ToInt32(reader["ProductoPadre"]);
        //            }
        //            if (reader["UnidadMedida"] != DBNull.Value)
        //            {
        //                stockEntity.UnidadMedidaAsUnidadMedida = UnidadMedidaDataAccess.ConvertToUnidadMedidaEntity(reader, "UnidadMedida");
        //            }

        //            stockEntity.SetLoadedState();

        //            stockEntityCollection.Add(stockEntity);

        //            #endregion
        //        }

        //        return stockEntityCollection;
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

