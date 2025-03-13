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
    public partial class SerieSalidaBussinesAction
    {
        //#region Implementation

        //public static SerieSalidaEntity Save(SerieSalidaEntity serieSalida)
        //{
        //    return Save(serieSalida, null, null);
        //}

        //public static SerieSalidaEntity Save(SerieSalidaEntity serieSalida, SqlConnection connection, SqlTransaction transaction)
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
        //        switch (serieSalida.CurrentState)
        //        {
        //            case EntityStatesEnum.Deleted:
        //                SerieSalidaDataAccess.Delete(serieSalida, connection, transaction);
        //                break;
        //            case EntityStatesEnum.Updated:
        //                SerieSalidaDataAccess.Update(serieSalida, connection, transaction);
        //                break;
        //            case EntityStatesEnum.New:
        //                serieSalida = SerieSalidaDataAccess.Insert(serieSalida, connection, transaction);
        //                break;
        //            default:
        //                break;
        //        }



        //        //                } 

        //        //End of Transaction
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            serieSalida.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return serieSalida;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (serieSalida != null) serieSalida.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}





        //public static SerieSalidaEntity LoadByPK(int Id)
        //{
        //    return LoadByPK(Id, null, null, 1);
        //}
        //public static SerieSalidaEntity LoadByPK(int Id, int deepLoadLevel)
        //{
        //    return LoadByPK(Id, null, null, deepLoadLevel);
        //}

        //public static SerieSalidaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        //{
        //    return LoadByPK(Id, connection, transaction, 1);
        //}

        //public static SerieSalidaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    try
        //    {


        //        SerieSalidaEntity serieSalida = SerieSalidaDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
        //        if (serieSalida != null)
        //        {
        //            if (deepLoadLevel > 1)
        //            {

        //            }

        //            serieSalida.SetLoadedState();
        //        }

        //        return serieSalida;
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
