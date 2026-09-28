using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Repository.IUnitOfWork;

namespace POS.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController:ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IUnitofWork unitofWork,IEmployeeService employeeService)
        {
            _unitofWork = unitofWork;
            _employeeService = employeeService;
        }

       // [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var data = await _employeeService.GetAllEmployee();
            return Ok(new ResponseModel { Data = data });
        }

       // [Authorize(Roles = "Admin")]
        [HttpPost("GetEmployeeByName")]
        public async Task<IActionResult>GetEmployeeByName(string employeeEmail)
        {
            var data = await _employeeService.GetEmployeeByName(employeeEmail);
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewEmployee")]
        public async Task<IActionResult> AddNewEmployee(AddNewEmployee input)
        {
            await _employeeService.AddNewEmployee(input);
            return Ok("Add employee successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(Guid id,UpdateEmployeeDTO input)
        {
            await _employeeService.UpdateEmployee(id, input);
            return Ok("Update successfully");
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(Guid id,ChangePasswordDTO input)
        {
            await _employeeService.ChangePassword(id,input);
            return Ok("Change Password Successfully!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id , DeleteDTO request)
        {
            await _employeeService.DeleteEmployee(id,request);
            return Ok("Delete successfully");
        }
    }
}
