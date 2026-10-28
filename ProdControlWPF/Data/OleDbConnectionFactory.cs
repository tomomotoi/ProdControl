using System;
using System.Data.OleDb;
using System.IO;

namespace ProdControlWPF.Data
{
    public sealed class OleDbConnectionFactory : IDbConnectionFactory
    {
        private const string DataDirectoryToken = "|DataDirectory|";
        private readonly string _connectionString;

        public OleDbConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Не задана строка подключения к базе данных.", nameof(connectionString));
            }

            var dataDirectory = AppDomain.CurrentDomain.BaseDirectory
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            _connectionString = connectionString.Replace(DataDirectoryToken, dataDirectory);
        }

        public OleDbConnection CreateConnection()
        {
            return new OleDbConnection(_connectionString);
        }
    }
}
