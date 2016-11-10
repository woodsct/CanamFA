using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CanamLiveFA.Startup))]
namespace CanamLiveFA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
