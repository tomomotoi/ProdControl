using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class RoutingRepository : IRoutingRepository
    {
        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string OperationsSql =
            "SELECT OperationID, OperationName, PositionID FROM Operation";

        private const string CoefficientsSql =
            "SELECT coefficient_id, coefficient_value FROM Coefficients ORDER BY coefficient_value ASC";

        private const string RoutingSql =
            "SELECT RoutingID, ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel, Tariff " +
            "FROM Routing WHERE ArticulID = ? ORDER BY OperationID";

        private const string PositionRateTimeSql =
            "SELECT PositionLevel.PositionRateTime FROM PositionLevel " +
            "INNER JOIN Operation ON PositionLevel.PositionID = Operation.PositionID " +
            "WHERE Operation.OperationID = ? AND PositionLevel.PositionLevel = ?";

        private const string InsertSql =
            "INSERT INTO Routing (ArticulID, OperationID, DopCost, TimeExecute, RateExecute, PositionLevel, Tariff) " +
            "VALUES (?, ?, ?, ?, ?, ?, ?)";

        private const string IdentitySql = "SELECT @@IDENTITY";

        private const string UpdateSql =
            "UPDATE Routing SET DopCost = ?, TimeExecute = ?, RateExecute = ?, PositionLevel = ?, Tariff = ? " +
            "WHERE RoutingID = ? AND ArticulID = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public RoutingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<ArticulLookup> GetArticuls()
        {
            var result = new List<ArticulLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = ArticulsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ArticulLookup(
                            Convert.ToInt32(reader["ArticulID"]),
                            reader["ArticulName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["ArticulName"])));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<OperationItem> GetOperations()
        {
            var result = new List<OperationItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = OperationsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new OperationItem
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

            return result;
        }

        public IReadOnlyList<CoefficientLookup> GetCoefficients()
        {
            var result = new List<CoefficientLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = CoefficientsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CoefficientLookup(
                            Convert.ToInt32(reader["coefficient_id"]),
                            ReadDouble(reader, "coefficient_value")));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<RoutingEntry> GetRouting(int articulId)
        {
            var result = new List<RoutingEntry>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = RoutingSql;
                command.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new RoutingEntry
                        {
                            RoutingId = Convert.ToInt32(reader["RoutingID"]),
                            ArticulId = reader["ArticulID"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["ArticulID"]),
                            OperationId = reader["OperationID"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["OperationID"]),
                            DopCost = ReadDouble(reader, "DopCost"),
                            TimeExecute = ReadDouble(reader, "TimeExecute"),
                            RateExecute = ReadDouble(reader, "RateExecute"),
                            PositionLevel = reader["PositionLevel"] is DBNull
                                ? 0
                                : Convert.ToInt32(reader["PositionLevel"]),
                            Tariff = ReadDouble(reader, "Tariff")
                        });
                    }
                }
            }

            return result;
        }

        public double? GetPositionRateTime(int operationId, int positionLevel)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = PositionRateTimeSql;
                command.Parameters.Add("@operationId", OleDbType.Integer).Value = operationId;
                command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = positionLevel;

                double? result = null;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = reader["PositionRateTime"] is DBNull
                            ? (double?)null
                            : Convert.ToDouble(reader["PositionRateTime"], CultureInfo.InvariantCulture);
                    }
                }

                return result;
            }
        }

        public void Save(IList<RoutingEntry> inserts, IList<RoutingEntry> updates)
        {
            if ((inserts == null || inserts.Count == 0) && (updates == null || updates.Count == 0))
            {
                return;
            }

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (inserts != null)
                        {
                            foreach (var entry in inserts)
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = InsertSql;

                                    AddInsertParameters(command, entry);
                                    command.ExecuteNonQuery();
                                }

                                using (var identityCommand = connection.CreateCommand())
                                {
                                    identityCommand.Transaction = transaction;
                                    identityCommand.CommandType = CommandType.Text;
                                    identityCommand.CommandText = IdentitySql;
                                    entry.RoutingId = Convert.ToInt32(identityCommand.ExecuteScalar());
                                }
                            }
                        }

                        if (updates != null)
                        {
                            foreach (var entry in updates)
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = UpdateSql;

                                    AddUpdateParameters(command, entry);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                        }

                        throw;
                    }
                }
            }
        }

        private static void AddInsertParameters(OleDbCommand command, RoutingEntry entry)
        {
            command.Parameters.Add("@articulId", OleDbType.Integer).Value = entry.ArticulId;
            command.Parameters.Add("@operationId", OleDbType.Integer).Value = entry.OperationId;
            command.Parameters.Add("@dopCost", OleDbType.Double).Value = entry.DopCost;
            command.Parameters.Add("@timeExecute", OleDbType.Double).Value = entry.TimeExecute;
            command.Parameters.Add("@rateExecute", OleDbType.Double).Value = entry.RateExecute;
            command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = entry.PositionLevel;
            command.Parameters.Add("@tariff", OleDbType.Double).Value = entry.Tariff;
        }

        private static void AddUpdateParameters(OleDbCommand command, RoutingEntry entry)
        {
            command.Parameters.Add("@dopCost", OleDbType.Double).Value = entry.DopCost;
            command.Parameters.Add("@timeExecute", OleDbType.Double).Value = entry.TimeExecute;
            command.Parameters.Add("@rateExecute", OleDbType.Double).Value = entry.RateExecute;
            command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = entry.PositionLevel;
            command.Parameters.Add("@tariff", OleDbType.Double).Value = entry.Tariff;
            command.Parameters.Add("@routingId", OleDbType.Integer).Value = entry.RoutingId;
            command.Parameters.Add("@articulId", OleDbType.Integer).Value = entry.ArticulId;
        }

        private static double ReadDouble(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull
                ? 0
                : Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }
    }
}
