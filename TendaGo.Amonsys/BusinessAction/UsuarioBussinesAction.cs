
//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------
// Copyright 2019, Binasystem
// Autor Edison Romero 
//-----------------------------------------------------------------------



#region using

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Net;
using ER.BE; 
using ER.DA; 
using System.Transactions;
using System.Configuration;

#endregion using

namespace ER.BA
{
  
    public partial class UsuarioBussinesAction
    {
        //public static UsuarioEntity LoadByToken(string Token)
        //{
        //    return LoadByToken(Token, null, null);
        //}
        //public static UsuarioEntity LoadByToken(string Token, SqlConnection connection, SqlTransaction transaction)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    try
        //    {

        //        UsuarioEntity usuario = UsuarioDataAccess.LoadByToken(Token, connection, transaction);
        //        if (usuario != null)
        //        {
        //            usuario.SetLoadedState();
        //        }

        //        return usuario;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}


    }
}


