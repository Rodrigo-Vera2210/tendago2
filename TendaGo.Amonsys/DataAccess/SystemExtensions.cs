using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace System
{
    public static class DataExtensions
    {
        public static string GetExecSql(this DbCommand command)
        {
            var sb = new StringBuilder();
            sb.Append($"EXECUTE {command.CommandText} ");
            
            foreach (DbParameter p in command.Parameters)
            {
                sb.Append($"{p.ParameterName} = {p.Value ?? "NULL"}");

                if (command.Parameters.Count != command.Parameters.IndexOf(p) + 1)
                {
                    sb.Append(", ");
                }
            }

            return $"{sb}";
        }
         
    }
}
