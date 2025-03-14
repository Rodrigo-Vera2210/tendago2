using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Repositories;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public ProductoService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public Task<ProductoEntity> LoadByPK(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<ProductoEntity> LoadByPK(int id)
        //{
        //    try
        //    {
        //        var empresa = await _procedimientos.Producto_LoadByPKAsync(id);

        //        var result = _mapper.Map<ProductoEntity>(empresa);

        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        //public async Task<ProductDto> PostProduct(ProductDto product)
        //{
        //    var productoEntity = new ProductoEntity();
        //    if (product.Id > 0)
        //    {
        //        productoEntity = ProductoBussinesAction.LoadByPK(product.Id);
        //        productoEntity.IdEmpresa = CurrentUser.IdEmpresa;
        //        productoEntity.CodigoBarra = product.CodigoBarra;
        //        productoEntity.CodigoProveedor = product.CodigoProveedor;
        //        productoEntity.CodigoInterno = product.CodigoInterno;
        //        productoEntity.TipoProducto = product.TipoProducto;
        //        productoEntity.Producto = product.Producto;
        //        productoEntity.ProductoPadre = product.ProductoPadre;
        //        productoEntity.Descripcion = product.Descripcion;
        //        if (product.HasFotoChanges)
        //        {
        //            //productoEntity.PathArchivo = product.PathArchivo;
        //            if (!string.IsNullOrEmpty(product.Foto))
        //            {
        //                productoEntity.PathArchivo = await UploadFile(product.Foto, $"{product.CodigoInterno}");
        //            }
        //            else
        //            {
        //                productoEntity.PathArchivo = "https://tendagoeast.binasystem.com/empresas/assets/NoImage.jpeg";
        //            }
        //        }


        //        //productoEntity.DescipcionBusqueda = product.DescripcionBusqueda;
        //        productoEntity.StockMinimo = product.StockMinimo ?? 0;
        //        productoEntity.Costo = product.Costo;
        //        productoEntity.UnidadMedida = product.UnidadMedida ?? 0;
        //        productoEntity.Descuento = product.Descuento ?? 0;
        //        productoEntity.CobraIva = product.CobraIva ?? false;
        //        productoEntity.IdCategoria = product.IdCategoria ?? 0;
        //        productoEntity.IdDivision = product.IdDivision ?? 0;
        //        productoEntity.IdLinea = product.IdLinea ?? 0;
        //        productoEntity.Volumen = product.Volumen ?? 0;
        //        productoEntity.RegistroSanitario = product.RegistroSanitario;
        //        productoEntity.Peso = product.Peso ?? 0;
        //        productoEntity.IdMarca = product.IdMarca ?? 0;
        //        productoEntity.IdEstado = product.IdEstado;

        //        if (productoEntity.IdEstado == 1)
        //        {
        //            if (productoEntity.CurrentState == EntityStatesEnum.Deleted)
        //                productoEntity.CurrentState = EntityStatesEnum.Updated;
        //        }

        //        if (productoEntity.CurrentState == EntityStatesEnum.Updated)
        //        {
        //            productoEntity.IpModificacion = product.IpModificacion;
        //            productoEntity.UsuarioModificacion = product.UsuarioModificacion;
        //            productoEntity.FechaModificacion = DateTime.Today;
        //        }
        //    }
        //    else
        //    {
        //        productoEntity = product.GlobalMapperConverter<ProductDto, ProductoEntity>();
        //        productoEntity.IpIngreso = product.IpIngreso;
        //        productoEntity.UsuarioIngreso = product.UsuarioIngreso;
        //        productoEntity.FechaIngreso = DateTime.Today;
        //        productoEntity.IdEstado = 1;
        //        productoEntity.IdEmpresa = CurrentUser.IdEmpresa;
        //        if (!string.IsNullOrEmpty(product.Foto))
        //        {
        //            productoEntity.PathArchivo = await UploadFile(product.Foto, $"{product.CodigoInterno}");
        //        }
        //        else
        //        {
        //            productoEntity.PathArchivo = "https://tendagoeast.binasystem.com/empresas/assets/NoImage.jpeg";
        //        }
        //        productoEntity.SetNewState();
        //    }

        //    productoEntity = ProductoBussinesAction.Save(productoEntity);
        //    return productoEntity.ToProductDto();
        //}
        //private string GetProductPath(string id)
        //{
        //    return $"{CurrentUser.IdEmpresa}/productos/{id}-v{DateTime.Now.ToFileTime()}.jpg".ToLower();
        //}

        //private async Task<string> UploadFile(Stream stream, string id)
        //{
        //    var file = stream.ToByteArray();
        //    var path = GetProductPath(id);

        //    await Storage.FileHandler.UploadAsync(path, file);
        //    return await Storage.FileHandler.GetFileAsync(path);
        //}

        //private async Task<string> UploadFile(string imagen, string id)
        //{
        //    var file = Convert.FromBase64String(imagen);
        //    var path = GetProductPath(id);

        //    var result = await Storage.FileHandler.UploadAsync(path, file);

        //    return $"{result.Uri}";
        //}
    }
}
