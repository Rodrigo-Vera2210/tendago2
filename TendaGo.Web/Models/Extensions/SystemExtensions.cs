using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using TendaGo.Common;
using TendaGo.Web;
using TendaGo.Web.ApplicationServices;
using TendaGo.Web.Infraestructura;
using TendaGo.Web.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using TendaGo;
using System.Net.Http;

namespace System
{
    public static class SystemExtensions
    { 
        public static int ToInt32(this string text)
        {
            int result;

            if (int.TryParse(text ?? "0", out result))
            {
                return result;
            }

            return 0;
        }

        public static bool Has(this HttpRequestBase Request, string query)
        {
            return Request.Url?.Query?.ToLower()?.Contains(query?.ToLower()) ?? false;
        }

        public static string GetString(this ICell cell)
        {
            if (cell != null)
            {
                var value = string.Empty;
                switch (cell.CellType)
                {
                    case CellType.Formula:
                    case CellType.Numeric:
                        value = Convert.ToString(cell.GetNumber());
                        break;
                    case CellType.String:
                        value = cell.StringCellValue;
                        break;
                    case CellType.Boolean:
                        value = Convert.ToString(cell.BooleanCellValue);
                        break; 
                    case CellType.Blank:
                    case CellType.Error:
                    case CellType.Unknown:
                    default:
                        break;
                }

                return value;
            }

            return default;
        }

        public static double GetNumber(this ICell cell)
        {
            if (cell != null)
            {
                

                var value = 0D;
                switch (cell.CellType)
                {
                    case CellType.Formula:
                        {
                            IFormulaEvaluator eval;
                            var workbook = cell.Sheet.Workbook;
                            if (workbook is XSSFWorkbook)
                                eval = new XSSFFormulaEvaluator(workbook);
                            else
                                eval = new HSSFFormulaEvaluator(workbook);

                            value = GetNumber(eval.EvaluateInCell(cell));
                            break;
                        }
                    case CellType.Numeric:
                        {
                            value = cell.NumericCellValue;
                            break;
                        }
                    case CellType.String:
                        {
                            var text = cell.StringCellValue;
                            double.TryParse(text, out value);
                            break;
                        }
                    case CellType.Boolean:
                        {
                            value = Convert.ToDouble(cell.BooleanCellValue);
                            break;
                        }
                    case CellType.Blank:
                    case CellType.Error:
                    case CellType.Unknown:
                    default:
                        break;
                }

                return value;
            }
            
            return default;
        }

        public static DateTime? GetDate(this ICell cell)
        {
            DateTime? value = default;
            if (cell != null)
            {
                switch (cell.CellType)
                {
                    case CellType.Formula:
                        {
                            IFormulaEvaluator eval;
                            var workbook = cell.Sheet.Workbook;
                            if (workbook is XSSFWorkbook)
                                eval = new XSSFFormulaEvaluator(workbook);
                            else
                                eval = new HSSFFormulaEvaluator(workbook);

                            value = GetDate(eval.EvaluateInCell(cell));
                            break;
                        }
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            value = cell.DateCellValue;
                        }
                        else
                        {
                            value = DateTime.FromOADate(cell.NumericCellValue);
                        }

                        break;
                    case CellType.String:
                        {
                            var text = cell.StringCellValue;
                            if (DateTime.TryParse(text, out DateTime date))
                            {
                                value = date;
                            }
                            break;
                        }
                    case CellType.Boolean:
                    case CellType.Blank:
                    case CellType.Error:
                    case CellType.Unknown:
                    default:
                        break;
                }

            }

