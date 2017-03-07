using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Auth.Identity.Services;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.FileUpload;
using Microsoft.Owin;
using Ninject.Modules;
using Ninject.Web.Common;
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
               .WhenInjectedInto(typeof(IAuthService))
               .InRequestScope();

            this.Bind<IFileUploadProvider>().To<FileUploadProvider>();
            this.Bind<IAccountManagementService>().To<AccountManagementService>().InRequestScope();
            this.Bind<ITripService>().To<TripService>().InRequestScope();
            this.Bind<ICityService>().To<CityService>().InRequestScope();
            this.Bind<ITagService>().To<TagService>().InRequestScope();
        }
    }
}