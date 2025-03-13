using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class ProductViewModel
    {
        public int IdEmpresa { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public int IdProducto { get; set; }
        public string TipoTransaccion { get; set; }
    }
}