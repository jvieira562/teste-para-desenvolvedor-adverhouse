using System;
using System.Data;

using MySql.Data.MySqlClient;

namespace Timesheet.Data.Connection
{
    public sealed class DatabaseConnection : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DatabaseConnection()
        {
            string connectionString = "Server=ENDERECO;Port=3306;Database=SEU_BANCO;Uid=usuario;Pwd=SENHA;SslMode=Required;";
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
