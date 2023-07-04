using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;

namespace Timesheet.Services.Interfaces
{
    public interface ITimesheetService
    {
        #region ["Usuarios"]
        Task<Usuario> BuscarUsuarioAsync(string email, string senha);
        Task<IEnumerable<Usuario>> BuscarUsuariosAsync();
        #endregion
        #region ["Projetos"]
        Task<IEnumerable<Projeto>> BuscarProjetosDoUsuario(int usuarioId);
        #region ["Jobs"]
        Task<IEnumerable<Job>> BuscarJobsDoProjetoAsync(int usuarioId, int projetoId);
        #endregion
        #endregion
        #region ["Aprovadores"]
        Task<IEnumerable<Aprovador>> BuscarAprovadoresDoProjetoAsync(int lancamentoId);
        #endregion
        #region ["Lançamentos"]
        Task<IEnumerable<LancamentoTimesheet>> BuscarLancamentosDoJobAsync(int usuarioId, int projetoId, int jobId);
        #endregion
    }
}
