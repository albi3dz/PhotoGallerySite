using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhotoGallerySite.Startup))]
namespace PhotoGallerySite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
