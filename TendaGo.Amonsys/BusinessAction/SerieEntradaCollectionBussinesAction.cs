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
    public partial class SerieEntradaCollectionBussinesAction
    {
        #region << Custom Stored Procedures >>


        #endregion

        #region Implementation

        public static SerieEntradaEntityCollection Save(SerieEntradaEntityCollection SerieEntradaCollection)
        {
            return Save(SerieEntradaCollection, null, null);
        }

        public static SerieEntradaEntityCollection Save(SerieEntradaEntityCollection SerieEntradaCollection, SqlConnection connection, SqlTransaction transaction)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);
                connection.Open();
                transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            }

            try
            {

                foreach (SerieEntradaEntity serieEntrada in SerieEntradaCollection)
                {
                    SerieEntradaBussinesAction.Save(serieEntrada, connection, transaction);
                }

                if (isBAParent && transaction != null)
                {
                    transaction.Commit();
                    SerieEntradaCollection.SetState(EntityStatesEnum.SavedSuccessfully);
                }

                return SerieEntradaCollection;
            }
            catch (Exception exc)
            {
                if (isBAParent && transaction != null)
                {
                    transaction.Rollback();
                    if (SerieEntradaCollection != null) SerieEntradaCollection.RollBackState();

                }
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #region << Find by All >>

        public static SerieEntradaEntityCollection FindByAll(SerieEntradaFindParameterEntity findParameter)
        {
            return FindByAll(findParameter, null, null, 1);
        }

        public static SerieEntradaEntityCollection FindByAll(SerieEntradaFindParameterEntity findParameter, int deepLoadLevel)
        {
            return FindByAll(findParameter, null, null, deepLoadLevel);
        }

        public static SerieEntradaEntityCollection FindByAll(SerieEntradaFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction)
        {
            return FindByAll(findParameter, connection, transaction, 1);
        }

        public static SerieEntradaEntityCollection FindByAll(SerieEntradaFindParameterEntity findParameter, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            SerieEntradaEntityCollection serieEntradaCollection = null;

            try
            {

                //                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                //                {
                serieEntradaCollection = SerieEntradaDataAccessCollection.FindByAll(findParameter, connection, transaction, deepLoadLevel);

                if (serieEntradaCollection != null && deepLoadLevel > 1)
                {
                    foreach (SerieEntradaEntity sector in serieEntradaCollection)
                    {

                    }

                }


                serieEntradaCollection.SetState(EntityStatesEnum.Loaded);
                //                    transactionScope.Complete();
                //
                //                }  //End of Transaction

                return serieEntradaCollection;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (isBAParent) connection.Close();
            }
        }

        #endregion


        #endregion Implementation
    }
}