            return value;
        }

    }



    namespace Web
    {


        public static class WebExtensions
        { 
            public static IPrincipal User
            {
                get
                {
                    return HttpContext.Current.User;
                }
            }

            public static string Token { get { return User?.Identity?.GetTokenUsuario(); } }

            public static List<WarehouseDto> GetLocales(this HttpSessionStateBase Session)
            {
                
                var locales = Session[$"Locales_{Token}"] as List<WarehouseDto>;

                if (locales == null)
                {
                    string inicioSesion = User.Identity.Name;
                    Session[$"Locales_{Token}"] = locales = InventarioAppService.ObtenerLocales(inicioSesion);
                }

                return locales ?? new List<WarehouseDto>();
            }

            public static List<ModuleDto> GetModules(this HttpSessionStateBase Session)
            {

                var modules = Session[$"Modules_{Token}"] as List<ModuleDto>;

                if (modules == null)
                {
                    Session[$"Modules_{Token}"] = modules = ModulosAppServices.ObtenerModulos();
                }

                return modules ?? new List<ModuleDto>();
            }

            public static List<DisplayDto> GetPantallas(this HttpSessionStateBase Session)
            {

                var pantallas = Session[$"Pantallas_{Token}"] as List<DisplayDto>;

                if (pantallas == null)
                {
                    Session[$"Pantallas_{Token}"] = pantallas = PantallasAppService.ObtenerPantallasPerfil();
                }

                return pantallas ?? new List<DisplayDto>();
            }

            public static EntityDto GetConsumidorFinal(this HttpSessionStateBase Session)
            {
                var entity = Session[$"ConsumidorFinal_{Token}"] as EntityDto;

                if (entity == null)
                {
                    Session[$"ConsumidorFinal_{Token}"] = entity = ClientesAppService.BuscarClientes("999999").FirstOrDefault();
                }

                return entity;
            }

            public static WarehouseDto GetLocal(this HttpSessionStateBase Session, int idLocal)
            {
                return Session
                    .GetLocales()?
                    .FirstOrDefault(x => x.Id == idLocal);
            }

            public static int? GetIdLocal(this HttpSessionStateBase Session)
            {
                return Session.GetLocal()?.Id;
            }

            public static WarehouseDto GetLocal(this HttpSessionStateBase Session)
            {
                var idLocal = Session[$"LocalSeleccionado_{Token}"] as int? ?? 0;
                return Session.GetLocal(idLocal);
            }
             
            public static void SetLocal(this HttpSessionStateBase Session,int idLocal, string local )
            {
                Session[$"LocalSeleccionado_{Token}"] = idLocal;
            }


            public static byte[] GetImageFrom(this HttpServerUtilityBase Server, string productImagePath)
            {
                var pathArchivo = Server.MapPath("~/Images/no_imagen.png");
                if (!string.IsNullOrEmpty(productImagePath))
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var response = client.GetByteArrayAsync(productImagePath);

                            if (response != null)
                            {
                                return response.Result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"GET IMAGE FROM: {productImagePath}", ex);
                    }
                }

                return File.ReadAllBytes(pathArchivo);
            }

            public static string ImageToBase64String(this HttpServerUtilityBase Server, string productImagePath)
            {
                return Tools.ConvertirByteArrayAImagen(Server.GetImageFrom(productImagePath));
            }
        }

        namespace Mvc
        {
        
            
            public static class MyMvcExtensions
            {
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase System.Web.Mvc.SelectList mediante
                //     los elementos especificados para la lista.
                //
                // Parámetros:
                //   items:
                //     Elementos.
                public static SelectList ToSelectList(this IEnumerable items) { return new SelectList(items); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase System.Web.Mvc.SelectList mediante
                //     los elementos especificados para la lista y un valor seleccionado.
                //
                // Parámetros:
                //   items:
                //     Elementos.
                //
                //   selectedValue:
                //     El valor seleccionado.
                public static SelectList ToSelectList(this IEnumerable items, object selectedValue) { return new SelectList(items, selectedValue); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase SelectList mediante los elementos
                //     especificados para la lista, el valor seleccionado y los valores deshabilitados.
                //
                // Parámetros:
                //   items:
                //     Los elementos usados para crear cada System.Web.Mvc.SelectListItem de la lista.
                //
                //   selectedValue:
                //     El valor seleccionado.Se usa para hacer coincidir la propiedad Selected del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   disabledValues:
                //     Los valores deshabilitados.Se usa para hacer coincidir la propiedad Disabled
                //     del System.Web.Mvc.SelectListItem correspondiente.
                public static SelectList ToSelectList(this IEnumerable items, object selectedValue, IEnumerable disabledValues) { return new SelectList(items,selectedValue, disabledValues); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase System.Web.Mvc.SelectList mediante
                //     los elementos especificados para la lista, el campo de valor de datos y el campo
                //     de texto de datos.
                //
                // Parámetros:
                //   items:
                //     Elementos.
                //
                //   dataValueField:
                //     Campo de valor de datos.
                //
                //   dataTextField:
                //     Campo de texto de datos.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField) { return new SelectList(items, dataValueField, dataTextField); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase System.Web.Mvc.SelectList mediante
                //     los elementos especificados para la lista, el campo de valor de datos, el campo
                //     de texto de datos y un valor seleccionado.
                //
                // Parámetros:
                //   items:
                //     Elementos.
                //
                //   dataValueField:
                //     Campo de valor de datos.
                //
                //   dataTextField:
                //     Campo de texto de datos.
                //
                //   selectedValue:
                //     El valor seleccionado.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, object selectedValue) { return new SelectList(items, dataValueField, dataTextField, selectedValue); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase SelectList mediante los elementos
                //     especificados para la lista, el campo de valor de datos, el campo de texto de
                //     datos, el campo de grupo de datos y el valor seleccionado.
                //
                // Parámetros:
                //   items:
                //     Los elementos usados para crear cada System.Web.Mvc.SelectListItem de la lista.
                //
                //   dataValueField:
                //     Campo de valor de datos.Se usa para hacer coincidir la propiedad Value del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataTextField:
                //     Campo de texto de datos.Se usa para hacer coincidir la propiedad Text del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataGroupField:
                //     Campo de grupo de datos.Se usa para hacer coincidir la propiedad Group del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   selectedValue:
                //     El valor seleccionado.Se usa para hacer coincidir la propiedad Selected del System.Web.Mvc.SelectListItem
                //     correspondiente.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue) { return new SelectList(items, dataValueField, dataTextField, dataGroupField, selectedValue); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase SelectList mediante los elementos
                //     especificados para la lista, el campo de valor de datos, el campo de texto de
                //     datos, el valor seleccionado y los valores deshabilitados.
                //
                // Parámetros:
                //   items:
                //     Los elementos usados para crear cada System.Web.Mvc.SelectListItem de la lista.
                //
                //   dataValueField:
                //     Campo de valor de datos.Se usa para hacer coincidir la propiedad Value del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataTextField:
                //     Campo de texto de datos.Se usa para hacer coincidir la propiedad Text del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   selectedValue:
                //     El valor seleccionado.Se usa para hacer coincidir la propiedad Selected del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   disabledValues:
                //     Los valores deshabilitados.Se usa para hacer coincidir la propiedad Disabled
                //     del System.Web.Mvc.SelectListItem correspondiente.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, object selectedValue, IEnumerable disabledValues) { return new SelectList(items, dataValueField, dataTextField, selectedValue, disabledValues); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase SelectList mediante los elementos
                //     especificados para la lista, el campo de valor de datos, el campo de texto de
                //     datos, el campo de grupo de datos, el valor seleccionado y los valores deshabilitados.
                //
                // Parámetros:
                //   items:
                //     Los elementos usados para crear cada System.Web.Mvc.SelectListItem de la lista.
                //
                //   dataValueField:
                //     Campo de valor de datos.Se usa para hacer coincidir la propiedad Value del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataTextField:
                //     Campo de texto de datos.Se usa para hacer coincidir la propiedad Text del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataGroupField:
                //     Campo de grupo de datos.Se usa para hacer coincidir la propiedad Group del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   selectedValue:
                //     El valor seleccionado.Se usa para hacer coincidir la propiedad Selected del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   disabledValues:
                //     Los valores deshabilitados.Se usa para hacer coincidir la propiedad Disabled
                //     del System.Web.Mvc.SelectListItem correspondiente.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue, IEnumerable disabledValues) { return new SelectList(items, dataValueField, dataTextField, dataGroupField, selectedValue, disabledValues); }
                //
                // Resumen:
                //     Inicializa una nueva instancia de la clase SelectList mediante los elementos
                //     especificados para la lista, el campo de valor de datos, el campo de texto de
                //     datos, el campo de grupo de datos,el valor seleccionado, los valores deshabilitados
                //     y los grupos deshabilitados.
                //
                // Parámetros:
                //   items:
                //     Los elementos usados para crear cada System.Web.Mvc.SelectListItem de la lista.
                //
                //   dataValueField:
                //     Campo de valor de datos.Se usa para hacer coincidir la propiedad Value del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataTextField:
                //     Campo de texto de datos.Se usa para hacer coincidir la propiedad Text del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   dataGroupField:
                //     Campo de grupo de datos.Se usa para hacer coincidir la propiedad Group del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   selectedValue:
                //     El valor seleccionado.Se usa para hacer coincidir la propiedad Selected del System.Web.Mvc.SelectListItem
                //     correspondiente.
                //
                //   disabledValues:
                //     Los valores deshabilitados.Se usa para hacer coincidir la propiedad Disabled
                //     del System.Web.Mvc.SelectListItem correspondiente.
                //
                //   disabledGroups:
                //     Los grupos deshabilitados.Se usa para hacer coincidir la propiedad Disabled del
                //     System.Web.Mvc.SelectListGroup correspondiente.
                public static SelectList ToSelectList(this IEnumerable items, string dataValueField, string dataTextField, string dataGroupField, object selectedValue, IEnumerable disabledValues, IEnumerable disabledGroups) { return new SelectList(items, dataValueField, dataTextField, dataGroupField, selectedValue, disabledValues, disabledGroups); }


            }
        }
    }
}