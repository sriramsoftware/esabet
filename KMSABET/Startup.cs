using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KMSABET.Startup))]
namespace KMSABET
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
