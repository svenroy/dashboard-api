using Dashboard.API.Application.Infrastructure.Persistence.Repositories;

namespace Dashboard.API.Application.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        IClientServicesRepository ClientServicesRepo { get; }
    }
}