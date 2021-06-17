using System;
using System.Data;

namespace Morpheus.Infrastructure.Infrastructure.Data
{
    public interface IDbConnector : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTransaction(IsolationLevel isolation);
    }
}
