using System.Collections.Generic;
using System.Dynamic;

namespace Timesheet.ViewModels
{
    public class JobViewModel
    {
        public int? JobId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<LancamentosViewModel> Lancamentos { get; set; } = new List<LancamentosViewModel>();

        public static JobViewModel Create(int jobId, string nome, string descricao, List<LancamentosViewModel> lancamentos)
        {
            return new JobViewModel
            {
                JobId = jobId,
                Nome = nome,
                Descricao = descricao,
                Lancamentos = lancamentos
            };
        }
    }
}