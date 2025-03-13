using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TendaGo.Common
{
    public class ReceiptDto
    {
        public string Id { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.Today;

        public string Notes { get; set; }
        
        public int IdCustomer { get; set; }

        public int IdCompany { get; set; }

        public short IdStatus { get; set; }

        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public string IpModifier { get; set; }
        public string IpCreator { get; set; }
        public decimal Valor { get; set; }

        public string IdReversa { get; set; }

        public List<ReceiptDetailDto> Details { get; set; } = new List<ReceiptDetailDto>();

        public List<ReceiptPaymentDto> Payments { get; set; } = new List<ReceiptPaymentDto>();
    }


    public class ReceiptDetailDto
    {
        public int Id { get; set; }
        public string IdCobroDebito { get; set; }
        public int IdCuentaPorCobrar { get; set; }
        public int? IdMedioPago { get; set; }
        public decimal Value { get; set; }
        public short IdStatus { get; set; }
        public string PayMethod { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public string IpModifier { get; set; }
        public string IpCreator { get; set; }
    }

    public class ReceiptPaymentDto
    {
        public int Id { get; set; }
        public string IdCobroDebito { get; set; }
        public int IdMedioPago { get; set; }
        public string Descripcion { get; set; }
        public decimal Value { get; set; }
        public short IdStatus { get; set; }

        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public string IpModifier { get; set; }
        public string IpCreator { get; set; }

    }

    public class ReceiptSummaryDto
    {
        public string IdCobroDebito { get; set; }
        
        public int IdEmpresa { get; set; }
        
        public DateTime Fecha { get; set; }
        
        public string Detalle { get; set; }

        public string IdVendedor { get; set; }
        
        public string CodigoSRI { get; set; }
        
        public string MedioPago { get; set; }
        
        public int IdMedioPago { get; set; }

        public string IdSalida { get; set; }
        
        public int IdLocal { get; set; }

        public int IdCliente { get; set; }

        public string Identificacion { get; set; }
        
        public string Cliente { get; set; }
        
        public decimal Valor { get; set; }
        
    }



}