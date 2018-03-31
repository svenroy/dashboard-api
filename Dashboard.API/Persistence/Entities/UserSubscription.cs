using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.API.Persistence.Entities
{
    public class UserSubscription
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("ClientService")]
        public Guid ClientServiceId { get; set; }

        public virtual ClientService ClientService { get; set; }

        public bool IsSubscribed { get; set; }

        public static UserSubscription Create(Guid userId, Guid clientServiceId)
        {
            return new UserSubscription
            {
                Id = Guid.NewGuid(),
                ClientServiceId = clientServiceId,
                UserId = userId,
                IsSubscribed = true
            };
        }
    }
}