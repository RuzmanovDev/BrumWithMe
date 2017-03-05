using System.Data.Entity;
using BrumWithMe.Data.Contracts;
using Bytes2you.Validation;

namespace BrumWithMe.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public UnitOfWork(DbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();

            this.context = context;
        }

        public bool Commit()
        {
            return this.context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            // Let ninject do it 
        }
    }
}
