using Timesheet.Models.Enums;

namespace Timesheet.ViewModels
{
    public class AprovadorViewModel
    {
        public int? UsuarioId { get; set; }
        public int? LancamentoId { get; set; }
        public StatusAprovador Status { get; set; } = StatusAprovador.PENDENTE;

        public static AprovadorViewModel Create(int usuarioId, int lancamentoId, StatusAprovador status)
        {
            return new AprovadorViewModel
            {
                UsuarioId = usuarioId,
                LancamentoId = lancamentoId,
                Status = status,
            };
        }
    }
}