using System;
using Timesheet.Models.Enums;

namespace Timesheet.Models
{
    public class LancamentoTimesheet
    {
        public int TimesheetId { get; set; }
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
        public int JobId { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public StatusLancamentoTimesheet Status { get; set; }
        
    }
}
