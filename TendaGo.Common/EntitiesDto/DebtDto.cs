using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class DebtDto
    {
        public int Id { get; set; }

        public string IdEntrada { get; set; }

        public int Numero { get; set; }

        public DateTime Periodo { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorPagado { get; set; }

        public decimal? Saldo { get; set; }

        public string IpIngreso { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string IpModificacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public short IdEstado { get; set; }
        public List<ReceivablePayMethodDto> MetodosPago { get; set; }

    }
}