using Dashboard.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.API.Application.Persistence.Repositories
{
    public interface IClientServicesRepository
    {
        Task<List<ClientServiceModel>> GetClientServicesForUserAsync(Guid userId);

        Task AddClientServiceAsync(ClientServiceModel model, Guid userId);

        Task<ClientProfileAndServicesModel> GetClientProfileAndServicesOrNullAsync(Guid userId, Guid clientId);
    }
}