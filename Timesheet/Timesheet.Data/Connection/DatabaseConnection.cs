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
            string connectionString = "Server=azure-server-mysql.mysql.database.azure.com;Port=3306;Database=testeadverhouse;Uid=sysadmin;Pwd=@TesTe201452;SslMode=Required;";
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
