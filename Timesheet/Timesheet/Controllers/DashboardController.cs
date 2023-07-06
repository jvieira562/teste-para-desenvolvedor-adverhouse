using System;
using System.Web.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Services.Interfaces;
using Timesheet.ViewModels.Builder;
using Timesheet.Models;
using Timesheet.ViewModels;

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
            var dashboard = await _builder.BuildViewModel("2023-07-06");

            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = Session["UsuarioAutenticado"] as Usuario;

                dashboard.Data = DateTime.Now;
                dashboard.Usuario = UsuarioViewModel.Create(usuarioAutenticado.UsuarioId, usuarioAutenticado.Nome,
                                    usuarioAutenticado.Email, usuarioAutenticado.Senha, new List<ProjetoViewModel>());

                return View(dashboard);
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpPost]
        public async Task<ActionResult> BuscarPorPeriodo()
        {
            string dataInicial = Request.Form["DataInicial"];
            var dashboard = await _builder.BuildViewModel(dataInicial);

            if (Session["UsuarioAutenticado"] != null)
            {
                var usuarioAutenticado = Session["UsuarioAutenticado"] as Usuario;

                dashboard.Usuario = UsuarioViewModel.Create(usuarioAutenticado.UsuarioId, usuarioAutenticado.Nome,
                                    usuarioAutenticado.Email, usuarioAutenticado.Senha, new List<ProjetoViewModel>());
                try
                {
                    var strData = dataInicial.Split('-');
                    int ano = int.Parse(strData[0]);
                    int mes = int.Parse(strData[1]);
                    int dia = int.Parse(strData[2]);
                    dashboard.Data = new DateTime(ano, mes, dia);

                } catch (Exception ex)
                {

                }

                return View("Index", dashboard);
            }
            return RedirectToAction("Index", "Login");
        }
    }
}