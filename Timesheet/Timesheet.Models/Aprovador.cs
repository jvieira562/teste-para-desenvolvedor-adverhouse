using Timesheet.Models.Enums;

namespace Timesheet.Models
{
    public class Aprovador
    {
        public int TimesheetId { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public StatusAprovador Status { get; set; }
    }
}
