using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;
using Timesheet.Services.Interfaces;

namespace Timesheet.ViewModels.Builder
{
    public class DashboardViewModelBuilder : IViewModelBuilder<DashboardViewModel>
    {
        private readonly ITimesheetService _timesheetService;

        public DashboardViewModelBuilder(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        public async Task<DashboardViewModel> BuildViewModel()
        {
            var usuarios = await _timesheetService.BuscarUsuariosComProjetosAsync();

            var model = new DashboardViewModel
            {
                Usuarios = await BuildUsuariosViewModel(usuarios)
            };

            return model;
        }

        private async Task<List<UsuarioViewModel>> BuildUsuariosViewModel(IEnumerable<Usuario> usuarios)
        {
            var usuariosViewModel = new List<UsuarioViewModel>();
            foreach (var usuario in usuarios)
            {
                var projetosDoUsuario = await _timesheetService.BuscarProjetosDoUsuario(usuario.UsuarioId);
                var projetosViewModel = await BuildProjetosViewModel(projetosDoUsuario);

                var usuarioViewModel = UsuarioViewModel.Create(usuario.UsuarioId, usuario.Nome, usuario.Email, usuario.Senha, projetosViewModel);
                usuariosViewModel.Add(usuarioViewModel);
            }

            return usuariosViewModel;
        }

        private async Task<List<ProjetoViewModel>> BuildProjetosViewModel(IEnumerable<Projeto> projetos)
        {
            var projetosViewModel = new List<ProjetoViewModel>();

            foreach (var projeto in projetos)
            {
                var jobsDoProjeto = await _timesheetService.BuscarJobsDoProjetoAsync(projeto.UsuarioId, projeto.ProjetoId);
                var jobsViewModel = await BuildJobsViewModel(jobsDoProjeto.ToList());

                var projetoViewModel = ProjetoViewModel.Create(projeto.Nome, projeto.Descricao, jobsViewModel);
                projetosViewModel.Add(projetoViewModel);
            }

            return projetosViewModel;
        }

        private async Task<List<JobViewModel>> BuildJobsViewModel(List<Job> jobs)
        {
            var jobsViewModel = new List<JobViewModel>();

            foreach (var job in jobs)
            {
                var lancamentos = await _timesheetService.BuscarLancamentosDoJobAsync(job.UsuarioId, job.ProjetoId, job.JobId);
                var lancamentosViewModel = await BuildLancamentosViewModel(lancamentos);

                var jobViewModel = JobViewModel.Create(job.JobId, job.Nome, job.Descricao, lancamentosViewModel);
                jobsViewModel.Add(jobViewModel);
            }

            return jobsViewModel;
        }

        private async Task<List<LancamentosViewModel>> BuildLancamentosViewModel(IEnumerable<LancamentoTimesheet> lancamentos)
        {
            var lancamentosViewModel = new List<LancamentosViewModel>();

            foreach (var lancamento in lancamentos)
            {
                var aprovadores = await _timesheetService.BuscarAprovadoresDoLancamentoAsync(lancamento.TimesheetId);
                var aprovadoresViewModel = BuildAprovadoresViewModel(aprovadores.ToList());

                var lancamentoViewModel = LancamentosViewModel.Create(lancamento.UsuarioId, lancamento.ProjetoId, lancamento.JobId, lancamento.TimesheetId, lancamento.Descricao, lancamento.Data, lancamento.Hora, lancamento.Status, aprovadoresViewModel);
                lancamentosViewModel.Add(lancamentoViewModel);
            }

            return lancamentosViewModel;
        }

        private List<AprovadorViewModel> BuildAprovadoresViewModel(List<Aprovador> aprovadores)
        {
            var aprovadoresViewModel = new List<AprovadorViewModel>();

            foreach (var aprovador in aprovadores)
            {
                var aprovadorViewModel = AprovadorViewModel.Create(aprovador.UsuarioId, aprovador.TimesheetId, aprovador.Status);
                aprovadoresViewModel.Add(aprovadorViewModel);
            }

            return aprovadoresViewModel;
        }


    }


}
