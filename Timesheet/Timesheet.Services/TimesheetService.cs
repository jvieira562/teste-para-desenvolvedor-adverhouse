using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamentoAsync(int lancamentoId)
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

        public async Task<IEnumerable<Usuario>> BuscarUsuariosComProjetosAsync()
        {
            var usuarios = await _usuarioRepository.BuscarUsuariosComProjetos();
            
            return usuarios;
        }
        public async Task<bool> AdicionarAprovadorAsync(int usuarioId, int lancamentoId)
        {
            var resultado = await _aprovadorRepository.AdicionarAprovador(usuarioId, lancamentoId);

            return resultado;
        }

        public async Task<bool> VerificaSeOAprovadorEstaNoLancamento(int usuarioId, int lancamentoId)
        {
            bool resultado = await _aprovadorRepository.VerificaSeOAprovadorEstaNoLancamento(usuarioId, lancamentoId);
            return resultado;
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuariosNaoAprovadoresAsync(int lancamentoId)
        {
            var usuariosNaoAprovadores = await _usuarioRepository.BuscarUsuariosNaoAprovadoresDoLancamento(lancamentoId);

            return usuariosNaoAprovadores;
        }

        public Task<bool> VerificaSeOAprovadorEstaNoLancamentoAsync(int usuarioId, int lancamentoId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.BuscarUsuarios();

            return usuarios;
        }

        public async Task<bool> RemoverAprovadorAsync(int aprovadorId, int lancamentoId)
        {
            bool resultado = await _aprovadorRepository.RemoverAprovadorAsync(aprovadorId, lancamentoId);

            return resultado;
        }
    }
}
