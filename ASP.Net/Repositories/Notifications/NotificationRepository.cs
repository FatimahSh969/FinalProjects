
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Notifications
{
    public class NotificationRepository
        : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(AirPortDBContext db) : base(db)
        {
        }

        public IEnumerable<Notification> GetNotificationsWithFlightDetails()
        {
            return _context.Notifications
                .Include(n => n.Flight)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }
    }
}
