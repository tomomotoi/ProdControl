using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class EmployeesRepository : IEmployeesRepository
    {
        private const string SelectPositionsSql =
            "SELECT [PositionID], [PositionName] FROM [Position]";

        private const string SelectEmployeesSql =
            "SELECT [EmployeeID], [EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID] " +
            "FROM [Employee]";

        private const string SearchByNameSql =
            "SELECT [EmployeeID], [EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID] " +
            "FROM [Employee] WHERE [EmployeeName] LIKE ?";

        private const string SearchByNumberSql =
            "SELECT [EmployeeID], [EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID] " +
            "FROM [Employee] WHERE [EmployeeNumber] LIKE ?";

        private const string SearchByPositionSql =
            "SELECT [EmployeeID], [EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID] " +
            "FROM [Employee] WHERE [PositionID] = ?";

        private const string SearchByLevelSql =
            "SELECT [EmployeeID], [EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID] " +
            "FROM [Employee] WHERE [PositionLevelID] = ?";

        private const string InsertSql =
            "INSERT INTO [Employee] ([EmployeeName], [EmployeeNumber], [PositionID], [PositionLevelID]) " +
            "VALUES (?, ?, ?, ?)";

        private const string UpdateSql =
            "UPDATE [Employee] SET [EmployeeName] = ?, [EmployeeNumber] = ?, [PositionID] = ?, " +
            "[PositionLevelID] = ? WHERE [EmployeeID] = ?";

        private const string DeleteSql =
            "DELETE FROM [Employee] WHERE [EmployeeID] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public EmployeesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<EmployeePositionLookup> GetPositions()
        {
            var positions = new List<EmployeePositionLookup>();

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
                        positions.Add(new EmployeePositionLookup(
                            Convert.ToInt32(reader["PositionID"]),
                            reader["PositionName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["PositionName"])));
                    }
                }
            }

            return positions;
        }

        public IReadOnlyList<EmployeeRow> GetEmployees()
        {
            return ReadEmployees(SelectEmployeesSql, null);
        }

        public IReadOnlyList<EmployeeRow> SearchByName(string employeeName)
        {
            return ReadEmployees(SearchByNameSql, command =>
                command.Parameters.Add("@employeeName", OleDbType.VarWChar, 255).Value =
                    "%" + (employeeName ?? string.Empty) + "%");
        }

        public IReadOnlyList<EmployeeRow> SearchByNumber(string employeeNumber)
        {
            return ReadEmployees(SearchByNumberSql, command =>
                command.Parameters.Add("@employeeNumber", OleDbType.VarWChar, 255).Value =
                    "%" + (employeeNumber ?? string.Empty) + "%");
        }

        public IReadOnlyList<EmployeeRow> SearchByPosition(int positionId)
        {
            return ReadEmployees(SearchByPositionSql, command =>
                command.Parameters.Add("@positionId", OleDbType.Integer).Value = positionId);
        }

        public IReadOnlyList<EmployeeRow> SearchByLevel(int positionLevel)
        {
            return ReadEmployees(SearchByLevelSql, command =>
                command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = positionLevel);
        }

        public void Save(
            IList<int> employeeIdsForRemoval,
            IList<EmployeeInsert> inserts,
            IList<EmployeeUpdate> updates)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    if (employeeIdsForRemoval != null)
                    {
                        foreach (var employeeId in employeeIdsForRemoval)
                        {
                            Delete(connection, transaction, employeeId);
                        }
                    }

                    if (inserts != null)
                    {
                        foreach (var insert in inserts)
                        {
                            Insert(connection, transaction, insert);
                        }
                    }

                    if (updates != null)
                    {
                        foreach (var update in updates)
                        {
                            Update(connection, transaction, update);
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        private IReadOnlyList<EmployeeRow> ReadEmployees(
            string sql,
            Action<OleDbCommand> bindParameters)
        {
            var employees = new List<EmployeeRow>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                bindParameters?.Invoke(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeRow
                        {
                            EmployeeId = Convert.ToInt32(reader["EmployeeID"]),
                            EmployeeName = reader["EmployeeName"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["EmployeeName"]),
                            EmployeeNumber = reader["EmployeeNumber"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["EmployeeNumber"]),
                            PositionId = reader["PositionID"] is DBNull
                                ? (int?)null
                                : Convert.ToInt32(reader["PositionID"]),
                            PositionLevel = reader["PositionLevelID"] is DBNull
                                ? string.Empty
                                : Convert.ToInt32(reader["PositionLevelID"])
                                    .ToString(CultureInfo.CurrentCulture)
                        });
                    }
                }
            }

            return employees;
        }

        private static void Delete(
            OleDbConnection connection,
            OleDbTransaction transaction,
            int employeeId)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = employeeId;

                command.ExecuteNonQuery();
            }
        }

        private static void Insert(
            OleDbConnection connection,
            OleDbTransaction transaction,
            EmployeeInsert insert)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;

                command.Parameters.Add("@employeeName", OleDbType.VarWChar, 255).Value =
                    insert.EmployeeName ?? string.Empty;
                command.Parameters.Add("@employeeNumber", OleDbType.VarWChar, 255).Value =
                    insert.EmployeeNumber ?? string.Empty;
                command.Parameters.Add("@positionId", OleDbType.Integer).Value = insert.PositionId;
                command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = insert.PositionLevel;

                command.ExecuteNonQuery();
            }
        }

        private static void Update(
            OleDbConnection connection,
            OleDbTransaction transaction,
            EmployeeUpdate update)
        {
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = UpdateSql;

                command.Parameters.Add("@employeeName", OleDbType.VarWChar, 255).Value =
                    update.EmployeeName ?? string.Empty;
                command.Parameters.Add("@employeeNumber", OleDbType.VarWChar, 255).Value =
                    update.EmployeeNumber ?? string.Empty;
                command.Parameters.Add("@positionId", OleDbType.Integer).Value = update.PositionId;
                command.Parameters.Add("@positionLevel", OleDbType.Integer).Value = update.PositionLevel;
                command.Parameters.Add("@employeeId", OleDbType.Integer).Value = update.EmployeeId;

                command.ExecuteNonQuery();
            }
        }
    }
}
