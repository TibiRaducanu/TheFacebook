using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheFacebook.Startup))]
namespace TheFacebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
