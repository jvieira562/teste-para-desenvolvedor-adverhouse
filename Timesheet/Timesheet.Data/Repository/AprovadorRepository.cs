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

        public async Task<bool> AdicionarAprovador(int usuarioId, int lancamentoId)
        {
            bool resultado = false;

            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"INSERT INTO Aprovadores (usuario_id, timesheet_id, status)
                    VALUES (@UsuarioId, @TimesheetId, @Status)";
                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId));
                command.Parameters.Add(new MySqlParameter("@TimesheetId", lancamentoId));
                command.Parameters.Add(new MySqlParameter("@Status", 2));

                var linhasAfetadas = command.ExecuteNonQuery();
                if (linhasAfetadas > 0) resultado = true;

                return resultado;
            }
        }

        public async Task<IEnumerable<Aprovador>> BuscarAprovadoresDoLancamento(int lancamentoId)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT u.usuario_id, u.Nome, a.timesheet_id, a.status 
                    FROM Usuarios AS u
                    INNER JOIN Aprovadores AS a
                    ON u.usuario_id = a.usuario_id
                    WHERE a.timesheet_id = @LancamentoId;";

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
                            Nome = reader["nome"].ToString(),
                            Status = (StatusAprovador)Enum.Parse(typeof(StatusAprovador), reader["status"].ToString()),
                        };
                        aprovadores.Add(aprovador);
                    }
                    return aprovadores;
                }
            }
        }

        public async Task<bool> RemoverAprovadorAsync(int aprovadorId, int lancamentoId)
        {
            bool resultado = false;

            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"DELETE FROM Aprovadores AS a
                    WHERE a.usuario_id = @UsuarioId
                      AND a.timesheet_id = @TimesheetId;";
                command.Parameters.Add(new MySqlParameter("@UsuarioId", aprovadorId));
                command.Parameters.Add(new MySqlParameter("@TimesheetId", lancamentoId));

                var linhasAfetadas = command.ExecuteNonQuery();
                if(linhasAfetadas > 0) resultado = true;
            }

            return resultado;
        }

        public async Task<bool> VerificaSeOAprovadorEstaNoLancamento(int usuarioId, int lancamentoId)
        {
            bool resultado = false;

            using(var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT 1 FROM Aprovadores WHERE usuario_id = @UsuarioId AND timesheet_id = @LancamentoId;";
                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId));
                command.Parameters.Add(new MySqlParameter("@LancamentoId", lancamentoId));

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader['1'].ToString() != null) resultado = true;
                        break;
                    }
                }
                return resultado;
            }
        }
    }
}
