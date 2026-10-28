using System;
using System.Data;

namespace ProdControlWPF.Data
{
    public sealed class OperationLogRepository : IOperationLogRepository
    {
        private const string ClearSql = "DELETE FROM OperationLog";

        private readonly IDbConnectionFactory _connectionFactory;

        public OperationLogRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public int Clear()
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = ClearSql;
                return command.ExecuteNonQuery();
            }
        }
    }
}
