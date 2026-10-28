using System.Data.OleDb;

namespace ProdControlWPF.Data
{
    public interface IDbConnectionFactory
    {
        OleDbConnection CreateConnection();
    }
}
