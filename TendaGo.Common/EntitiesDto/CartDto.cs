using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common.EntitiesDto
{
    public class CartDto
    {
        public int IdLocal { get; set; }
        public string PhotoUrl { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string CodigoInterno { get; set; }
        public string ProductoPadre { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public string TipoUnidad { get; set; }
        public int IdTipoUnidad { get; set; }
        public decimal? CantidadMinima { get; set; }
        public decimal CantidadTotal { get; set; }
        public decimal? StockInventario { get; set; }
    }
}
