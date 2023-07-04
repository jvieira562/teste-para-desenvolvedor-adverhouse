using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> BuscarJobsDoProjetoAsync(int usuarioId, int projetoId);
    }
}
