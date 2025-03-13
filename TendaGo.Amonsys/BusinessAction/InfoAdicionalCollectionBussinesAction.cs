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
    public partial class InfoAdicionalCollectionBussinesAction
    {

        //#region Implementation

        //public static InfoAdicionalEntityCollection Save(InfoAdicionalEntityCollection infoCollection)
        //{
        //    return Save(infoCollection, null, null);
        //}

        //public static InfoAdicionalEntityCollection Save(InfoAdicionalEntityCollection infoCollection, SqlConnection connection, SqlTransaction transaction)
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

        //        foreach (InfoAdicionalEntity info in infoCollection)
        //        {
        //            InfoAdicionalBussinesAction.Save(info, connection, transaction);
        //        }

        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            infoCollection.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return infoCollection;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (infoCollection != null) infoCollection.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}

        //#endregion

        //#region << Find by All >>

        //public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter)
        //{
        //    return FindByAll(findParameter, null, null, 1);
        //}

        //public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter, int deepLoadLevel)
        //{
        //    return FindByAll(findParameter, null, null, deepLoadLevel);
        //}

        //public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction)
        //{
        //    return FindByAll(findParameter, connection, transaction, 1);
        //}

        //public static InfoAdicionalEntityCollection FindByAll(InfoAdicionalFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    InfoAdicionalEntityCollection infoAddCollection = null;

        //    try
        //    {

        //        //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
        //        //                {
        //        infoAddCollection = InfoAdicionalDataAccessCollection.FindByAll(findParameter, connection, transaction, deepLoadLevel);
        //        infoAddCollection.SetState(EntityStatesEnum.Loaded);
        //        //                    transactionScope.Complete();
        //        //
        //        //                }  //End of Transaction

        //        return infoAddCollection;
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

        //#endregion
    }
}
