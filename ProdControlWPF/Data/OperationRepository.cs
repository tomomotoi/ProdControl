using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class OperationRepository : IOperationRepository
    {
        private const string SelectOperationsSql =
            "SELECT [OperationID], [OperationName], [PositionID] FROM [Operation]";

        private const string SelectPositionsSql =
            "SELECT [PositionID], [PositionName] FROM [Position]";

        private const string ExistsSql =
            "SELECT COUNT(*) FROM [Operation] WHERE [OperationName] LIKE ?";

        private const string InsertSql =
            "INSERT INTO [Operation] " +
            "([OperationName], [OperationKoefAddCost], [OperationTimewithKoef], [OpetationRate], [PositionID]) " +
            "VALUES (?, 0, 0, 0, ?)";

        private const string UsedSql =
            "SELECT COUNT(*) FROM [Routing] WHERE [OperationID] = ?";

        private const string DeleteSql =
            "DELETE FROM [Operation] WHERE [OperationID] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public OperationRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IList<OperationItem> GetOperations()
        {
            var operations = new List<OperationItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SelectOperationsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        operations.Add(new OperationItem
                        {
                            Id = Convert.ToInt32(reader["OperationID"]),
                            Name = reader["OperationName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["OperationName"]),
                            PositionId = reader["PositionID"] is DBNull
                                ? (int?)null
                                : Convert.ToInt32(reader["PositionID"])
                        });
                    }
                }
            }

            return operations;
        }

        public IList<PositionItem> GetPositions()
        {
            var positions = new List<PositionItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SelectPositionsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        positions.Add(new PositionItem
                        {
                            Id = Convert.ToInt32(reader["PositionID"]),
                            Name = reader["PositionName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["PositionName"])
                        });
                    }
                }
            }

            return positions;
        }

        public bool OperationNameExists(string operationName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = ExistsSql;
                command.Parameters.Add("@operationName", OleDbType.VarWChar, 255).Value =
                    operationName ?? string.Empty;

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public int InsertOperation(string operationName, int positionId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;

                command.Parameters.Add("@operationName", OleDbType.VarWChar, 255).Value =
                    operationName ?? string.Empty;
                command.Parameters.Add("@positionId", OleDbType.Integer).Value = positionId;

                return command.ExecuteNonQuery();
            }
        }

        public bool IsOperationUsed(int operationId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = UsedSql;
                command.Parameters.Add("@operationId", OleDbType.Integer).Value = operationId;

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public int DeleteOperation(int operationId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@operationId", OleDbType.Integer).Value = operationId;

                return command.ExecuteNonQuery();
            }
        }
    }
}
