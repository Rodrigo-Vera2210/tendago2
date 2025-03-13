using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TendaGo.Common
{

    /// <summary>
    /// Pago detalle
    /// </summary>
    public class PaymentModel
    {
        /// <summary>
        /// CodigoSRI de Forma de Pago del Catalogo PaymentMethods
        /// </summary>
        [Required]
        public string PaymentMethodCode { get; set; }

        /// <summary>
        /// CodigoSRI de Forma de Pago del Catalogo PaymentMethods
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Valor total
        /// </summary>
        [Required]
        public decimal Total { get; set; }

        /// <summary>
        /// Plazo
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Unidad de tiempo
        /// </summary>
        public string TimeUnit { get; set; }

    }
}
