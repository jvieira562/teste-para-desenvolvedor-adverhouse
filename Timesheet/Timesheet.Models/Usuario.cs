using Timesheet.Models.Enums;

namespace Timesheet.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }


        public static TipoUsuario ConverterTipo(int tipo)
        {
            if(tipo == 2) return TipoUsuario.ADMIN;
            
            else return TipoUsuario.NORMAL;
        }
    }
}
