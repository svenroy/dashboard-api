using System;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.API.Infrastructure.Persistence.Entities
{
    public class ClientProfile
    {
        [Required]
        [Key]
        public Guid Client { get; set; }

        [Required]
        public string Name { get; set; }

        public string Website { get; set; }
    }
}
