using System;

namespace Dashboard.API.Domain.Models
{
    public class Service
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public string Key { get; set; }

        public bool IsSubscribed { get; set; }
    }
}