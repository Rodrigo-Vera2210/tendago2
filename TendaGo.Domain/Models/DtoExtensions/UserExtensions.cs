using AutoMapper;
using ER.BE;

namespace TendaGo.Domain
{
    public static class UserExtensions
    {
        internal static SellerDto ToSellerDto(this UsuarioEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<UsuarioEntity, SellerDto>()
                        .ForMember(model => model.Id, x => x.MapFrom(p => p.InicioSesion)));

            var mapper = conf.CreateMapper();
            var dto = mapper.Map<SellerDto>(entity);

            return dto;
        }

        internal static UserDto ToUserDto(this UsuarioEntity entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<UsuarioEntity, UserDto>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<UserDto>(entity);

            dto.RaizArchivo = CommonFunctions.UrlWeb + entity.IdEmpresaAsEmpresa?.RaizArchivo;

            return dto;
        }

        internal static UsuarioEntity ToUsuarioEntity(this UserDto entity)
        {
            var conf = new MapperConfiguration(config => config.CreateMap<UserDto, UsuarioEntity>());
            var mapper = conf.CreateMapper();
            var dto = mapper.Map<UsuarioEntity>(entity);
            return dto;
        }

        internal static CompanyDto ToCompanyDto(this EmpresaEntity entity)
        {
            var mapper = new MapperConfiguration(config => config.CreateMap<CompanyDto, EmpresaEntity>()).CreateMapper();
            var dto = mapper.Map<CompanyDto>(entity);

            return dto;
        }


    }

}