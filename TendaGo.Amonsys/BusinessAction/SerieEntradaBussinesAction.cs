using ER.BE;
using ER.DA;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER.BA
{
    public partial class SerieEntradaBussinesAction
    {
        //#region Implementation

        //public static SerieEntradaEntity Save(SerieEntradaEntity serieEntrada)
        //{
        //    return Save(serieEntrada, null, null);
        //}

        //public static SerieEntradaEntity Save(SerieEntradaEntity serieEntrada, SqlConnection connection, SqlTransaction transaction)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
        //        connection.Open();
        //        transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

        //    }

        //    try
        //    {

        //        //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
        //        //                {
        //        /*

        //        */
        //        switch (serieEntrada.CurrentState)
        //        {
        //            case EntityStatesEnum.Deleted:
        //                SerieEntradaDataAccess.Delete(serieEntrada, connection, transaction);
        //                break;
        //            case EntityStatesEnum.Updated:
        //                SerieEntradaDataAccess.Update(serieEntrada, connection, transaction);
        //                break;
        //            case EntityStatesEnum.New:
        //                serieEntrada = SerieEntradaDataAccess.Insert(serieEntrada, connection, transaction);
        //                break;
        //            default:
        //                break;
        //        }



        //        //                } 

        //        //End of Transaction
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            serieEntrada.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return serieEntrada;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (serieEntrada != null) serieEntrada.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}





        //public static SerieEntradaEntity LoadByPK(int Id)
        //{
        //    return LoadByPK(Id, null, null, 1);
        //}
        //public static SerieEntradaEntity LoadByPK(int Id, int deepLoadLevel)
        //{
        //    return LoadByPK(Id, null, null, deepLoadLevel);
        //}

        //public static SerieEntradaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        //{
        //    return LoadByPK(Id, connection, transaction, 1);
        //}

        //public static SerieEntradaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    try
        //    {


        //        SerieEntradaEntity serieEntrada = SerieEntradaDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
        //        if (serieEntrada != null)
        //        {
        //            if (deepLoadLevel > 1)
        //            {

        //            }

        //            serieEntrada.SetLoadedState();
        //        }

        //        return serieEntrada;
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


        //#endregion Implementation
    }
}
