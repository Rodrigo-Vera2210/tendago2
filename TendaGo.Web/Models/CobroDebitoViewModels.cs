using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TendaGo.Web.Models
{
    public class CobroDebitoViewModel
    {
        public string Id { get; set; }
        public int IdEmpresa { get; set; }
        [Display(Name="Fecha de Emision"), Required]
        public DateTime FechaEmision { get; set; } = DateTime.Today;
        public string Observaciones { get; set; }

        [Display(Name = "Cliente"), Required]
        public int IdCliente { get; set; }

        public List<DetalleCobroViewModel> Detalles { get; set; } = new List<DetalleCobroViewModel>();

        public List<DetalleMedioCobroViewModel> Pagos { get; set; } = new List<DetalleMedioCobroViewModel>();

        // Datos del cliente --para uso interno.
        public string Identificacion { get; set; } = "";
        public string RazonSocial { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string Correo { get; set; } = "";
        public decimal TotalPagos { get; set; } = 0M;
        public decimal Total { get; set; } = 0M;
        public string IdReversa { get; set; }
    }


    public class DetalleCobroViewModel
    {
        public int Id { get; set; }
        public string IdSalida { get; set; } = "";

        public int? IdMedioPago { get; set; }
        public int Numero { get; set; }

        [Required]
        public int IdCuentaPorCobrar { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public decimal Cuota { get; set; }
        public string MetodoPago { get; set; } = "";
    }

    public class DetalleMedioCobroViewModel
    {
        public int Id { get; set; }
        [Required]
        public int IdMedioPago { get; set; }
        public string MedioPago { get; set; }
        [Required]
        public decimal Valor { get; set; }

        public string Descripcion { get; set; }
    }

    public class CierreCajaViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Fecha de Apertura"), Required]
        public DateTime FechaApertura { get; set; }
        public TimeSpan HoraApertura { get; set; }

        [Display(Name = "Fecha del Cierre"), Required]
        public DateTime? FechaCierre { get; set; }
        
        public int IdEmpresa { get; set; }
        public int IdLocal { get; set; }
        public string IdCajero { get; set; }
        public string Observaciones { get; set; }

        [Display(Name = "Saldo Inicial")]
        public decimal SaldoInicial { get; set; } // + Valor entregado al cajero al aperturar la caja
        
        [Display(Name = "Total Ingresos"), Required]
        public decimal TotalIngresos => Detalles?.Sum(x => x.Valor) ?? 0; // + Total de los valores Cobrados

        [Display(Name = "Total Egresos"), Required]
        public decimal TotalEgresos { get; set; } // - Total de los valores Pagados
        
        [Display(Name = "Saldo Final"), Required]
        public decimal SaldoFinal => (SaldoInicial + TotalIngresos) - TotalEgresos; // - Saldo final = (SaldoInicial + TotalCobros) - TotalPagos

        public List<DetalleRecibosViewModel> Detalles { get; set; } = new List<DetalleRecibosViewModel>();

        public List<DetalleMedioCobroViewModel> Pagos => Detalles?.GroupBy(x => x.IdMedioPago)
            .Select(x => new DetalleMedioCobroViewModel
            {
                IdMedioPago = x.Key,
                MedioPago = x.FirstOrDefault()?.MedioPago,
                Valor = x.Sum(m => m.Valor)
            })?.ToList();

    }


    public class DetalleRecibosViewModel
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

    public class ReciboSearchModel
    {
        public string term { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }     
        internal DateTime? getFechaFin()
        {
            DateTime date;
            // Al final se le agrega un dia para que traiga las ordenes hasta las 12:00am (en el siguiente dia)
            return DateTime.TryParse(fechaFin, CultureInfo.GetCultureInfo("es-EC"), DateTimeStyles.None, out date) ? ((DateTime?)(date.AddDays(1))) : default;
        }

        internal DateTime? getFechaInicio()
        {
            DateTime date;
            return DateTime.TryParse(fechaInicio, CultureInfo.GetCultureInfo("es-EC"), DateTimeStyles.None, out date) ? (DateTime?)date : null;
        }
    }

}