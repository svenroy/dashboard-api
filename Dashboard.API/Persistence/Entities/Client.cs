using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.API.Persistence.Entities
{
    public class Client
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Website { get; set; }

        public virtual ICollection<ClientService> ClientServices { get; set; }
    }
}
