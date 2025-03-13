using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.EntitiesDto
{
    public class ProductReportDto
    {
        public int IdEmpresa { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public int IdProducto { get; set; }

        public string TipoTransaccion { get; set; }
    }
}
