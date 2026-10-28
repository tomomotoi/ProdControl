using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class NewBillRepository : INewBillRepository
    {
        private const string EmployeesSql = "SELECT EmployeeID, EmployeeName FROM Employee";

        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string IdleOperationsSql =
            "SELECT idle_operation_id, idle_operation_name, cost_percentage FROM IdleOperation";

        private const string OperationsByArticulSql =
            "SELECT OperationID, OperationName, OperationTimewithKoef FROM Operation " +
            "WHERE OperationID IN (SELECT OperationID FROM Routing WHERE ArticulID = ?) " +
            "ORDER BY OperationID";

        private const string IdlePercentageSql =
            "SELECT cost_percentage FROM IdleOperation WHERE idle_operation_id = ?";

        private const string InsertSql =
            "INSERT INTO OperationLog (EmployeeID, OperationID, ArticulID, DateExecution, TimeExecution, [Count], DocumentNumber, workDay) " +
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

        private readonly IDbConnectionFactory _connectionFactory;

        public NewBillRepository(IDbConnectionFactory connectionFactory)
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

        public IReadOnlyList<IdleOperationLookup> GetIdleOperations()
        {
            var result = new List<IdleOperationLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = IdleOperationsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var percentage = reader.IsDBNull(2)
                            ? 0d
                            : Convert.ToDouble(reader.GetValue(2), CultureInfo.InvariantCulture);

                        result.Add(new IdleOperationLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            percentage));
                    }
                }
            }

            return result;
        }

        public IReadOnlyList<OperationLookup> GetOperationsByArticul(int articulId)
        {
            var result = new List<OperationLookup>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = OperationsByArticulSql;
                command.Parameters.Add("@articulId", OleDbType.Integer).Value = articulId;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new OperationLookup(
                            reader.GetInt32(0),
                            reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2), CultureInfo.InvariantCulture)));
                    }
                }
            }

            return result;
        }

        public double GetIdlePercentage(int idleOperationId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = IdlePercentageSql;
                command.Parameters.Add("@idleOperationId", OleDbType.Integer).Value = idleOperationId;

                var value = command.ExecuteScalar();

                return value == null || value == DBNull.Value
                    ? 0d
                    : Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        public void SaveOperationLog(IReadOnlyList<OperationLogEntry> entries)
        {
            if (entries == null)
            {
                throw new ArgumentNullException(nameof(entries));
            }

            if (entries.Count == 0)
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
                    command.CommandText = InsertSql;

                    command.Parameters.Add("@employeeId", OleDbType.Integer);
                    command.Parameters.Add("@operationId", OleDbType.Integer);
                    command.Parameters.Add("@articulId", OleDbType.Integer);
                    command.Parameters.Add("@dateExecution", OleDbType.Date);
                    command.Parameters.Add("@timeExecution", OleDbType.Integer);
                    command.Parameters.Add("@count", OleDbType.Integer);
                    command.Parameters.Add("@documentNumber", OleDbType.VarWChar, 255);
                    command.Parameters.Add("@workDay", OleDbType.Integer);

                    try
                    {
                        foreach (var entry in entries)
                        {
                            command.Parameters[0].Value = entry.EmployeeId;
                            command.Parameters[1].Value = entry.OperationId;
                            command.Parameters[2].Value = entry.ArticulId;
                            command.Parameters[3].Value = entry.DateExecution.Date;
                            command.Parameters[4].Value = entry.ExecutionTime;
                            command.Parameters[5].Value = entry.Count;
                            command.Parameters[6].Value = entry.DocumentNumber ?? string.Empty;
                            command.Parameters[7].Value = entry.WorkDay;

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
    }
}
