using System;
namespace Dashboard.API.Models
{
    public class UserService
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceUrl { get; set; }
    }
}
