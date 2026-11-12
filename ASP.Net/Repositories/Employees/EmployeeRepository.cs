using SmartAirPort.Models;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Employees
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(AirPortDBContext db) : base(db)
        {
        }

        public IEnumerable<Employee> GetAllEmployeesWithDepartment()
        {
            return _context.Employees.Include(e => e.Department).ToList();
        }

        Employee IEmployeeRepository.GetEmployeeWithDepartment(int? id)
        {
            Employee? emp = _context.Employees.Include(e => e.Department).First(e => e.Id == id);
            return emp;
        }


    }
}
