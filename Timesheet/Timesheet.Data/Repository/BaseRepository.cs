using Timesheet.Data.Connection;

namespace Timesheet.Data.Repository
{
    /// <summary>
    /// Classe base para os repositórios da camada de data.
    /// Essa classe é destinada apenas para uso interno na camada de data.
    /// Evite usá-la fora desse contexto.
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly DatabaseConnection _connection;

        protected BaseRepository(DatabaseConnection connection)
        {
            _connection = connection;
        }
    }
}
