using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryWebApplication.Startup))]
namespace LibraryWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
