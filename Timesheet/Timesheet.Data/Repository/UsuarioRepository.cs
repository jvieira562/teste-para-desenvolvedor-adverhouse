﻿using System.Threading.Tasks;
using System.Collections.Generic;

using Timesheet.Models;
using Timesheet.Data.Connection;
using Timesheet.Data.Repository.Interfaces;

using MySql.Data.MySqlClient;

namespace Timesheet.Data.Repository
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        #region ["Construtores"]
        public UsuarioRepository(DatabaseConnection connection) : base(connection)
        {
        }
        #endregion

        #region ["Consultas"]

        public async Task<Usuario> BuscarUsuario(string email, string senha)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT usuario_id, nome, email, tipo 
                      FROM Usuarios 
                    WHERE email = @Email 
                      AND senha = @Senha;";

                command.Parameters.Add(new MySqlParameter("@Email", email));
                command.Parameters.Add(new MySqlParameter("@Senha", senha));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = "VAZIO",
                            Tipo = Usuario.ConverterTipo(int.Parse(reader["tipo"].ToString())),
                        };

                        return usuario;
                    }
                    return null;
                }
            }
        }

        public async Task<Usuario> BuscarUsuarioAtravesDoId(int usuarioId)
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText = @"SELECT usuario_id, nome, email, tipo FROM Usuarios WHERE usuario_id = @UsuarioId;";
                command.Parameters.Add(new MySqlParameter("@UsuarioId", usuarioId));

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = "VAZIO",
                            Tipo = Usuario.ConverterTipo(int.Parse(reader["tipo"].ToString())),
                        };

                        return usuario;
                    }

                    return null;
                }
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuariosComProjetos()
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText =
                    @"SELECT DISTINCT u.usuario_id,
                      u.nome,
                      u.email,
                      u.tipo
                    FROM Usuarios AS u
                    LEFT JOIN Projetos AS p 
                      ON u.usuario_id = p.usuario_id
                    ORDER BY u.usuario_id;";

                using (var reader = command.ExecuteReader())
                {
                    var usuarios = new List<Usuario>();

                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = "VAZIO",
                            Tipo = Usuario.ConverterTipo(int.Parse(reader["tipo"].ToString())),
                        };

                        usuarios.Add(usuario);
                    }

                    return usuarios;
                }
            }
        }

        public async Task<IEnumerable<Usuario>> BuscarUsuarios()
        {
            using (var command = _connection.Connection.CreateCommand())
            {
                command.CommandText = @"SELECT usuario_id, nome, email, tipo FROM Usuarios;";

                using (var reader = command.ExecuteReader())
                {
                    var usuarios = new List<Usuario>();

                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            UsuarioId = int.Parse(reader["usuario_id"].ToString()),
                            Nome = reader["nome"].ToString(),
                            Email = reader["email"].ToString(),
                            Senha = "VAZIO",
                            Tipo = Usuario.ConverterTipo(int.Parse(reader["tipo"].ToString())),
                        };

                        usuarios.Add(usuario);
                    }

                    return usuarios;
                }
            }
        }

        #endregion

    }
}