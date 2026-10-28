using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class KnittingInvoiceRepository : IKnittingInvoiceRepository
    {
        private const string EmployeesSql = "SELECT EmployeeID, EmployeeName FROM Employee";

        private const string ArticulsSql = "SELECT ArticulID, ArticulName FROM Articul";

        private const string PartsSql = "SELECT id, part_name FROM ClothingParts";

        private const string MachinesSql =
            "SELECT id, machine_make + ' ' + machine_model AS machine_displayname FROM KnittingMachines";

        private const string InsertSql =
            "INSERT INTO [KnittingOperationLog] " +
            "([document_number], [article_id], [employee_id], [part_id], [machine_id], [amount], " +
            "[date_execution], [time_execution], [machines_in_zone], [work_day]) " +
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

        private readonly IDbConnectionFactory _connectionFactory;

        public KnittingInvoiceRepository(IDbConnectionFactory connectionFactory)
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

        public void SaveOperationLog(IReadOnlyList<KnittingOperationLogEntry> entries)
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

                    command.Parameters.Add("@documentNumber", OleDbType.VarWChar, 255);
                    command.Parameters.Add("@articulId", OleDbType.Integer);
                    command.Parameters.Add("@employeeId", OleDbType.Integer);
                    command.Parameters.Add("@partId", OleDbType.Integer);
                    command.Parameters.Add("@machineId", OleDbType.Integer);
                    command.Parameters.Add("@amount", OleDbType.Integer);
                    command.Parameters.Add("@dateExecution", OleDbType.Date);
                    command.Parameters.Add("@timeExecution", OleDbType.Double);
                    command.Parameters.Add("@machinesInZone", OleDbType.Integer);
                    command.Parameters.Add("@workDay", OleDbType.Integer);

                    try
                    {
                        foreach (var entry in entries)
                        {
                            command.Parameters[0].Value = entry.DocumentNumber ?? string.Empty;
                            command.Parameters[1].Value = entry.ArticulId;
                            command.Parameters[2].Value = entry.EmployeeId;
                            command.Parameters[3].Value = entry.PartId;
                            command.Parameters[4].Value = entry.MachineId;
                            command.Parameters[5].Value = entry.Amount;
                            command.Parameters[6].Value = entry.DateExecution.Date;
                            command.Parameters[7].Value = entry.ExecutionTime;
                            command.Parameters[8].Value = entry.MachinesInZone;
                            command.Parameters[9].Value = entry.WorkDay;

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
