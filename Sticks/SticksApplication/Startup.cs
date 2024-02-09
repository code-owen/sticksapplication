using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SticksApplication.Startup))]
namespace SticksApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
