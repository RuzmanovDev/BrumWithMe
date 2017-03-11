using System;

namespace BrumWithMe.Data.Contracts
{
    public interface IUnitOfWorkEF : IDisposable
    {
        bool Commit();
    }
}
