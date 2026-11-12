using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Employees
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
            IEnumerable<Employee> GetAllEmployeesWithDepartment();

            Employee GetEmployeeWithDepartment(int? id);


        }
    }

