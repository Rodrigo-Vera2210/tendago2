using Microsoft.Owin;
using Owin;
using TendaGo.Web.App_Start;

[assembly: OwinStartupAttribute(typeof(TendaGo.Web.Startup))]
namespace TendaGo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutoMapperConfig.Configure();
        }
    }
}
