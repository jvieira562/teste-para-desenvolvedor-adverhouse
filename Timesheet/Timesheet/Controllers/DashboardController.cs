using System.Web.Mvc;
using System.Threading.Tasks;
using Timesheet.Services.Interfaces;
using Timesheet.ViewModels.Builder;

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

            return View(dashboard);
        }
    }
}