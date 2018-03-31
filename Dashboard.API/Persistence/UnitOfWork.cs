using Dashboard.API.Application.Persistence;
using Dashboard.API.Application.Persistence.Repositories;
using Dashboard.API.Persistence.Repositories;
using System;

namespace Dashboard.API.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SeviiContext _context;

        public UnitOfWork(SeviiContext context)
        {
            _context = context;
        }

        private IClientServicesRepository _clientServicesRepo;

        public IClientServicesRepository ClientServicesRepo =>
            _clientServicesRepo = _clientServicesRepo ?? new ClientServicesRepository(_context);

        private IClientProfilesRepository _clientProfilesRepo;

        public IClientProfilesRepository ClientProfilesRepo =>
            _clientProfilesRepo = _clientProfilesRepo ?? new ClientProfilesRepository(_context);

        private IUserSubscriptionsRepository _userSubscriptionsRepository;

        public IUserSubscriptionsRepository UserSubscriptionsRepo =>
            _userSubscriptionsRepository = _userSubscriptionsRepository ?? new UserSubscriptionsRepository(_context);

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
