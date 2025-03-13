using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Web;

namespace TendaGo.Common
{
    /// <summary>
    /// DOCUMENTO: FACTURA
    /// </summary>
    public class InvoiceRequestModel
    {
        /// <summary>
        /// Punto de establecimiento
        /// </summary>
        public string EstablishmentCode { get; set; }
        /// <summary>
        /// Punto de Emision
        /// </summary>
        public string IssuePointCode { get; set; }
        /// <summary>
        /// Fecha de Emision del Documento (REQUERIDO)
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string IssuedOn { get; set; }

        /// <summary>
        /// ID de la lista de Contribuyentes (Opcional, si no existe se lo crea automaticamente con la informacion enviada)
        /// </summary>
        public long ContributorId { get; set; }

        /// <summary>
        /// Tipo de identificacion del Contribuyente
        /// </summary>
        public string IdentificationType { get; set; } = "05";

        /// <summary>
        /// Identificacion del Contribuyente
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        /// Nombre del Contribuyente
        /// </summary>
        public string ContributorName { get; set; }

        /// <summary>
        /// Telefono del Contribuyente
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Direccion del Contribuyente
        /// </summary>
        public string Address { get; set; } = "";

        /// <summary>
        /// Correos a los que se enviara el documento electronico
        /// </summary>
        public string EmailAddresses { get; set; } = "";

        /// <summary>
        /// Moneda (DOLAR de forma predeterminada)
        /// </summary>
        public string Currency { get; set; } = "DOLAR";

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Reason { get; set; } = "";

        /// <summary>
        /// Importe Total del Documento. Formato decimal 0.00
        /// </summary>
        public decimal Total { get; set; } = 0M;

        /// <summary>
        /// Guia de Remision (OPCIONAL)
        /// </summary>
        public string ReferralGuide { get; set; }

        /// <summary>
        /// Subtotal Iva. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal SubtotalVat { get; set; }

        /// <summary>
        /// Subtotal IVA 0. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal SubtotalVatZero { get; set; }

        /// <summary>
        /// Subtotal No Objeto. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal SubtotalNotSubject { get; set; }

        /// <summary>
        /// Subtotal Exento. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal SubtotalExempt { get; set; }

        /// <summary>
        /// Subtotal. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Total Descuento. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal TotalDiscount { get; set; }

        /// <summary>
        /// ICE, Impuesto a los consumos especiales. Special Consum Tax. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal SpecialConsumTax { get; set; }

        /// <summary>
        /// Valor del IVA. Formato decimal 0.00
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal ValueAddedTax { get; set; }

        /// <summary>
        /// Propina. Formato decimal 0.00
        /// </summary>
        public decimal Tip { get; set; }

        /// <summary>
        /// Detalle del metodo de pago para la factura. NOTA: Si no es especificado se asume EFECTIVO.
        /// </summary>
        public List<PaymentModel> Payments { get; set; }

        /// <summary>
        /// Detalle de productos de la factura
        /// </summary>
        public List<InvoiceDetailModel> Details { get; set; }


        /// <summary>
        /// Campos adicionales
        /// </summary>
        public List<AdditionalFieldModel> AdditionalFields { get; set; }

        /// <summary>
        /// Emitir el documento
        /// </summary>
        public bool Status { get; set; }

    }

    /// <summary>
    /// Detalle de la Factura
    /// </summary>
    public class InvoiceDetailModel : DocumentDetailModel
    {

    }


    /// <summary>
    /// Factura de Exportacion
    /// </summary>
    public class ExportInvoiceRequestModel : InvoiceRequestModel
    {

        /// <summary>
        /// incoTermFactura
        /// </summary>
        [MaxLength(10)]
        public string InvoiceIncoTerm { get; set; }

        /// <summary>
        /// lugarIncoTerm
        /// </summary>
        [MaxLength(300)]
        public string PlaceIncoTerm { get; set; }

        /// <summary>
        /// PaisOrigen
        /// </summary>
        [MaxLength(3)]
        public string OriginCountry { get; set; }

        /// <summary>
        /// PuertoEmbarque
        /// </summary>
        [MaxLength(300)]
        public string PortBoarding { get; set; }

        /// <summary>
        /// PuertoDestino
        /// </summary>
        [MaxLength(300)]
        public string DestinationPort { get; set; }

        /// <summary>
        /// Pais Destino
        /// </summary>
        [MaxLength(3)]
        public string DestinationCountry { get; set; }

        /// <summary>
        /// PaisAdquisicion
        /// </summary>
        [MaxLength(3)]
        public string CountryAcquisition { get; set; }

        /// <summary>
        /// incoTermTotalSinImpuestos 
        /// </summary>
        [MaxLength(10)]
        public string TotalWithoutTaxesIncoTerm { get; set; }

    }

