using BAL.IService;
using Microsoft.AspNetCore.Identity;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAL.Service
{
    internal class EmployeeService:IEmployeeService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly DataContent _content;
        public EmployeeService(IUnitofWork unitofWork,DataContent content)
        {
            _unitofWork = unitofWork;
            _content = content;
        }

        public async Task<IEnumerable<Employees>> GetAllEmployee()
        {
            var data = await _unitofWork.Employee.GetAll();
            return  data ;
        }

        public async Task<IEnumerable<EmployeeAccessDTO>> GetEmployeeByName(string employeeEmail)
        {
            var data = await _unitofWork.Employee.GetByCondition(x => x.Email == employeeEmail);
            return data.Select(x => new EmployeeAccessDTO { employeeId = x.EmployeeId.ToString(),employeeName=x.EmployeeName,role=x.Role });
        }
        public async Task AddNewEmployee(AddNewEmployee input)
        {
           
            var data =new Employees()
            {
                EmployeeName = input.EmployeeName,
                FullName=input.fullName, 
                Role=input.role,
                Status=input.status,
                Email=input.email,
                Phone=input.phone,
                Department=input.department,
                Position=input.position,
                ActiveFlag=input.activeFlag, 
                CreatedAt=input.createdDate, 
                CreatedBy=input.createdBy
            };
         
            data.Password = new PasswordHasher<Employees>().HashPassword(data, input.password);
            await _unitofWork.Employee.Add(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task UpdateEmployee(Guid id, UpdateEmployeeDTO input)
        {
            var data = (await _unitofWork.Employee.GetByCondition(x => x.EmployeeId == id)).FirstOrDefault();
            if(data!= null)
            {
                data.EmployeeName = input.employeeName;
                data.Password = input.password;
                data.FullName = input.fullName;
                data.Role = input.role;
                data.Status = input.status;
                data.Email = input.email;
                data.Phone = input.phone;
                data.Department = input.department;
                data.Position = input.position;
                data.ActiveFlag = input.activeFlag;
                data.UpdatedAt = input.updatedDate;
                data.UpdatedBy = input.updatedBy;
            }
             _unitofWork.Employee.Update(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Guid id , DeleteDTO request)
        {
            var data = (await _unitofWork.Employee.GetByCondition(x => x.EmployeeId == id)).FirstOrDefault();
            if (data != null)
            {
                data.ActiveFlag = false;
                data.UpdatedBy = request.updatedBy;
                data.UpdatedAt = DateTime.UtcNow;
            }
            _unitofWork.Employee.Update(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task ChangePassword(Guid id, ChangePasswordDTO input)
        {
            var employee = (await _unitofWork.Employee.GetByCondition(
                x => x.EmployeeId == id && x.ActiveFlag == true
            )).FirstOrDefault();

            if (employee == null)
            {
                throw new Exception("Employee not found.");
            }

            if (string.IsNullOrWhiteSpace(input.currentPassword))
            {
                throw new Exception("Current password is required.");
            }

            if (string.IsNullOrWhiteSpace(input.newPassword))
            {
                throw new Exception("New password is required.");
            }

            if (input.currentPassword == input.newPassword)
            {
                throw new Exception("New password must be different from current password.");
            }

            var passwordHasher = new PasswordHasher<Employees>();

            // Verify current password
            var verificationResult = passwordHasher.VerifyHashedPassword(
                employee,
                employee.Password,
                input.currentPassword
            );

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid current password.");
            }

            // Hash and save new password
            employee.Password = passwordHasher.HashPassword(
                employee,
                input.newPassword
            );

            employee.UpdatedAt = input.updatedDate;
            employee.UpdatedBy = input.updatedBy;

            await _unitofWork.SaveChangesAsync();
        }
    }
}
