using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class ReceivableDto
    {
        public int Id { get; set; }
        public string IdSalida { get; set; }
        
        public int Numero { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorPagado { get; set; }
        public decimal Saldo { get; set; }
        public string Ip { get; set; }
        public string Usuario { get; set; }

        public short IdEstado { get; set; }
        public  int IdMedioPago { get; set; }
        public string NumeroFactura { get; set; }

        public List<ReceivablePayMethodDto> MetodosPago { get; set; }
    }

    public class ReceivablePayMethodDto {
        public int Id { get; set; }
        public string IdCobroDebito { get; set; }
        public int IdMedioPago { get; set; }
        public string MetodoPago { get; set; }
        public decimal Valor { get; set; }
        public string Descripcion { get; set; }
        public string IdCierreCaja { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public decimal ValorOriginal { get; set; }
        public bool Estado { get; set; }
    }
}