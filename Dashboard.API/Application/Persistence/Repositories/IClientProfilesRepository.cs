using Dashboard.API.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Dashboard.API.Application.Persistence.Repositories
{
    public interface IClientProfilesRepository
    {
        Task UpdateOrAddProfileAsync(ClientProfileModel model, Guid userId);

        Task<ClientProfileModel> GetProfileByIdAsync(Guid userId);
    }
}
