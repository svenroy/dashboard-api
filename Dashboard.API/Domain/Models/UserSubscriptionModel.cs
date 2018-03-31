using System;
using System.Collections.Generic;

namespace Dashboard.API.Domain.Models
{
    public class UserSubscriptionModel
    {
        public List<ClientAndUserServices> Subscriptions { get; set; } = new List<ClientAndUserServices>();

        public class ClientAndUserServices
        {
            public Guid ClientId { get; set; }

            public string Name { get; set; }

            public List<Service> Services { get; set; } = new List<Service>();
        }
    }
}