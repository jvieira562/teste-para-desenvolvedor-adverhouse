namespace Timesheet.Models
{
    public class Job
    {
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
        public int JobId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
