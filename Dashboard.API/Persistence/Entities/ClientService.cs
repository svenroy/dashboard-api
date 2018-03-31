using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.API.Persistence.Entities
{
    public class ClientService
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Client")]
        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Key { get; set; }

        public string DefaultValue { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}