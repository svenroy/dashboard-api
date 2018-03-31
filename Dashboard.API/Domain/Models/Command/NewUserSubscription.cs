using System;
using System.Collections.Generic;

namespace Dashboard.API.Domain.Models.Command
{
    public class NewUserSubscription
    {
        public Guid ClientId { get; set; }

        public List<Guid> ClientServiceIds { get; set; } = new List<Guid>();
    }
}