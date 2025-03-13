using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class InputDto
    {
        public string Id { get; set; }

        public int IdEmpresa { get; set; }

        public int IdLocal { get; set; }

        public string Periodo { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoTransaccion { get; set; }

        public int IdProveedor { get; set; }

        public EntityDto Proveedor { get; set; }
        public decimal? Subtotal0 { get; set; }

        public decimal? Subtotal12 { get; set; }

        public decimal Total { get; set; }

        public decimal Saldo { get; set; }

        public decimal? ValorAdicional { get; set; }

        public string NumeroFacturaPedido { get; set; }

        public string TransaccionPadre { get; set; }

        public string TipoTransaccionPadre { get; set; }

        public string EstadoActual { get; set; }

        public DateTime? FechaHoraEntrega { get; set; }

        public int IdFormaPago { get; set; }

        public short? IdMonedaOrigen { get; set; }
        public string IdMonedaOrigen_DisplayMember { get; set; }

        public string Ruc { get; set; }

        public decimal? Tasa { get; set; }

        public string IpIngreso { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string IpModificacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public short IdEstado { get; set; }

        public OutputDto Salida { get; set; }

        public List<InputDetailDto> DetalleEntradaFromIdEntrada { get; set; }
        public List<DebtDto> CuentaPorPagarFromIdEntrada { get; set; }
        //public List<ReceivableInputDto> CuentasPorPagar { get; set; }
        public int IdLocalOrigen { get; set; }
        public string Observaciones { get; set; }
    }
    public class InputDeleteDto
    {
        public string IdEntrada { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}