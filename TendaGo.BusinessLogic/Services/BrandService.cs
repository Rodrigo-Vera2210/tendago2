using AutoMapper;
using ER.BE;
using ER.DA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.BusinessLogic.Services
{
    public class BrandService : IBrandService
    {
        private readonly ITendaGOContextProcedures _procedimientos;
        private readonly IMapper _mapper;

        public BrandService(ITendaGOContextProcedures procedimientos, IMapper mapper)
        {
            _procedimientos = procedimientos;
            _mapper = mapper;
        }

        public Task<BrandDto> GetBrandEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrandDto>> GetBrands(MarcaFindParameterEntity findParameter)
        {
            throw new NotImplementedException();
        }

        public Task<BrandDto> PostBrand(BrandDto brand)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<BrandDto>> GetBrands(MarcaFindParameterEntity findParameter)
        //{
        //    try
        //    {
        //        var brands = await _procedimientos.Marca_FindByAllAsync(findParameter.Id, findParameter.IdEmpresa, findParameter.Marca, findParameter.IpIngreso, findParameter.UsuarioIngreso, findParameter.FechaIngreso, findParameter.IpModificacion, findParameter.UsuarioModificacion, findParameter.FechaModificacion, findParameter.IdEstado);
        //        var brandsDtoList = _mapper.Map<List<BrandDto>>(brands);
        //        return brandsDtoList;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //public Task<BrandDto> PostBrand(BrandDto brand)
        //{
        //    try
        //    {
        //        MarcaEntity marcaEntity;
        //        if (brand.Id != 0)
        //        {
        //            marcaEntity = MarcaBussinesAction.LoadByPK(brand.Id);
        //            marcaEntity.Marca = brand.Marca;
        //            marcaEntity.IdEstado = brand.IdEstado;
        //            if (marcaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
        //            {
        //                marcaEntity.UsuarioModificacion = brand.UsuarioModificacion;
        //                marcaEntity.FechaModificacion = DateTime.Now;
        //                marcaEntity.IpModificacion = brand.IpModificacion;
        //            }
        //        }
        //        else
        //        {
        //            marcaEntity = brand.ToMarcaEntity();
        //            //var usuarioentity = brand.Id != 0 ? GetAuthenticatedUserByToken(brand.UsuarioModificacion) : GetAuthenticatedUserByToken(brand.UsuarioIngreso);
        //            marcaEntity.IdEmpresa = CurrentUser.IdEmpresa;
        //            marcaEntity.FechaIngreso = DateTime.Now;
        //        }
        //        marcaEntity = MarcaBussinesAction.Save(marcaEntity);
        //        return marcaEntity.ToBrandDto();
        //    }
        //    catch (Exception ex)
        //    {
        //        string UserError = "Ocurrio un error general, reintente";
        //        if (ex.GetMessage().Contains("UQ_Marca"))
        //        {
        //            UserError = "No puede ingresar una Linea duplicada";
        //        }
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", UserError));
        //    }
        //}

        //public Task<MarcaEntity> GetBrandEntity(int id)
        //{
        //    var brand = MarcaBussinesAction.LoadByPK(id);

        //    if (brand == null || brand.IdEmpresa != CurrentUser.IdEmpresa)
        //        throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.NotFound, "Marca no existe", "La marca solicitada no existe"));

        //    return brand;
        //}

    }
}
