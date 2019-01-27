namespace Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Save;

    public interface INotificationService
    {
        Task<NotificationViewModel> CreateNotification(NotificationSaveViewModel model);

        Task<NotificationViewModel> GetNotificationById(int id);

        Task<List<NotificationViewModel>> GetNotifications();

        Task<bool> UpdateNotification(int id, NotificationSaveViewModel model);

        Task<bool> DeleteNotification(int id);
    }
}