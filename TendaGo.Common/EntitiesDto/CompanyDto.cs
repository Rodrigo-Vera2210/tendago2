using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
	public class CompanyDto
	{
		public int Id { get; set; }
		public string NombreEmpresa { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string Logo { get; set; }
		public string RaizArchivo { get; set; }

		public bool FacturaPOS { get; set; }
		public bool FlujoInventario { get; set; }
		public bool Importacion { get; set; }
		public bool IncluyeIVA { get; set; }
		public decimal Iva { get; set; }

		public short IdEstado { get; set; }
	}
}