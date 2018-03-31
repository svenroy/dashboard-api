using System.Collections.Generic;

namespace Dashboard.API.Domain.Models
{
    public class ClientProfileAndServicesModel
    {
        public ClientProfileModel Profile { get; set; }

        public List<ClientServiceModel> Services { get; set; } = new List<ClientServiceModel>();
    }
}