namespace Domain.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AutoMapper;
    using DataAccess;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Models.Save;

    public class NotificationService : INotificationService
    {
        private ApplicationDbContext context;

        private IMapper mapper;

        public NotificationService(DataContext context, IMapper mapper)
        {
            this.context = context.DbContext;
            this.mapper = mapper;
        }

        public async Task<NotificationViewModel> CreateNotification(NotificationSaveViewModel model)
        {
            var lastNotification = await this.context.Notifications
                .AsNoTracking()
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync();

            var newNotification = new Notification(model.Text, lastNotification?.OrderIndex ?? 1, model.IsStickied);

            await this.context.AddAsync(newNotification);
            await this.context.SaveChangesAsync();

            return this.mapper.Map<NotificationViewModel>(newNotification);
        }

        public async Task<NotificationViewModel> GetNotificationById(int id)
        {
            var notification = await this.context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (notification == null)
            {
                return await Task.FromException<NotificationViewModel>(
                    new Exception($"No entry found for the id: {id}, {HttpStatusCode.NotFound}"));
            }

            return this.mapper.Map<NotificationViewModel>(notification);
        }

        public async Task<List<NotificationViewModel>> GetNotifications()
        {
            var notifications = await this.context.Notifications
                .AsNoTracking()
                .ToListAsync();

            if (!notifications.Any())
            {
                return await Task.FromException<List<NotificationViewModel>>(
                    new Exception($"No entries found, {HttpStatusCode.NotFound}"));
            }

            return this.mapper.Map<List<NotificationViewModel>>(notifications);
        }

        public async Task<bool> UpdateNotification(int id, NotificationSaveViewModel model)
        {
            var notification = await this.context.Notifications
                .FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (notification == null)
            {
                return await Task.FromException<bool>(
                    new Exception($"No entry found for the id {id}, {HttpStatusCode.NotFound}"));
            }

            notification.Update(model.Text, model.IsStickied);

            return await this.context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var notification = await this.context.Notifications
                .FirstOrDefaultAsync(t => t.Id.Equals(id));

            if (notification == null)
            {
                return await Task.FromException<bool>(
                    new Exception($"No entry found for the id {id}, {HttpStatusCode.NotFound}"));
            }

            this.context.Notifications.Remove(notification);

            return await this.context.SaveChangesAsync() > 0;
        }
    }
}