using AutoMapper;
using ER.BA;
using ER.BE;
using System;
using System.Linq;

namespace TendaGo.Common
{
    internal static class RucExtensions
    {
        internal static RucDtoLite ToRucDtoLite(this RucEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<LocalBodegaEntity, RucDtoLite>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<RucDtoLite>(entity);
            return dto;
        }

        internal static RucDto ToRucDto(this RucEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<LocalBodegaEntity, RucDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<RucDto>(entity);
            return dto;
        }

        internal static LocalBodegaEntity ToLocalBodegaEntity(this WarehouseDto dto)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<WarehouseDto, LocalBodegaEntity>());
            var mapper = conf.CreateMapper();
            var entity = mapper.Map<LocalBodegaEntity>(dto);
            return entity;
        }

        internal static SalidaEntity GenerarSalida(this AdjustInventoryDto model)
        {
            var detalles = model.Detalle.Where(x => x.Cantidad < 0).ToList();

            if (detalles.Count == 0)
            {
                return default;
            }

            SalidaEntity salida = new SalidaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "ENTREGADA",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                FechaHoraEntregaPropuesta = DateTime.Now,
                IdEstado = 1,
                IdCliente = DEFAULT_TRANSFER_ENTITY,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "AN",
                IdFormaPago = 1,
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = model.IdLocal,
                IdVendedor = model.UsuarioIngreso,
                Observaciones = model.Observaciones,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleSalidaFromIdSalida = new DetalleSalidaEntityCollection()
            };

            salida.DetalleSalidaFromIdSalida.AddRange(detalles.Select(m =>
                {
                    var detalle = m.ToDetalleSalidaEntity();

                    detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                    detalle.FechaFabricacion = DateTime.Now;
                    detalle.IdLocal = model.IdLocal;
                    detalle.Cantidad = -m.Cantidad;
                    detalle.CostoVenta = m.Precio;

                    return detalle;
                }));

            salida.SetNewState();

            return salida;
        }

        internal static EntradaEntity GenerarEntrada(this AdjustInventoryDto model)
        {
            var detalles = model.Detalle.Where(x => x.Cantidad > 0).ToList();

            if (detalles.Count == 0)
            {
                return default;
            }

            EntradaEntity entrada = new EntradaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "ENTREGADA",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                IdEstado = 1,
                IdProveedor = DEFAULT_TRANSFER_ENTITY,
                IdFormaPago = 1,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "AP",
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = model.IdLocal,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleEntradaFromIdEntrada = new DetalleEntradaEntityCollection()
            };

            entrada.DetalleEntradaFromIdEntrada.AddRange(detalles.Select(m =>
            {
                var detalle = m.ToDetalleEntradaEntity();

                detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                detalle.FechaFabricacion = DateTime.Now;
                detalle.IdLocal = model.IdLocal;
                detalle.CostoVenta = m.Precio;

                return detalle;
            }));

            entrada.SetNewState();

            return entrada;
        }


        const int DEFAULT_TRANSFER_ENTITY = 71;

        internal static SalidaEntity GenerarSalida(this TransferDto model, int local)
        {
            SalidaEntity salida = new SalidaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "APROBADA",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                FechaHoraEntregaPropuesta = DateTime.Now,
                IdEstado = 1,
                IdCliente = DEFAULT_TRANSFER_ENTITY,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "GE",
                IdFormaPago = 1,
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = local,
                IdVendedor = model.IdVendedor,
                Observaciones = model.Observaciones,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleSalidaFromIdSalida = new DetalleSalidaEntityCollection()
            };

            salida.DetalleSalidaFromIdSalida.AddRange(model.Detalle.Select(m =>
            {
                var detalle = m.ToDetalleSalidaEntity();

                detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                detalle.FechaFabricacion = DateTime.Now;
                detalle.IdLocal = local;

                return detalle;
            }));

            salida.SetNewState();

            return salida;
        }

        internal static EntradaEntity GenerarEntrada(this TransferDto model, int local)
        {
            EntradaEntity entrada = new EntradaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "EN PROCESO",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                IdEstado = 1,
                IdProveedor = DEFAULT_TRANSFER_ENTITY,
                IdFormaPago = 1,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "GR",
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = local,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleEntradaFromIdEntrada = new DetalleEntradaEntityCollection()
            };

            entrada.DetalleEntradaFromIdEntrada.AddRange(model.Detalle.Select(m =>
            {
                var detalle = m.ToDetalleEntradaEntity();

                detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                detalle.FechaFabricacion = DateTime.Now;
                detalle.IdLocal = local;

                return detalle;
            }));

            entrada.SetNewState();

            return entrada;
        }

        //Para Ajuste
        internal static SalidaEntity GenerarSalidaAju(this AdjustInventoryDto model)
        {
            var detalles = model.Detalle.ToList();

            if (detalles.Count == 0)
            {
                return default;
            }

            SalidaEntity salida = new SalidaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "ENTREGADA",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                FechaHoraEntregaPropuesta = DateTime.Now,
                IdEstado = 1,
                IdCliente = DEFAULT_TRANSFER_ENTITY,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "AN",
                IdFormaPago = 1,
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = model.IdLocal,
                IdVendedor = model.UsuarioIngreso,
                Observaciones = model.Observaciones,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleSalidaFromIdSalida = new DetalleSalidaEntityCollection()
            };

            salida.DetalleSalidaFromIdSalida.AddRange(detalles.Select(m =>
            {
                var detalle = m.ToDetalleSalidaEntity();
                var stockActual = StockCollectionBussinesAction.StockInventario(model.IdEmpresa, m.IdProducto, model.IdLocal).ConvertirToDto<StockDto>().FirstOrDefault();
                var tipoUnidad = TipoUnidadBussinesAction.LoadByPK(m.IdTipoUnidad);

                detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                detalle.FechaFabricacion = DateTime.Now;
                detalle.IdLocal = model.IdLocal;

                //if(detalle.Cantidad==0)
                //    detalle.Cantidad = (stockActual.StockInventario ?? 0M);
                //else
                detalle.Cantidad = ((stockActual.StockInventario / tipoUnidad.Cantidad) ?? 0M);

                detalle.CostoVenta = m.Precio;

                return detalle;
            }));

            salida.SetNewState();

            return salida;
        }

        internal static EntradaEntity GenerarEntradaAju(this AdjustInventoryDto model)
        {
            var detalles = model.Detalle.Where(x => x.Cantidad > 0).ToList();

            if (detalles.Count == 0)
            {
                return default;
            }

            EntradaEntity entrada = new EntradaEntity
            {
                TransaccionPadre = model.Id,
                TipoTransaccionPadre = model.TipoTransaccion,
                EstadoActual = "ENTREGADA",
                Fecha = DateTime.Today,
                FechaIngreso = DateTime.Now,
                IdEstado = 1,
                IdProveedor = DEFAULT_TRANSFER_ENTITY,
                IdFormaPago = 1,
                IdEmpresa = model.IdEmpresa,
                TipoTransaccion = "AP",
                Periodo = DateTime.Today.ToString("yyyyMM"),
                IdLocal = model.IdLocal,
                IpIngreso = model.IpIngreso,
                UsuarioIngreso = model.UsuarioIngreso,
                DetalleEntradaFromIdEntrada = new DetalleEntradaEntityCollection()
            };

            entrada.DetalleEntradaFromIdEntrada.AddRange(detalles.Select(m =>
            {
                var detalle = m.ToDetalleEntradaEntity();

                detalle.IdProveedor = DEFAULT_TRANSFER_ENTITY;
                detalle.FechaFabricacion = DateTime.Now;
                detalle.IdLocal = model.IdLocal;
                detalle.CostoVenta = m.Precio;

                return detalle;
            }));

            entrada.SetNewState();

            return entrada;
        }
    }
}