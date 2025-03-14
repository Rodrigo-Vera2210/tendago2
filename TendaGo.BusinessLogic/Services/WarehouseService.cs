using ER.BE;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;

namespace TendaGo.BusinessLogic.Services
{
    public class WarehouseService
    {
        //public List<WarehouseDto> GetWarehouses()
        //{
        //    var warehouses = LocalBodegaCollectionBussinesAction.FindByAll(new LocalBodegaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
        //    var warehousesDtoList = warehouses.Select(w => w.ToWarehouseDto()).ToList();
        //    return warehousesDtoList;
        //}

        //public WarehouseDto GetWarehouse(string id)
        //{
        //    int idConverted;
        //    bool isValidId = int.TryParse(id, out idConverted);
        //    if (!isValidId)
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, "El parametro id, es invalido", "Id invalido"));


        //    var warehouse = LocalBodegaCollectionBussinesAction.FindByAll(new LocalBodegaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa, Id = idConverted });
        //    return warehouse.FirstOrDefault().ToWarehouseDto();
        //}


        //public static List<TransferDto> Transferir(TransferRequest model, SqlConnection connection, SqlTransaction transaction)
        //{
        //    var locales = model.Detalle.Select(x => new { x.IdLocal }).Distinct().Select(x => x.IdLocal).ToList();
        //    var results = new List<TransferDto>();
        //    foreach (var idLocalOrigen in locales)
        //    {
        //        var bodegaDestino = LocalBodegaBussinesAction.LoadByPK(model.IdLocalDestino, connection, transaction);
        //        if (bodegaDestino == null)
        //            throw new Exception("No ha especificado la bodega de destino");

        //        if (idLocalOrigen == model.IdLocalDestino)
        //            throw new Exception("La bodega origen no puede ser igual al destino.");

        //        var bodegaDesde = LocalBodegaBussinesAction.LoadByPK(idLocalOrigen, connection, transaction);
        //        if (bodegaDesde == null)
        //            throw new Exception("No ha especificado la bodega de origen");

        //        var result = model.ToTransferDto();
        //        result.IdLocalOrigen = result.IdLocalOrigen > 0 ? result.IdLocalOrigen : idLocalOrigen;
        //        result.Id = Guid.NewGuid().ToString();
        //        result.TipoTransaccion = "TRAN";

        //        result.Detalle = model.Detalle.Where(x => x.IdLocal == idLocalOrigen).ToList();

        //        var salida = result.GenerarSalida(idLocalOrigen);
        //        var entrada = result.GenerarEntrada(result.IdLocalDestino);

        //        result.Salida = SalidaBussinesAction.Guardar(salida, connection, transaction)?.ToOutputDto();
        //        result.Entrada = EntradaBussinesAction.Guardar(entrada, connection, transaction)?.ToInputDto();

        //        results.Add(result);
        //    }

        //    return results;

        //}


        //public static void SetDetallesEntrada(EntradaEntity entrada, List<InputDetailDto> detalles)
        //{
        //    if (detalles.Count == 0)
        //    {
        //        throw new Exception("No se puede eliminar todos los productos, en su lugar debe anular la Orden");
        //    }
        //    if (entrada.DetalleEntradaFromIdEntrada == null)
        //    {
        //        entrada.DetalleEntradaFromIdEntrada = GetDetallesEntity(entrada.Id) ?? new DetalleEntradaEntityCollection();
        //    }

        //    if (detalles.Count > 0)
        //    {
        //        var deleted = entrada.DetalleEntradaFromIdEntrada.Where(detalle => !detalles.Any(detail => detail.IdProducto == detalle.IdProducto));

        //        // Este es un proceso para validar que se esta eliminando los detalles correctos. 
        //        // De lo contrario no los elimina hasta corregir el tema de los pedidos grandes.

        //        var del = deleted.Count();
        //        var cant = detalles.Count;
        //        var last = entrada.DetalleEntradaFromIdEntrada.Count;
        //        var rest = last - cant;

        //        // Los detalles eliminados deben coincidir con 
        //        // la cantidad de detalles faltantes
        //        if (rest == del)
        //        {
        //            foreach (var item in deleted)
        //            {
        //                item.IdEstado = 0;
        //                item.FechaModificacion = entrada.FechaModificacion;
        //                item.UsuarioModificacion = entrada.UsuarioModificacion;
        //                item.IpModificacion = entrada.IpModificacion;
        //                item.SetDeletedState();
        //            }
        //        }

        //        foreach (var detalle in detalles)
        //        {
        //            //var detalleEntity = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(m => m.Id == entrada.Id);
        //            var detalleEntity = entrada.DetalleEntradaFromIdEntrada.FirstOrDefault(m => m.IdProducto == detalle.IdProducto);

        //            if (detalleEntity != null)
        //            {
        //                // Actualizo los detalles con los datos enviados
        //                detalleEntity.UpdateEntity(detalle);
        //                detalleEntity.IdEntrada = entrada.Id;
        //                detalleEntity.Periodo = entrada.Periodo;
        //                detalleEntity.TipoTransaccion = entrada.TipoTransaccion;
        //                detalleEntity.IdEstado = 1;
        //                detalleEntity.Fecha = entrada.Fecha;
        //                detalleEntity.FechaModificacion = entrada.FechaModificacion;
        //                detalleEntity.UsuarioModificacion = entrada.UsuarioModificacion;
        //                detalleEntity.IpModificacion = entrada.IpModificacion;
        //                detalleEntity.SetUpdatedState();
        //            }
        //            else
        //            {
        //                detalleEntity = detalle.ToDetalleEntradaEntity();
        //                detalleEntity.IdEntrada = entrada.Id;
        //                detalleEntity.Periodo = entrada.Periodo;
        //                detalleEntity.TipoTransaccion = entrada.TipoTransaccion;
        //                detalleEntity.IdEstado = 1;
        //                detalleEntity.Fecha = entrada.Fecha;
        //                detalleEntity.FechaIngreso = entrada.FechaModificacion ?? entrada.FechaIngreso;
        //                detalleEntity.UsuarioIngreso = entrada.UsuarioModificacion ?? entrada.UsuarioIngreso;
        //                detalleEntity.IpIngreso = entrada.IpModificacion ?? entrada.IpIngreso;
        //                detalleEntity.SetNewState();

        //                entrada.DetalleEntradaFromIdEntrada.Add(detalleEntity);
        //            }
        //        }
        //    }
        //}

        //static DetalleEntradaEntityCollection GetDetallesEntity(string idEntrada)
        //{
        //    return DetalleEntradaCollectionBussinesAction.FindByAll(new DetalleEntradaFindParameterEntity { IdEntrada = idEntrada, IdEstado = 1 }, 0); ;
        //}

    }
}
