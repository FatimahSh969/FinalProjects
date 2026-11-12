using SmartAirPort.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartAirPort.Data
{
    public class AirPortDBContext :DbContext
    {
        public AirPortDBContext(DbContextOptions<AirPortDBContext> options) : base(options)
        {
        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }
    }
}
