using SmartAirPort.Models;
using SmartAirPort.Repositories.Bookings;
using SmartAirPort.Repositories.Employees;
using SmartAirPort.Repositories.Flights;
using SmartAirPort.Repositories.Notifications;
using SmartAirPort.Repositories.Roles;
using System.Security;

namespace SmartAirPort.Repositories.Base
{
    public interface IUnitOfWork
    {
        IRepository<Airline> Airlines { get; }
        IRepository<Aircraft> Aircraft { get; }
        IRepository<Gate> Gates { get; }
        IFlightRepository Flights { get; }
        IEmployeeRepository Employees { get; }
        IRepository<Department> Departments { get; }
        IRepository<Permission> Permissions { get; }
        IRoleRepository Roles { get; }
        IRepository<User> Users { get; }
        IBookingRepository Bookings { get; }
        INotificationRepository Notifications { get; }
        int SaveChanges();


    }
}
