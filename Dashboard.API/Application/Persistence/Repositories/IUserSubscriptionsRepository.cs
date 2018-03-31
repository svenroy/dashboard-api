using Dashboard.API.Domain.Models;
using Dashboard.API.Domain.Models.Command;
using System;
using System.Threading.Tasks;

namespace Dashboard.API.Application.Persistence.Repositories
{
    public interface IUserSubscriptionsRepository
    {
        Task<UserSubscriptionModel> GetUserSubscriptionsAsync(Guid userId);

        Task SetSubscriptionAsync(Guid userId, NewUserSubscription userSubscription);

        Task SubscribeAsync(Guid userId, Guid serviceId);

        Task UnsubscribeAsync(Guid userId, Guid serviceId);
    }
}
