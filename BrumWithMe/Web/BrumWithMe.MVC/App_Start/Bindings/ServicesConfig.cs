using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Auth.Identity.Services;
using Microsoft.Owin;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrumWithMe.MVC.App_Start.Bindings
{
    public class ServicesConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthService>().To<AuthService>();
            this.Bind<IOwinContext>()
               .ToMethod(c => HttpContext.Current.GetOwinContext())
               .WhenInjectedInto(typeof(IAuthService));
        }
    }
}