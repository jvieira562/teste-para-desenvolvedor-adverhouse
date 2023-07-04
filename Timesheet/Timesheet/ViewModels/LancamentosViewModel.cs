using System;
using System.Collections.Generic;

using Timesheet.Models.Enums;

namespace Timesheet.ViewModels
{
    public class LancamentosViewModel
    {
        public int? UsuarioId { get; set; }
        public int? ProjetoId { get; set; }
        public int? JobId { get; set; }
        public int? LancamentoId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; } // objeto.Data = new DateTime(2023, 7, 1); Atribuindo apenas a data (01/07/2023), a hora será 00:00:00
        public TimeSpan Hora { get; set; } // objeto.Hora = new TimeSpan(10, 30, 0); Atribuindo apenas a hora (10:30), os segundos serão 0
        public StatusLancamentoTimesheet Status { get; set; } = StatusLancamentoTimesheet.NAO_VALIDADO;
        public List<AprovadorViewModel> Aprovadores { get; set; } = new List<AprovadorViewModel>();

        public static LancamentosViewModel Create(int usuarioId, int projetoId, int jobId, int lancamentoId, string descricao, DateTime data, TimeSpan hora, StatusLancamentoTimesheet status, List<AprovadorViewModel> aprovadores)
        {
            return new LancamentosViewModel
            {
                UsuarioId = usuarioId,
                ProjetoId = projetoId,
                JobId = jobId,
                LancamentoId = lancamentoId,
                Descricao = descricao,
                Data = data,
                Hora = hora,
                Status = status,
                Aprovadores = aprovadores

            };
        }
    }
}