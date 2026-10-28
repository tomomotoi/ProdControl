using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class DetailsRepository : IDetailsRepository
    {
        private const string SelectDetailsSql =
            "SELECT [id], [part_name] FROM [ClothingParts] ORDER BY [id] DESC";

        private const string InsertSql =
            "INSERT INTO [ClothingParts] ([part_name]) VALUES (?)";

        private const string UpdateSql =
            "UPDATE [ClothingParts] SET [part_name] = ? WHERE [id] = ?";

        private const string DeleteSql =
            "DELETE FROM [ClothingParts] WHERE [id] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public DetailsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IList<DetailItem> GetDetails()
        {
            var details = new List<DetailItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SelectDetailsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(new DetailItem
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader.IsDBNull(reader.GetOrdinal("part_name"))
                                ? string.Empty
                                : Convert.ToString(reader["part_name"])
                        });
                    }
                }
            }

            return details;
        }

        public int InsertDetail(string partName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;
                command.Parameters.Add("@partName", OleDbType.VarWChar).Value = partName;

                return command.ExecuteNonQuery();
            }
        }

        public int UpdateDetail(int id, string partName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = UpdateSql;
                command.Parameters.Add("@partName", OleDbType.VarWChar).Value = partName;
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;

                return command.ExecuteNonQuery();
            }
        }

        public int DeleteDetail(int id)
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
    }
}
