using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class FacturaViewModels
    {
        public int IdEmpresa { get; set; }
        [Required]
        public string FechaDesde { get; set; }
        [Required]
        public string FechaHasta { get; set; }
        [Required]
        public string RUC { get; set; }
        public string IdProveedor { get; set; }
    }
}