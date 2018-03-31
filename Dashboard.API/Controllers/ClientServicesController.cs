using Dashboard.API.Application.Extensions;
using Dashboard.API.Application.Persistence;
using Dashboard.API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var res = await _unitOfWork.ClientServicesRepo.GetClientServicesForUserAsync(User.GetUserId());
            return res;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClientServiceModel model)
        {
            await _unitOfWork.ClientServicesRepo.AddClientServiceAsync(model, User.GetUserId());
            return new JsonResult(null);
        }
    }
}