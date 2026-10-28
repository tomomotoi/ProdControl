using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class TariffsCheckRepository : ITariffsCheckRepository
    {
        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string MissingTariffsSql =
            "SELECT Routing.RoutingID, Routing.ArticulID, Routing.OperationID, Routing.TimeExecute, " +
            "PositionLevel.PositionRateTime " +
            "FROM (Routing INNER JOIN Operation ON Routing.OperationID = Operation.OperationID) " +
            "INNER JOIN PositionLevel ON Routing.PositionLevel = PositionLevel.[PositionLevel] " +
            "AND Operation.PositionID = PositionLevel.PositionID " +
            "WHERE Routing.Tariff = 0 ORDER BY Routing.ArticulID, Routing.RoutingID";

        private const string UpdateSql =
            "UPDATE Routing SET Tariff = ? WHERE ArticulID = ? AND OperationID = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public TariffsCheckRepository(IDbConnectionFactory connectionFactory)
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

        public IReadOnlyList<TariffCandidate> GetMissingTariffs()
        {
            var result = new List<TariffCandidate>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = MissingTariffsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new TariffCandidate
                        {
                            RoutingId = Convert.ToInt32(reader["RoutingID"]),
                            ArticulId = Convert.ToInt32(reader["ArticulID"]),
                            OperationId = Convert.ToInt32(reader["OperationID"]),
                            TimeExecute = ReadDouble(reader, "TimeExecute"),
                            PositionRateTime = ReadDouble(reader, "PositionRateTime")
                        });
                    }
                }
            }

            return result;
        }

        public void SaveTariffs(IList<TariffUpdate> updates)
        {
            if (updates == null || updates.Count == 0)
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
                        foreach (var update in updates)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.Transaction = transaction;
                                command.CommandType = CommandType.Text;
                                command.CommandText = UpdateSql;
                                command.Parameters.Add("@tariff", OleDbType.Double).Value = update.Tariff;
                                command.Parameters.Add("@articulId", OleDbType.Integer).Value = update.ArticulId;
                                command.Parameters.Add("@operationId", OleDbType.Integer).Value = update.OperationId;
                                command.ExecuteNonQuery();
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

        private static double ReadDouble(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull
                ? 0
                : Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }
    }
}
