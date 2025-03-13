using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class LiteStockDto
    {
        /*public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int IdBodega { get; set; }
        public string DescripcionBodega { get; set; }
        public decimal StockDisponible { get; set; }
        */
        public string Id { get; set; }
        public string CodigoInterno { get; set; } 
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public string PathArchivo { get; set; }
        public int IdLocal { get; set; }
        public string Local { get; set; }
        public string Direccion { get; set; }
        public decimal? StockInventario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? CostoPromedioPonderado { get; set; }
        public decimal? Costo { get; set; }
        public int? ProductoPadre { get; set; }
        public int IdTipoUnidad { get; set; }
        public string NombreUnidadMedida { get; set; }
    }

    public class StockDto : LiteStockDto
    {

        public int IdEmpresa { get; set; }

        public string Tipo { get; set; }

        public string IdSalidaEntrada { get; set; }

        public string IdDetalle { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal? ValorTotal { get; set; }

        public decimal? CantidadTipoUnidad { get; set; }

        public decimal Cantidad { get; set; }

        public decimal? SaldoInventario { get; set; }

        public short? IdEstado { get; set; }

    }

    public class StockBodegaDto
    {
        public int IdEmpresa { get; set; }
        public int IdProducto { get; set; }
        public int IdLocal { get; set; }
    }
}