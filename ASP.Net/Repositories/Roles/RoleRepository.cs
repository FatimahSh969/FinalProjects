
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Roles
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AirPortDBContext db) : base(db)
        {
        }

        public Role? GetRoleWithPermissions(int id)
        {
            return _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role> GetAllRolesWithDetails()
        {
            return _context.Roles
                .Include(r => r.Permissions)
                .Include(r => r.Users)
                .ToList();
        }
    }
}
