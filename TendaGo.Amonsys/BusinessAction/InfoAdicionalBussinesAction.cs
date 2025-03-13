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
    public partial class InfoAdicionalBussinesAction
    {

        //#region Implementation

        //public static InfoAdicionalEntity Save(InfoAdicionalEntity infoAdicional)
        //{
        //    return Save(infoAdicional, null, null);
        //}

        //public static InfoAdicionalEntity Save(InfoAdicionalEntity infoAdicional, SqlConnection connection, SqlTransaction transaction)
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
        //                            if( ciudad.IdProvinciaAsProvincia != null && ciudad.IdProvinciaAsProvincia.CanSave )
        //                            {
        //                                ciudad.IdProvincia = ProvinciaBussinesAction.Save(ciudad.IdProvinciaAsProvincia , connection,transaction).Id;
        //                            }


        //        */
        //        switch (infoAdicional.CurrentState)
        //        {
        //            case EntityStatesEnum.Deleted:
        //                //InfoAdicionalDataAccess.Delete(infoAdicional, connection, transaction);
        //                break;
        //            case EntityStatesEnum.Updated:
        //                //InfoAdicionalDataAccess.Update(infoAdicional, connection, transaction);
        //                break;
        //            case EntityStatesEnum.New:
        //                infoAdicional = InfoAdicionalDataAccess.Insert(infoAdicional, connection, transaction);
        //                break;
        //            default:
        //                break;
        //        }



        //        //                } 

        //        //End of Transaction
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Commit();
        //            infoAdicional.SetState(EntityStatesEnum.SavedSuccessfully);
        //        }

        //        return infoAdicional;
        //    }
        //    catch (Exception exc)
        //    {
        //        if (isBAParent && transaction != null)
        //        {
        //            transaction.Rollback();
        //            if (infoAdicional != null) infoAdicional.RollBackState();

        //        }
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
