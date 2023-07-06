using Timesheet.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> BuscarUsuario(string email, string senha);
        Task<Usuario> BuscarUsuarioAtravesDoId(int usuarioId);
        Task<IEnumerable<Usuario>> BuscarUsuariosComProjetos();
        Task<IEnumerable<Usuario>> BuscarUsuariosNaoAprovadoresDoLancamento(int lancamentoId);
        Task<IEnumerable<Usuario>> BuscarUsuarios();
    }
}
