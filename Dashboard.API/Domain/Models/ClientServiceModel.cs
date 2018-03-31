using Dashboard.API.Persistence.Entities;
using System;

namespace Dashboard.API.Domain.Models
{
    public class ClientServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string DefaultValue { get; set; }

        public string Description { get; set; }

        public bool Subscribed { get; set; }

        public static ClientServiceModel Create(ClientService clientService)
        {
            return new ClientServiceModel
            {
                Id = clientService.Id,
                Name = clientService.Name,
                Description = clientService.Description,
                Key = clientService.Key,
                DefaultValue = clientService.DefaultValue
            };
        }
    }
}