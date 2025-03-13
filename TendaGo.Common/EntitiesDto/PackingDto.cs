using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class PackingDto  
    {
        public string Id { get; set; }
        public short IdEmpresa { get; set; }
        public string IdSalida { get; set; }
        public int Cantidad { get; set; }
        public int IdTipoPaquete { get; set; }
        public PackageTypeDto TipoPaquete { get; set; }
         
    }
     



}
