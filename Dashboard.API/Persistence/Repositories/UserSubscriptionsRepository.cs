using Dashboard.API.Application.Persistence.Repositories;
using Dashboard.API.Domain.Models;
using Dashboard.API.Domain.Models.Command;
using Dashboard.API.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.Persistence.Repositories
{
    public class UserSubscriptionsRepository : IUserSubscriptionsRepository
    {
        private readonly SeviiContext _context;

        public UserSubscriptionsRepository(SeviiContext context)
        {
            _context = context;
        }

        public async Task<UserSubscriptionModel> GetUserSubscriptionsAsync(Guid userId)
        {
            var clientIds = await _context.UserSubscriptions.Select(d => d.ClientService.ClientId).ToListAsync();

            var clientServices = _context.ClientServices
                .Include(d => d.Client)
                .Where(d => clientIds.Any(x => x == d.ClientId))
                .GroupBy(d => d.ClientId);

            var result = new UserSubscriptionModel();

            foreach (var clientServiceGroup in clientServices)
            {
                var subscription = new UserSubscriptionModel.ClientAndUserServices
                {
                    ClientId = clientServiceGroup.Key,
                    Name = clientServiceGroup.First().Client.Name
                };

                foreach (var service in clientServiceGroup)
                {
                    var serviceSubscription = await _context.UserSubscriptions.FirstOrDefaultAsync(
                        d => d.ClientServiceId == service.Id &&
                             d.UserId == userId);

                    var isSubscribed = serviceSubscription != null && serviceSubscription.IsSubscribed;

                    subscription.Services.Add(new Service
                    {
                        Id = service.Id,
                        DefaultValue = service.DefaultValue,
                        Name = service.Name,
                        Key = service.Key,
                        IsSubscribed = isSubscribed
                    });
                }

                result.Subscriptions.Add(subscription);
            }

            return result;
        }

        public async Task SetSubscriptionAsync(Guid userId, NewUserSubscription userSubscription)
        {
            var existingSubscriptions = await _context.UserSubscriptions.Where(d =>
                d.UserId == userId &&
                d.ClientService.ClientId == userSubscription.ClientId).ToListAsync();

            _context.UserSubscriptions.RemoveRange(existingSubscriptions);

            var clientServiceIds =
                from newSub in userSubscription.ClientServiceIds
                join srv in _context.ClientServices
                    on newSub equals srv.Id
                where srv.ClientId == userSubscription.ClientId
                select newSub;

            foreach (var serviceId in clientServiceIds)
            {
                await _context.UserSubscriptions.AddAsync(UserSubscription.Create(userId, serviceId));
            }

            await _context.SaveChangesAsync();
        }

        public async Task SubscribeAsync(Guid userId, Guid serviceId)
        {
            var service = await _context.ClientServices.FindAsync(serviceId);
            if (service == null)
                return;

            var existingSubscription =
                await _context.UserSubscriptions.FirstOrDefaultAsync(d =>
                    d.UserId == userId && d.ClientServiceId == serviceId);

            if (existingSubscription != null)
                existingSubscription.IsSubscribed = true;
            else
            {
                await _context.UserSubscriptions.AddAsync(UserSubscription.Create(userId, serviceId));
            }

            await _context.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(Guid userId, Guid serviceId)
        {
            var subscription =
                await _context.UserSubscriptions.FirstOrDefaultAsync(d =>
                    d.UserId == userId && d.ClientServiceId == serviceId);

            if (subscription == null)
                return;

            subscription.IsSubscribed = false;
            await _context.SaveChangesAsync();
        }
    }
}
