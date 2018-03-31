using Dashboard.API.Application.Dtos;
using Dashboard.API.Application.Extensions;
using Dashboard.API.Application.Persistence;
using Dashboard.API.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "client")]
    public class ClientProfilesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientProfilesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<HttpResponse<ClientProfileModel>> Get()
        {
            var model = await _unitOfWork.ClientProfilesRepo.GetProfileByIdAsync(User.GetUserId());

            var response = new HttpResponse<ClientProfileModel>();

            if (model == null)
                response.Status = HttpStatusCode.NotFound;
            else
                response.Payload = model;

            return response;
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromBody]ClientProfileModel model)
        {
            await _unitOfWork.ClientProfilesRepo.UpdateOrAddProfileAsync(model, User.GetUserId());
            return new JsonResult(null);
        }
    }
}