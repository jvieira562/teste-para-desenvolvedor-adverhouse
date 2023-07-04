using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface ILancamentoRepository
    {
        Task<IEnumerable<LancamentoTimesheet>> BuscarLancamentosDoJob(int usuarioId, int projetoId, int jobId);
    }
}
