using System.Web.Mvc;
using Timesheet.Services.Interfaces;

namespace Timesheet.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ITimesheetService _timesheetService;

        public BaseController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }
        
    }
}
