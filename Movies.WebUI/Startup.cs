using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Movies.WebUI.Startup))]
namespace Movies.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
