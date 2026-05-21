using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public interface IEmployeeService
    {
        Task AddNewEmployee(AddNewEmployee input);
        Task<IEnumerable<Employees>> GetAllEmployee();
        Task<IEnumerable<EmployeeAccessDTO>> GetEmployeeByName(string employeeName);
        Task UpdateEmployee(Guid id, UpdateEmployeeDTO input);
        Task DeleteEmployee(Guid id ,string updatedBy);
    }
}
