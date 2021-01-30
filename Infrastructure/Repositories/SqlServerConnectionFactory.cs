using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Connection factory to create <see cref="SqlServerConnectionFactory"/> objects.
    /// </summary>
    /// <seealso cref="IConnectionFactory" />
    public class SqlServerConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerConnectionFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public SqlServerConnectionFactory(string connectionString)
        {
            ConnectionString = !string.IsNullOrWhiteSpace(connectionString)
                ? connectionString
                : throw new ArgumentNullException(nameof(connectionString));
        }

        /// <inheritdoc />
        public string ConnectionString { get; }

        /// <inheritdoc />
        public IDbConnection CreateConnection() => new SqlConnection(ConnectionString);
    }
}
