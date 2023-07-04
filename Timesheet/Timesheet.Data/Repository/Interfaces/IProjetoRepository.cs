using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> BuscarProjetosAsync();
        Task<IEnumerable<Projeto>> BuscarProjetosDoUsuarioAsync(int usuarioId);
    }
}
