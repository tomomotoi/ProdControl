using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public sealed class CoefficientsRepository : ICoefficientsRepository
    {
        private const string SelectCoefficientsSql =
            "SELECT [coefficient_id], [coefficient_value] FROM [Coefficients] " +
            "ORDER BY [coefficient_value] ASC";

        private const string ExistsSql =
            "SELECT [coefficient_value] FROM [Coefficients] WHERE [coefficient_value] = ?";

        private const string InsertSql =
            "INSERT INTO [Coefficients] ([coefficient_value]) VALUES (?)";

        private const string DeleteSql =
            "DELETE FROM [Coefficients] " +
            "WHERE [coefficient_id] = ? AND [coefficient_value] = ?";

        private readonly IDbConnectionFactory _connectionFactory;

        public CoefficientsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IList<CoefficientItem> GetCoefficients()
        {
            var coefficients = new List<CoefficientItem>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = SelectCoefficientsSql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coefficients.Add(new CoefficientItem
                        {
                            Id = Convert.ToInt32(reader["coefficient_id"]),
                            Value = Convert.ToDouble(reader["coefficient_value"])
                        });
                    }
                }
            }

            return coefficients;
        }

        public bool CoefficientExists(double value)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = ExistsSql;
                command.Parameters.Add("@coefficientValue", OleDbType.Double).Value = value;

                using (var reader = command.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public int InsertCoefficient(double value)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;
                command.Parameters.Add("@coefficientValue", OleDbType.Double).Value = value;

                return command.ExecuteNonQuery();
            }
        }

        public int DeleteCoefficient(int id, double value)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = DeleteSql;
                command.Parameters.Add("@coefficientId", OleDbType.Integer).Value = id;
                command.Parameters.Add("@coefficientValue", OleDbType.Double).Value = value;

                return command.ExecuteNonQuery();
            }
        }
    }
}
