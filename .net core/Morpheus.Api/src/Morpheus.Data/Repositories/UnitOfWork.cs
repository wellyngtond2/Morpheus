using Morpheus.Core.Repositories;
using Morpheus.Infrastructure.Infrastructure.Data;
using System.Data;

namespace Morpheus.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        IPersonRespository _personRespository;

        public UnitOfWork(IDbConnector dbConnector)
        {
            DbConnector = dbConnector;
        }

        public IDbConnector DbConnector { get; }


        public IPersonRespository PersonRespository => _personRespository ?? (_personRespository = new PersonRespository(DbConnector));

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            DbConnector.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            var connectionIsOpen = DbConnector?.Transaction?.Connection?.State == ConnectionState.Open;
            if (connectionIsOpen)
            {
                DbConnector.Transaction.Commit();
            }
        }

        public void Dispose()
        {
            DbConnector.Transaction?.Dispose();
            DbConnector.Connection?.Dispose();
        }

        public void RollbackTransaction()
        {

            var connectionIsOpen = DbConnector?.Transaction?.Connection?.State == ConnectionState.Open;
            if (connectionIsOpen)
            {
                DbConnector.Transaction.Rollback();
            }
        }
    }
}
