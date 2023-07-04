using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;
using Timesheet.Data.Connection;
using Timesheet.Data.Repository.Interfaces;

using MySql.Data.MySqlClient;

namespace Timesheet.Data.Repository
{
    public class ProjetoRepository : BaseRepository, IProjetoRepository
    {
        #region ["Construtores"]
        public ProjetoRepository(DatabaseConnection connection) : base(connection)
        {
            
        }
        #endregion

        #region ["Consultas"]
        public async Task<IEnumerable<Projeto>> BuscarProjetosAsync()
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT usuario_id, projeto_id, nome, descricao FROM Projetos;";

                using (var reader = command.ExecuteReader())
                {
                    var projetos = new List<Projeto>();

                    while(reader.Read())
                    {
                        var projeto = new Projeto
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            ProjetoId = int.Parse(reader["projeto_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Descricao = reader["descricao"].ToString(),
                        };

                        projetos.Add(projeto);
                    }
                    return projetos;
                }
            }
        }

        public async Task<IEnumerable<Projeto>> BuscarProjetosDoUsuarioAsync(int usuarioId)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT usuario_id, projeto_id, nome, descricao
                      FROM Projetos
                    WHERE usuario_id = @UsuarioId;";

                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId.ToString()));

                using (var reader = command.ExecuteReader())
                {
                    var projetos = new List<Projeto>();

                    while(reader.Read())
                    {
                        var projeto = new Projeto
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            ProjetoId = int.Parse(reader["projeto_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Descricao = reader["descricao"].ToString(),
                        };
                        projetos.Add(projeto);
                    }
                    return projetos;
                }
            }
        }

        #endregion
    }
}
