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
    public partial class EstadisticaBussinesAction
    {
        public static EstadisticaEntity LoadByPK(int Id)
        {
            return LoadByPK(Id, null, null, 1);
        }
        public static EstadisticaEntity LoadByPK(int Id, int deepLoadLevel)
        {
            return LoadByPK(Id, null, null, deepLoadLevel);
        }

        public static EstadisticaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadByPK(Id, connection, transaction, 1);
        }

        public static EstadisticaEntity LoadByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            try
            {


                EstadisticaEntity estadistic = EstadisticaDataAccess.LoadByPK(Id, connection, transaction, deepLoadLevel);
                if (estadistic != null)
                {
                    if (deepLoadLevel > 1)
                    {

                    }

                    estadistic.SetLoadedState();
                }

                return estadistic;
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

        public static VentasMensualEntityCollection LoadMesByPK(int Id)
        {
            return LoadMesByPK(Id, null, null, 1);
        }
        public static VentasMensualEntityCollection LoadMesByPK(int Id, int deepLoadLevel)
        {
            return LoadMesByPK(Id, null, null, deepLoadLevel);
        }
        public static VentasMensualEntityCollection LoadMesByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadMesByPK(Id, connection, transaction, 1);
        }

        public static VentasMensualEntityCollection LoadMesByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            VentasMensualEntityCollection estadistic = null;

            try
            {


                estadistic = EstadisticaDataAccess.LoadMesByPK(Id, connection, transaction, deepLoadLevel);
                if (estadistic != null)
                {
                    if (deepLoadLevel > 1)
                    {

                    }

                    //estadistic.SetLoadedState();
                }

                return estadistic;
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


        public static VentasMensualEntityCollection LoadCobroByPK(int Id, int deepLoadLevel)
        {
            return LoadCobroByPK(Id, null, null, deepLoadLevel);
        }
        public static VentasMensualEntityCollection LoadCobroByPK(int Id, SqlConnection connection, SqlTransaction transaction)
        {
            return LoadCobroByPK(Id, connection, transaction, 1);
        }
        public static VentasMensualEntityCollection LoadCobroByPK(int Id, SqlConnection connection, SqlTransaction transaction, int deepLoadLevel)
        {
            bool isBAParent = false;
            if (connection == null)
            {
                isBAParent = true;
                connection = new SqlConnection(ConfigurationManager.AppSettings["TendaGo"]);

            }

            VentasMensualEntityCollection estadistic = null;

            try
            {


                estadistic = EstadisticaDataAccess.LoadCobroByPK(Id, connection, transaction, deepLoadLevel);
                if (estadistic != null)
                {
                    if (deepLoadLevel > 1)
                    {

                    }

                    //estadistic.SetLoadedState();
                }

                return estadistic;
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
    }
}
