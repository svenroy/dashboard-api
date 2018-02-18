using Dashboard.API.Application.Infrastructure.Persistence.Repositories;
using Dashboard.API.Controllers;
using Dashboard.API.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Infrastructure.Persistence.Repositories
{
    public class ClientServicesRepository : IClientServicesRepository
    {
        private readonly SeviiContext _context;

        public ClientServicesRepository(SeviiContext context)
        {
            _context = context;
        }

        public Task<List<ClientServiceModel>> GetClientServicesByIdAsync(Guid userId)
        {
            return _context.ClientServices
                .Where(d => d.Client == userId)
                .Where(d => !d.IsDeleted)
                .Select(d => new ClientServiceModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Url = d.Url
                }).ToListAsync();
        }

        public async Task AddClientServiceAsync(ClientServiceModel model, Guid userId)
        {
            var entry = new ClientService
            {
                Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
                Name = model.Name,
                Client = userId,
                Description = model.Description,
                Url = model.Url
            };

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public Task UpdateClientServiceAsync(ClientServiceModel model)
        {
            var clientService = _context.ClientServices.Find(model.Id);

            if (clientService == null)
                return Task.FromResult(0);

            clientService.Description = model.Description;
            clientService.Url = model.Url;
            clientService.Name = model.Name;

            return _context.SaveChangesAsync();
        }

        public Task DeleteClientServiceAsync(Guid id)
        {
            var clientService = _context.ClientServices.Find(id);

            if (clientService == null)
                return Task.FromResult(0);

            clientService.IsDeleted = true;
            return _context.SaveChangesAsync();
        }
    }
}
