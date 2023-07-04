using Timesheet.Data.Connection;

namespace Timesheet.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseConnection _connection;

        public UnitOfWork(DatabaseConnection connection)
        {
            _connection = connection;
        }
        public void BeginTransaction()
        {
            _connection.Transaction = _connection.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _connection.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _connection.Transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
