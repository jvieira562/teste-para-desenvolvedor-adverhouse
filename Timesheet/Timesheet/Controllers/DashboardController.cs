using System.Web.Mvc;
using System.Threading.Tasks;

using Timesheet.Services.Interfaces;
using Timesheet.ViewModels.Builder;
using Timesheet.Models;
using Timesheet.ViewModels;
using System.Collections.Generic;

namespace Timesheet.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        #region ["Construtores"]
        private readonly DashboardViewModelBuilder _builder;
        
        public DashboardController(ITimesheetService timesheetService, DashboardViewModelBuilder builder) : base(timesheetService)
        {
            _builder = builder;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dashboard = await _builder.BuildViewModel();

            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = Session["UsuarioAutenticado"] as Usuario;

                dashboard.Usuario = UsuarioViewModel.Create(usuarioAutenticado.UsuarioId, usuarioAutenticado.Nome,
                                    usuarioAutenticado.Email, usuarioAutenticado.Senha, new List<ProjetoViewModel>());
                return View(dashboard);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public async Task<ActionResult> BuscarAprovadores()    
        {
            var dashboard = await _builder.BuildViewModel();

            return View(dashboard);
        }
    }
}