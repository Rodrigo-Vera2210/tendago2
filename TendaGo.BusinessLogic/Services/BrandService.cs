using AutoMapper;
using ER.BE;
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
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

        public async Task<List<BrandDto>> GetBrands(MarcaFindParameterEntity findParameter)
        {
            try
            {
                var brands = await _procedimientos.Marca_FindByAllAsync(findParameter.Id, findParameter.IdEmpresa, findParameter.Marca, findParameter.IpIngreso, findParameter.UsuarioIngreso, findParameter.FechaIngreso, findParameter.IpModificacion, findParameter.UsuarioModificacion, findParameter.FechaModificacion, findParameter.IdEstado);
                var brandsDtoList = _mapper.Map<List<BrandDto>>(brands);
                return brandsDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<BrandDto> PostBrand(BrandDto brand, int idEmpresa)
        {
            try
            {
                MarcaEntity marcaEntity;
                if (brand.Id != 0)
                {
                    var result = _procedimientos.Marca_LoadByPKAsync(brand.Id);
                    marcaEntity = _mapper.Map<MarcaEntity>(result);
                    marcaEntity.Marca = brand.Marca;
                    marcaEntity.IdEstado = brand.IdEstado;
                    if (marcaEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        marcaEntity.UsuarioModificacion = brand.UsuarioModificacion;
                        marcaEntity.FechaModificacion = DateTime.Now;
                        marcaEntity.IpModificacion = brand.IpModificacion;
                    }
                }
                else
                {
                    marcaEntity = _mapper.Map<MarcaEntity>(brand);
                    //var usuarioentity = brand.Id != 0 ? GetAuthenticatedUserByToken(brand.UsuarioModificacion) : GetAuthenticatedUserByToken(brand.UsuarioIngreso);
                    marcaEntity.IdEmpresa = idEmpresa;
                    marcaEntity.FechaIngreso = DateTime.Now;
                }
                marcaEntity = await Save(marcaEntity);

                var response = _mapper.Map<BrandDto>(marcaEntity);

                return response;
            }
            catch (Exception ex)
            {
                string UserError = "Ocurrio un error general, reintente";
                if (ex.Message.Contains("UQ_Marca"))
                {
                    UserError = "No puede ingresar una Linea duplicada";
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent($"{ex.Message}") });
            }
        }

        public async Task<MarcaEntity> GetBrandEntity(int id, int idEmpresa)
        {
            var result = await _procedimientos.Marca_LoadByPKAsync(id);

            var brand = _mapper.Map<MarcaEntity>(result.FirstOrDefault());

            if (brand == null || brand.IdEmpresa != idEmpresa)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Marca no existe La marca solicitada no existe") });

            return brand;
        }

        private async Task<MarcaEntity> Save(MarcaEntity marca)
        {
            try
            {
                switch (marca.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        await Delete(marca);
                        break;
                    case EntityStatesEnum.Updated:
                        await Update(marca);
                        break;
                    case EntityStatesEnum.New:
                        marca = await Insert(marca);
                        break;
                    default:
                        break;

                }

                return marca;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<MarcaEntity> Insert(MarcaEntity marca)
        {
            try
            {
                OutputParameter<int?> idMarca = new();

                var result = await _procedimientos.Marca_InsertAsync(
                        marca.Id,
                        marca.Marca,
                        marca.IpIngreso,
                        marca.UsuarioIngreso,
                        marca.FechaIngreso,
                        marca.IpModificacion,
                        marca.UsuarioModificacion,
                        marca.FechaModificacion,
                        marca.IdEstado,
                        idMarca
                    );

                return await GetBrandEntity((int)idMarca.Value, marca.IdEmpresa);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task Update(MarcaEntity marca)
        {
            try
            {
                OutputParameter<int> idMarca = new();


                var result = await _procedimientos.Marca_UpdateAsync(
                                                marca.Id,
                                                marca.IdEmpresa,
                                                marca.Marca,
                                                marca.IpIngreso,
                                                marca.UsuarioIngreso,
                                                marca.FechaIngreso,
                                                marca.IpModificacion,
                                                marca.UsuarioModificacion,
                                                marca.FechaModificacion,
                                                marca.IdEstado,
                                                idMarca
                                            );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Delete a entity
        /// </summary>
        public async Task Delete(MarcaEntity marca)
        {
            try
            {
                var result = await _procedimientos.Marca_DeleteAsync(
                                                    marca.Id,
                                                    marca.FechaModificacion,
                                                    marca.UsuarioModificacion,
                                                    marca.IpModificacion
                                                   );


            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
    
}
