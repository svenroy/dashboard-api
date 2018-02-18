using Dashboard.API.Application.Extensions;
using Dashboard.API.Application.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "client")]
    public class ClientServicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientServicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<List<ClientServiceModel>> Get()
        {
            var res = await _unitOfWork.ClientServicesRepo.GetClientServicesByIdAsync(User.GetUserId());
            return res;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClientServiceModel model)
        {
            await _unitOfWork.ClientServicesRepo.AddClientServiceAsync(model, User.GetUserId());
            return new JsonResult(null);
        }

        [HttpPut]
        public async Task<JsonResult> Put(ClientServiceModel model)
        {
            await _unitOfWork.ClientServicesRepo.UpdateClientServiceAsync(model);
            return new JsonResult(null);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(Guid id)
        {
            await _unitOfWork.ClientServicesRepo.DeleteClientServiceAsync(id);
            return new JsonResult(null);
        }
    }

    public class ClientServiceModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}