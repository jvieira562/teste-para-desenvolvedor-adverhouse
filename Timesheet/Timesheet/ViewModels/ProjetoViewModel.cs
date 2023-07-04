using System.Collections.Generic;

namespace Timesheet.ViewModels
{
    public class ProjetoViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<JobViewModel> Jobs { get; set; } = new List<JobViewModel>();

        public static ProjetoViewModel Create(string nome, string descricao, List<JobViewModel> jobs)
        {
            return new ProjetoViewModel
            {
                Nome = nome,
                Descricao = descricao,
                Jobs = jobs
            };
        }

    }
}