using System;

namespace BrumWithMe.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
