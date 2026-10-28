using System;
using System.Data;
using System.Data.OleDb;

namespace ProdControlWPF.Data
{
    public sealed class NewArticulRepository : INewArticulRepository
    {
        private const string InsertSql =
            "INSERT INTO [Articul] ([ArticulName], [ArticulText]) VALUES (?, ?)";

        private readonly IDbConnectionFactory _connectionFactory;

        public NewArticulRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public int InsertArticul(string articulName, string articulText)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandType = CommandType.Text;
                command.CommandText = InsertSql;

                command.Parameters.Add("@articulName", OleDbType.VarWChar, 255).Value =
                    articulName ?? string.Empty;
                command.Parameters.Add("@articulText", OleDbType.VarWChar, 255).Value =
                    articulText ?? string.Empty;

                return command.ExecuteNonQuery();
            }
        }
    }
}
