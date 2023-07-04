using Timesheet.Data.Repository.Interfaces;
using Timesheet.Data.UnitOfWork;

namespace Timesheet.Services
{
    /// <summary>
    /// Classe base para os serviços da camada de serviço.
    /// Essa classe é destinada apenas para uso interno na camada de serviço.
    /// Evite usá-la fora desse contexto.
    /// </summary>
    public abstract class _BaseService
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IUsuarioRepository _usuarioRepository;
        protected readonly IProjetoRepository _projetoRepository;
        protected readonly IJobRepository _jobRepository;
        protected readonly ILancamentoRepository _lancamentoRepository;
        protected readonly IAprovadorRepository _aprovadorRepository;   
        public _BaseService(IUnitOfWork uow, IUsuarioRepository usuarioRepository, IProjetoRepository projetoRepository, 
            IJobRepository jobRepository, ILancamentoRepository lancamentoRepository, IAprovadorRepository aprovadorRepository)
        {
            _uow = uow;
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
            _jobRepository = jobRepository;
            _lancamentoRepository = lancamentoRepository;
            _aprovadorRepository = aprovadorRepository;
        }
    }
}