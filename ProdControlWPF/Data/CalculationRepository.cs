using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class CalculationRepository : ICalculationRepository
    {
        private const string EmployeesSql =
            "SELECT EmployeeID, EmployeeName, EmployeeNumber, PositionID, PositionLevelID " +
            "FROM Employee ORDER BY EmployeeID";

        private const string PositionSql =
            "SELECT PositionID FROM Employee WHERE EmployeeID = ?";

        private const string KnittingZonesSql =
            "SELECT machines_in_zone, SUM(time_execution) AS zone_time " +
            "FROM KnittingOperationLog WHERE employee_id = ?";

        private const string SewingSalarySql =
            "SELECT OperationLog.[Count], Routing.Tariff, OperationLog.TimeExecution, OperationLog.ArticulID " +
            "FROM OperationLog INNER JOIN Routing " +
            "ON OperationLog.ArticulID = Routing.ArticulID " +
            "AND OperationLog.OperationID = Routing.OperationID " +
            "WHERE OperationLog.EmployeeID = ?";

        private const string SewingTimeSql =
            "SELECT OperationID, ArticulID, TimeExecution, [Count] " +
            "FROM OperationLog WHERE EmployeeID = ?";

        private const string RoutingTimeSql =
            "SELECT TimeExecute FROM Routing WHERE ArticulID = ? AND OperationID = ?";

        private const string TariffPerMinuteSql =
            "SELECT PositionRateTime FROM PositionLevel " +
            "WHERE PositionID = (SELECT PositionID FROM Employee WHERE EmployeeID = ?) " +
            "AND PositionLevel.PositionLevel = " +
            "(SELECT Employee.PositionLevelID FROM Employee WHERE EmployeeID = ?)";

        private const string PeriodWorkDaysSql =
            "SELECT workDay, DateExecution FROM OperationLog " +
            "WHERE EmployeeID = ? AND DateExecution BETWEEN ? AND ? " +
            "GROUP BY workDay, DateExecution";

        private const string WorkDayForDateSql =
            "SELECT workDay FROM OperationLog " +
            "WHERE EmployeeID = ? AND DateExecution = ? " +
            "GROUP BY DateExecution, workDay";

        private readonly IDbConnectionFactory _connectionFactory;

        public CalculationRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<CalculationRow> GetEmployees()
        {
            var result = new List<CalculationRow>();

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
                        result.Add(new CalculationRow
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeID"]),
                            EmployeeName = ReadString(reader, "EmployeeName"),
                            EmployeeNumber = ReadString(reader, "EmployeeNumber"),
                            PositionId = ReadInt32(reader, "PositionID"),
                            PositionLevelId = ReadInt32(reader, "PositionLevelID")
                        });
                    }
                }
            }

            return result;
        }

        public int GetPositionId(int employeeId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = PositionSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;

                var value = command.ExecuteScalar();

                return value == null || value is DBNull ? 0 : Convert.ToInt32(value);
            }
        }

        public IReadOnlyList<KnittingZoneTime> GetKnittingZoneTimes(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo)
        {
            var result = new List<KnittingZoneTime>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = KnittingZonesSql
                    + BuildDateCondition(usePeriod, "date_execution")
                    + " GROUP BY machines_in_zone";
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                AddDateParameters(command, usePeriod, dateFrom, dateTo);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new KnittingZoneTime
                        {
                            MachinesInZone = ReadInt32(reader, "machines_in_zone"),
                            TotalTime = ReadDouble(reader, "zone_time")
                        });
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<SewingSalaryEntry> GetSewingSalaryEntries(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo)
        {
            var result = new List<SewingSalaryEntry>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SewingSalarySql + BuildDateCondition(usePeriod, "DateExecution");
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                AddDateParameters(command, usePeriod, dateFrom, dateTo);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SewingSalaryEntry
                        {
                            Count = ReadDouble(reader, "Count"),
                            Tariff = ReadDouble(reader, "Tariff"),
                            TimeExecution = ReadDouble(reader, "TimeExecution"),
                            ArticulId = ReadInt32(reader, "ArticulID")
                        });
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<SewingTimeEntry> GetSewingTimeEntries(
            int employeeId,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo)
        {
            var result = new List<SewingTimeEntry>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SewingTimeSql + BuildDateCondition(usePeriod, "DateExecution");
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                AddDateParameters(command, usePeriod, dateFrom, dateTo);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SewingTimeEntry
                        {
                            OperationId = ReadInt32(reader, "OperationID"),
                            ArticulId = ReadInt32(reader, "ArticulID"),
                            TimeExecution = ReadDouble(reader, "TimeExecution"),
                            Count = ReadDouble(reader, "Count")
                        });
                    }
                }
            }

            return result;
        }

        public double? GetRoutingTime(int articulId, int operationId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = RoutingTimeSql;
                command.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;
                command.Parameters.Add("@operationId", OleDbType.Integer).Value = operationId;

                var value = command.ExecuteScalar();

                return value == null || value is DBNull
                    ? (double?)null
                    : Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        public double GetTariffPerMinute(int employeeId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = TariffPerMinuteSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                command.Parameters.Add("@employeeId2", OleDbType.Integer).Value = employeeId;

                var value = command.ExecuteScalar();

                return value == null || value is DBNull
                    ? 0
                    : Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        public IReadOnlyList<WorkDayEntry> GetPeriodWorkDays(
            int employeeId,
            DateTime dateFrom,
            DateTime dateTo)
        {
            var result = new List<WorkDayEntry>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = PeriodWorkDaysSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                command.Parameters.Add("@dateFrom", OleDbType.Date).Value = dateFrom.Date;
                command.Parameters.Add("@dateTo", OleDbType.Date).Value = dateTo.Date;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["DateExecution"] is DBNull)
                        {
                            continue;
                        }

                        result.Add(new WorkDayEntry
                        {
                            WorkDay = ReadInt32(reader, "workDay"),
                            DateExecution = Convert.ToDateTime(reader["DateExecution"]).Date
                        });
                    }
                }
            }

            return result;
        }

        public int? GetWorkDayForDate(int employeeId, DateTime date)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = WorkDayForDateSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;
                command.Parameters.Add("@date", OleDbType.Date).Value = date.Date;

                var value = command.ExecuteScalar();

                return value == null || value is DBNull
                    ? (int?)null
                    : Convert.ToInt32(value);
            }
        }

        private static string BuildDateCondition(bool usePeriod, string columnName)
        {
            return usePeriod
                ? " AND (" + columnName + " BETWEEN ? AND ?)"
                : " AND " + columnName + " = ?";
        }

        private static void AddDateParameters(
            OleDbCommand command,
            bool usePeriod,
            DateTime dateFrom,
            DateTime dateTo)
        {
            if (usePeriod)
            {
                command.Parameters.Add("@dateFrom", OleDbType.Date).Value = dateFrom.Date;
                command.Parameters.Add("@dateTo", OleDbType.Date).Value = dateTo.Date;
                return;
            }

            command.Parameters.Add("@date", OleDbType.Date).Value = dateFrom.Date;
        }

        private static string ReadString(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull ? string.Empty : Convert.ToString(value);
        }

        private static int ReadInt32(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull ? 0 : Convert.ToInt32(value);
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
