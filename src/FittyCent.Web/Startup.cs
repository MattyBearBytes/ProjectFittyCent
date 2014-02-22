using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FittyCent.Web.Startup))]
namespace FittyCent.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
