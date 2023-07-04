using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Data.Connection;
using Timesheet.Data.Repository.Interfaces;
using Timesheet.Models;
using Timesheet.Models.Enums;

namespace Timesheet.Data.Repository
{
    public class AprovadorRepository : BaseRepository, IAprovadorRepository
    {
        public AprovadorRepository(DatabaseConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamento(int lancamentoId)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT usuario_id, timesheet_id, status FROM Aprovadores
                    WHERE timesheet_id = @LancamentoId;";

                command.Parameters.Add(new MySqlParameter("@LancamentoId", lancamentoId));

                using (var reader = command.ExecuteReader())
                {
                    var aprovadores = new List<Aprovador>();

                    while (reader.Read())
                    {
                        var aprovador = new Aprovador
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            TimesheetId = int.Parse(reader["timesheet_id"].ToString()),
                            Status = (StatusAprovador)Enum.Parse(typeof(StatusAprovador), reader["status"].ToString()),
                        };
                        aprovadores.Add(aprovador);
                    }
                    return aprovadores;
                }
            }
        }
    }
}
