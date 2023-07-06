using System;
using System.Collections.Generic;

namespace Timesheet.ViewModels
{
    public class DashboardViewModel
    {
        public LoginViewModel Login { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public List<UsuarioViewModel> Usuarios { get; set; } = new List<UsuarioViewModel>();
        public DateTime Data{ get; set; }
    }
}
