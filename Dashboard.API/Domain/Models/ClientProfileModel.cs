using Dashboard.API.Persistence.Entities;
using System;

namespace Dashboard.API.Domain.Models
{
    public class ClientProfileModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public static ClientProfileModel Create(Client profile)
        {
            return new ClientProfileModel
            {
                Id = profile.Id,
                Name = profile.Name
            };
        }
    }
}
