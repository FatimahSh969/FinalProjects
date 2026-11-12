
using Microsoft.AspNetCore.Mvc;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var notifications = _unitOfWork.Notifications
                .GetNotificationsWithFlightDetails();

            return View(notifications);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsRead(int id)
        {
            var notification = _unitOfWork.Notifications.GetById(id);

            if (notification == null)
                return NotFound();

            notification.IsRead = true;

            _unitOfWork.Notifications.Update(notification);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllAsRead()
        {
            var notifications = _unitOfWork.Notifications.GetAll();

            foreach (var notification in notifications)
            {
                if (!notification.IsRead)
                {
                    notification.IsRead = true;
                    _unitOfWork.Notifications.Update(notification);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
