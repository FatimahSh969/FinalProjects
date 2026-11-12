using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Roles
{
    public interface IRoleRepository : IRepository<Role>
    {

        Role? GetRoleWithPermissions(int id);

        IEnumerable<Role> GetAllRolesWithDetails();
    }
}
