using Dashboard.API.Application.Persistence.Repositories;
using Dashboard.API.Domain.Models;
using Dashboard.API.Persistence.Entities;
using System;
using System.Threading.Tasks;

namespace Dashboard.API.Persistence.Repositories
{
    public class ClientProfilesRepository : IClientProfilesRepository
    {
        private readonly SeviiContext _context;

        public ClientProfilesRepository(SeviiContext context)
        {
            _context = context;
        }

        public async Task UpdateOrAddProfileAsync(ClientProfileModel model, Guid userId)
        {
            var profile = await _context.Clients.FindAsync(userId);

            if (profile != null)
                profile.Name = model.Name;
            else
            {
                profile = new Client
                {
                    Id = userId,
                    Name = model.Name
                };

                await _context.Clients.AddAsync(profile);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ClientProfileModel> GetProfileByIdAsync(Guid userId)
        {
            var profile = await _context.Clients.FindAsync(userId);

            return profile == null
                ? null
                : ClientProfileModel.Create(profile);
        }
    }
}
