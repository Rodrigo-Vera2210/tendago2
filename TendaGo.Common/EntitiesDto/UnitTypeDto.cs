using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class UnitTypeDto
    {
        public int Id { get; set; }
        public short IdEmpresa { get; set; }
        public int IdProducto { get; set; }

        [Display(Name = "T.Unidad")]
        public string TipoUnidad { get; set; }
        [Display(Name = "Cant.")]
        public decimal Cantidad { get; set; }

        [Display(Name = "Cant. Min.")]
        public decimal CantidadMinima { get; set; }

        [Display(Name = "% Margen")]
        public decimal Margen { get; set; }
        public int UnidadMedidad { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Estado")]
        public short IdEstado { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Costo { get; set; }

        public bool? Plantilla { get; set; }

        [Display(Name = "IVA")]
        public bool? IncluyeIva { get; set; }

        public string Producto { get; set; }
        public bool? CobraIva { get; set; }
        [Display(Name = "Unid. Medida")]
        public string UnidadMedida { get; set; }
    }
     
    public enum UnitTypeStatusEnum : short
    {
        All = -1,
        Inactive = 0,
        Active = 1

    }
}