using ER.BA;
using ER.BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;

namespace TendaGo
{
    public static class CommonFunctions
    {
        /// <summary>
        /// Url de la pagina web
        /// </summary>
        public static string UrlWeb => ConfigurationManager.AppSettings["web:url"];

        /// <summary>
        /// Url base de la WebApi
        /// </summary>
        public static string UrlWebApiBase => ConfigurationManager.AppSettings["api:url"];

        /// <summary>
        /// Url Servicio de Facturacion Electronica Ecuafac
        /// </summary>
        public static string UrlEcuafact => ConfigurationManager.AppSettings["api:ecuafact"];

        /// <summary>
        /// Url Servicio de Facturacion Electronica FacturaGo
        /// </summary>
        public static string UrlFacturago => ConfigurationManager.AppSettings["api:facturago"];

        public static HttpError GetCustomHttpError(string devMessage, string userMessage, HttpStatusCode errorCode)
        {
            string httpErrorCode = ((int)errorCode).ToString();
            var error = new HttpError
            {
                { "TechnicalMessage", devMessage },
                { "UserMessage", userMessage },
                { "ErrorCode", httpErrorCode }
            };
            return error;
        }

        public static void Log(string source, params object[] messages)
        {

            try
            {
                var sb = new StringBuilder();

                foreach (var item in messages)
                {
                    if (item is Exception)
                    {
                        sb.AppendLine((item as Exception).ToString());
                    }
                    else if (item is string)
                    {
                        sb.AppendLine((item as string).ToString());
                    }
                    else if (item is ValueType)
                    {
                        sb.AppendLine(item.ToString());
                    }
                }


                var path = $"{HostingEnvironment.MapPath("/logs")}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Path.Combine(path, $"log_{source}_{DateTime.Now.ToFileTime()}.log");

                File.AppendAllText(filename, sb.ToString());
            }
            catch { }
        }

        public static HttpResponseMessage BuildHttpErrorResponse(this HttpRequestMessage Request, string devMessage, HttpStatusCode statusCode, string userMessage = null)
        {
            return Request.CreateErrorResponse(statusCode, GetCustomHttpError(devMessage, userMessage ?? devMessage, statusCode));
        }

        public static HttpResponseException BuildHttpErrorException(this HttpRequestMessage Request, HttpStatusCode statusCode, string devMessage, string userMessage = null)
        {
            var errorResponse = BuildHttpErrorResponse(Request, devMessage, statusCode, userMessage);
            return new HttpResponseException(errorResponse);
        }

        public static HttpResponseException BuildExceptionResponse(this HttpRequestMessage Request, Exception ex)
        {
            var errorResponse = Request.BuildHttpErrorResponse(HttpStatusCode.InternalServerError, ex.GetAllMessages(), ex.GetMessage());
            return new HttpResponseException(errorResponse);
        }

        public static string JoinString<T>(this IEnumerable<T> list, string separator = default)
        {
            var text = new StringBuilder();
            foreach (var item in list)
            {
                if (!string.IsNullOrEmpty(separator) && text.Length > 0)
                {
                    text.Append(separator);
                }

                text.Append(item);
            }

            return $"{text}";
        }

        /// <summary>
        /// Obtiene el objeto Usuario mediante el username del usuario autenticado
        /// </summary>
        /// <param name="username">username del inicio de sesion</param>
        /// <returns>Retorna el objeto Usuario</returns>
        public static UsuarioEntity GetAuthUser(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new Exception("El username no es válido");
            var usuarioEntity = UsuarioBussinesAction.LoadByPK(username);
            return usuarioEntity;
        }

        #region Validacion Cedula o ruc
        public static bool ValidarDigitos(string numero)
        {
            int numeroProvincias = 24;
            int n1 = int.Parse(numero.Substring(0, 1)) * 10 + int.Parse(numero.Substring(1, 1)); //numero de provincvia 1 - 24

            if (n1 == 0 || n1 > numeroProvincias)                                         //si no coincide con un # de provincia retorna false
                return false;
            else if (numero.Length == 13)
            {
                if ((numero.Substring(10, 3) == "000") || (numero.Substring(9, 4) == "0000")) //el RUC para Juridicos no puenden ser 000
                    return false;                                                           //el RUC para Instituciones Públicas no puede ser 0000
            }
            else if ((int.Parse(numero.Substring(2, 1)) == 7) || (int.Parse(numero.Substring(2, 1)) == 8)) //el tercer dígito de la indentificación no puede ser 7 ni 8
                return false;

            return true;
        }

