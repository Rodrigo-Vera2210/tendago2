using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TendaGo.Common
{
    /// <summary>
    /// Impuesto
    /// </summary>
    public class TaxModel
    { 
        /// <summary>
        /// CodigoSRI del impuesto
        /// </summary>
        [MaxLength(1)]
        public string Code { get; set; }

        /// <summary>
        /// Codigo del procentaje del impuesto
        /// </summary>
        [MaxLength(4)]
        public string PercentageCode { get; set; }

        /// <summary>
        /// Tarifa del impuesto
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Base Imponible
        /// </summary>
        public decimal TaxableBase { get; set; }

        /// <summary>
        /// Valor del impuesto
        /// </summary>
        public decimal TaxValue { get; set; }
    }

}
