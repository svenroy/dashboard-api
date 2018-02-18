using Dashboard.API.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.API.Application.Infrastructure.Persistence.Repositories
{
    public interface IClientServicesRepository
    {
        Task<List<ClientServiceModel>> GetClientServicesByIdAsync(Guid userId);

        Task AddClientServiceAsync(ClientServiceModel model, Guid userId);

        Task UpdateClientServiceAsync(ClientServiceModel model);

        Task DeleteClientServiceAsync(Guid id);
    }
}