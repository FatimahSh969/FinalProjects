
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Notifications
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IEnumerable<Notification> GetNotificationsWithFlightDetails();
    }
}
