using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common
{
    public class StatisticsDto
    {
        public decimal ValorVentasDia { get; set; }
        public decimal ValorVentasMes { get; set; }
        public decimal CantidadNPDia { get; set; }
        public decimal CantidadCTDia { get; set; }
        public decimal ValorCotizacionesMes { get; set; }        

    }

    public class StatisticsMonthDto
    {
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int IdLocal { get; set; }
        public string Local { get; set; }

    }

}