    /// <summary>
    /// DOCUMENTO: FACTURAGO
    /// </summary>
    public partial class ComprobanteRequestModel
    {
        [JsonProperty("ruc")]
        public string Ruc { get; set; }

        [JsonProperty("tipoComprobante")]
        public string TipoComprobante { get; set; }

        [JsonProperty("establecimiento")]
        public string Establecimiento { get; set; }

        [JsonProperty("puntoEmision")]
        public string PuntoEmision { get; set; }

        [JsonProperty("fechaEmision")]
        public string FechaEmision { get; set; }

        [JsonProperty("direccionEstablecimiento")]
        public string DireccionEstablecimiento { get; set; }

        [JsonProperty("tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }

        [JsonProperty("identificacion")]
        public string Identificacion { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }
      
        [JsonProperty("valorTotal")]
        public decimal ValorTotal { get; set; }

        [JsonProperty("factura")]
        public FacturaRequestModel Factura { get; set; }

        [JsonProperty("detalleComprobante")]
        public List<DetalleComprobanteRequestModel> DetalleComprobante { get; set; }

        [JsonProperty("campoAdicional")]
        public List<CampoAdicionalRequestModel> CampoAdicional { get; set; }

        [JsonProperty("detallePago")]
        public List<PagoRequestModel> DetallePago { get; set; }
    }

    public partial class NotaCreditoRequestModel: ComprobanteRequestModel
    {
        [JsonProperty("codDocModificado")]
        public string CodDocModificado { get; set; }

        [JsonProperty("numDocModificado")]
        public string NumDocModificado { get; set; }

        [JsonProperty("fechaEmisionDocSustento")]
        public string FechaEmisionDocSustento { get; set; }

        [JsonProperty("valorModificacion")]
        public decimal ValorModificacion { get; set; }

        [JsonProperty("motivo")]
        public string Motivo { get; set; }

        [JsonProperty("totalSinImpuestos")]
        public decimal TotalSinImpuestos { get; set; }       

        [JsonProperty("moneda")]
        public string Moneda { get; set; }     

        [JsonProperty("porcentajeIva")]
        public int PorcentajeIva { get; set; }

        [JsonProperty("valorRetIva")]
        public decimal ValorRetIva { get; set; }

        [JsonProperty("valorRetRenta")]
        public decimal ValorRetRenta { get; set; }
    }

        public partial class CampoAdicionalRequestModel
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }
    }

    public partial class DetalleComprobanteRequestModel
    {
        [JsonProperty("codigoPrincipal")]
        public string CodigoPrincipal { get; set; }

        [JsonProperty("codigoAuxiliar")]
        public string CodigoAuxiliar { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("unidadMedida")]
        public string UnidadMedida { get; set; }

        [JsonProperty("cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("precioUnitario")]
        public decimal PrecioUnitario { get; set; }

        [JsonProperty("precioSinSubsidio")]
        public decimal PrecioSinSubsidio { get; set; }

        [JsonProperty("descuento")]
        public decimal Descuento { get; set; }

        [JsonProperty("precioTotalSinImpuesto")]
        public decimal PrecioTotalSinImpuesto { get; set; }

        [JsonProperty("detalleImpuesto")]
        public List<DetalleImpuestoRequestModel> DetalleImpuesto { get; set; }
    }

    public partial class DetalleImpuestoRequestModel
    {
        [JsonProperty("idImpuesto")]
        public int IdImpuesto { get; set; }

        [JsonProperty("idTarifa")]
        public int IdTarifa { get; set; }

        [JsonProperty("baseImponible")]
        public decimal BaseImponible { get; set; }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }
    }

    public partial class PagoRequestModel
    {
        [JsonProperty("idFormaPago")]
        public int IdFormaPago { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("plazo")]
        public int Plazo { get; set; }

        [JsonProperty("idUnidadTiempo")]
        public int IdUnidadTiempo { get; set; }
    }

    public partial class FacturaRequestModel
    {
        [JsonProperty("totalSinImpuestos")]
        public decimal TotalSinImpuestos { get; set; }

        [JsonProperty("totalSubsidio")]
        public decimal TotalSubsidio { get; set; }

        [JsonProperty("totalDescuento")]
        public decimal TotalDescuento { get; set; }

        [JsonProperty("importeTotal")]
        public decimal ImporteTotal { get; set; }

        [JsonProperty("moneda")]
        public string Moneda { get; set; }

        [JsonProperty("placa")]
        public string Placa { get; set; }

        [JsonProperty("propina")]
        public decimal Propina { get; set; }

        [JsonProperty("porcentajeIva")]
        public int PorcentajeIva { get; set; }

        [JsonProperty("valorRetIva")]
        public decimal ValorRetIva { get; set; }

        [JsonProperty("valorRetRenta")]
        public decimal ValorRetRenta { get; set; }
    }

}