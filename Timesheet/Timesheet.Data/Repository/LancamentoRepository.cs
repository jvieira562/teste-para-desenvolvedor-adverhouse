using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

using MySql.Data.MySqlClient;

using Timesheet.Models;
using Timesheet.Models.Enums;
using Timesheet.Data.Connection;
using Timesheet.Data.Repository.Interfaces;

namespace Timesheet.Data.Repository
{
    public class LancamentoRepository : BaseRepository, ILancamentoRepository
    {
        public LancamentoRepository(DatabaseConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<LancamentoTimesheet>> BuscarLancamentosDoJob(int usuarioId, int projetoId, int jobId)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT timesheet_id, descricao, data, hora, status 
                    FROM LancamentosTimesheet
                    WHERE usuario_id = @UsuarioId 
                      AND projeto_id = @ProjetoId
                      AND job_id = @JobId;";

                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId));
                command.Parameters.Add(new MySqlParameter("@ProjetoId", projetoId));
                command.Parameters.Add(new MySqlParameter("@JobId", jobId));

                using (var reader = command.ExecuteReader())
                {
                    var lancamentos = new List<LancamentoTimesheet>();

                    while (reader.Read())
                    {
                        var hora = TimeSpan.Parse(reader["hora"].ToString());
                        var lancamento = new LancamentoTimesheet
                        {
                            TimesheetId = int.Parse(reader["timesheet_id"].ToString()),
                            Descricao = reader["descricao"].ToString(),
                            Data = DateTime.Now,
                            Hora = new TimeSpan(hora.Hours, hora.Minutes, 0),
                            Status = (StatusLancamentoTimesheet)Enum.Parse(typeof(StatusLancamentoTimesheet), reader["status"].ToString())
                        };

                        lancamentos.Add(lancamento);
                    }
                    return lancamentos;
                }
            }
        }
    }
}
