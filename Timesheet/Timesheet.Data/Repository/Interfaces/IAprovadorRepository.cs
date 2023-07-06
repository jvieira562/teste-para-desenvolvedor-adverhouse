using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;

namespace Timesheet.Data.Repository.Interfaces
{
    public interface IAprovadorRepository
    {
        Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamento(int lancamentoId);
        Task<bool> AdicionarAprovador(int usuarioId, int lancamentoId);
        Task<bool> VerificaSeOAprovadorEstaNoLancamento(int usuarioId, int lancamentoId);
        Task<bool> RemoverAprovadorAsync(int aprovadorId, int lancamentoId);
        Task<bool> AprovarOuReprovarLancamentoAsync(int aprovadorId, int lancamentoId, int status);
    }
}
