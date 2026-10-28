using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class ReportRepository : IReportRepository
    {
        private const string EmployeesSql =
            "SELECT EmployeeID, EmployeeName, EmployeeNumber FROM Employee";

        private const string EmployeeReportSql =
            "SELECT EmployeeID, EmployeeName, DateExecution, DocumentNumber, OperationName, " +
            "ArticulName, RateTime, NotRate, [Count] FROM ReportEmployee " +
            "WHERE EmployeeID IN ({0}) AND DateExecution BETWEEN ? AND ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public ReportRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<ReportEmployeeListItem> GetEmployees()
        {
            var result = new List<ReportEmployeeListItem>();

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
                        result.Add(new ReportEmployeeListItem
                        {
                            EmployeeId = ReadInt32(reader, "EmployeeID"),
                            EmployeeName = ReadString(reader, "EmployeeName"),
                            EmployeeNumber = ReadString(reader, "EmployeeNumber")
                        });
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<EmployeeReportRow> GetEmployeeReport(
            IReadOnlyList<int> employeeIds, DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<EmployeeReportRow>();

            if (employeeIds == null || employeeIds.Count == 0)
            {
                return result;
            }

            var ids = new List<int>();
            foreach (var employeeId in employeeIds)
            {
                if (!ids.Contains(employeeId))
                {
                    ids.Add(employeeId);
                }
            }

            var placeholders = new StringBuilder();
            for (var index = 0; index < ids.Count; index++)
            {
                if (index > 0)
                {
                    placeholders.Append(", ");
                }

                placeholders.Append('?');
            }

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = string.Format(EmployeeReportSql, placeholders);

                for (var index = 0; index < ids.Count; index++)
                {
                    command.Parameters.Add("@employeeId" + index, OleDbType.Integer).Value = ids[index];
                }

                command.Parameters.Add("@dateFrom", OleDbType.Date).Value = dateFrom.Date;
                command.Parameters.Add("@dateTo", OleDbType.Date).Value = dateTo.Date;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new EmployeeReportRow
                        {
                            EmployeeName = ReadString(reader, "EmployeeName"),
                            DateExecution = ReadDateTime(reader, "DateExecution"),
                            DocumentNumber = ReadString(reader, "DocumentNumber"),
                            OperationName = ReadString(reader, "OperationName"),
                            ArticulName = ReadString(reader, "ArticulName"),
                            Count = ReadInt32(reader, "Count"),
                            EmployeeId = ReadInt32(reader, "EmployeeID"),
                            RateTime = ReadDouble(reader, "RateTime"),
                            NotRate = ReadInt32(reader, "NotRate")
                        });
                    }
                }
            }

            return result;
        }

        private static string ReadString(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull ? string.Empty : Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        private static int ReadInt32(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull ? 0 : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private static double ReadDouble(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull
                ? 0
                : Convert.ToDouble(value, CultureInfo.InvariantCulture);
        }

        private static DateTime ReadDateTime(OleDbDataReader reader, string columnName)
        {
            var value = reader[columnName];

            return value is DBNull ? DateTime.MinValue : Convert.ToDateTime(value, CultureInfo.InvariantCulture);
        }
    }
}
