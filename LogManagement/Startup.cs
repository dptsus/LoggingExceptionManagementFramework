using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogManagement.Startup))]
namespace LogManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
