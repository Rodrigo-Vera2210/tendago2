using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public static class MetodosDeExtension
    {
        public const string formatoFecha = "dd/MM/yyyy";
        public const string formatoValor = "#,##0.00";

        public static string ToCustomFormatString(this DateTime fecha)
        {
            return fecha.ToString(formatoFecha);
        }

        public static string ToCustomFormatString(this DateTime? fecha)
        {
            if (fecha == null)
            {
                return string.Empty;
            }
            else
            {
                return fecha.Value.ToString(formatoFecha);
            }
        }

        public static string ToCustomFormatString(this decimal valor)
        {
            return valor.ToString(formatoValor);
        }

        public static string ToCustomFormatString(this decimal? valor)
        {
            if (valor == null)
            {
                return string.Empty;
            }
            else
            {
                return valor.Value.ToString(formatoValor);
            }
        }

        public static string ToMultiLineStringHtml(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) { return valor; }
            return valor.Replace(Environment.NewLine, "<br />");
        }
    }   

}