        public static bool ValidarNumeroIdentificacion(string numero)
        {
            try
            {
                List<int> id = new List<int>();

                for (int i = 0; i < 10; i++)
                    id.Add(int.Parse(numero.Substring(i, 1)));  //Llenando la lista de números en id

                #region "Validación del # de Identificación"
                if (ValidarDigitos(numero))      //Validación previa del número
                {
                    if ((numero.Length == 10))  //Cédula
                    {
                        return ValidarCedula(id);  //Validando el # número de cédula
                    }
                    else if (numero.Length == 13) //RUC
                    {
                        if (int.Parse(numero.Substring(2, 1)) < 6)  //Validando el # de Ruc para Entidad Natural
                            return ValidarCedula(id);
                        //else
                        //    return ValidarRuc(id);  //Validando el # de Ruc para Entidad Jurídica y Públicas
                    }
                }
                else
                    return false;
                #endregion

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #region "Validación del Ruc para Entidades Jurídicas y Públicas"
        private static bool ValidarRuc(List<int> ident)
        {
            int suma = 0;
            int residuo;
            int digitoVerificador = 0;      //si el residuo del módulo es 0 el dígito verificador es 0

            #region "Coeficientes  4 3 2 7 6 5 4 3 2"
            List<int> coeficiente = new List<int>();
            coeficiente.Add(4);
            coeficiente.Add(3);
            coeficiente.Add(2);
            coeficiente.Add(7);
            coeficiente.Add(6);
            coeficiente.Add(5);
            coeficiente.Add(4);
            coeficiente.Add(3);
            coeficiente.Add(2);
            #endregion

            int modulo = 11; //Módulo 11 para RUC Jurídico y Público

            #region "Proceso del Algoritmo"
            if (ident[2] == 9)      //Entidad Jurídica
            {
                for (int i = 0; i < 9; i++)                   //se multiplica solo los 9 primeros dígitos
                    ident[i] = ident[i] * coeficiente[i];  //multiplicación de cada digito por su respectivo coeficiente
            }
            else if (ident[2] == 6)             //Entidad Pública    
            {
                for (int i = 0; i < 8; i++)        //se multiplica solo los 9 primeros dígitos
                {
                    ident[i] = ident[i] * coeficiente[i + 1];  //multiplicación de cada digito por su respectivo coeficiente
                    if (i == 9)                 //para este caso  solo se toman encuenta los 8 primeros 
                        ident[i] = 0;
                }
            }

            for (int i = 0; i < 9; i++)             //suma de los valores que resultaron de la multiplicación
                suma = suma + ident[i];

            residuo = suma % modulo;       //se calcula el módulo en este caso 11

            if (residuo != 0)        //si el residuo del módulo no es 0 se calcula el digito verificador
                digitoVerificador = modulo - residuo;
            #endregion

            #region "Verificación"
            if (digitoVerificador == ident[9])  //si el dígito verificador es igual al décimo dígito del 
                return true;                    //número de identificación, el # es correcto (true)
            else                                //caso contrario retornamos false
                return false;
            #endregion
        }
        #endregion

        #region "Validación de la Cédula y del RUC para Entidad Natural"
        private static bool ValidarCedula(List<int> ident)
        {
            int suma = 0;
            int residuo;
            int digitoVerificador = 0; //si el residuo del módulo es 0 el dígito verificador es 0

            #region "Coeficientes  2 1 2 1 2 1 2 1 2"
            List<int> coeficiente = new List<int>();
            coeficiente.Add(2);
            coeficiente.Add(1);
            coeficiente.Add(2);
            coeficiente.Add(1);
            coeficiente.Add(2);
            coeficiente.Add(1);
            coeficiente.Add(2);
            coeficiente.Add(1);
            coeficiente.Add(2);
            #endregion

            int modulo = 10; //Módulo 10 para Cédula y RUC Natural

            #region "Proceso del Algoritmo"
            for (int i = 0; i < 9; i++)        //se multiplica solo los 9 primeros dígitos
            {
                ident[i] = ident[i] * coeficiente[i];

                if (ident[i] >= 10)         //Si el producto es >= 10 deben sumarse sus dígitos 
                    ident[i] = ident[i] - 9; //14 = 1 + 4 = 5 (14-9 = 5) 
            }

            for (int i = 0; i < 9; i++)      //suma de los valores que resultaron de la multiplicación
                suma = suma + ident[i];  //y descomposición

            residuo = suma % modulo; //se cálcula el módulo en este caso 10

            if (residuo != 0)        //si el residuo del módulo no es 0 se calcula el digito verificador
                digitoVerificador = modulo - residuo;
            #endregion

            #region "Verificación"
            if (digitoVerificador == ident[9]) //si el dígito verificador es igual al décimo dígito del
                return true;                   //número de identificación, el # es correcto (true)
            else                               //caso contrario retornamos false
                return false;
            #endregion
        }
        #endregion

        #endregion

        #region DataTables to or From Objects
        public static IList<T> ConvertirToDto<T>(this System.Data.DataSet obj)
        {
            var dto = ConvertTo<T>(obj.Tables[0]);

            //var conf = new MapperConfiguration(config => config.CreateMap<DetalleReclamoEntity, DetalleReclamoDto>());
            //var mapper = conf.CreateMapper();
            //var dto = mapper.Map<DetalleReclamoEntity, DetalleReclamoDto>(obj);

            return dto;
        }
        public static DataTable ConvertTo<T>(this IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(this IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                var type = obj.GetType();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = type.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (prop != null)
                    {
                        try
                        {
                            if (row[column.ColumnName] != DBNull.Value)
                            {
                                object value = row[column.ColumnName];
                                if (prop.PropertyType != column.DataType &&
                                    prop.PropertyType == typeof(string))
                                {
                                    value = value.ToString();
                                }
                                prop.SetValue(obj, value, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            // You can log something here
                            ex.Data.Add("TechnicalMessage", "No se encontro columna " + column.ColumnName + "En el objeto");

                            Console.WriteLine(ex);
                            //throw ex;
                            //throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.InternalServerError, "No se encontro columna " + column.ColumnName + "En el objeto", "Acesso no válido"));
                        }
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        public static string GetContentAsString(this HttpContent Content)
        {
            try
            {
                var stream = Content.ReadAsStreamAsync().Result;
                stream.Position = 0;

                return new StreamReader(stream).ReadToEnd();
            }
            catch (Exception) { }

            return string.Empty;
        }
        #endregion
    }
}