using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;
using Timesheet.Data.Connection;
using Timesheet.Data.Repository.Interfaces;

using MySql.Data.MySqlClient;

namespace Timesheet.Data.Repository
{
    public class JobRepository : BaseRepository, IJobRepository
    {
        #region ["Construtores"]
        public JobRepository(DatabaseConnection connection) : base(connection)
        {
        }
        #endregion

        #region ["Consultas"]
        public async Task<IEnumerable<Job>> BuscarJobsDoProjetoAsync(int usuarioId, int projetoId)
        {
            using(var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT usuario_id, projeto_id, job_id, nome, descricao
                      FROM Jobs 
                    WHERE usuario_id = @UsuarioId 
                      AND projeto_id = @ProjetoId;";

                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId.ToString()));
                command.Parameters.Add(new MySqlParameter("@ProjetoId", projetoId.ToString()));

                using (var reader = command.ExecuteReader())
                {
                    var jobs = new List<Job>();

                    while (reader.Read())
                    {
                        var job = new Job
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            ProjetoId = int.Parse(reader["projeto_id"].ToString()),
                            JobId = int.Parse(reader["job_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Descricao = reader["descricao"].ToString(),
                        };

                        jobs.Add(job);
                    }
                    return jobs;
                }
            }
        }
        #endregion
    }
}
