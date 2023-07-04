using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheet.Models;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface IAprovadorRepository
    {
        Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamento(int lancamentoId);
    }
}
