using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class MachinesRepository : IMachinesRepository
    {
        private const int TextSize = 130;

        private const string SelectMachinesSql =
            "SELECT [id], [machine_make], [machine_model] FROM [KnittingMachines] ORDER BY [id] DESC";

        private const string InsertSql =
            "INSERT INTO [KnittingMachines] ([machine_make], [machine_model]) VALUES (?, ?)";

        private const string UpdateSql =
            "UPDATE [KnittingMachines] SET [machine_make] = ?, [machine_model] = ? WHERE [id] = ?";

        private const string DeleteSql =
            "DELETE FROM [KnittingMachines] WHERE [id] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public MachinesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IList<MachineItem> GetMachines()
        {
            var machines = new List<MachineItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SelectMachinesSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        machines.Add(new MachineItem
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Make = reader["machine_make"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["machine_make"]),
                            Model = reader["machine_model"] is DBNull
                                ? string.Empty
                                : Convert.ToString(reader["machine_model"])
                        });
                    }
                }
            }

            return machines;
        }

        public int InsertMachine(string make, string model)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;
                AddTextParameter(command, "@make", make);
                AddTextParameter(command, "@model", model);

                return command.ExecuteNonQuery();
            }
        }

        public int UpdateMachine(int id, string make, string model)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = UpdateSql;
                AddTextParameter(command, "@make", make);
                AddTextParameter(command, "@model", model);
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;

                return command.ExecuteNonQuery();
            }
        }

        public int DeleteMachine(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;

                return command.ExecuteNonQuery();
            }
        }

        private static void AddTextParameter(OleDbCommand command, string name, string value)
        {
            var parameter = command.Parameters.Add(name, OleDbType.VarWChar);
            parameter.Size = TextSize;
            parameter.Value = value;
        }
    }
}
