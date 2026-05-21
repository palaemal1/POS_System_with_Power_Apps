using BAL.IService;
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

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var data = await _customerService.GetAllCustomer();
            return Ok(new ResponseModel { Data=data});
        }

        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(AddCustomerDTO input)
        {
            await _customerService.AddNewCustomer(input);
            return Ok("Add new customer.");
        }

        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomer input)
        {
            await _customerService.UpdateCustomer(id, input);
            return Ok("Update successfully");
        }

        [HttpGet("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(Guid id, string updatedBy)
        {
            await _customerService.DeleteCustomer(id,updatedBy);
            return Ok("Delete data successfully");
        }
    }
}
