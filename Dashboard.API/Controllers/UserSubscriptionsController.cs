using Dashboard.API.Application.Dtos;
using Dashboard.API.Application.Extensions;
using Dashboard.API.Application.Persistence;
using Dashboard.API.Domain.Models;
using Dashboard.API.Domain.Models.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Dashboard.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class UserSubscriptionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSubscriptionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<HttpResponse<UserSubscriptionModel>> Get()
        {
            var subscriptions = await _unitOfWork.UserSubscriptionsRepo.GetUserSubscriptionsAsync(User.GetUserId());
            return new HttpResponse<UserSubscriptionModel>(subscriptions);
        }

        [HttpGet]
        [Route("getClientAndServices/{id}")]
        public async Task<HttpResponse<ClientProfileAndServicesModel>> Get([FromRoute] Guid id)
        {
            var result = await _unitOfWork.ClientServicesRepo.GetClientProfileAndServicesOrNullAsync(User.GetUserId(), id);

            if (result == null)
                return new HttpResponse<ClientProfileAndServicesModel>
                {
                    Status = HttpStatusCode.NotFound
                };

            return new HttpResponse<ClientProfileAndServicesModel>
            {
                Status = HttpStatusCode.OK,
                Payload = result
            };
        }

        [HttpPost]
        public async Task<HttpResponse> Post([FromBody] NewUserSubscription model)
        {
            await _unitOfWork.UserSubscriptionsRepo.SetSubscriptionAsync(User.GetUserId(), model);
            return new HttpResponse { Status = HttpStatusCode.Created };
        }

        [HttpPost]
        [Route("subscribe")]
        public async Task<HttpResponse> Subscribe([FromBody] Guid serviceId)
        {
            await _unitOfWork.UserSubscriptionsRepo.SubscribeAsync(User.GetUserId(), serviceId);
            return new HttpResponse { Status = HttpStatusCode.Created };
        }

        [HttpPost]
        [Route("unsubscribe")]
        public async Task<HttpResponse> Unsubscribe([FromBody] Guid serviceId)
        {
            await _unitOfWork.UserSubscriptionsRepo.UnsubscribeAsync(User.GetUserId(), serviceId);
            return new HttpResponse { Status = HttpStatusCode.Created };
        }
    }
}