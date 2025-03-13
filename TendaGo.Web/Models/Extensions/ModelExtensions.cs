using AutoMapper;
using TendaGo.Common;
using TendaGo.Web.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using TendaGo.Common.EntitiesDto;

namespace TendaGo.Web.Models 
{
    public static class ModelExtensions
    {
        public static EntityDto ToEntityDto(this EntidadViewModel model)
        {
            return Mapper.Map<EntityDto>(model);
        }

        public static TransferRequest ToRequest(this TransferenciaViewModel model)
        {
            return Mapper.Map<TransferRequest>(model);
        }

        public static TransferDto ToTransfer(this InputDto model)
        {
            return Mapper.Map<TransferDto>(model);
        }

        public static TransferDto ToTransfer(this OutputDto model)
        {
            return Mapper.Map<TransferDto>(model);
        }

        public static OutputDto ToOutputDto(this NotaPedidoModel model)
        {
            return Mapper.Map<OutputDto>(model);
        }

        public static DocumentDto ToDocumentDto(this DocumentViewModel model)
        {
            return Mapper.Map<DocumentDto>(model);
        }

        public static EntradaViewModel ToModel(this InputDto model)
        {
            return Mapper.Map<EntradaViewModel>(model);
        }

        public static NotaPedidoModel ToModel(this OutputDto model)
        {
            return Mapper.Map<NotaPedidoModel>(model);
        }

        public static List<FormaPagoSRIDto> ToFormaPagoSRIDto(this ResultList<List<FormaPagoSRIDto>> model)
        {
            return Mapper.Map<List<FormaPagoSRIDto>>(model.Result);
        }

        public static SalesOrderInvoiceDto ToInvoiceDto(this FacturaNotaPedidoViewModel model, DocumentViewModel factura = null)
        {
            var result = Mapper.Map<SalesOrderInvoiceDto>(model);
            result.Factura = factura.ToDocumentDto() ?? result.Factura;
            return result;
        }

        public static DocumentDto ToDocumentDto(this OutputDto dto)
        {
            var model = dto.ToModel();
            return model.ToDocumentDto();
        }

        public static DocumentDto ToDocumentDto(this NotaPedidoModel model)
        {
            var factura = model.ToFactura();
            return factura.ToDocumentDto();
        }

        public static DocumentViewModel ToFactura(this NotaPedidoModel model)
        {
            var plazo = 0;

            if (model.CuentasPorCobrar.Count > 0)
            {
                var minFecha = model.CuentasPorCobrar.Min(m => m.FechaVencimiento);
                var maxFecha = model.CuentasPorCobrar.Max(m => m.FechaVencimiento);

                plazo = Convert.ToInt32((maxFecha - minFecha).TotalDays);
            }
            
            var percent = (model.PorcentajeFactura > 0) ? decimal.Round(model.PorcentajeFactura / 100M, 2) : 0M;
            var detalles = model.DetalleNotaPedido.Select(m =>
            {
                decimal ivaDecimal = Convert.ToDecimal(m.PorcentajeTarifaImpuesto);
                var iva = 1 + (ivaDecimal / 100);// 1.porcentajeIva

                return new DocumentDetailViewModel
                {
                    IdProducto = m.IdProducto,
                    DescripcionProducto = m.DescripcionProducto,
                    NombreProducto = m.NombreProducto,
                    CodigoInterno = m.CodigoInterno,
                    Nombre = m.Nombre,
                    TipoIce = "0",
                    Descuento = 0M,
                    Cantidad = m.Cantidad,
                    UnidadMedida = m.UnidadMedida,
                    IdTarifaImpuesto = m.IdTarifaImpuesto,
                    PorcentajeTarifaImpuesto = m.PorcentajeTarifaImpuesto,
                    Precio = decimal.Round((m.Precio * percent) / ((!m.Rise && m.IncluyeIva) ? iva : 1M), 4),
                    IdTipoUnidad = m.IdTipoUnidad,
                    TipoIva = (!m.Rise && m.CobraIva ? "2" : "0")
                };

            }).ToList();

            // De forma predeterminada se genera como nota de venta
            var documentType = "01" /* FACTURA */; 
            if (!string.IsNullOrEmpty(model.Ruc))
            {
                var ruc = RucsAppService.ObtenerRuc(model.Ruc);
                if (ruc.Rise)
                {
                    documentType = "02" /* NOTA DE VENTA */;
                }
            }


            var document = new DocumentViewModel
            {
                Entidad = model.Cliente.ToEntityDto(),
                IdEntidad = model.IdCliente,
                IdSalida = model.Id,
                IdTipoDocumento = documentType, 
                ConsumidorFinal = model.ConsumidorFinal,
                Notas = model.Observaciones,
                Fecha = DateTime.Now,
                RUC = model.Ruc,
                FormaPago = "01", // EFECTIVO
                IdFormaPagoSri = model.IdFormaPagoSri, 
                Plazo = plazo,
                UnidadTiempo = "dias",
                Detalles = detalles,
                Total = detalles.Sum(m => m.Subtotal),
                TotalSinImpuesto = detalles.Sum(m => m.SubtotalSinImpuesto),
                Base0 = detalles.Where(m => m.TipoIva == "0").Sum(m => m.SubtotalSinImpuesto),
                BaseIva = detalles.Where(m => m.TipoIva == "2").Sum(m => m.SubtotalSinImpuesto),
                TotalDescuento = detalles.Sum(m => m.Descuento),
                TotalIva = detalles.Sum(m => m.Iva),
                TotalIce = detalles.Sum(m => m.Ice),

            };





            return document;
        }
        
        public static Usuario ToUsuario(this UserDto dto)
        {
            return Mapper.Map<UserDto, Usuario>(dto);
        }

        public static Usuario ToUsuario(this UserDto dto, Usuario user)
        {
            return user = Mapper.Map<UserDto, Usuario>(dto, user);
        }

        public static UserDto ToUserDto(this Usuario usuario)
        {
            return Mapper.Map<Usuario, UserDto>(usuario);
        }


    }
}