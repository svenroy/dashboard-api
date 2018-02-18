using Dashboard.API.Application.Infrastructure.Persistence;
using Dashboard.API.Application.Infrastructure.Persistence.Repositories;
using Dashboard.API.Infrastructure.Persistence.Repositories;
using System;

namespace Dashboard.API.Infrastructure.Persistence
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

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
