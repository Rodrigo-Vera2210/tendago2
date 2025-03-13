using System;

namespace TendaGo.Common 
{
	public class DocumentDetailDto
	{
		public string Id { get; set; }
		public string IdDocumento { get; set; }
		public int IdTipoUnidad { get; set; }
		public int IdProducto { get; set; }
		public decimal Precio { get; set; }
		public decimal Cantidad { get; set; }
		public decimal Descuento { get; set; }
		public decimal SubtotalSinImpuesto { get; set; }
		public string TipoIva { get; set; }
		public string TipoIce { get; set; }
		public decimal Iva { get; set; }
		public decimal Ice { get; set; }

	}

}