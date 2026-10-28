using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class SearchBillRepository : ISearchBillRepository
    {
        private const string EmployeesSql = "SELECT EmployeeID, EmployeeName FROM Employee";

        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string OperationsSql = "SELECT OperationID, OperationName FROM Operation";

        private const string SelectLogSql =
            "SELECT LogID, EmployeeID, OperationID, ArticulID, DateExecution, TimeExecution, [Count], DocumentNumber, workDay " +
            "FROM OperationLog";

        private const string DeleteSql = "DELETE FROM OperationLog WHERE LogID = ?";

        private const string UpdateSql =
            "UPDATE OperationLog SET EmployeeID = ?, OperationID = ?, ArticulID = ?, DateExecution = ?, " +
            "TimeExecution = ?, [Count] = ?, DocumentNumber = ?, workDay = ? WHERE LogID = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public SearchBillRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<EmployeeLookup> GetEmployees()
        {
            var result = new List<EmployeeLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = EmployeesSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new EmployeeLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1)));
                    }
                }
            }

            return result;
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
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1)));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<OperationLookup> GetOperations()
        {
            var result = new List<OperationLookup>();

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
                        result.Add(new OperationLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            0));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<SearchBillRow> Search(SearchBillCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var conditions = new List<string>();

            if (criteria.FilterMode == SearchBillFilterMode.Employee
                || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
            {
                conditions.Add("EmployeeID = ?");
            }

            if (criteria.FilterMode == SearchBillFilterMode.Articul
                || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
            {
                conditions.Add("ArticulID = ?");
            }

            if (criteria.FilterMode == SearchBillFilterMode.DocumentNumber)
            {
                conditions.Add("DocumentNumber LIKE ?");
            }

            if (criteria.UsePeriod)
            {
                conditions.Add("DateExecution >= ?");
                conditions.Add("DateExecution <= ?");
            }

            var sql = conditions.Count == 0
                ? SelectLogSql
                : SelectLogSql + " WHERE " + string.Join(" AND ", conditions);

            return ReadRows(sql, command =>
            {
                if (criteria.FilterMode == SearchBillFilterMode.Employee
                    || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
                {
                    command.Parameters.Add("@employeeId", OleDbType.Integer).Value =
                        criteria.EmployeeId.GetValueOrDefault();
                }

                if (criteria.FilterMode == SearchBillFilterMode.Articul
                    || criteria.FilterMode == SearchBillFilterMode.EmployeeAndArticul)
                {
                    command.Parameters.Add("@articulId", OleDbType.Integer).Value =
                        criteria.ArticulId.GetValueOrDefault();
                }

                if (criteria.FilterMode == SearchBillFilterMode.DocumentNumber)
                {
                    command.Parameters.Add("@documentNumber", OleDbType.VarWChar).Value =
                        criteria.DocumentNumber ?? string.Empty;
                }

                if (criteria.UsePeriod)
                {
                    command.Parameters.Add("@dateFrom", OleDbType.Date).Value = criteria.DateFrom.Date;
                    command.Parameters.Add("@dateTo", OleDbType.Date).Value = criteria.DateTo.Date;
                }
            });
        }

        public IReadOnlyList<SearchBillRow> GetAll()
        {
            return ReadRows(SelectLogSql, null);
        }

        public void SaveChanges(IReadOnlyList<int> deleteLogIds, IReadOnlyList<SearchBillUpdate> updates)
        {
            if (deleteLogIds == null)
            {
                throw new ArgumentNullException(nameof(deleteLogIds));
            }

            if (updates == null)
            {
                throw new ArgumentNullException(nameof(updates));
            }

            if (deleteLogIds.Count == 0 && updates.Count == 0)
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
                        if (deleteLogIds.Count > 0)
                        {
                            DeleteRows(connection, transaction, deleteLogIds);
                        }

                        if (updates.Count > 0)
                        {
                            UpdateRows(connection, transaction, updates);
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

        private static void DeleteRows(OleDbConnection connection, OleDbTransaction transaction, IReadOnlyList<int> logIds)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@logId", OleDbType.Integer);

                foreach (var logId in logIds)
                {
                    command.Parameters[0].Value = logId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void UpdateRows(OleDbConnection connection, OleDbTransaction transaction, IReadOnlyList<SearchBillUpdate> updates)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = UpdateSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer);
                command.Parameters.Add("@operationId", OleDbType.Integer);
                command.Parameters.Add("@articulId", OleDbType.Integer);
                command.Parameters.Add("@dateExecution", OleDbType.Date);
                command.Parameters.Add("@timeExecution", OleDbType.Integer);
                command.Parameters.Add("@count", OleDbType.Integer);
                command.Parameters.Add("@documentNumber", OleDbType.VarWChar, 255);
                command.Parameters.Add("@workDay", OleDbType.Integer);
                command.Parameters.Add("@logId", OleDbType.Integer);

                foreach (var update in updates)
                {
                    command.Parameters[0].Value = update.EmployeeId;
                    command.Parameters[1].Value = update.OperationId;
                    command.Parameters[2].Value = update.ArticulId;
                    command.Parameters[3].Value = update.DateExecution.HasValue
                        ? (object)update.DateExecution.Value.Date
                        : DBNull.Value;
                    command.Parameters[4].Value = update.TimeExecution;
                    command.Parameters[5].Value = update.Count;
                    command.Parameters[6].Value = update.DocumentNumber == null
                        ? (object)DBNull.Value
                        : update.DocumentNumber;
                    command.Parameters[7].Value = update.WorkDay;
                    command.Parameters[8].Value = update.LogId;

                    command.ExecuteNonQuery();
                }
            }
        }

        private IReadOnlyList<SearchBillRow> ReadRows(string sql, Action<OleDbCommand> configureCommand)
        {
            var result = new List<SearchBillRow>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                configureCommand?.Invoke(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SearchBillRow
                        {
                            LogId = ReadInt32(reader, 0),
                            EmployeeId = ReadInt32(reader, 1),
                            OperationId = ReadInt32(reader, 2),
                            ArticulId = ReadInt32(reader, 3),
                            DateExecution = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4).Date,
                            TimeExecution = ReadInt32(reader, 5).ToString(CultureInfo.CurrentCulture),
                            Count = ReadInt32(reader, 6).ToString(CultureInfo.CurrentCulture),
                            DocumentNumber = reader.IsDBNull(7) ? null : reader.GetValue(7).ToString(),
                            WorkDay = ReadInt32(reader, 8).ToString(CultureInfo.CurrentCulture)
                        });
                    }
                }
            }

            return result;
        }

        private static int ReadInt32(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? 0
                : Convert.ToInt32(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }
    }
}
