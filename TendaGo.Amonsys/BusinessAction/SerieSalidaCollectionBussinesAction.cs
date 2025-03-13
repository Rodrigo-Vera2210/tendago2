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
    public partial class SerieSalidaCollectionBussinesAction
    {
        //#region << Custom Stored Procedures >>


        //#endregion

        //#region Implementation

        //public static SerieSalidaEntityCollection Save(SerieSalidaEntityCollection SerieSalidaCollection)
        //{
        //    return Save(SerieSalidaCollection, null, null);
        //}

        //public static SerieSalidaEntityCollection Save(SerieSalidaEntityCollection SerieSalidaCollection, SqlConnection connection, SqlTransaction transaction)
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

        //        foreach (SerieSalidaEntity serieSalida in SerieSalidaCollection)
        //        {
        //            SerieSalidaBussinesAction.Save(serieSalida, connection, transaction);
        //        }

        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            SerieSalidaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return SerieSalidaCollection;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (SerieSalidaCollection != null) SerieSalidaCollection.RollBackState();

        //        }
        //        throw exc;
        //    }
        //    finally
        //    {
        //        if (isBAParent) connection.Close();
        //    }
        //}

        //#region << Find by All >>

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter)
        //{
        //    return FindByAll(findParameter, null, null, 1);
        //}

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter, int deepLoadLevel)
        //{
        //    return FindByAll(findParameter, null, null, deepLoadLevel);
        //}

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction)
        //{
        //    return FindByAll(findParameter, connection, transaction, 1);
        //}

        //public static SerieSalidaEntityCollection FindByAll(SerieSalidaFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        //{
        //    bool isBAParent = false;
        //    if (connection == null)
        //    {
        //        isBAParent = true;
        //        connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

        //    }

        //    SerieSalidaEntityCollection serieSalidaCollection = null;

        //    try
        //    {

        //        //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
        //        //                {
        //        serieSalidaCollection = SerieSalidaDataAccessCollection.FindByAll(findParameter, connection, transaction, deepLoadLevel);

        //        if (serieSalidaCollection != null && deepLoadLevel > 1)
        //        {
        //            foreach (SerieSalidaEntity sector in serieSalidaCollection)
        //            {

        //            }

        //        }


        //        serieSalidaCollection.SetState(EntityStatesEnum.Loaded);
        //        //                    transactionScope.Complete();
        //        //
        //        //                }  //End of Transaction

        //        return serieSalidaCollection;
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


        //#endregion Implementation
    }
}
