    using SmartAirPort.Data;
    using SmartAirPort.Models;
    using SmartAirPort.Repositories.Bookings;
    using SmartAirPort.Repositories.Employees;
    using SmartAirPort.Repositories.Flights;
using SmartAirPort.Repositories.Notifications;
    using SmartAirPort.Repositories.Roles;
using System.Security;

    namespace SmartAirPort.Repositories.Base
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly AirPortDBContext _context;
            public IRepository<Airline> Airlines { get; }

            public IRepository<Aircraft> Aircraft { get; }

            public IRepository<Gate> Gates { get; }

            public IFlightRepository Flights { get; }
            public IRoleRepository Roles { get; }
            public IEmployeeRepository Employees { get; }
            public IRepository<Department> Departments { get; }
            public IRepository<Permission> Permissions { get; }
            public IRepository<User> Users { get; }
            public INotificationRepository Notifications { get; }

        public IBookingRepository Bookings { get; }
        public UnitOfWork(AirPortDBContext context)
            {
                _context = context;
                Airlines = new Repository<Airline>(_context);
                Aircraft = new Repository<Aircraft>(_context);
                Gates = new Repository<Gate>(_context);
                Flights = new FlightRepository(_context);
                Employees = new EmployeeRepository(_context);
                Departments = new Repository<Department>(_context);
                Roles = new RoleRepository(_context);
                Permissions = new Repository<Permission>(_context);
                Users = new Repository<User>(_context);
                Bookings = new BookingRepository(_context);
                Notifications = new NotificationRepository(_context);   
        }

            public int SaveChanges()
            {
                return _context.SaveChanges();
            }
        }
    }