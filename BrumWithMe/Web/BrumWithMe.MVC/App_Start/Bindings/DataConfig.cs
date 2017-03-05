using BrumWithMe.Data;
using BrumWithMe.Data.Contracts;
using BrumWithMe.Data.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Ninject.Web.Common;
using Ninject;

namespace BrumWithMe.MVC.App_Start.Bindings
{
    public class DataConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<BrumWithMeDbContext>().InRequestScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>)).InRequestScope();
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<UnitOfWork>()).InRequestScope();
        }
    }
}