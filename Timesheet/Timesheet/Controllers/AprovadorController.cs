﻿using System;
using System.Web.Mvc;
using System.Threading.Tasks;

using Timesheet.Services.Interfaces;

namespace Timesheet.Controllers
{
    [AllowAnonymous]
    public class AprovadorController : BaseController
    {
        public AprovadorController(ITimesheetService timesheetService) : base(timesheetService)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar()
        {
            try
            {
                int usuarioId = int.Parse(Request.Form["UsuarioId"]);
                int lancamentoId = int.Parse(Request.Form["LancamentoId"]);

                await _timesheetService.AdicionarAprovadorAsync(usuarioId, lancamentoId);

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }
        [HttpGet]
        public async Task<ActionResult> BuscarAprovadoresDoLancamento(int lancamentoId)
        {
            try
            {
                var aprovadores = await _timesheetService.BuscarAprovadoresDoLancamentoAsync(lancamentoId);

                return Json(aprovadores, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public async Task<ActionResult> RemoverAprovador(int aprovadorId, int lancamentoId)
        {
            _timesheetService.RemoverAprovadorAsync(aprovadorId, lancamentoId);
            return new HttpStatusCodeResult(204);
        }

        [HttpPost]
        public async Task<ActionResult> AprovarOuReprovarLancamento(int aprovadorId, int lancamentoId, int status)
        {
            var statusCode = 400;

            var resultado = await _timesheetService.AprovarOuReprovarLancamentoAsync(aprovadorId, lancamentoId, status);

            if (resultado) statusCode = 204;

            return new HttpStatusCodeResult(statusCode);
        }
    }
}
