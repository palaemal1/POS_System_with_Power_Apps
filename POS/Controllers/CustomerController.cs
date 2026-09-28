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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ICustomerService _customerService;
        public CustomerController(IUnitofWork unitofWork,ICustomerService customerService)
        {
            _unitofWork = unitofWork;
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var data = await _customerService.GetAllCustomer();
            return Ok(new ResponseModel { Data=data});
        }

        [Authorize(Roles = "Admin,Cashier")]
        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(AddCustomerDTO input)
        {
            await _customerService.AddNewCustomer(input);
            return Ok("Add new customer.");
        }

        [Authorize(Roles = "Admin,Cashier")]
        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomer input)
        {
            await _customerService.UpdateCustomer(id, input);
            return Ok("Update successfully");
        }

        [Authorize(Roles = "Admin,Cashier")]
        [HttpPatch("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id, DeleteDTO request)
        {
            await _customerService.DeleteCustomer(id,request);
            return Ok("Delete data successfully");
        }
    }
}
