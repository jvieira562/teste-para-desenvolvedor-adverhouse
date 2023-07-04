using Timesheet.Services.Interfaces;

namespace Timesheet.ViewModels.Builder
{
    /// <summary>
    /// Classe base para os builder da camada de aplicação.
    /// Essa classe é destinada apenas para uso interno na camada de aplicação.
    /// Evite usá-la fora desse contexto.
    /// </summary>
    public abstract class BaseViewModelBuilder
    {
        protected readonly ITimesheetService _timesheetService;

        public BaseViewModelBuilder(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }
    }
}
