using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class TransferRequest
    {
        public int IdLocalDestino { get; set; }

        public int IdEmpresa { get; set; }
         
        public string IdVendedor { get; set; }

        public string Periodo { get; set; }

        public DateTime Fecha { get; set; }
        
        public string EstadoActual { get; set; }
        public string Observaciones { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        
        public List<TransferDetailDto> Detalle { get; set; }


    }


    public class TransferDto : TransferRequest
    {
        public string Id { get; set; }
        public string TipoTransaccion { get; set; }
        public string IdEntrada { get; set; }
        public string IdSalida { get; set; }
        public int IdLocalOrigen { get; set; }
        public OutputDto Salida { get; set; }
        public InputDto Entrada { get; set; }
    }

    public class AdjustInventoryRequest
    { 
        public int IdLocal { get; set; } 
        public DateTime Fecha { get; set; } 
        public string Observaciones { get; set; }
        public string IpIngreso { get; set; }
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; } 
        public List<AdjustInventoryDetailDto> Detalle { get; set; }  
    }

    public class AdjustInventoryDto : AdjustInventoryRequest
    {
        public string Id { get; set; }
        public int IdEmpresa { get; set; }

        public string Periodo { get; set; }
        public string EstadoActual { get; set; }
        public string TipoTransaccion { get; set; }
        public string IdEntrada { get; set; }
        public string IdSalida { get; set; }
        public OutputDto Salida { get; set; }
        public InputDto Entrada { get; set; }
    }

    public class MermaDto : AdjustInventoryRequest
    {
        public string Id { get; set; }
        public int IdEmpresa { get; set; }        
    }

    public class AdjustInventoryDetailDto : TransferDetailDto
    { 
    
    }
}