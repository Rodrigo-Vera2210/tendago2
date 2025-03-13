using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    /// <summary>
    /// FACTURA ELECTRONICA
    /// </summary>
    public class InvoiceModel
    {
        /// <summary>
        /// Identificador Unico del Documento
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Registro Unico de Contribuyente
        /// </summary>
        public string RUC { get; set; }

        /// <summary>
        /// Código del Documento a Emitir (Tabla 3 Ficha Tecnica)
        /// </summary>
        public string DocumentTypeCode { get; set; }

        /// <summary>
        /// Numero del Documento
        /// </summary>
        [NotMapped]
        public string DocumentNumber
        {
            get
            {
                return $"{EstablishmentCode}-{IssuePointCode}-{Sequential}";
            }
            set { } // do nothing x) por si acaso
        }

        /// <summary>
        /// Fecha de Emision del Documento
        /// </summary>
        public DateTime IssuedOn { get; set; }

        /// <summary>
        /// Codigo Establecimiento
        /// </summary>
        public string EstablishmentCode { get; set; }

        /// <summary>
        /// Punto de Emisión del Comprobante
        /// </summary>
        public string IssuePointCode { get; set; }

        /// <summary>
        /// Secuencial
        /// </summary>
        public string Sequential { get; set; }


        /// <summary>
        /// Emails, Emails para enviar el comprobante.
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// Fecha de creacion del documento
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Razon Social
        /// </summary>
        public string BussinesName { get; set; }

        /// <summary>
        /// Nombre Comercial
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Direccion Principal
        /// </summary>
        public string MainAddress { get; set; }
        /// <summary>
        /// Estado Habilitado del Documento
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Tipo de Identificacion del Contribuyente
        /// </summary>
        public string ContributorIdentificationType { get; set; }
        /// <summary>
        /// Nombre del Contribuyente
        /// </summary>
        public string ContributorName { get; set; }
        /// <summary>
        /// Numero de Identificacion del Contribuyente
        /// </summary>
        public string ContributorIdentification { get; set; }
        /// <summary>
        /// Direccion del Contribuyente
        /// </summary>
        public string ContributorAddress { get; set; }
        /// <summary>
        /// Moneda
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Motivo o Explicacion del Documento
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Valor Total del Documento
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Clave de Acceso
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// Fecha de autorizacion
        /// </summary>
        public string AuthorizationDate { get; set; }
        /// <summary>
        /// Numero de Autorizacion
        /// </summary>
        public string AuthorizationNumber { get; set; }
        /// <summary>
        /// PDF
        /// </summary>
        public string PDF { get; set; }
        /// <summary>
        /// XML
        /// </summary>
        public string XML { get; set; } 
          
        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }
           
    }

    public class DocumentResult
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ruc")]
        public string Ruc { get; set; }

        [JsonProperty("nombreComercial")]
        public string NombreComercial { get; set; }

        [JsonProperty("direccionEstablecimiento")]
        public string DireccionEstablecimiento { get; set; }

        [JsonProperty("tipoComprobante")]
        public string TipoComprobante { get; set; }

        [JsonProperty("establecimiento")]
        public string Establecimiento { get; set; }

        [JsonProperty("puntoEmision")]
        public string PuntoEmision { get; set; }

        [JsonProperty("secuencial")]
        public long Secuencial { get; set; }

        [JsonProperty("fechaEmision")]
        public DateTime FechaEmision { get; set; }

        [JsonProperty("claveAcceso")]
        public string ClaveAcceso { get; set; }

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
        public double ValorTotal { get; set; }

        //[JsonProperty("tipoEmision")]
        //public int TipoEmision { get; set; }

        //[JsonProperty("ambiente")]
        //public int Ambiente { get; set; }

        //[JsonProperty("enProceso")]
        //public bool EnProceso { get; set; }

        //[JsonProperty("fechaProceso")]
        //public DateTime FechaProceso { get; set; }

        //[JsonProperty("fechaAutorizacion")]
        //public DateTime FechaAutorizacion { get; set; }

        //[JsonProperty("numeroAutorizacion")]
        //public string NumeroAutorizacion { get; set; }

        //[JsonProperty("resultado")]
        //public bool Resultado { get; set; }

        //[JsonProperty("notificado")]
        //public bool Notificado { get; set; }

        //[JsonProperty("fechaNotificacion")]
        //public DateTime FechaNotificacion { get; set; }

        //[JsonProperty("ride")]
        //public string Ride { get; set; }

        //[JsonProperty("xml")]
        //public string Xml { get; set; }

        //[JsonProperty("estado")]
        //public string Estado { get; set; }
    }
}

public class OperationResult<T>
{
    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }
}