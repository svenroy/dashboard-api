using System;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.API.Infrastructure.Persistence.Entities
{
    public class ClientService
    {
        public Guid Id { get; set; }

        [Required]
        public Guid Client { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}