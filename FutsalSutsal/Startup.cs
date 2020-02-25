using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FutsalSutsal.Startup))]
namespace FutsalSutsal
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app) => ConfigureAuth(app);
    }
}
