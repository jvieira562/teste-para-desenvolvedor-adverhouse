using System.Collections.Generic;
using System.Threading.Tasks;

using Timesheet.Models;
using Timesheet.Services.Interfaces;
using Timesheet.Data.UnitOfWork;
using Timesheet.Data.Repository.Interfaces;

namespace Timesheet.Services
{
    public class TimesheetService : _BaseService, ITimesheetService
    {
        #region ["Construtores"]
        public TimesheetService(IUnitOfWork uow, IUsuarioRepository usuarioRepository, IProjetoRepository projetoRepository, 
            IJobRepository jobRepository, ILancamentoRepository lancamentoRepository, IAprovadorRepository aprovadorRepository)
            : base(uow, usuarioRepository, projetoRepository, jobRepository, lancamentoRepository, aprovadorRepository)
        {
        }
        #endregion

        public async Task<IEnumerable<Aprovador>> BuscarAprovadoresDoProjetoAsync(int lancamentoId)
        {
            var aprovadores = await _aprovadorRepository.BuscarAprovadoresDoLancamento(lancamentoId);

            return aprovadores;
        }

        public async Task<IEnumerable<Job>> BuscarJobsDoProjetoAsync(int usuarioId, int projetoId)
        {
            var jobs = await _jobRepository.BuscarJobsDoProjetoAsync(usuarioId, projetoId);
            
            return jobs;
        }

        public async Task<IEnumerable<LancamentoTimesheet>> BuscarLancamentosDoJobAsync(int usuarioId, int projetoId, int jobId)
        {
            var lancamentos = await _lancamentoRepository.BuscarLancamentosDoJob(usuarioId, projetoId, jobId);

            return lancamentos;
        }

        public async Task<IEnumerable<Projeto>> BuscarProjetosDoUsuario(int usuarioId)
        {
            var projetos = await _projetoRepository.BuscarProjetosDoUsuarioAsync(usuarioId);
            
            return projetos;
        }


        public async Task<Usuario> BuscarUsuarioAsync(string email, string senha)
        {
            var usuario = await _usuarioRepository.BuscarUsuario(email, senha);
            
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.BuscarUsuariosComProjetos();
            
            return usuarios;
        }

    }
}
