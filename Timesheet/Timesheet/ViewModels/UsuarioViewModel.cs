using System.Collections.Generic;

namespace Timesheet.ViewModels
{
    public class UsuarioViewModel
    {
        public int? UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public IEnumerable<ProjetoViewModel> Projetos { get; set; } = new List<ProjetoViewModel>();


        public static UsuarioViewModel Create(int usuarioId, string nome, string email, string senha, IEnumerable<ProjetoViewModel> projetos)
        {
            return new UsuarioViewModel
            {
                UsuarioId = usuarioId,  
                Nome = nome,
                Email = email,
                Senha = senha,
                Projetos = projetos
            };
        }
    }
}
