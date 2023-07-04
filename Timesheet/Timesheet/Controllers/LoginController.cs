using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Timesheet.Services.Interfaces;
using Timesheet.ViewModels;

namespace Timesheet.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(ITimesheetService timesheetService) : base(timesheetService)
        {
        }
        public async Task<ActionResult> Index()
        {
            var model = new LoginViewModel();
            string email = Request.Form["email"];
            string senha = Request.Form["senha"];
            if(email != null && senha != null)
            {
                var usuario = await _timesheetService.BuscarUsuarioAsync(email, senha);
                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.Nome, false);
                    return RedirectToAction("Index", "Dashboard");
                }

                model.Mensagem = "E-mail ou senha inválidos!";
            }

            return View("Login", model);
        }
        public async Task<ActionResult> Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
