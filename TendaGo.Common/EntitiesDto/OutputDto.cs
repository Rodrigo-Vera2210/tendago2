using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{

    public class OutputCreateRequestModel
    {
        /// <summary>
        /// Codigo del local desde donde se realiza la transaccion
        /// </summary>
        public int IdLocal { get; set; }

        /// <summary>
        /// Usuario del vendedor
        /// </summary>
        [Required(ErrorMessage = "Debe enviar datos del usuario")]
        public string IdVendedor { get; set; }

        public string Periodo { get; set; }

        public DateTime Fecha { get; set; }

        /// <summary>
        /// Tipo de transaccion de salida de inventario NP:Nota de pedido, GR:Guia de remision, TR: Transferencia
        /// </summary>
        [Required(ErrorMessage = "Debe enviar el tipo de transaccion")]
        public string TipoTransaccion { get; set; }
        /// <summary>
        /// Codigo del cliente
        /// </summary>
        [Required(ErrorMessage = "Debe enviar codigo del cliente")]
        public int IdCliente { get; set; }

        public string NombreCliente { get; set; }

        
        /// <summary>
        /// Si es una nota de pedido debe especificar si se factura
        /// </summary>
        public bool Facturar { get; set; }
        /// <summary>
        /// Ruc de la empresa que se usara para la facturacion
        /// </summary>
        public string Ruc { get; set; }
        public decimal? Subtotal0 { get; set; }
        public decimal? SubtotalIva { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? ValorDescuento { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public decimal Saldo { get; set; }
        /// <summary>
        /// Fecha propuesta para la entrega de la mercaderia
        /// </summary>
        public DateTime FechaHoraEntregaPropuesta { get; set; }
        /// <summary>
        /// Fecha de entrega real
        /// </summary>
        public DateTime? FechaHoraEntregaReal { get; set; }
        /// <summary>
        /// Estado de la Orden: EN PROCESO, APROBADA, REVISADA, EMPAQUETADA, ENTREGADA
        /// </summary>
        public string EstadoActual { get; set; }
        public string Observaciones { get; set; }
        public int IdFormaPago { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public short IdEstado { get; set; }
        public string TransaccionPadre { get; set; }
        public bool EntregaInmediata { get; set; }

        public string TipoTransaccionPadre { get; set; }
        public int Plazo { get; set; }
        public int Cuotas { get; set; }
        public DocumentDto Factura { get; set; }

        public List<OutputDetailDto> DetalleNotaPedido { get; set; }
        public List<ReceivableDto> CuentasPorCobrar { get; set; }
        public List<AditionalInfoDto> InformacionAdicional { get; set; }
        public decimal? ValorAdicional { get; set; }
        public string Entrega { get; set; }
        public bool BorrarCobros { get; set; }

    }

    public class OutputRequestModel: OutputCreateRequestModel
    {
        public List<PackingDto> DetalleEmpaquetado { get; set; }
       
    }

    public class OutputDto : OutputRequestModel
    {
        public int IdEmpresa { get; set; }

        public string Id { get; set; }
        public ClientDto Cliente { get; set; }

        public InputDto Entrada { get; set; }

        public bool Rise { get; set; }
        public decimal ValorNc { get; set; }
    }

    public class OutputDeleteDto
    {
        public string IdSalida { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Observaciones { get; set; }

    }

    public class OutputLoadModel
    {
        public string TransaccionPadre { get; set; }

        public string TipoTransaccionPadre { get; set; }

        public List<OutputRequestModel> ListaPedidos { get; set; }

        public string IpIngreso { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime FechaIngreso { get; set; }

    }   
    
    public enum TransactionType
    {
        NotaPedido,
        Compra,
        SalidaBodega,
        EntradaBodega,
        Merma,
        Cotizacion
    }

    public enum TransactionStatus
    {
        Cotizacion = 0, EnProceso = 1, Aprobada, Revisada, Empaquetada, Entregada, Anulada, Facturada
    }
     
}