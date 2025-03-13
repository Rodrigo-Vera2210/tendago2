using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ER.DA
{
    public class _DataAccessSchema
    {

        //protected static void ConfigureSchema(SqlDataReader reader, string fkColumnName)
        //{
        //    if (!schemas.ContainsKey(fkColumnName))
        //    {
        //        FillSchema(reader, fkColumnName);
        //    }
        //    else if(schemas[fkColumnName].Length != reader.FieldCount)
        //    {
        //        schemas.Remove(fkColumnName);
        //        FillSchema(reader, fkColumnName);
        //    }
        //}

        //private static void FillSchema(SqlDataReader reader, string fkColumnName)
        //{
        //    var table = reader.GetSchemaTable();

        //    var rows = new List<string>();
        //    foreach (DataRow row in table.Rows)
        //    {
        //        rows.Add(Convert.ToString(row[0]));
        //    }

        //    schemas.Add(fkColumnName, rows.ToArray());
        //}

        //protected static bool ColumnExists(string fkColumnName, string columnName)
        //{
        //    if (schemas.ContainsKey(fkColumnName))
        //    {
        //        return schemas[fkColumnName].Any(m => m.ToLower() == columnName.ToLower());
        //    }

        //    return false;
        //}

        //protected static Dictionary<string, string[]> schemas = new Dictionary<string, string[]>();

    }
}
