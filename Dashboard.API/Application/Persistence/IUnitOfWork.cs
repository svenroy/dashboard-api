using Dashboard.API.Application.Persistence.Repositories;

namespace Dashboard.API.Application.Persistence
{
    public interface IUnitOfWork
    {
        IClientServicesRepository ClientServicesRepo { get; }

        IClientProfilesRepository ClientProfilesRepo { get; }

        IUserSubscriptionsRepository UserSubscriptionsRepo { get; }
    }
}