using System;
using System.Data.Entity;
using BrumWithMe.Data;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject;

namespace BrumWithMe.MVC.App_Start.Bindings
{
    public class DataConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<BrumWithMeDbContext>().InRequestScope();
            this.Bind(typeof(IRepositoryEf<>)).To(typeof(EfGenericRepository<>)).InRequestScope();
            this.Bind(typeof(IProjectableRepositoryEf<>)).To(typeof(ProjectableRepositoryEf<>)).InRequestScope();
            this.Bind<Func<IUnitOfWorkEF>>().ToMethod(ctx => () => ctx.Kernel.Get<EfUnitOfWork>()).InRequestScope();
        }
    }
}