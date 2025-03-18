using ER.BE;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    [TokenAuthorize]
    public class BrandsController : ApiControllerBase
    {
        private readonly IMarcaService _marcaService;
        private readonly IBrandService _brandService;

        public BrandsController(IMarcaService marcaService, IBrandService brandService,IUsuarioService usuarioService) : base(usuarioService)
        {
            _marcaService = marcaService;
            _brandService = brandService;
        }

        [HttpGet, Route("")]
        public async Task<List<BrandDto>> GetBrands()
        {
            try
            {
                var brands = await _brandService.GetBrands(new MarcaFindParameterEntity { IdEmpresa = CurrentUser.IdEmpresa });
                return brands;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost, Route("")]
        public async Task<BrandDto> PostBrand(BrandDto brand)
        {
            try
            {
                var brands = await _brandService.PostBrand(brand, CurrentUser.IdEmpresa);

                return brands;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet, Route("{id}")]
        public async Task<BrandDto> GetBrand(int id)
        {
            try
            {
                var brand = await _brandService.GetBrandEntity(id, CurrentUser.IdEmpresa);

                return brand;
            }
            catch (System.Web.Http.HttpResponseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("Ocurrio un error general, reintente") });
            }
        }

    }
}
