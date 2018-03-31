using Dashboard.API.Application.Persistence.Repositories;
using Dashboard.API.Domain.Models;
using Dashboard.API.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Persistence.Repositories
{
    public class ClientServicesRepository : IClientServicesRepository
    {
        private readonly SeviiContext _context;

        public ClientServicesRepository(SeviiContext context)
        {
            _context = context;
        }

        public Task<List<ClientServiceModel>> GetClientServicesForUserAsync(Guid userId)
        {
            return _context.ClientServices
                .Where(d => d.ClientId == userId).Where(d => !d.IsDeleted)
                .Select(d => ClientServiceModel.Create(d)).ToListAsync();
        }

        public async Task AddClientServiceAsync(ClientServiceModel model, Guid userId)
        {
            var client = await _context.Clients.FindAsync(userId);
            if (client == null)
                await _context.Clients.AddAsync(new Client { Id = userId, Name = "" });

            var clientService = new ClientService
            {
                Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id,
                Name = model.Name,
                ClientId = userId,
                Description = model.Description,
                Key = model.Key,
                DefaultValue = model.DefaultValue
            };

            await _context.AddAsync(clientService);
            await _context.SaveChangesAsync();
        }

        public async Task<ClientProfileAndServicesModel> GetClientProfileAndServicesOrNullAsync(Guid userId, Guid clientId)
        {
            var profile = await _context.Clients.FindAsync(clientId);

            if (profile == null)
                return null;

            var services = await _context.ClientServices
                .Where(d => d.ClientId == clientId).Where(d => !d.IsDeleted)
                .Select(d => ClientServiceModel.Create(d)).ToListAsync();

            var servicesWithSubscribedStatus = new List<ClientServiceModel>();

            foreach (var service in services)
            {
                service.Subscribed =
                    await _context.UserSubscriptions.FirstOrDefaultAsync(d =>
                        d.UserId == userId && d.ClientServiceId == service.Id) != null;

                servicesWithSubscribedStatus.Add(service);
            }

            var result = new ClientProfileAndServicesModel
            {
                Profile = ClientProfileModel.Create(profile),
                Services = servicesWithSubscribedStatus
            };

            return result;
        }
    }
}
