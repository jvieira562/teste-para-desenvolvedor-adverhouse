using Ninject;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Timesheet.Data.Repository;
using Timesheet.Data.Repository.Interfaces;
using Timesheet.Data.UnitOfWork;
using Timesheet.Services.Interfaces;
using Timesheet.Services;
using Timesheet.Data.Connection;
using Ninject.Web.Common;

namespace Timesheet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DatabaseConnection>().ToSelf().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IUsuarioRepository>().To<UsuarioRepository>().InRequestScope();
            kernel.Bind<IProjetoRepository>().To<ProjetoRepository>().InRequestScope();
            kernel.Bind<IJobRepository>().To<JobRepository>().InRequestScope();
            kernel.Bind<ILancamentoRepository>().To<LancamentoRepository>().InRequestScope();
            kernel.Bind<IAprovadorRepository>().To<AprovadorRepository>().InRequestScope();

            kernel.Bind<ITimesheetService>().To<TimesheetService>().InRequestScope();
        }

    }
}
