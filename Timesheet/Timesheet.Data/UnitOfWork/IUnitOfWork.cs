using System;

namespace Timesheet.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        void BeginTransaction();
    }
}
