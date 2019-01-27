namespace UltraV.Controllers
{
    using System.Threading.Tasks;
    using Domain.Models.Save;
    using Domain.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Web.Http;

    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationSaveViewModel model)
        {
            return this.Ok(await this.notificationService.CreateNotification(model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById([FromRoute] int id)
        {
            return this.Ok(await this.notificationService.GetNotificationById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            return this.Ok(await this.notificationService.GetNotifications());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateNotification(
            [FromRoute] int id,
            [FromBody] NotificationSaveViewModel model)
        {
            return this.Ok(await this.notificationService.UpdateNotification(id, model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            return this.Ok(await this.notificationService.DeleteNotification(id));
        }
    }
}