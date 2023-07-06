using System.Web.Mvc;
using System.Threading.Tasks;

using Timesheet.Services.Interfaces;

namespace Timesheet.Controllers
{
    public class UsuarioController : BaseController
    {
        public UsuarioController(ITimesheetService timesheetService) : base(timesheetService)
        {
        }
        [HttpGet]
        public async Task<ActionResult> BuscarNaoAprovadores(int lancamentoId)
        {
            var usuarios = await _timesheetService.BuscarUsuariosNaoAprovadoresAsync(lancamentoId);

            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> BuscarUsuarios()
        {
            var usuarios = await _timesheetService.BuscarUsuariosAsync();

            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

    }
}
