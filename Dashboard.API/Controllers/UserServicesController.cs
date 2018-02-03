using System.Collections.Generic;
using Dashboard.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserServicesController : Controller
    {
        [HttpGet]
        public IEnumerable<UserService> Get()
        {
            var services = new List<UserService>
            {
                new UserService{
                    Name = "Lloyd's bank",
                    Value = "£205.10"
                },
                new UserService{
                    Name= "Assetz bank",
                    Value = "£13, 576"
                },
                new UserService{
                    Name = "Blockfolio",
                    Value = "£10, 000"
                }
            };

            return services;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
