using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MiniOglasi.Startup))]
namespace MiniOglasi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
