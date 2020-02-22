using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrnamentsWebApplication.Startup))]
namespace OrnamentsWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
