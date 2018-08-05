using BrumWithMe.Auth.Identity.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrumWithMe.MVC.Startup))]
namespace BrumWithMe.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // TODO: NInject
            var identityConfig = new IdentityConfig();
            identityConfig.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
