using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(preguntados_ppodgaiz.Startup))]
namespace preguntados_ppodgaiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
