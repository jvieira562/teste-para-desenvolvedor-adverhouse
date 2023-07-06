using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;

namespace Timesheet.Services.Interfaces
{
    public interface ITimesheetService
    {
        #region ["Usuarios"]
        Task<Usuario> BuscarUsuarioAsync(string email, string senha);
        Task<IEnumerable<Usuario>> BuscarUsuariosComProjetosAsync();
        Task<IEnumerable<Usuario>> BuscarUsuariosAsync();
        Task<IEnumerable<Usuario>> BuscarUsuariosNaoAprovadoresAsync(int lancamentoId);
        #endregion
        #region ["Projetos"]
        Task<IEnumerable<Projeto>> BuscarProjetosDoUsuario(int usuarioId);
        #region ["Jobs"]
        Task<IEnumerable<Job>> BuscarJobsDoProjetoAsync(int usuarioId, int projetoId);
        #endregion
        #endregion
        #region ["Aprovadores"]
        Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamentoAsync(int lancamentoId);
        Task<bool> AdicionarAprovadorAsync(int usuarioId, int lancamentoId);
        Task<bool> VerificaSeOAprovadorEstaNoLancamentoAsync(int usuarioId, int lancamentoId);
        Task<bool> RemoverAprovadorAsync(int aprovadorId, int lancamentoId);
        #endregion
        #region ["Lançamentos"]
        Task<IEnumerable<LancamentoTimesheet>> BuscarLancamentosDoJobAsync(int usuarioId, int projetoId, int jobId);
        #endregion
    }
}
