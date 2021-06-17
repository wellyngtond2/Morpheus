using System;
using System.Data;

namespace Morpheus.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRespository PersonRespository { get; }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        void CommitTransaction();

        void RollbackTransaction();
    }
}