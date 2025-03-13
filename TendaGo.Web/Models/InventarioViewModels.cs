using TendaGo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{

    public class TransferenciaViewModel
    {
        public string Id { get; set; } 
        public string IdVendedor { get; set; }
        public string Observaciones { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime Fecha { get; set; }

        public string EstadoActual { get; set; }

        [Display(Name ="Local Origen")]
        public int IdLocalOrigen { get; set; }

        [Display(Name = "Local Destino")]
        public int IdLocalDestino { get; set; }

        public List<DetalleTransferenciaViewModel> Detalle { get; set; }
    }


    public class DetalleTransferenciaViewModel
    {

        public string Id { get; set; }

        public int IdProducto { get; set; }
        
        public string CodigoInterno { get; set; }

        public string DescripcionProducto { get; set; }
        
        [Display(Name = "Tipo de Unidad")]
        public int IdTipoUnidad { get; set; }
        public string TipoUnidad { get; set; }

        public decimal CantidadMinima { get; set; }


        [Display(Name = "Cantidad Solicitada")]
        public decimal Cantidad { get; set; }

        public int? IdLocal { get; set; }

        public decimal Stock { get; set; }

        public short Estado { get; set; }

        public List<UnitTypeDto> Unidades { get; set; }
        public string Unidad { get; set; }

        public List<LiteStockDto> StockLocales { get; set; }

        [Display(Name = "Estado")]
        public bool ItemValido { get; set; } = true;
        public string Observacion { get; set; }
    }



    public class MermaViewModel
    {
        public string Id { get; set; }
        public string IdEmpresa { get; set; }
        public string IdVendedor { get; set; }
        public string Observaciones { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required]
        public DateTime Fecha { get; set; }

        public DateTime FechaIngreso { get; set; }
        
        public string EstadoActual { get; set; }
    
        public int? IdLocal { get; set; }

        public string UsuarioIngreso { get; set; }
        public string IpIngreso { get; set; }
        public List<DetalleTransferenciaViewModel> Detalle { get; set; }
    }

}