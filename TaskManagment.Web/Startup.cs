using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskManagment.Web.Startup))]
namespace TaskManagment.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
