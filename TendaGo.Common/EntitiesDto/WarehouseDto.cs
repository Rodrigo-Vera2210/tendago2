using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class WarehouseDtoLite
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string Tipo { get; set; }
        public short IdEstado { get; set; }
        public string DefaultRUC { get; set; }

    }

    public class WarehouseDto : WarehouseDtoLite
    {
        //public int Id { get; set; }
        public int IdEmpresa { get; set; }
        //public string Tipo { get; set; }
        //public string Local { get; set; }
        public string Direccion { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        //public short IdEstado { get; set; }

    }
}