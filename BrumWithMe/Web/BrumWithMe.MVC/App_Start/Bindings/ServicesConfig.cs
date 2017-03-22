using System.Web;
using AutoMapper;
using BrumWithMe.Auth.Identity.Contracts;
using BrumWithMe.Auth.Identity.Services;
using BrumWithMe.Services.Data.Contracts;
using BrumWithMe.Services.Data.Services;
using BrumWithMe.Services.Providers.Mapping;
using BrumWithMe.Services.Providers.Mapping.Contracts;
using BrumWithMe.Services.Providers.TimeProviders;
using Microsoft.Owin;
using Ninject.Modules;
using Ninject.Web.Common;

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

            this.Bind<IMapper>()
                .ToMethod(c => MappingProfile.InitializeAutoMapper().CreateMapper());

            this.Bind<IMappingProvider>().To<MappingProvider>().InRequestScope();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>().InRequestScope();
            this.Bind<IAccountManagementService>().To<AccountManagementService>().InRequestScope();
            this.Bind<ITripService>().To<TripService>().InRequestScope();
            this.Bind<ICarService>().To<CarService>().InRequestScope();
            this.Bind<ICityService>().To<CityService>().InRequestScope();
            this.Bind<ITagService>().To<TagService>().InRequestScope();
            this.Bind<IReviewService>().To<ReviewService>().InRequestScope();
            this.Bind<IReportService>().To<ReportService>().InRequestScope();
            this.Bind<IUserDashboardService>().To<UserDashboardService>().InRequestScope();
        }
    }
}