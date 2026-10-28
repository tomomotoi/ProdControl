using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class KnittingSearchRepository : IKnittingSearchRepository
    {
        private const string EmployeesSql = "SELECT EmployeeID, EmployeeName FROM Employee";

        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string PartsSql = "SELECT id, part_name FROM ClothingParts";

        private const string MachinesSql =
            "SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines";

        private const string SelectSql =
            "SELECT id, document_number, article_id, employee_id, part_id, amount, machine_id, " +
            "date_execution, time_execution, machines_in_zone, work_day FROM KnittingOperationLog";

        private const string OrderSql = " ORDER BY date_execution DESC, id DESC";

        private const string DeleteSql = "DELETE FROM KnittingOperationLog WHERE id = ?";

        private const string UpdateSql =
            "UPDATE [KnittingOperationLog] SET [document_number] = ?, [article_id] = ?, " +
            "[employee_id] = ?, [part_id] = ?, [amount] = ?, [machine_id] = ?, " +
            "[date_execution] = ?, [time_execution] = ?, [machines_in_zone] = ?, " +
            "[work_day] = ? WHERE [id] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public KnittingSearchRepository(IDbConnectionFactory connectionFactory)
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

        public IReadOnlyList<ClothingPartLookup> GetParts()
        {
            var result = new List<ClothingPartLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = PartsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ClothingPartLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1)));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<KnittingMachineLookup> GetMachines()
        {
            var result = new List<KnittingMachineLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = MachinesSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KnittingMachineLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1)));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<KnittingSearchRow> Search(KnittingSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var conditions = new List<string>();

            switch (criteria.FilterMode)
            {
                case KnittingSearchFilterMode.Employee:
                    if (criteria.EmployeeId.HasValue)
                    {
                        conditions.Add("employee_id = ?");
                    }

                    break;

                case KnittingSearchFilterMode.Articul:
                    if (criteria.ArticulId.HasValue)
                    {
                        conditions.Add("article_id = ?");
                    }

                    break;

                case KnittingSearchFilterMode.EmployeeAndArticul:
                    if (criteria.EmployeeId.HasValue && criteria.ArticulId.HasValue)
                    {
                        conditions.Add("employee_id = ?");
                        conditions.Add("article_id = ?");
                    }

                    break;

                case KnittingSearchFilterMode.DocumentNumber:
                    if (!string.IsNullOrWhiteSpace(criteria.DocumentNumber))
                    {
                        conditions.Add("document_number LIKE ?");
                    }

                    break;
            }

            if (criteria.UsePeriod)
            {
                conditions.Add("date_execution >= ?");
                conditions.Add("date_execution <= ?");
            }

            var sql = SelectSql
                + (conditions.Count == 0 ? string.Empty : " WHERE " + string.Join(" AND ", conditions))
                + OrderSql;

            return ReadRows(sql, command =>
            {
                switch (criteria.FilterMode)
                {
                    case KnittingSearchFilterMode.Employee:
                        if (criteria.EmployeeId.HasValue)
                        {
                            command.Parameters.Add("@employeeId", OleDbType.Integer).Value =
                                criteria.EmployeeId.Value;
                        }

                        break;

                    case KnittingSearchFilterMode.Articul:
                        if (criteria.ArticulId.HasValue)
                        {
                            command.Parameters.Add("@articulId", OleDbType.Integer).Value =
                                criteria.ArticulId.Value;
                        }

                        break;

                    case KnittingSearchFilterMode.EmployeeAndArticul:
                        if (criteria.EmployeeId.HasValue && criteria.ArticulId.HasValue)
                        {
                            command.Parameters.Add("@employeeId", OleDbType.Integer).Value =
                                criteria.EmployeeId.Value;
                            command.Parameters.Add("@articulId", OleDbType.Integer).Value =
                                criteria.ArticulId.Value;
                        }

                        break;

                    case KnittingSearchFilterMode.DocumentNumber:
                        if (!string.IsNullOrWhiteSpace(criteria.DocumentNumber))
                        {
                            command.Parameters.Add("@documentNumber", OleDbType.VarWChar).Value =
                                "%" + criteria.DocumentNumber.Trim() + "%";
                        }

                        break;
                }

                if (criteria.UsePeriod)
                {
                    command.Parameters.Add("@dateFrom", OleDbType.Date).Value = criteria.DateFrom.Date;
                    command.Parameters.Add("@dateTo", OleDbType.Date).Value =
                        criteria.DateTo.Date.AddDays(1).AddSeconds(-1);
                }
            });
        }

        public IReadOnlyList<KnittingSearchRow> GetAll()
        {
            return ReadRows(SelectSql + OrderSql, null);
        }

        public void Delete(int logId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@id", OleDbType.Integer).Value = logId;

                command.ExecuteNonQuery();
            }
        }

        public void SaveUpdates(IReadOnlyList<KnittingSearchUpdate> updates)
        {
            if (updates == null)
            {
                throw new ArgumentNullException(nameof(updates));
            }

            if (updates.Count == 0)
            {
                return;
            }

            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = UpdateSql;

                    command.Parameters.Add("@documentNumber", OleDbType.VarWChar, 255);
                    command.Parameters.Add("@articulId", OleDbType.Integer);
                    command.Parameters.Add("@employeeId", OleDbType.Integer);
                    command.Parameters.Add("@partId", OleDbType.Integer);
                    command.Parameters.Add("@amount", OleDbType.Integer);
                    command.Parameters.Add("@machineId", OleDbType.Integer);
                    command.Parameters.Add("@dateExecution", OleDbType.Date);
                    command.Parameters.Add("@timeExecution", OleDbType.Double);
                    command.Parameters.Add("@machinesInZone", OleDbType.Integer);
                    command.Parameters.Add("@workDay", OleDbType.Integer);
                    command.Parameters.Add("@id", OleDbType.Integer);

                    try
                    {
                        foreach (var update in updates)
                        {
                            command.Parameters[0].Value = string.IsNullOrEmpty(update.DocumentNumber)
                                ? (object)DBNull.Value
                                : update.DocumentNumber;
                            command.Parameters[1].Value = ToDbValue(update.ArticulId);
                            command.Parameters[2].Value = ToDbValue(update.EmployeeId);
                            command.Parameters[3].Value = ToDbValue(update.PartId);
                            command.Parameters[4].Value = update.Amount;
                            command.Parameters[5].Value = ToDbValue(update.MachineId);
                            command.Parameters[6].Value = update.DateExecution.HasValue
                                ? (object)update.DateExecution.Value.Date
                                : DBNull.Value;
                            command.Parameters[7].Value = update.TimeExecution.HasValue
                                ? (object)update.TimeExecution.Value
                                : DBNull.Value;
                            command.Parameters[8].Value = update.MachinesInZone;
                            command.Parameters[9].Value = update.WorkDay;
                            command.Parameters[10].Value = update.LogId;

                            command.ExecuteNonQuery();
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

        private static object ToDbValue(int? value)
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }

        private IReadOnlyList<KnittingSearchRow> ReadRows(string sql, Action<OleDbCommand> configureCommand)
        {
            var result = new List<KnittingSearchRow>();

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
                        result.Add(new KnittingSearchRow
                        {
                            LogId = ReadInt32(reader, 0),
                            DocumentNumber = reader.IsDBNull(1) ? string.Empty : reader.GetValue(1).ToString(),
                            ArticulId = ReadNullableInt32(reader, 2),
                            EmployeeId = ReadNullableInt32(reader, 3),
                            PartId = ReadNullableInt32(reader, 4),
                            Count = ReadInt32(reader, 5).ToString(CultureInfo.CurrentCulture),
                            MachineId = ReadNullableInt32(reader, 6),
                            DateExecution = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                            TimeExecution = ReadDouble(reader, 8).ToString(CultureInfo.CurrentCulture),
                            MachinesInZone = ReadInt32(reader, 9).ToString(CultureInfo.CurrentCulture),
                            WorkDay = ReadInt32(reader, 10).ToString(CultureInfo.CurrentCulture)
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

        private static int? ReadNullableInt32(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? (int?)null
                : Convert.ToInt32(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }

        private static double ReadDouble(OleDbDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal)
                ? 0d
                : Convert.ToDouble(reader.GetValue(ordinal), CultureInfo.InvariantCulture);
        }
    }
}
