using ER.BE;
using System.Linq;

namespace TendaGo.Domain
{
    public static class ReceiptsExtensions
    {
        public static ReceiptDto ToReceiptDto(this CobroDebitoEntity recibo)
        {
            return new ReceiptDto
            {
                Id = recibo.Id,
                IdCompany = recibo.IdEmpresa,
                IdCustomer = recibo.IdCliente,
                Notes = recibo.Detalle,
                IssuedOn = recibo.Fecha,
                Valor = recibo.Valor,
                IdReversa = recibo.IdReversa,
                Details = recibo.DetalleCobroFromIdCobroDebito?.Select(m => m.ToReceiptDetailDto()).ToList(),
                Payments = recibo.DetalleMedioCobroFromIdCobroDebito?.Select(p => p.ToReceiptPaymentDto()).ToList()
            };
        }

        public static ReceiptDetailDto ToReceiptDetailDto(this DetalleCobroEntity model)
        {
            return new ReceiptDetailDto
            {
                IdCobroDebito = model.IdCobroDebito,
                IdCuentaPorCobrar = model.IdCuentaPorCobrar,
                Value = model.Valor,
                PayMethod = model.IdMedioPago_DisplayMember,
                IdStatus = model.IdEstado,
                CreatedBy = model.UsuarioIngreso,
                ModifiedBy = model.UsuarioModificacion,
                CreatedOn = model.FechaIngreso,
                ModifiedOn = model.FechaModificacion,
                IpModifier = model.IpModificacion
            };
        }

        public static ReceiptPaymentDto ToReceiptPaymentDto(this DetalleMedioCobroEntity model)
        {
            return new ReceiptPaymentDto
            {
                IdCobroDebito = model.IdCobroDebito,
                IdMedioPago = model.IdMedioPago,
                Value = model.Valor,

                IdStatus = model.IdEstado,
                CreatedBy = model.UsuarioIngreso,
                ModifiedBy = model.UsuarioModificacion,
                CreatedOn = model.FechaIngreso,
                ModifiedOn = model.FechaModificacion,
                IpModifier = model.IpModificacion
            };
        }


        public static CobroDebitoEntity ToCobroDebitoEntity(this ReceiptDto dto)
        {
            var entity = new CobroDebitoEntity
            {
                Id = dto.Id,
                IdEmpresa = dto.IdCompany,
                Detalle = dto.Notes ?? "",
                Fecha = dto.IssuedOn,
                IdCliente = dto.IdCustomer,
                IdEstado = dto.IdStatus,
                UsuarioIngreso = dto.CreatedBy,
                FechaIngreso = dto.CreatedOn,
                IpIngreso = dto.IpCreator,

                UsuarioModificacion = dto.ModifiedBy,
                FechaModificacion = dto.ModifiedOn,
                IpModificacion = dto.IpModifier,

                DetalleCobroFromIdCobroDebito = new DetalleCobroEntityCollection(),
                DetalleMedioCobroFromIdCobroDebito = new DetalleMedioCobroEntityCollection()
            };

            entity.DetalleCobroFromIdCobroDebito.AddRange(dto.Details.Select(m => m.ToDetalleCobroEntity()));
            entity.DetalleMedioCobroFromIdCobroDebito.AddRange(dto.Payments.Select(m => m.ToDetalleMedioCobroEntity()));

            return entity;
        }

        public static DetalleMedioCobroEntity ToDetalleMedioCobroEntity(this ReceiptPaymentDto dto)
        {
            return new DetalleMedioCobroEntity
            {
                Id = dto.Id,
                IdCobroDebito = dto.IdCobroDebito,
                IdMedioPago = dto.IdMedioPago,
                Descripcion = dto.Descripcion,
                Valor = dto.Value,

                IdEstado = dto.IdStatus,
                UsuarioIngreso = dto.CreatedBy,
                UsuarioModificacion = dto.ModifiedBy,
                FechaIngreso = dto.CreatedOn,
                FechaModificacion = dto.ModifiedOn,
                IpModificacion = dto.IpModifier,
                IpIngreso = dto.IpCreator
            };
        }

        public static DetalleCobroEntity ToDetalleCobroEntity(this ReceiptDetailDto dto)
        {
            return new DetalleCobroEntity
            {
                Id = dto.Id,
                IdCobroDebito = dto.IdCobroDebito,
                IdCuentaPorCobrar = dto.IdCuentaPorCobrar,
                Valor = dto.Value,

                IdEstado = dto.IdStatus,
                UsuarioIngreso = dto.CreatedBy,
                UsuarioModificacion = dto.ModifiedBy,
                FechaIngreso = dto.CreatedOn,
                FechaModificacion = dto.ModifiedOn,
                IpModificacion = dto.IpModifier,
                IpIngreso = dto.IpCreator
            };
        }

    }
}