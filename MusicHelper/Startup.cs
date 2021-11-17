using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicHelper.Startup))]
namespace MusicHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
