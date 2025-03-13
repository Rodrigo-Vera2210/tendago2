using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class SearchProductModel
    {
        public ProductStatus StateForSearch { get; set; }
        public string SearchTerm { get; set; }
    }

    public enum ProductStatus
    {
        All = -1,
        OnlyInactive = 0,
        OnlyActive = 1
    }
    public class SearchOutputReport
    {
        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int IdCliente{ get; set; }
        public string TipoTransaccion { get; set; }
        public int IdProducto { get; set; }
        public int IdDivision { get; set; }
        public int IdLinea { get; set; }
        public int IdCategoria { get; set; }
        public string Ruc { get; set; }
        public string EstadoActual { get; set; }

    }
}