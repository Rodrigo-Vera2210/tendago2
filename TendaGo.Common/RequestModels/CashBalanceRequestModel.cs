using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendaGo.Common 
{
    public class CashBalanceDto
    {
        public string Id { get; set; }

        public DateTime FechaApertura { get; set; } 
        public DateTime FechaCierre { get; set; }

        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public string IdCajero { get; set; }
        public string Observaciones { get; set; }

        public decimal SaldoInicial { get; set; }
        public decimal TotalIngresos { get; set; }

        public decimal TotalEgresos { get; set; }
        public decimal SaldoFinal { get; set; }

    }


    public class CashBalanceRequestModel : CashBalanceDto
    {
        public string UsuarioIngreso { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IpIngreso { get; set; }
        public List<CashBalanceDetailRequestModel> Detalles { get; set; } = new List<CashBalanceDetailRequestModel>();
    }


    public class CashBalanceDetailRequestModel
    {
        public string IdCobroDebito { get; set; }
        public decimal Valor { get; set; }
    }

}
