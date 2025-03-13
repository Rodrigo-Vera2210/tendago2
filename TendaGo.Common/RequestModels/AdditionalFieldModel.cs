using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TendaGo.Common
{
    /// <summary>
    /// Campo Adicional del documento
    /// </summary>
    public class AdditionalFieldModel
    {
        /// <summary>
        /// Nombre o etiqueta del campo adicional
        /// </summary>
        [MaxLength(300)]
        public string Name { get; set; }

        /// <summary>
        /// Valor del Campo adicional
        /// </summary>
        [MaxLength(300)]
        public string Value { get; set; }

        /// <summary>
        /// Número de Línea. Orden en el que aparecerá (Iniciando desde Posición 1)
        /// </summary>
        public int LineNumber { get; set; }
    }


}
