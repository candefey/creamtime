using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(creamtime.Startup))]
namespace creamtime
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
