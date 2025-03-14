using ER.BA;
using ER.BE;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TendaGo.Common;
using TendaGo.Domain.Services;

namespace TendaGo.Api.Controllers
{
    //[TokenAuthorize]
    //public class BrandsController : ApiControllerBase
    //{
    //    private readonly IMarcaService _marcaService;
    //    private readonly IBrandService _brandService;

    //    public BrandsController(IMarcaService marcaService, IBrandService brandService)
    //    {
    //        _marcaService = marcaService;
    //        _brandService = brandService;
    //    }

    //    [HttpGet]
    //    public async Task<List<BrandDto>> GetBrands()
    //    {
    //        try
    //        {
    //            var brands = await _brandService.GetBrands(new MarcaFindParameterEntity { IdEmpresa = 1 });
    //            return brands;
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
            
    //    }

    //    [HttpPost]
    //    public async Task<BrandDto> PostBrand(BrandDto brand)
    //    {
    //        try
    //        {
    //            var brands = await _brandService.PostBrand(brand);

    //            return brands;
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }

    //    }

    //    [Route("brands/{id}")]
    //    public async Task<BrandDto> GetBrand(int id)
    //    {
    //        try
    //        {
    //            var brand = await _brandService.GetBrandEntity(id);

    //            return brand;
    //        }
    //        catch (HttpResponseException)
    //        {
    //            throw;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new HttpResponseException(Request.BuildHttpErrorResponse(HttpStatusCode.BadRequest, $"{ex.GetAllMessages()}", "Ocurrio un error general, reintente"));
    //        }
    //    }

    //}
}
