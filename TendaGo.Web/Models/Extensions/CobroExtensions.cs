using TendaGo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TendaGo.Web.Models
{
    public static class CobroExtensions
    {
        public static HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }
        public static IPrincipal User
        {
            get
            {
                return HttpContext.User;
            }
        }

        public static string UserName { get
            {
                return HttpContext.User.Identity.Name;
            }
        }
        public static string UserIPAddress
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public static CobroDebitoViewModel ToViewModel(this ReceiptDto dto)
        {
            var cliente = ApplicationServices.ClientesAppService.ObtenerClientePorId(dto.IdCustomer);

            return new CobroDebitoViewModel
            {
                Id = dto.Id,
                IdCliente = dto.IdCustomer,
                Identificacion = cliente.Identificacion,
                Correo = cliente.Correo,
                Direccion = cliente.Correo,
                RazonSocial = cliente.RazonSocial,
                Telefono = cliente.Telefono,
                Observaciones = dto.Notes,
                IdEmpresa = dto.IdCompany,
                FechaEmision = dto.IssuedOn,
                IdReversa = dto.IdReversa,
                Detalles = dto.Details.Select(m =>
                {
                    var cxc = ApplicationServices.CuentasPorCobrarAppService.CuentaPorCobrarPorId(m.IdCuentaPorCobrar);

                    return new DetalleCobroViewModel
                    {
                        Id = m.Id,
                        IdCuentaPorCobrar = m.IdCuentaPorCobrar,
                        Cuota = m.Value,
                        Numero = cxc.Numero,
                        IdSalida = cxc.IdSalida,                             
                        Valor = m.Value,
                        MetodoPago=m.PayMethod,
                    };
                }
                ).ToList(),
                Pagos = dto.Payments.Select(m =>
                {
                    return new DetalleMedioCobroViewModel
                    {
                        Id = m.Id,
                        IdMedioPago = m.IdMedioPago,
                        Valor = m.Value
                    };
                }).ToList(),
                Total = dto.Details.Sum(m => m.Value)>0 ? dto.Details.Sum(m => m.Value) : dto.Valor ,
                TotalPagos = dto.Payments.Sum(m => m.Value)
            };
        }

        public static ReceiptDto ToReceiptDto(this CobroDebitoViewModel model)
        {
            return new ReceiptDto
            {
                IdCompany = model.IdEmpresa,
                IdCustomer = model.IdCliente,
                IssuedOn = model.FechaEmision.Date,
                Notes = model.Observaciones,
                Details = model.Detalles.Select( item => item.ToReceiptDetailDto()).ToList(),
                Payments = model.Pagos.Select(pago => pago.ToReceiptPaymentDto()).ToList(),
                IdStatus = 1
            };
        }

        public static ReceiptPaymentDto ToReceiptPaymentDto(this DetalleMedioCobroViewModel pago)
        {
            return new ReceiptPaymentDto
            {
                Id = pago.Id,
                IdMedioPago = pago.IdMedioPago,
                Descripcion=pago.Descripcion,
                Value = pago.Valor,
                CreatedBy = User.Identity.Name,
                CreatedOn = DateTime.Now,
                IpCreator = UserIPAddress,
                IdStatus = 1
            };
        }

        public static ReceiptDetailDto ToReceiptDetailDto(this DetalleCobroViewModel item)
        {
            return new ReceiptDetailDto
            {
                Id = item.Id,
                IdCuentaPorCobrar = item.IdCuentaPorCobrar,
                Value = item.Valor,
                CreatedBy = UserName,
                CreatedOn = DateTime.Now,
                IpCreator = UserIPAddress,
                IdStatus = 1
            };
        }
    }
}