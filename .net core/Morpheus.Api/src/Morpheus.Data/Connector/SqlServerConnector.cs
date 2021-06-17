using Morpheus.Infrastructure.Infrastructure.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Morpheus.Data.Connector
{
    /// <summary>
    /// Responsible for handling a SqlServer connection, processing database transactions and ensuring the consistency of the transaction.
    /// </summary>
    public class SqlServerConnector : IDbConnector
    {
        private bool _disposed;
        
        public SqlServerConnector(string connectionString)
        {
            Connection = SqlClientFactory.Instance.CreateConnection();
            Connection.ConnectionString = connectionString;
        }

        /// <summary>
        /// Database connection, through which all transactions will be committed to.
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// Database transaction associated with the connection.
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// Begins a transaction in the <see cref="Connection"/> source.
        /// It's useful to guarantee the consistency of all operations made within
        /// the transaction, enabling rolling back all scripts ran in case of error.
        /// </summary>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Transaction != null)
            {
                return Transaction;
            }

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            return (Transaction = Connection.BeginTransaction(isolationLevel));
        }

        /// <summary>
        /// Dispose the resources used along the lifecycle of this instance.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            
            Transaction?.Dispose();
            Connection?.Dispose();

            GC.SuppressFinalize(this);

            _disposed = true;
        }
    }
}
