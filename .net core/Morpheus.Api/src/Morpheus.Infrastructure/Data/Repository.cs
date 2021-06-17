using System.Data;

namespace Morpheus.Infrastructure.Infrastructure.Data
{
    public abstract class Repository
    {
        private readonly IDbConnector _dbConnector;

        protected Repository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        protected IDbConnection Connection => _dbConnector.Connection;

        protected IDbTransaction Transaction => _dbConnector.Transaction;
    }
}
