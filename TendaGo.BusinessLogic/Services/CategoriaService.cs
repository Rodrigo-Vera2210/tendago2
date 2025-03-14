using AutoMapper;
using ER.BA;
using ER.BE;
using ER.DA.Models;
using ER.DA;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using TendaGo.Domain.Models;

namespace TendaGo.BusinessLogic.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ITendaGOContextProcedures _procedures;
        private readonly IMapper _mapper;

        public CategoriaService(ITendaGOContextProcedures procedures, IMapper mapper)
        {
            _procedures = procedures;
            _mapper = mapper;
        }

        public async Task<CategoryDto> PostCategory(CategoryDto category)
        {
            try
            {
                CategoriaEntity catEntity;
                if (category.Id != 0)
                {
                    catEntity = _mapper.Map<CategoriaEntity>(await _procedures.Categoria_LoadByPKAsync(category.Id));
                    catEntity.IdLinea = category.IdLinea;
                    catEntity.Categoria = category.Categoria;
                    catEntity.IdEstado = category.IdEstado;
                    if (catEntity.CurrentState.Equals(EntityStatesEnum.Updated))
                    {
                        catEntity.UsuarioModificacion = category.UsuarioModificacion;
                        catEntity.IpModificacion = category.IpModificacion;
                        catEntity.FechaModificacion = DateTime.Today;
                    }
                }
                else
                {
                    catEntity = _mapper.Map<CategoriaEntity>(category);
                    catEntity.FechaIngreso = DateTime.Today;
                }
                ////var usuarioentity = category.Id != 0 ? GetAuthenticatedUserByToken(category.UsuarioModificacion) : GetAuthenticatedUserByToken(category.UsuarioIngreso);

                //catEntity.IdEmpresa = CurrentUser.IdEmpresa;
                switch (catEntity.CurrentState)
                {
                    case EntityStatesEnum.Deleted:
                        await Delete(catEntity);
                        break;
                    case EntityStatesEnum.Updated:
                        await Update(catEntity);
                        break;
                    case EntityStatesEnum.New:
                        catEntity = await Insert(catEntity);
                        break;
                    default:
                        break;
                }

                return _mapper.Map<CategoryDto>(catEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CategoriaDTO> GetCategoriaEntity(int id, UsuarioDTO usuario)
        {
            try
            {
                var result = await _procedures.Categoria_LoadByPKAsync(id);

                var cat = _mapper.Map<CategoriaDTO>(result.FirstOrDefault());

                if (cat == null || cat.IdEmpresa != usuario.IdEmpresa) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Categoría no existe El registro solicitado no existe") });
                
                return cat;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public async Task<CategoriaEntityCollection> FindByAll(CategoriaFindParameterEntity findParameter)
        {
            try
            {
                var categorias = await _procedures.Categoria_FindByAllAsync(
                                                        findParameter.Id,
                                                        findParameter.IdEmpresa,
                                                        findParameter.IdLinea,
                                                        findParameter.Categoria,
                                                        findParameter.IpIngreso,
                                                        findParameter.UsuarioIngreso,
                                                        findParameter.FechaIngreso,
                                                        findParameter.IpModificacion,
                                                        findParameter.UsuarioModificacion,
                                                        findParameter.FechaModificacion,
                                                        findParameter.IdEstado
                                                    );

                var result = _mapper.Map<CategoriaEntityCollection>(categorias);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoriaEntityCollection> FindByAllPaged(CategoriaFindParameterEntity findParameter, int pageNumber, int pageSize)
        {
            try
            {
                var categorias = await _procedures.Categoria_FindByAllPagedAsync(
                                                        findParameter.Id,
                                                        findParameter.IdEmpresa,
                                                        findParameter.IdLinea,
                                                        findParameter.Categoria,
                                                        findParameter.IpIngreso,
                                                        findParameter.UsuarioIngreso,
                                                        findParameter.FechaIngreso,
                                                        findParameter.IpModificacion,
                                                        findParameter.UsuarioModificacion,
                                                        findParameter.FechaModificacion,
                                                        findParameter.IdEstado,
                                                        pageNumber,
                                                        pageSize
                                                    );

                var result = _mapper.Map<CategoriaEntityCollection>(categorias);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public async Task<CategoriaEntity> Insert(CategoriaEntity categoria)
        {
            try
            {
                OutputParameter<int?> idCategoria = new();

                await _procedures.Categoria_InsertAsync(
                    categoria.IdEmpresa,
                    categoria.IdLinea,
                    categoria.Categoria.ToUpper(),
                    categoria.IpIngreso,
                    categoria.UsuarioIngreso.ToUpper(),
                    categoria.FechaIngreso,
                    categoria.IpModificacion.ToUpper(),
                    categoria.UsuarioModificacion.ToUpper(),
                    categoria.FechaModificacion,
                    categoria.IdEstado,
                    idCategoria);

                categoria.Id = (int)idCategoria.Value;

                return categoria;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task Update(CategoriaEntity categoria)
        {
            try
            {
                await _procedures.Categoria_UpdateAsync(
                    categoria.Id,
                    categoria.IdEmpresa,
                    categoria.IdLinea,
                    categoria.Categoria.ToUpper(),
                    categoria.IpIngreso,
                    categoria.UsuarioIngreso.ToUpper(),
                    categoria.FechaIngreso,
                    categoria.IpModificacion.ToUpper(),
                    categoria.UsuarioModificacion.ToUpper(),
                    categoria.FechaModificacion,
                    categoria.IdEstado
                    );
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task Delete(CategoriaEntity categoria)
        {
            try
            {

                await _procedures.Categoria_DeleteAsync(
                    categoria.Id
                    , categoria.FechaModificacion
                    , categoria.UsuarioModificacion.ToUpper()
                    , categoria.IpModificacion.ToUpper()
                    );

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
